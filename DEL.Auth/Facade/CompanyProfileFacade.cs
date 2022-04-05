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
    public class CompanyProfileFacade : BaseFacade
    {
        private readonly GenService _service = new GenService();

        public ResponseDto SaveCompanyProfile(OfficeProfileDto companyDto, long? userId)
        {
            var response = new ResponseDto();
            OfficeProfile entity;

            if (companyDto.Id > 0)
            {
                entity = _service.GetById<OfficeProfile>(companyDto.Id);
                entity.EditDate = DateTime.Now;
                if (userId != null)
                    entity.EditedBy = userId;

                entity.Name = companyDto.Name;
                entity.Address = companyDto.Address;
                entity.PhoneNo = companyDto.PhoneNo;
                entity.Email = companyDto.Email;
                entity.Fax = companyDto.Fax;
                entity.ContactPerson = companyDto.ContactPerson;
                entity.CompanyType = companyDto.CompanyType;
                //entity.CompanyTypeName = companyDto.CompanyTypeName;
                entity.ParentId = companyDto.ParentId;
                //entity.ParentName 
            }
            else
            {
                entity = Mapper.Map<OfficeProfile>(companyDto);
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
        public string GetUpdateBillNo(long officeId)
        {
            try
            {
                var cp = _service.GetById<OfficeProfile>(officeId);
                var nBill = Convert.ToDecimal(cp.BillNo.Trim()) + 1;
                cp.BillNo = "" + nBill;
                _service.Save(cp);
                return "BN-" + nBill;
            }
            catch (Exception)
            {

                throw;
            }


        }
        public string GetUpdateInvoiceNo(long officeId)
        {
            try
            {
                var cp = _service.GetById<OfficeProfile>(officeId);
                var nIv = Convert.ToDecimal(cp.InvoiceNo.Trim()) + 1;
                cp.InvoiceNo = "" + nIv;
                _service.Save(cp);
                return "CIV-" + nIv;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //public string GetUpdateCr()
        //{
        //    try
        //    {
        //        var cp = _service.GetById<CompanyProfile>(1);
        //        var nCr = Convert.ToDecimal(cp.InvoiceNo.Trim()) + 1;
        //        cp.InvoiceNo = "" + nCr;
        //        _service.Save(cp);
        //        return "CV-" + nCr;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        //public string GetUpdateJv()
        //{
        //    try
        //    {
        //        var cp = _service.GetById<CompanyProfile>(1);
        //        var nJv = Convert.ToDecimal(cp.InvoiceNo.Trim()) + 1;
        //        cp.InvoiceNo = "" + nJv;
        //        _service.Save(cp);
        //        return "JV-" + nJv + "-" + DateTime.Now.Year.ToString();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        public string GetUpdateVoucherNo(long officeId)
        {
            var cp = _service.GetById<OfficeProfile>(officeId);
            var nVn = Convert.ToDecimal(cp.VoucherNo.Trim()) + 1;
            try
            {
                
                cp.VoucherNo = "" + nVn;
                _service.Save(cp);
                
            }
            catch (Exception ex)
            {
                //throw;
            }
            return "CRM-" + DateTime.Now.Year + "-" + nVn;
        }

        public string GetUpdateCDvNo(long officeId)
        {
            try
            {
                var cp = _service.GetById<OfficeProfile>(officeId);
                var nVn = Convert.ToDecimal(cp.CDvNo.Trim()) + 1;
                cp.CDvNo = "" + nVn;
                _service.Save(cp);
                return "" + DateTime.Now.Year + "-" + nVn;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetUpdateBDvNo(long officeId)
        {
            try
            {
                var cp = _service.GetById<OfficeProfile>(officeId);
                var nVn = Convert.ToDecimal(cp.BDvNo.Trim()) + 1;
                cp.BDvNo = "" + nVn;
                _service.Save(cp);
                return "" + DateTime.Now.Year + "-" + nVn;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetUpdateCCvNo(long officeId)
        {
            try
            {
                var cp = _service.GetById<OfficeProfile>(officeId);
                var nVn = Convert.ToDecimal(cp.CCvNo.Trim()) + 1;
                cp.CCvNo = "" + nVn;
                _service.Save(cp);
                return "" + DateTime.Now.Year + "-" + nVn;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetUpdateBCvNo(long officeId)
        {
            try
            {
                var cp = _service.GetById<OfficeProfile>(officeId);
                var nVn = Convert.ToDecimal(cp.BCvNo.Trim()) + 1;
                cp.BCvNo = "" + nVn;
                _service.Save(cp);
                return "" + DateTime.Now.Year + "-" + nVn;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetUpdateJvNo(long officeId)
        {
            try
            {
                var cp = _service.GetById<OfficeProfile>(officeId);
                var nVn = Convert.ToDecimal(cp.JvNo.Trim()) + 1;
                cp.JvNo = "" + nVn;
                _service.Save(cp);
                return "" + DateTime.Now.Year + "-" + nVn;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public string GetUpdateInvoiceNo()
        //{
        //    try
        //    {
        //        var cp = _service.GetById<CompanyProfile>(1);
        //        var nInv = Convert.ToDecimal(cp.InvoiceNo.Trim()) + 1;
        //        cp.InvoiceNo = "" + nInv;
        //        _service.Save(cp);
        //        return "INV-" + nInv;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public string GetUpdatedChalanNo()
        {
            try
            {
                var cp = _service.GetById<OfficeProfile>(1);
                var nCln = Convert.ToDecimal(cp.ChallanNo.Trim()) + 1;
                cp.ChallanNo = "" + nCln;
                _service.Save(cp);
                return "CLN-" + nCln;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GetUpdatedRequisitionNo()
        {
            try
            {
                var cp = _service.GetById<OfficeProfile>(1);
                decimal nCln = 0;
                if (cp == null)
                {
                    nCln = 00000000;
                }
                nCln = Convert.ToDecimal(cp.RequisitionNo.Trim()) + 1;
                cp.RequisitionNo = "" + nCln;
                _service.Save(cp);
                return "RQN-" + nCln;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetUpdatedPreProdNo()
        {
            try
            {
                var cp = _service.GetById<OfficeProfile>(1);
                var prdno = Convert.ToDecimal(cp.PreProdNo.Trim()) + 1;
                cp.PreProdNo = "" + prdno;
                _service.Save(cp);
                return "PPDN-" + prdno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetUpdatedProdNo()
        {
            try
            {
                var cp = _service.GetById<OfficeProfile>(1);
                var prdno = Convert.ToDecimal(cp.ProdNo.Trim()) + 1;
                cp.ProdNo = "" + prdno;
                _service.Save(cp);
                return "PDN-" + prdno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ResponseDto UpdateClosingDate()
        {
            ResponseDto response = new ResponseDto();
            var model = _service.GetAll<OfficeProfile>().FirstOrDefault();
            var entity = new OfficeProfile();
            try
            {
                entity = _service.GetById<OfficeProfile>(model.Id);
                entity.SystemDate = model.SystemDate.AddDays(1);
                _service.Save(entity);
                response.Success = true;
                response.Message = "Date Update Successfully";
                //return true;
            }
            catch (Exception)
            {
                response.Message = "Date Updated Failed";
                //return false;
            }
            return response;
        }

        public ResponseDto UpdateClosingDate(List<long> officeIds)
        {
            ResponseDto response = new ResponseDto();
            var model = _service.GetAll<OfficeProfile>().Where(o => officeIds.Contains(o.Id));
            //var entity = new OfficeProfile();
            using (var tran = new TransactionScope())
            {

                try
                {
                    foreach (var entity in model)
                    {
                        entity.SystemDate = entity.SystemDate.AddDays(1);
                        _service.Save(entity);
                    }
                    response.Success = true;
                    response.Message = "Date Update Successfully";
                    tran.Complete();
                    
                    //return true;
                }
                catch (Exception)
                {
                    tran.Dispose();
                    
                    response.Message = "Date Updated Failed";
                    //return false;
                }
            }
            return response;
        }

        public DateTime DateToday(long? CompanyId)
        {
            DateTime SystemDate;
            if (CompanyId != null && CompanyId > 0)
            {
                var company = _service.GetById<OfficeProfile>((long)CompanyId);
                if (company != null)
                    SystemDate = company.SystemDate;
                else
                    SystemDate = DateTime.MinValue;
            }
            else
            {
                SystemDate = _service.GetAll<OfficeProfile>().Select(r => r.SystemDate).FirstOrDefault();
            }
            return SystemDate;
        }

        public DateTime GetCurrentFiscalYear()
        {
            return _service.GetById<OfficeProfile>(1).FiscalYear;
        }

        public List<OfficeProfileDto> GetAllCompanyProfiles()
        {
            return Mapper.Map<List<OfficeProfileDto>>(_service.GetAll<OfficeProfile>().ToList());
        }

        public List<OfficeProfileDto> GetAllActiveCompanyProfiles()
        {
            var temp = Mapper.Map<List<OfficeProfileDto>>(_service.GetAll<OfficeProfile>().Where(c => c.Status == EntityStatus.Active).ToList());
            return Mapper.Map<List<OfficeProfileDto>>(_service.GetAll<OfficeProfile>().Where(c => c.Status == EntityStatus.Active).ToList());
        }

        public OfficeProfileDto GetCompanyProfileById(long CompanyProfileId)
        {
            return Mapper.Map<OfficeProfileDto>(_service.GetById<OfficeProfile>(CompanyProfileId));
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
        //public List<OfficeProfileDto> GetAllSubCompaniesAndProjects(long CompanyId)
        //{
        //    var list = new List<OfficeProfileDto>();
        //    var subCompanies = _service.GetAll<OfficeProfile>().Where(c => c.ParentId == CompanyId).ToList();
        //    if (subCompanies != null && subCompanies.Count > 0)
        //    {
        //        list.AddRange(Mapper.Map<List<OfficeProfileDto>>(subCompanies));
        //        foreach (var company in subCompanies)
        //        {

        //            list.AddRange(GetAllSubCompaniesAndProjects(company.Id));
        //        }
        //        return list;
        //    }
        //    else
        //        return new List<OfficeProfileDto>();
        //}

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

        public DateTime LastClosingDate(long? officeId)
        {
            DateTime closingDate;
            if (officeId != null)
                closingDate = _service.GetById<OfficeProfile>((long)officeId).SystemDate;
            else
                closingDate = _service.GetAll<OfficeProfile>().Select(c => c.SystemDate).FirstOrDefault();
            return closingDate;
        }
        public List<long> GetDependentIds(long officeId)
        {
            var projects = _service.GetAll<OfficeProfile>().Where(l => l.ParentId == officeId && l.CompanyType == CompanyType.Project);
            if (projects != null && projects.Count() > 0)
                return projects.Select(p => p.Id).ToList();
            return null;
        }
        public long GetEmployeeCode()
        {
            try
            {
                var cp = _service.GetById<OfficeProfile>(1);
                long nVn = 0;
                if (cp != null)
                {
                    if (cp.EmployeeNo == 0)
                    {
                        nVn = 000000001;
                        cp.EmployeeNo = nVn;
                        _service.Save(cp);
                    }
                    else
                    {
                        nVn = cp.EmployeeNo + 1;
                        cp.EmployeeNo = nVn;
                        _service.Save(cp);
                    }

                }

                return nVn;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
