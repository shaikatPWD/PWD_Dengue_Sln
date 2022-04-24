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
            var office = GenService.GetAll<HrOffice>();
            var work = GenService.GetAll<WorkRecordDetails>();

            var td = from s in work
                     join r in office on s.OfficeId equals r.Id
                     select s;

            var data = td.GroupBy(d => new { d.OfficeId, d.HrOffice.Name }).ToList().Select(x => new WorkRecordDetailsDto
            {
                Id = x.Key.OfficeId,
                OfficeName = x.Key.Name,
                //CompletionDatetxt = string.Join("<br>\n", x.Select(i => i.CompletionDate))
                CompletionDatetxt = GetOnlyDate((long)x.Key.OfficeId)//string.Join(" ", x.Select(i => i.CompletionDate))
            });
            return data.ToList();
        }

        public List<WorkRecordDetailsDto> GetAllWorkDetReport()//(List<long?> ApplicationId) long officeId
        {
            var office = GenService.GetAll<HrOffice>();
            var work = GenService.GetAll<WorkRecordDetails>();

            var td = from s in work
                     join r in office on s.OfficeId equals r.Id
                     select s;

            var data = td.GroupBy(d => new { d.OfficeId, d.HrOffice.Name }).ToList().Select(x => new WorkRecordDetailsDto
            {
                Id = x.Key.OfficeId,
                OfficeName = x.Key.Name,
                IsPondsCleanUptxt = GetIsPondCompleteTxt((long)x.Key.OfficeId,1),
                IsPondsCleanUpDatetxt = GetdateList((long)x.Key.OfficeId, 1),
                IsWastageCleanUptxt = GetIsPondCompleteTxt((long)x.Key.OfficeId, 2),
                IsWastageCleanUpDatetxt = GetdateList((long)x.Key.OfficeId, 2),
                IsMedicalCollegeCleanUptxt = GetIsPondCompleteTxt((long)x.Key.OfficeId,3 ),
                IsMedicalCollegeCleanUpDatetxt = GetdateList((long)x.Key.OfficeId, 3),
                IsOfficeAndHouseholdCleanUptxt = GetIsPondCompleteTxt((long)x.Key.OfficeId, 4),
                IsOfficeAndHouseholdCleanUpDatetxt = GetdateList((long)x.Key.OfficeId, 4),
                IsStillWaterCleanUptxt = GetIsPondCompleteTxt((long)x.Key.OfficeId, 5),
                IsStillWaterCleanUpDatetxt = GetdateList((long)x.Key.OfficeId, 5),
                IsCuringWaterCleanUptxt = GetIsPondCompleteTxt((long)x.Key.OfficeId, 6),
                IsCuringWaterCleanUpDatetxt = GetdateList((long)x.Key.OfficeId, 6),
                IsUnderConstructionBuildingCleanUptxt = GetIsPondCompleteTxt((long)x.Key.OfficeId, 7),
                IsUnderConstructionBuildingCleanUpDatetxt = GetdateList((long)x.Key.OfficeId, 7)
            });
            return data.ToList();
        }

        public string GetIsPondCompleteTxt(long officeId, long assetId)
        {
            var work = GenService.GetAll<WorkRecordDetails>().Where(w=> w.OfficeId == officeId && w.AssetId == assetId);

            var isPondCompleteTxt = work.Select(i => i.CompletionDate).ToList().Count > 0 ? "Yes" : "";

            return isPondCompleteTxt;
        }

        public string GetOnlyDate(long officeId)
        {
            var work = GenService.GetAll<WorkRecordDetails>().Where(o => o.OfficeId == officeId);
            List<string> datesString = new List<string>();

            foreach (var d in work)
            {
                string[] words = d.CompletionDate.ToString().Split(' ');
                datesString.Add(words[0]);
            }

            //datesString.Add(work.Select(i => i.CompletionDate));
            var dates = string.Join("<br>\n", datesString);

            //var datestr = datesString.Replace(",", Environment.NewLine); //"\n".Join(dates);

            return dates;
        }

        public string GetdateList(long officeId, long assetId)
        {
            var work = GenService.GetAll<WorkRecordDetails>().Where(o => o.OfficeId == officeId && o.AssetId == assetId);
            List<string> datesString = new List<string>();

            foreach (var d in work)
            {
                string[] words = d.CompletionDate.ToString().Split(' ');
                datesString.Add(words[0]);
            }

            //datesString.Add(work.Select(i => i.CompletionDate));
            var dates = string.Join("<br>\n", datesString);

            //var datestr = datesString.Replace(",", Environment.NewLine); //"\n".Join(dates);

            return dates;
        }


        public List<WorkRecordDetailsDto> DengueReport()//(List<long?> ApplicationId) long officeId
        {
            var office = GenService.GetAll<HrOffice>();
            var work = GenService.GetAll<WorkRecordDetails>();

            var td = from s in work
                     join r in office on s.OfficeId equals r.Id
                     select s;

            var data = td.GroupBy(d => new { d.OfficeId, d.HrOffice.Name }).Select(x => new WorkRecordDetailsDto
            {
                Id = x.Key.OfficeId,
                OfficeName = x.Key.Name
            }).ToList();
            return data.ToList();
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



