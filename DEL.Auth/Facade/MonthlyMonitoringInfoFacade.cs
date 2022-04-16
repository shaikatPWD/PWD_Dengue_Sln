using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using AutoMapper;
using DEL.Auth.DTO;
using DEL.Auth.Infrastructure;
using PagedList;

namespace DEL.Auth.Facade
{
    public class MonthlyMonitoringInfoFacade : BaseFacade
    {
        public List<WorkActivityDto> LoadMonthlyMonitorinInfoByOffice(long? officeId)//(List<long?> ApplicationId) long officeId
        {
            var offAsslist = GenService.GetAll<OfficeAssets>().Where(o => o.HrOfficeId == officeId).ToList();
            var data = new List<WorkActivityDto>();
            offAsslist.ToList().ForEach(o =>
            {
                data.Add(new WorkActivityDto()
                {
                    OfficeAssetId = o.Id,
                    OfficeAssetName = o.AssetName,
                    Period = DateTime.Now.ToString("MMMM dd")
                });
            });
            return data;
        }

        public List<WorkActivityDto> LoadPeriods(long? officeId)//(List<long?> ApplicationId) long officeId
        {
            var offAsslist = GenService.GetAll<OfficeAssets>().Where(o => o.HrOfficeId == officeId).ToList();
            var currentPeriod = DateTime.Now.ToString("MMMM-yy");
            var workActivitylist = GenService.GetAll<WorkActivity>().Where(w => w.Period == currentPeriod).ToList();
            var shiftIds = offAsslist.Select(s => s.Id).ToList();
            var data = new List<WorkActivityDto>();
            var query = (from x in workActivitylist
                         where shiftIds.Contains((long)x.OfficeAssetId)
                         select x.OfficeAssetId);
            var acCount = query.Select(c => c.Value).Distinct().Count();

            if (acCount != offAsslist.Count)
            {
                data.Add(new WorkActivityDto()
                {
                    Period = currentPeriod
                });
            }
            return data;
        }

        public List<WorkActivityDto> LoadAllPeriods(long? officeId)//(List<long?> ApplicationId) long officeId
        {
            var officeName = GenService.GetById<HrOffice>((long)officeId).Name;
            var workActivitylist = GenService.GetAll<WorkActivity>();//.Where(w => w.Period == currentPeriod).ToList();
            var data = new List<WorkActivityDto>();
            data = workActivitylist.GroupBy(d => new {d.Period }).Select(x => new WorkActivityDto
            {
                OfficeName = officeName,
                Period = x.Key.Period
            }).ToList();//.GroupBy(x => x.Period);

            return data;//(List<WorkActivityDto>)data.GroupBy(x => x.Period);
        }

        public ResponseDto SaveWorkActivityforInstallation(WorkActivityDto dto, long userId)
        {
            var entity = new WorkActivity();
            var response = new ResponseDto();
            entity = Mapper.Map<WorkActivity>(dto);

            entity.OfficeAssetId = (long)dto.OfficeAssetId;
            entity.Status = EntityStatus.Active;
            entity.CreatedBy = userId;
            entity.CreateDate = DateTime.Now;
            using (var tran = new TransactionScope())
            {
                try
                {
                    GenService.Save(entity);
                    response.Id = entity.Id;
                    GenService.SaveChanges();
                    tran.Complete();
                }
                catch (Exception ex)
                {
                    tran.Dispose();
                    response.Message = "Workrecord saving failed";
                    return response;
                }
            }
            response.Success = true;
            response.Id = entity.Id;
            response.Message = "Workrecord saved successfully";
            //}
            return response;
        }

        public ResponseDto SaveWorkActivityDetails(List<OfficeAssetsDto> dto, long userId)
        {
            //var entity = new List<WorkActivity>();
            var response = new ResponseDto();
            //entity = Mapper.Map<List<WorkActivity>>(dto);
            using (var tran = new TransactionScope())
            {
                var entity = Mapper.Map<List<WorkActivity>>(dto);
                foreach (var item in entity)
                {
                    //var entity2 = Mapper.Map<WorkActivity>(item);
                    //item.Id = 0;
                    item.Period = DateTime.Now.ToString("MMMM-yy");
                    item.OfficeAssetId = item.Id;
                    item.Id = 0;
                    item.CreateDate = DateTime.Now;
                    item.CreatedBy = userId;
                    item.Status = EntityStatus.Active;
                    GenService.Save(item);

                }
                GenService.SaveChanges();
                tran.Complete();
            }
            response.Success = true;
            response.Message = "Activity Details Saved Successfully..";
            return response;
        }

