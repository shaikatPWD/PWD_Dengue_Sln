//using DEL.UI.Assetss.Auth.Models;
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
    public class AssetsFacade : BaseFacade
    {
        public List<AssetsDto> GetAssets(long? officeId)//(List<long?> ApplicationId) long officeId
        {
            var districts = GenService.GetAll<Assets>().ToList(); //.Where(o=>o.HrOfficeId == officeId).ToList();

            var data = districts.Select(x => new AssetsDto
            {
                Id = x.Id,
                AssetType = x.AssetType,
                AssetTypeFull = x.AssetTypeFull,
                WorkRecordDetails = GetAssetsDetails(x.Id, (long)officeId),
                OrderId = x.OrderId
            }).ToList();
            return data;
        }

        public List<WorkRecordDetailsDto> GetAssetsDetails(long asid, long officeId)//(List<long?> ApplicationId) long officeId
        {
            var districts = GenService.GetAll<WorkRecordDetails>().Where(a => a.AssetId == asid && a.OfficeId ==officeId).ToList(); //.Where(o=>o.HrOfficeId == officeId).ToList();

            var data = districts.Select(x => new WorkRecordDetailsDto
            {
                Id = x.Id,
                AssetId = asid,
                AssetBuildingName = x.AssetBuildingName,
                CompletionDate=x.CompletionDate,
                Image1 = x.Image1,
                Image2 = x.Image2

            }).ToList();
            return data;
        }

        public void SaveAssets(AssetsDto assetsDto)
        {
            if (assetsDto.Id > 0)
            {
                var assets = GenService.GetById<Assets>((long)assetsDto.Id);
                assets.AssetType = assetsDto.AssetType;
                assets.AssetTypeFull = assetsDto.AssetTypeFull;
                assets.OrderId = assetsDto.OrderId;
                GenService.Save(assets);
            }
            else
            {
                GenService.Save(new Assets
                {
                    AssetType = assetsDto.AssetType,
                    AssetTypeFull = assetsDto.AssetTypeFull,
                    OrderId = assetsDto.OrderId
                });
            }
            GenService.SaveChanges();
        }

        public void DeleteAssets(long id)
        {
            GenService.Delete<Assets>(id);
            GenService.SaveChanges();
        }
        public List<HrOfficeDto> GetAllCompanyProfiles()
        {
            return Mapper.Map<List<HrOfficeDto>>(GenService.GetAll<HrOffice>().Where(c => c.Status == EntityStatus.Active).ToList());
        }
    }
}



