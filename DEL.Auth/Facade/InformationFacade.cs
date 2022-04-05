using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using AutoMapper;
using DEL.Auth.DTO;
using DEL.Auth.Infrastructure;
using PagedList;

namespace DEL.Auth.Facade
{
    public class InformationFacade : BaseFacade
    {
        public List<InformationDto> GetInformations()//(List<long?> ApplicationId)
        {
            var districts = GenService.GetAll<Information>().OrderBy(a => a.OrderID).ToList();

            var data = districts.Select(x => new InformationDto
            {
                Id = x.Id,
                //Name = x.Name,
                //BnName = x.BnName,
                OrderID = x.OrderID
            }).ToList();
            return data;
        }

        public long GetPendingObs()
        {
            long result = 0;
            var pendingObs = GenService.GetAll<Information>().Where(p => p.ComplainStatus == ComplainStatus.Pending);
            result = pendingObs.Count();
            return result;
        }
        public long GetInProgressObs()
        {
            long result = 0;
            var inprogObs = GenService.GetAll<Information>().Where(p => p.ComplainStatus == ComplainStatus.Inprogress);
            result = inprogObs.Count();
            return result;
        }
        public long GetCompletedObs()
        {
            long result = 0;
            var completeObs = GenService.GetAll<Information>().Where(p => p.ComplainStatus == ComplainStatus.Completed);
            result = completeObs.Count();
            return result;
        }

        public ResponseDto SaveUpdateActions(InformationDto informationDto, long userId)
        {
            var entity = new Information();
            ResponseDto response = new ResponseDto();
            if (informationDto.Id != null && informationDto.Id > 0)
            {
                entity = GenService.GetById<Information>((long)informationDto.Id);
                informationDto.CreateDate = entity.CreateDate;
                informationDto.CreatedBy = entity.CreatedBy;
                if (informationDto.Status == null)
                    informationDto.Status = entity.Status;
                using (var tran = new TransactionScope())
                {
                    try
                    {
                        Mapper.Map(informationDto, entity);
                        entity.EditDate = DateTime.Now;
                        GenService.Save(entity);
                        response.Id = entity.Id;
                        GenService.SaveChanges();
                        tran.Complete();
                    }
                    catch (Exception ex)
                    {
                        tran.Dispose();
                        response.Message = "Information Action updating failed";
                        return response;
                    }
                }
                response.Success = true;
                response.Id = entity.Id;
                response.Message = "Information updated successfully";
            }
            else
            {
                entity = Mapper.Map<Information>(informationDto);

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
                        response.Message = "Job-Card saving failed";
                        return response;
                    }
                }
                response.Success = true;
                response.Id = entity.Id;
                response.Message = "Job-Card saved successfully";
            }
            return response;
        }

        public InformationDto LoadInformation(long id)
        {
            var information = new InformationDto();
            try
            {
                var info = GenService.GetById<Information>(id);
                if (info != null)
                    return Mapper.Map<InformationDto>(info);

            }
            catch (Exception ex)
            {
                //return null;
            }
            return information;
        }

        public IPagedList<InformationDto> InofrmationList(int pageSize, int pageCount, string searchString, long UserId)
        {
            var allApp = GenService.GetAll<Information>().Where(s => s.Status == EntityStatus.Active).Select(s => new InformationDto
            {
                Id = s.Id,
                FullName = s.FullName,
                Mobile = s.Mobile,
                AreaName = s.DhakaInOut != 0 ? s.Area.BnName : s.District.BnName,
                Location = s.Location,
                Remarks = s.Remarks,
                StatusName = s.ComplainStatus.ToString()
            });
            if (!string.IsNullOrEmpty(searchString))
                allApp = allApp.Where(a => a.AreaName.ToLower().Contains(searchString.ToLower()) || a.Location.ToLower().Contains(searchString.ToLower())
                    || a.Remarks.ToLower().Contains(searchString.ToLower()));
            var temp = allApp.OrderBy(r => r.Id).ToPagedList(pageCount, pageSize);
            return temp;
        }
    }
}



