//using DEL.UI.OfficeAssetss.Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEL.Auth.DTO;
using DEL.Auth.Infrastructure;
using System.Globalization;
using System.Transactions;
using AutoMapper;



namespace DEL.Auth.Facade
{
    public class OfficeAssetsFacade : BaseFacade
    {
        public List<OfficeAssetsDto> GetOfficeAssets(long? officeId)//(List<long?> ApplicationId) long officeId
        {
            var districts = GenService.GetAll<OfficeAssets>().Where(o => o.HrOfficeId == officeId).ToList(); //.Where(o=>o.HrOfficeId == officeId).ToList();

            var data = districts.Select(x => new OfficeAssetsDto
            {
                Id = x.Id,
                AssetName = x.AssetName,
                Description = x.Description,
                Note = x.Note
            }).ToList();
            return data;
        }

        public void SaveOfficeAssets(OfficeAssetsDto officeAssetsDto, long? officeid)
        {
            if (officeAssetsDto.Id > 0)
            {
                var officeAssets = GenService.GetById<OfficeAssets>((long)officeAssetsDto.Id);
                officeAssets.AssetName = officeAssetsDto.AssetName;
                officeAssets.Description = officeAssetsDto.Description;
                officeAssets.Note = officeAssetsDto.Note;
                officeAssets.Period = DateTime.Now.ToString("MMMM-yy");
                officeAssets.HrOfficeId = (long)officeid;//officeAssetsDto.HrOfficeId;
                GenService.Save(officeAssets);
            }
            else
            {
                GenService.Save(new OfficeAssets
                {
                    AssetName = officeAssetsDto.AssetName,
                    Description = officeAssetsDto.Description,
                    Note = officeAssetsDto.Note,
                    Period = DateTime.Now.ToString("MMMM-yy"),
                    HrOfficeId = (long)officeid//(int)officeAssetsDto.HrOfficeId
                });
            }
            GenService.SaveChanges();
        }
        public void DeleteOfficeAssets(long id)
        {
            GenService.Delete<OfficeAssets>(id);
            GenService.SaveChanges();
        }
        public List<HrOfficeDto> GetAllCompanyProfiles()
        {
            return Mapper.Map<List<HrOfficeDto>>(GenService.GetAll<HrOffice>().Where(c => c.Status == EntityStatus.Active).ToList());
        }
    }
}



