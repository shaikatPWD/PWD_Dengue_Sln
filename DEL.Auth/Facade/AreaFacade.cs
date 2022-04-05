//using DEL.UI.Areas.Auth.Models;
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
    public class AreaFacade : BaseFacade
    {
        public List<AreaDto> GetAreas()//(List<long?> ApplicationId)
        {
            var districts = GenService.GetAll<Area>().OrderBy(a=>a.OrderID).ToList();

            var data = districts.Select(x => new AreaDto
            {
                Id = x.Id,
                Name = x.Name,
                BnName = x.BnName,
                OrderID = x.OrderID
            }).ToList();
            return data;
        }

        public void SaveArea(AreaDto areaDto)
        {
            if (areaDto.Id > 0)
            {
                var area = GenService.GetById<Area>((long)areaDto.Id);
                area.Name = areaDto.Name;
                area.BnName = areaDto.BnName;
                area.OrderID = (int)areaDto.OrderID;
                area.HrOfficeId = (int)areaDto.HrOfficeId;
                GenService.Save(area);
            }
            else
            {
                GenService.Save(new Area
                {
                    Name = areaDto.Name,
                    BnName = areaDto.BnName,
                    OrderID = (int)areaDto.OrderID,
                    HrOfficeId = (int)areaDto.HrOfficeId
            });
            }
            GenService.SaveChanges();
        }

        public void DeleteArea(long id)
        {
            GenService.Delete<Area>(id);
            GenService.SaveChanges();
        }
        public List<HrOfficeDto> GetAllCompanyProfiles()
        {
            return Mapper.Map<List<HrOfficeDto>>(GenService.GetAll<HrOffice>().Where(c=>c.Status==EntityStatus.Active).ToList());
        }
    }
}



