using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEL.Auth.Infrastructure;
using DEL.Auth.Service;
using Microsoft.Practices.Unity;

namespace DEL.Auth.Facade
{
    public static class FacadeBootStrapper
    {

        public static void RegisterFacadesAndServices(IUnityContainer container)
        {
            RegisterServices(container);
            RegisterFacades(container);
        }
        public static void RegisterFacades(IUnityContainer container)
        {
            //container.RegisterType<IProductFacade, ProductFacade>(new HierarchicalLifetimeManager());
        }
        public static void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<IUnitOfWork, DELAuthServiceAuthUnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType(typeof(IDELAuthService<>), typeof(DELAuthServiceHrmService<>));
        }
    }
}
