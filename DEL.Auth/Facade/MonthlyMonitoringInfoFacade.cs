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
        public ResponseDto SaveMonthlyMonitoringInfo(List<MonthlyMonitoringInfoDto> dto, long userId, long? officeId)
        {
            var entity = new MonthlyMonitoringInfo();
            var response = new ResponseDto();

            //if (dto.Count > 0)
            //{
            //    foreach (var item in dto)
            //    {
            //        entity = GenService.GetById<MonthlyMonitoringInfo>((long)item.Id);
            //        item.OfficeId = officeId;
            //        item.OfficeAssetId = entity.OfficeAssetId;
            //        item.
            //    }
            //}

            //if (dto.Id != null && dto.Id > 0)
            //{
            //    entity = GenService.GetById<WorkRecordDetails>((long)dto.Id);
            //    dto.OfficeId = officeId > 0 ? officeId : entity.OfficeId;
            //    dto.OfAssetId = entity.AssetId;
            //    dto.CreateDate = entity.CreateDate;
            //    dto.CreatedBy = entity.CreatedBy;
            //    if (dto.Status == null)
            //        dto.Status = entity.Status;
            //    using (var tran = new TransactionScope())
            //    {
            //        try
            //        {
            //            Mapper.Map(dto, entity);
            //            entity.EditDate = DateTime.Now;
            //            GenService.Save(entity);
            //            response.Id = entity.Id;
            //            GenService.SaveChanges();
            //            tran.Complete();
            //        }
            //        catch (Exception ex)
            //        {
            //            tran.Dispose();
            //            response.Message = "Workrecord updating failed";
            //            return response;
            //        }
            //    }
            //    response.Success = true;
            //    response.Id = entity.Id;
            //    response.Message = "Information updated successfully";
            //}
            //else
            //{
            if (dto.Count > 0)
            {
                entity = Mapper.Map<MonthlyMonitoringInfo>(dto);
                foreach (var item in dto)
                {
                    if (officeId > 0)
                        entity.OfficeId = officeId;
                    entity.Status = EntityStatus.Active;
                    entity.CreatedBy = userId;
                    entity.CreateDate = DateTime.Now;
                    using (var tran = new TransactionScope())
                    {
                        try
                        {
                            GenService.Save(entity);
                            //response.Id = entity.Id;
                            GenService.SaveChanges();
                            tran.Complete();
                        }
                        catch (Exception ex)
                        {
                            tran.Dispose();
                            //response.Message = "Workrecord saving failed";
                            //return response;
                        }
                    }

                }
                response.Success = true;
                //response.Id = entity.Id;
                response.Message = "Workrecord saved successfully";
            }
            //}
            return response;
        }        
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

        public List<MonthlyMonitoringInfoDto> LoadMonthlyMonitorinInfoByOffice(long? officeId)//(List<long?> ApplicationId) long officeId
        {
            var districts = GenService.GetAll<MonthlyMonitoringInfo>().Where(w => w.OfficeId == officeId).ToList(); //.Where(o=>o.HrOfficeId == officeId).ToList();

            var data = districts.Select(x => new MonthlyMonitoringInfoDto
            {
                Id = x.Id,
                //OfficeId = x.OfficeId,
                OfficeAssetId = x.OfficeAssets.Id,
                OfficeAssetName = x.OfficeAssets.AssetName,
                IsPondCleanUpName = Enum.GetName(typeof(IsComplete), x.IsPondsCleanUp),
                IsWastageCleanUpName = Enum.GetName(typeof(IsComplete), x.IsWastageCleanUp),
                IsMedicalCollegeCleanUpName = Enum.GetName(typeof(IsComplete), x.IsMedicalCollegeCleanUp),
                IsOfficeAndHouseholdCleanUpName = Enum.GetName(typeof(IsComplete), x.IsOfficeAndHouseholdCleanUp),
                IsStillWaterCleanUpName = Enum.GetName(typeof(IsComplete), x.IsStillWaterCleanUp),
                IsCuringWaterCleanUpName = Enum.GetName(typeof(IsComplete), x.IsCuringWaterCleanUp),
                IsUnderConstructionBuildingCleanUpName = Enum.GetName(typeof(IsComplete), x.IsUnderConstructionBuildingCleanUp)

            }).ToList();
            return data;
        }

        public AssetsDto GetAssetNamebyAssetId(long assetId)//(List<long?> ApplicationId) long officeId
        {
            return Mapper.Map<AssetsDto>(GenService.GetById<Assets>(assetId));
        }
    }
}



