using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
//using WebUI;
//using Finix.Southern.Util;
//using Finix.Southern.DTO;
//using Finix.Southern.Infrastructure;
//using Finix.Southern.Infrastructure.Models;

namespace WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //ControllerBuilder.Current.DefaultNamespaces.Add("Finix.Accounts.Controllers");
            //automapping
            App_Start.AutoMapperBootstrapper.BootStrapAutoMaps();
            //set seed_data path
            DEL.Auth.Util.AuthSystem.SeedDataPath = string.Format(@"{0}\..\..\DEL.Auth\{1}\{2}",
                AppDomain.CurrentDomain.GetData("DataDirectory"),
                "App_Data",
                "seed_data");
            //DEL.GoBangla.Util.GoBanglaSystem.SeedDataPath = string.Format(@"{0}\..\..\DEL.GoBangla\{1}\{2}",
            //   AppDomain.CurrentDomain.GetData("DataDirectory"),
            //   "App_Data",
            //   "seed_data");
            //Finix.FillingStation.Util.FillingStationSystem.SeedDataPath = string.Format(@"{0}\..\..\Finix.FillingStation\{1}\{2}",
            //   AppDomain.CurrentDomain.GetData("DataDirectory"),
            //   "App_Data",
            //   "seed_data");
            //Finix.CNGStation.Util.CNGStationSystem.SeedDataPath = string.Format(@"{0}\..\..\Finix.CNGStation\{1}\{2}",
            //   AppDomain.CurrentDomain.GetData("DataDirectory"),
            //   "App_Data",
            //   "seed_data");
            //Finix.HRM.Util.HRMSystem.SeedDataPath = string.Format(@"{0}\..\..\Finix.HRM\{1}\{2}",
            //   AppDomain.CurrentDomain.GetData("DataDirectory"),
            //   "App_Data",
            //   "seed_data");
            //Task.Run(() => BGWorker.DoWork(null, null));
        }
    }
    //public static class BGWorker
    //{
    //    public static void DoWork(object sender, DoWorkEventArgs e)
    //    {
    //        int delay = 5;
    //        try { delay = Convert.ToInt16(ConfigReader.GetAppSetting("service_interval_in_seconds")); }
    //        catch { }
    //       // Dummywork();
    //        while (true)
    //        {
    //            N.ProcessNotifications();
    //            System.Threading.Thread.Sleep(delay * 1000);
    //        }
    //    }
    //    private static void Dummywork()
    //    {
    //        var notifications = new List<NotificationDto>();
    //        notifications.Add(
    //            new NotificationDto
    //            {
    //                NotificationType = NotificationType.LeadAssigned,
    //                Message = "NotificationType_LeadAssigned",
    //                NotificationStatusType = NotificationStatusType.New,
    //                RefId=67,
    //                MenuName="Sub Menu",
    //                MenuId=23,
    //                Url = "/Auth/Submodule/Index"
    //            }); notifications.Add(
    //             new NotificationDto
    //             {
    //                 NotificationType = NotificationType.LeadAssigned,
    //                 Message = "NotificationType_LeadAssigned",
    //                 NotificationStatusType = NotificationStatusType.New,
    //                 RefId=98,
    //                 MenuName = "Menu",
    //                 MenuId = 23,
    //                 Url = "/Auth/Menu/Index"
    //             });
    //        notifications.Add(
    //            new NotificationDto
    //            {
    //                NotificationType = NotificationType.ApplicationWaitingForApprovalByTL,
    //                Message = "NotificationType_ApplicationWaitingForApprovalByTL",
    //                NotificationStatusType = NotificationStatusType.New,
    //                RefId=90,
    //                MenuName = "Role Menu",
    //                MenuId = 23,
    //                Url = "/Auth/Role/Index"
    //            });
    //        new NotificationFacade().SaveNotifications(notifications);
    //    }
    //}
}
