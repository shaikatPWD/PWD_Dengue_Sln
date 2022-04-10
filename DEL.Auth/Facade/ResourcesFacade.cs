//using DEL.UI.Resourcess.Auth.Models;
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
    public class ResourcesFacade : BaseFacade
    {
        public List<ResourcesDto> GetResources(long? officeid)//(List<long?> ApplicationId)
        {
            var districts = GenService.GetAll<Resources>().Where(r => r.HrOfficeId == officeid).OrderBy(a => a.OrderID).ToList();

            var data = districts.Select(x => new ResourcesDto
            {
                Id = x.Id,
                ResourceName = x.ResourceName,
                Quantity = x.Quantity,
                OrderID = x.OrderID
            }).ToList();
            return data;
        }

        public void SaveResources(ResourcesDto resourcesDto, long? officeid)
        {
            if (resourcesDto.Id > 0)
            {
                var resources = GenService.GetById<Resources>((long)resourcesDto.Id);
                resources.ResourceName = resourcesDto.ResourceName;
                resources.Quantity = resourcesDto.Quantity;
                resources.OrderID = (int)resourcesDto.OrderID;
                resources.HrOfficeId = (long)officeid;//(int)resourcesDto.HrOfficeId;
                GenService.Save(resources);
            }
            else
            {
                GenService.Save(new Resources
                {
                    ResourceName = resourcesDto.ResourceName,
                    Quantity = resourcesDto.Quantity,
                    OrderID = (int)resourcesDto.OrderID,
                    HrOfficeId = (long)officeid//(int)resourcesDto.HrOfficeId
                });
            }
            GenService.SaveChanges();
        }

        public void DeleteResources(long id)
        {
            GenService.Delete<Resources>(id);
            GenService.SaveChanges();
        }
        public List<HrOfficeDto> GetAllCompanyProfiles()
        {
            return Mapper.Map<List<HrOfficeDto>>(GenService.GetAll<HrOffice>().Where(c => c.Status == EntityStatus.Active).ToList());
        }
    }
}



