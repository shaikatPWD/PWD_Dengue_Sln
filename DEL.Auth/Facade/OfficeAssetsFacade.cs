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
        public List<OfficeAssetsDto> GetOfficeAssets()//(List<long?> ApplicationId) long officeId
        {
            var districts = GenService.GetAll<OfficeAssets>().ToList(); //.Where(o=>o.HrOfficeId == officeId).ToList();

            var data = districts.Select(x => new OfficeAssetsDto
            {
                Id = x.Id,
                AssetName = x.AssetName,
                Description = x.Description,
                Note = x.Note
            }).ToList();
            return data;
        }

        public void SaveOfficeAssets(OfficeAssetsDto officeAssetsDto)
        {
            if (officeAssetsDto.Id > 0)
            {
                var officeAssets = GenService.GetById<OfficeAssets>((long)officeAssetsDto.Id);
                officeAssets.AssetName = officeAssetsDto.AssetName;
                officeAssets.Description = officeAssetsDto.Description;
                officeAssets.Note = officeAssetsDto.Note;
                officeAssets.HrOfficeId = (int)officeAssetsDto.HrOfficeId;
                GenService.Save(officeAssets);
            }
            else
            {
                GenService.Save(new OfficeAssets
                {
                    AssetName = officeAssetsDto.AssetName,
                    Description = officeAssetsDto.Description,
                    Note = officeAssetsDto.Note,
                    HrOfficeId = (int)officeAssetsDto.HrOfficeId
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
            return Mapper.Map<List<HrOfficeDto>>(GenService.GetAll<HrOffice>().Where(c=>c.Status==EntityStatus.Active).ToList());
        }
    }
}



