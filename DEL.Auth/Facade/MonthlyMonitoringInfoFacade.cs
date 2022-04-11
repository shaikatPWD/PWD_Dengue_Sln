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
        //public ResponseDto SaveMonthlyMonitoringInfo(List<MonthlyMonitoringInfoDto> dto, long userId, long? officeId)
        //{
        //    var entity = new MonthlyMonitoringInfo();
        //    var response = new ResponseDto();

        //    //if (dto.Count > 0)
        //    //{
        //    //    foreach (var item in dto)
        //    //    {
        //    //        entity = GenService.GetById<MonthlyMonitoringInfo>((long)item.Id);
        //    //        item.OfficeId = officeId;
        //    //        item.OfficeAssetId = entity.OfficeAssetId;
        //    //        item.
        //    //    }
        //    //}

        //    //if (dto.Id != null && dto.Id > 0)
        //    //{
        //    //    entity = GenService.GetById<WorkRecordDetails>((long)dto.Id);
        //    //    dto.OfficeId = officeId > 0 ? officeId : entity.OfficeId;
        //    //    dto.OfAssetId = entity.AssetId;
        //    //    dto.CreateDate = entity.CreateDate;
        //    //    dto.CreatedBy = entity.CreatedBy;
        //    //    if (dto.Status == null)
        //    //        dto.Status = entity.Status;
        //    //    using (var tran = new TransactionScope())
        //    //    {
        //    //        try
        //    //        {
        //    //            Mapper.Map(dto, entity);
        //    //            entity.EditDate = DateTime.Now;
        //    //            GenService.Save(entity);
        //    //            response.Id = entity.Id;
        //    //            GenService.SaveChanges();
        //    //            tran.Complete();
        //    //        }
        //    //        catch (Exception ex)
        //    //        {
        //    //            tran.Dispose();
        //    //            response.Message = "Workrecord updating failed";
        //    //            return response;
        //    //        }
        //    //    }
        //    //    response.Success = true;
        //    //    response.Id = entity.Id;
        //    //    response.Message = "Information updated successfully";
        //    //}
        //    //else
        //    //{
        //    if (dto.Count > 0)
        //    {
        //        entity = Mapper.Map<MonthlyMonitoringInfo>(dto);
        //        foreach (var item in dto)
        //        {
        //            if (officeId > 0)
        //                entity.OfficeId = officeId;
        //            entity.Status = EntityStatus.Active;
        //            entity.CreatedBy = userId;
        //            entity.CreateDate = DateTime.Now;
        //            using (var tran = new TransactionScope())
        //            {
        //                try
        //                {
        //                    GenService.Save(entity);
        //                    //response.Id = entity.Id;
        //                    GenService.SaveChanges();
        //                    tran.Complete();
        //                }
        //                catch (Exception ex)
        //                {
        //                    tran.Dispose();
        //                    //response.Message = "Workrecord saving failed";
        //                    //return response;
        //                }
        //            }

        //        }
        //        response.Success = true;
        //        //response.Id = entity.Id;
        //        response.Message = "Workrecord saved successfully";
        //    }
        //    //}
        //    return response;
        //}
        //public WorkRecordDetailsDto LoadWorkRecord(long id)
        //{
        //    var workRecord = new WorkRecordDetailsDto();
        //    try
        //    {
        //        var info = GenService.GetById<WorkRecordDetails>(id);
        //        if (info != null)
        //            return Mapper.Map<WorkRecordDetailsDto>(info);

        //    }
        //    catch (Exception ex)
        //    {
        //        //return null;
        //    }
        //    return workRecord;
        //}

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

        #region Needfull Codes
        //var data = offAsslist.Select(x => new OfficeAssetsDto
        //{
        //    Id = x.Id,
        //    AssetName = x.AssetName
        //    AssetTypeFull = x.AssetTypeFull,
        //    WorkRecordDetails = GetAssetsDetails(x.Id),
        //    OrderId = x.OrderId
        //}).ToList();

        //var activityData = workActivities.Where(a => offAsslist.Select(i => i.Id).Contains((long)a.OfficeAssetId)).ToList();
        //var data = new List<WorkActivityDto>();
        ////var districts = GenService.GetAll<WorkActivity>().Where(w => w.OfficeId == officeId).ToList(); //.Where(o=>o.HrOfficeId == officeId).ToList();
        //bool hasData = false;
        //activityData.ForEach(o =>
        //{
        //    data.ForEach(a =>
        //    {
        //        if (a.Period == o.Period) { hasData = true; return; };
        //    });
        //    if (!hasData)
        //    {
        //        data.Add(new WorkActivityDto()
        //        {
        //            Id= o.Id,
        //            OfficeAssetId = o.OfficeAssetId,
        //            OfficeAssetName = offAsslist.First(x => x.Id == o.OfficeAssetId).AssetName,//o.OfficeAssetName,
        //            Date = o.Date,
        //            Period = o.Period,
        //            IsPondsCleanUp = o.IsPondsCleanUp,
        //            IsPondsCleanUpName = Enum.GetName(typeof(IsComplete), o.IsPondsCleanUp),
        //            IsWastageCleanUp = o.IsWastageCleanUp,
        //            IsWastageCleanUpName = Enum.GetName(typeof(IsComplete), o.IsPondsCleanUp),
        //            IsMedicalCollegeCleanUp = o.IsMedicalCollegeCleanUp,
        //            IsMedicalCollegeCleanUpName = Enum.GetName(typeof(IsComplete), o.IsPondsCleanUp),
        //            IsOfficeAndHouseholdCleanUp = o.IsOfficeAndHouseholdCleanUp,
        //            IsOfficeAndHouseholdCleanUpName = Enum.GetName(typeof(IsComplete), o.IsPondsCleanUp),
        //            IsStillWaterCleanUp = o.IsStillWaterCleanUp,
        //            IsStillWaterCleanUpName = Enum.GetName(typeof(IsComplete), o.IsPondsCleanUp),
        //            IsCuringWaterCleanUp = o.IsCuringWaterCleanUp,
        //            IsCuringWaterCleanUpName = Enum.GetName(typeof(IsComplete), o.IsPondsCleanUp),
        //            IsUnderConstructionBuildingCleanUp = o.IsUnderConstructionBuildingCleanUp,
        //            IsUnderConstructionBuildingCleanUpName = Enum.GetName(typeof(IsComplete), o.IsPondsCleanUp),
        //        });
        //    }
        //});
        //data.ToList().GroupBy(x => x.Period.ToString());
        #endregion

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
    }
}



