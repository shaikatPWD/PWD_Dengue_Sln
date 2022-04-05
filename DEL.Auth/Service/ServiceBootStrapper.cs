using System;
using DEL.Auth.Infrastructure;
using Microsoft.Practices.Unity;

namespace DEL.Auth.Service
{
    public static class ServicesBootStrapper
    {
        public static Func<LifetimeManager> DefaultLifetime = () => new HierarchicalLifetimeManager();
        public static void InitializeAll(IUnityContainer container)
        {
            Initialize(container);
        }

        public static void Initialize(IUnityContainer container)
        {
            container.RegisterType<IUnitOfWork, DELAuthServiceAuthUnitOfWork>(DefaultLifetime());
        }

    }
}
