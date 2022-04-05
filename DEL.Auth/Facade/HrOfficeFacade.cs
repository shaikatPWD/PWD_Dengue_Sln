using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DEL.Auth.DTO;
using DEL.Auth.Infrastructure;
using DEL.Auth.Service;
using AutoMapper;

namespace DEL.Auth.Facade
{
    public class HrOfficeFacade : BaseFacade
    {
        private readonly GenService _service = new GenService();

        public ResponseDto SaveCompanyProfile(HrOfficeDto companyDto, long? userId)
        {
            var response = new ResponseDto();
            HrOffice entity;

            if (companyDto.Id > 0)
            {
                entity = _service.GetById<HrOffice>((long)companyDto.Id);
                entity.EditDate = DateTime.Now;
                if (userId != null)
                    entity.EditedBy = userId;

                entity.Name = companyDto.Name;
                entity.BnName = companyDto.BnName;
                entity.ZoneId = companyDto.ZoneId;
                entity.CircleId = companyDto.CircleId;
                entity.DivisionId = companyDto.DivisionId;
                entity.Phone = companyDto.Phone;
                entity.PhoneResidence = companyDto.PhoneResidence;
                entity.Fax = companyDto.Fax;
                entity.Email = companyDto.Email;                
                
                //entity.CompanyTypeName = companyDto.CompanyTypeName;
                
                //entity.ParentName 
            }
            else
            {
                entity = Mapper.Map<HrOffice>(companyDto);
                entity.CreateDate = DateTime.Now;
                if (userId != null)
                    entity.CreatedBy = userId;

            }
            try
            {
                _service.Save(entity);
                response.Success = true;
            }
            catch (Exception)
            {

            }
            return response;
        }    

        public List<HrOfficeDto> GetAllCompanyProfiles()
        {
            return Mapper.Map<List<HrOfficeDto>>(_service.GetAll<HrOffice>().Where(c => c.Status == EntityStatus.Active).ToList());
        }

        public List<HrOfficeDto> GetAllActiveCompanyProfiles()
        {
            //var temp = Mapper.Map<List<HrOfficeDto>>(_service.GetAll<HrOffice>().Where(c => c.Id!=0 && c.Status == EntityStatus.Active).ToList());
            return Mapper.Map<List<HrOfficeDto>>(_service.GetAll<HrOffice>().Where(c => c.IsShow ==IsShow.Yes && c.Status == EntityStatus.Active).ToList());
        }

        public HrOfficeDto GetCompanyProfileById(long CompanyProfileId)
        {
            return Mapper.Map<HrOfficeDto>(_service.GetById<HrOffice>(CompanyProfileId));
        }

        public List<OwnOfficeBranchDto> GetAccessbleSubCompaniesAndProjects(long UserId, long OfficeId)
        {
            var list = new List<OwnOfficeBranchDto>();
            var directPermissions = _service.GetAll<UserOfficeApplication>().Where(u => u.UserId == UserId && u.Status == EntityStatus.Active).ToList();
            
            var filteredPermissions = FilterSubPremissions(directPermissions.Select(d => d.OfficeId).ToList(), OfficeId);
            if (filteredPermissions != null)
                list.AddRange(filteredPermissions);
            return list;
        }

        public List<OwnOfficeBranchDto> GetAllSubCompaniesAndProjects(long CompanyId)
        {
            var list = new List<OwnOfficeBranchDto>();
            var subCompanies = _service.GetAll<OwnOfficeBranch>().Where(c => c.Id == CompanyId).ToList();
            if (subCompanies != null && subCompanies.Count > 0)
            {
                list.AddRange(Mapper.Map<List<OwnOfficeBranchDto>>(subCompanies));
                foreach (var company in subCompanies)
                {

                    list.AddRange(GetAllSubCompaniesAndProjects(company.Id));
                }
                return list;
            }
            else
                return new List<OwnOfficeBranchDto>();
        }
        

        #region helpers
        private List<OwnOfficeBranchDto> FilterSubPremissions(List<long> companyPermissions, long CurrentCompany)
        {
            var result = new List<OwnOfficeBranchDto>();
            var subCompanies = _service.GetAll<OwnOfficeBranch>().Where(c => c.Id == CurrentCompany).ToList();
            if (subCompanies == null || subCompanies.Count < 1)
                return null;
            var permissions = subCompanies.Where(s => companyPermissions.Contains(s.Id)).ToList();
            result.AddRange(Mapper.Map<List<OwnOfficeBranchDto>>(permissions));


            //foreach (var company in subCompanies)
            //{
            //    var permission = FilterSubPremissions(companyPermissions, company.Id);
            //    if (permission != null && permission.Count > 0)
            //        result.AddRange(permission);
            //}
            return result;
        }
        #endregion
        public void DeleteHrOffice(long id)
        {
            GenService.Delete<HrOffice>(id);
            GenService.SaveChanges();
        }

    }
}
