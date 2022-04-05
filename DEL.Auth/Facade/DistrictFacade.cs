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
    public class DistrictFacade : BaseFacade
    {
        public List<DistrictDto> GetDistricts()//(List<long?> ApplicationId)
        {
            var districts = GenService.GetAll<District>().ToList();



            var data = districts.Select(x => new DistrictDto
            {
                Id = x.Id,
                Name = x.Name,
                BnName = x.BnName,
                OrderID = x.OrderID
            }).ToList();
            return data;
        }

        public void SaveDistrict(DistrictDto districtDto)
        {
            if (districtDto.Id > 0)
            {
                var district = GenService.GetById<District>((long)districtDto.Id);
                district.Name = districtDto.Name;
                district.BnName = districtDto.BnName;
                district.OrderID = (int)districtDto.OrderID;
                GenService.Save(district);

            }
            else
            {
                GenService.Save(new District
                {
                    Name = districtDto.Name,
                    BnName = districtDto.BnName,
                    OrderID = (int)districtDto.OrderID
                });
            }
            GenService.SaveChanges();
        }

        public void DeleteDistrict(long id)
        {
            GenService.Delete<District>(id);
            GenService.SaveChanges();
        }        
    }
}



