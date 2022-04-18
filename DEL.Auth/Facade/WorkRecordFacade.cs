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
    public class WorkRecordFacade : BaseFacade
    {
        public ResponseDto SaveWorkRecord(WorkRecordDetailsDto dto, long userId, long? officeId)
        {
            var entity = new WorkRecordDetails();
            var response = new ResponseDto();
            //if (dto.Id != null && dto.Id > 0)
            //{
            //    entity = GenService.GetById<WorkRecordDetails>((long)dto.Id);
            //    dto.OfficeId = officeId > 0 ? officeId : entity.OfficeId;
            //    dto.AssetId = entity.AssetId;
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
            //            #region comments
            //            //if (dto.WorkRecordDetails != null)
            //            //{
            //            //    foreach (var item in dto.WorkRecordDetails)
            //            //    {
            //            //        WorkRecordDetails details;
            //            //        if (item.Id > 0)
            //            //        {
            //            //            details = GenService.GetById<WorkRecordDetails>(item.Id);
            //            //            item.Status = details.Status;
            //            //            item.CreateDate = details.CreateDate;
            //            //            item.CreatedBy = details.CreatedBy;
            //            //            item.WorkRecordId = details.WorkRecordId;
            //            //            item.EditDate = DateTime.Now;
            //            //            item.EditedBy = userId;
            //            //            Mapper.Map(item, details);
            //            //            GenService.Save(details);
            //            //        }
            //            //        else
            //            //        {
            //            //            details = new WorkRecordDetails();
            //            //            item.CreateDate = DateTime.Now;
            //            //            item.CreatedBy = userId;
            //            //            item.Status = EntityStatus.Active;
            //            //            item.WorkRecordId = entity.Id;
            //            //            details = Mapper.Map<WorkRecordDetails>(item);
            //            //            GenService.Save(details);
            //            //        }
            //            //    }
            //            //}
            //            //if (dto.RemoveWorkRecordDetails != null)
            //            //{
            //            //    foreach (var item in dto.RemoveWorkRecordDetails)
            //            //    {
            //            //        var details = GenService.GetById<WorkRecordDetails>(item);
            //            //        if (details != null)
            //            //        {
            //            //            details.Status = EntityStatus.Inactive;
            //            //            details.EditDate = DateTime.Now;
            //            //            details.EditedBy = userId;
            //            //        }
            //            //        GenService.Save(details);
            //            //    }
            //            //}
            //            #endregion
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
            string file1 = DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + "_Image_1_" + Convert.ToString(officeId) + "_" + Convert.ToString(dto.AssetId);
            string file2 = DateTime.Now.ToString("yyyyMMddHHmmssfffffff") + "_Image_2_" + Convert.ToString(officeId) + "_" + Convert.ToString(dto.AssetId);
            dto.Image1 = dto.Image1 != null ? ConvertImage(dto.Image1.Substring(23), file1) : null;
            dto.Image2 = dto.Image2 != null ? ConvertImage(dto.Image2.Substring(23), file2) : null;
            entity = Mapper.Map<WorkRecordDetails>(dto);
            if (officeId > 0)
                entity.OfficeId = officeId;
            entity.AssetId = (long)dto.AssetId;
            entity.Status = EntityStatus.Active;
            entity.CreatedBy = userId;
            entity.CreateDate = DateTime.Now;
            using (var tran = new TransactionScope())
            {
                try
                {
                    #region comments
                    //if (dto.WorkRecordDetails != null && dto.WorkRecordDetails.Count > 0)
                    //{
                    //    entity.WorkRecordDetails = Mapper.Map<List<WorkRecordDetails>>(dto.WorkRecordDetails);
                    //    foreach (var item in entity.WorkRecordDetails)
                    //    {
                    //        item.CreateDate = DateTime.Now;
                    //        item.CreatedBy = userId;
                    //        item.Status = EntityStatus.Active;
                    //    }
                    //}
                    #endregion
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
        public string ConvertImage(string img, string fileName)
        {
            string serverpath = HttpContext.Current.Server.MapPath("~/UploadImages/InfoImages/");
            string filePath = serverpath + fileName + ".jpg";
            File.WriteAllBytes(filePath, Convert.FromBase64String(img));
            return fileName;
        }
        public WorkRecordDetailsDto LoadWorkRecord(long id)
        {
            var workRecord = new WorkRecordDetailsDto();
            try
            {
                var info = GenService.GetById<WorkRecordDetails>(id);
                if (info != null)
                    return Mapper.Map<WorkRecordDetailsDto>(info);

            }
            catch (Exception ex)
            {
                //return null;
            }
            return workRecord;
        }

        public List<WorkRecordDetailsDto> GetWorkRecords(long officeId, long assetId)//(List<long?> ApplicationId) long officeId
        {
            var districts = GenService.GetAll<WorkRecordDetails>().Where(w => w.OfficeId == officeId && w.AssetId == assetId).ToList(); //.Where(o=>o.HrOfficeId == officeId).ToList();

            var data = districts.Select(x => new WorkRecordDetailsDto
            {
                Id = x.Id,
                OfficeId = x.OfficeId,
                AssetId = x.AssetId,
                AssetName = x.Assets.AssetTypeFull,
                AssetBuildingName = x.AssetBuildingName,
                CompletionDate = x.CompletionDate,
                Image1 = x.Image1,
                Image2 = x.Image2,
                OrderId = x.OrderId
            }).ToList();
            return data;
        }

        public AssetsDto GetAssetNamebyAssetId(long assetId)//(List<long?> ApplicationId) long officeId
        {
            return Mapper.Map<AssetsDto>(GenService.GetById<Assets>(assetId));
        }

        public List<HrOfficeDto> GetAllWorksOffices()//(List<long?> ApplicationId) long officeId
        {
            var districts = GenService.GetAll<HrOffice>().Where(w => w.DivisionId > 0 && w.Status == EntityStatus.Active).ToList(); //.Where(o=>o.HrOfficeId == officeId).ToList();

            var data = districts.Select(x => new HrOfficeDto
            {
                Id = x.Id,
                Name = x.Name
                
            }).ToList();
            return data;
        }

        public List<WorkRecordDetailsDto> GetAllWorkDetOffices()//(List<long?> ApplicationId) long officeId
        {
            var office = GenService.GetAll<HrOffice>();//.Where(w => w.DivisionId > 0 && w.Status == EntityStatus.Active).ToList(); //.Where(o=>o.HrOfficeId == officeId).ToList();
            var work = GenService.GetAll<WorkRecordDetails>();

            var td = from s in work
                     join r in office on s.OfficeId equals r.Id
                     //where s.Entity_ID == getEntity
                     select s;

            var data = td.Select(x => new WorkRecordDetailsDto
            {
                Id = x.OfficeId,
                OfficeName = x.HrOffice.Name

            }).ToList();
            return data;
        }

        public List<AssetsDto> GetAssets(long id)//(List<long?> ApplicationId) long officeId
        {
            var districts = GenService.GetAll<Assets>().ToList(); //.Where(o=>o.HrOfficeId == officeId).ToList();

            var data = districts.Select(x => new AssetsDto
            {
                Id = x.Id,
                AssetType = x.AssetType,
                AssetTypeFull = x.AssetTypeFull,
                WorkRecordDetails = GetAssetsDetails(x.Id, id),
                OrderId = x.OrderId
            }).ToList();
            return data;
        }

        public List<WorkRecordDetailsDto> GetAssetsDetails(long asid, long offId)//(List<long?> ApplicationId) long officeId
        {
            var districts = GenService.GetAll<WorkRecordDetails>().Where(a => a.AssetId == asid && a.OfficeId == offId).ToList(); //.Where(o=>o.HrOfficeId == officeId).ToList();

            var data = districts.Select(x => new WorkRecordDetailsDto
            {
                Id = x.Id,
                AssetId = asid,
                AssetBuildingName = x.AssetBuildingName,
                CompletionDate = x.CompletionDate,
                Image1 = x.Image1,
                Image2 = x.Image2

            }).ToList();
            return data;
        }
    }
}



