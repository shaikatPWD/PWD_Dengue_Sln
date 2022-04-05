using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.App_Start
{
    public class AutoMapperBootstrapper
    {
        public static void BootStrapAutoMaps()
        {
            DefaultMappings();
        }

        private static void DefaultMappings()
        {
            //GenService _service = new GenService();

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new DEL.Auth.Facade.AutoMaps.AuthMappingProfile());
                //cfg.AddProfile(new DEL.GoBangla.Facade.AutoMaps.GoBanglaMappingProfile());
                //cfg.AddProfile(new DEL.Accounts.Facade.AutoMaps.AccountsMappingProfile());
                //cfg.AddProfile(new DEL.IPDC.Facade.AutoMaps.IPDCMappingProfile());
            });

        }
    }
}