        public List<WorkActivityDto> GetWorkActivityRecordsByInstallationPeriod(long officeAssetId, string period)//(List<long?> ApplicationId) long officeId
        {
            var districts = GenService.GetAll<WorkActivity>().Where(w => w.OfficeAssetId == officeAssetId && w.Period == period).ToList(); //.Where(o=>o.HrOfficeId == officeId).ToList();

            var data = districts.Select(x => new WorkActivityDto
            {
                Id = x.Id,
                OfficeAssetId = x.OfficeAssetId,
                OfficeAssetName = x.OfficeAssets.AssetName,
                Date = x.Date,
                IsPondsCleanUp = x.IsPondsCleanUp,
                IsPondsCleanUpName = Enum.GetName(typeof(IsComplete), x.IsPondsCleanUp),
                IsWastageCleanUp = x.IsWastageCleanUp,
                IsWastageCleanUpName = Enum.GetName(typeof(IsComplete), x.IsWastageCleanUp),
                IsMedicalCollegeCleanUp = x.IsMedicalCollegeCleanUp,
                IsMedicalCollegeCleanUpName = Enum.GetName(typeof(IsComplete), x.IsMedicalCollegeCleanUp),
                IsOfficeAndHouseholdCleanUp = x.IsOfficeAndHouseholdCleanUp,
                IsOfficeAndHouseholdCleanUpName = Enum.GetName(typeof(IsComplete), x.IsOfficeAndHouseholdCleanUp),
                IsStillWaterCleanUp = x.IsStillWaterCleanUp,
                IsStillWaterCleanUpName = Enum.GetName(typeof(IsComplete), x.IsStillWaterCleanUp),
                IsCuringWaterCleanUp = x.IsCuringWaterCleanUp,
                IsCuringWaterCleanUpName = Enum.GetName(typeof(IsComplete), x.IsCuringWaterCleanUp),
                IsUnderConstructionBuildingCleanUp = x.IsUnderConstructionBuildingCleanUp,
                IsUnderConstructionBuildingCleanUpName = Enum.GetName(typeof(IsComplete), x.IsUnderConstructionBuildingCleanUp)
            }).ToList();
            return data;
        }
        public OfficeAssetsDto GetOfficeAssetNamebyId(long assetId)//(List<long?> ApplicationId) long officeId
        {
            return Mapper.Map<OfficeAssetsDto>(GenService.GetById<OfficeAssets>(assetId));
        }
        public List<OfficeAssetsDto> GetWorkActivityRecordsByPeriod()//(List<long?> ApplicationId) long officeId
        {
            var offAsslist = GenService.GetAll<OfficeAssets>();
            var data = new List<OfficeAssetsDto>();

            var dateText = DateTime.Now.ToString("dd/MM/yyyy");
            var name = Enum.GetName(typeof(IsComplete), 0);
            //var name2 = Enum.GetName(typeof(IsComplete), 2);

            data = offAsslist.Select(t => new OfficeAssetsDto()
            {
                Id = t.Id,
                AssetName = t.AssetName,
                Date = DateTime.Now,
                DateText = dateText,
                IsPondsCleanUp = IsComplete.NA,
                IsPondsCleanUpName = name,
                IsWastageCleanUp = IsComplete.NA,
                IsWastageCleanUpName = name,
                IsMedicalCollegeCleanUp = IsComplete.NA,
                IsMedicalCollegeCleanUpName = name,
                IsOfficeAndHouseholdCleanUp = IsComplete.NA,
                IsOfficeAndHouseholdCleanUpName = name,
                IsStillWaterCleanUp = IsComplete.NA,
                IsStillWaterCleanUpName = name,
                IsCuringWaterCleanUp = IsComplete.NA,
                IsCuringWaterCleanUpName = name,
                IsUnderConstructionBuildingCleanUp = IsComplete.NA,
                IsUnderConstructionBuildingCleanUpName = name
            }).ToList();

            return data;
        }

        public List<WorkActivityDto> GetWorkActivityRecordsByPeriod(string period)//(List<long?> ApplicationId) long officeId
        {
            var districts = GenService.GetAll<WorkActivity>().Where(w => w.Period == period).ToList(); //.Where(o=>o.HrOfficeId == officeId).ToList();

            var data = districts.Select(x => new WorkActivityDto
            {
                Id = x.Id,
                OfficeAssetId = x.OfficeAssetId,
                OfficeAssetName = x.OfficeAssets.AssetName,
                Date = x.Date,
                IsPondsCleanUp = x.IsPondsCleanUp,
                IsPondsCleanUpName = Enum.GetName(typeof(IsComplete), x.IsPondsCleanUp),
                IsWastageCleanUp = x.IsWastageCleanUp,
                IsWastageCleanUpName = Enum.GetName(typeof(IsComplete), x.IsWastageCleanUp),
                IsMedicalCollegeCleanUp = x.IsMedicalCollegeCleanUp,
                IsMedicalCollegeCleanUpName = Enum.GetName(typeof(IsComplete), x.IsMedicalCollegeCleanUp),
                IsOfficeAndHouseholdCleanUp = x.IsOfficeAndHouseholdCleanUp,
                IsOfficeAndHouseholdCleanUpName = Enum.GetName(typeof(IsComplete), x.IsOfficeAndHouseholdCleanUp),
                IsStillWaterCleanUp = x.IsStillWaterCleanUp,
                IsStillWaterCleanUpName = Enum.GetName(typeof(IsComplete), x.IsStillWaterCleanUp),
                IsCuringWaterCleanUp = x.IsCuringWaterCleanUp,
                IsCuringWaterCleanUpName = Enum.GetName(typeof(IsComplete), x.IsCuringWaterCleanUp),
                IsUnderConstructionBuildingCleanUp = x.IsUnderConstructionBuildingCleanUp,
                IsUnderConstructionBuildingCleanUpName = Enum.GetName(typeof(IsComplete), x.IsUnderConstructionBuildingCleanUp)
            }).ToList();
            return data;
        }
    }
}



