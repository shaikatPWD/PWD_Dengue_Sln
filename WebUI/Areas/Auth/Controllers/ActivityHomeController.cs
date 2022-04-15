using DEL.Auth.DTO;
using DEL.Auth.Facade;
using DEL.Auth.Infrastructure;
using System.Collections.Generic;
using WebUI.Controllers;
using System.Web.Mvc;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;


namespace WebUI.Areas.Auth.Controllers
{
    public class ActivityHomeController : BaseController
    {
        private readonly MonthlyMonitoringInfoFacade _monthlyMonitoring;
        private readonly EnumFacade _enumFacade = new EnumFacade();
        private string _period = string.Empty;

        public ActivityHomeController(MonthlyMonitoringInfoFacade monthlyMonitoring)
        {
            this._monthlyMonitoring = monthlyMonitoring;
        }
        public ActionResult PerodList()
        {
            return View();
        }
        public ActionResult ActivitiesDetails()
        {

            return View();
        }
        public ActionResult ActivitiesDetailsEntry()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveWorkActivityDetails(List<OfficeAssetsDto> dto)
        {
            var result = _monthlyMonitoring.SaveWorkActivityDetails(dto, SessionHelper.UserProfile.UserId);
            return Json(result, JsonRequestBehavior.DenyGet);
        }
        //[HttpGet]
        //public JsonResult LoadWorkRecord(long id)
        //{
        //    var result = _workRecord.LoadWorkRecord(id);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult GetIsComplete()
        {
            var allJobType = _enumFacade.GetIscomplete();
            return Json(allJobType, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOfficeAssetNamebyId(long officeassetId)
        {
            var ModuleList = _monthlyMonitoring.GetOfficeAssetNamebyId(officeassetId);
            return Json(ModuleList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadPeriods()
        {
            var ModuleList = _monthlyMonitoring.LoadPeriods(SessionHelper.UserProfile.SelectedOfficeId);
            return Json(ModuleList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetWorkActivityRecordsByPeriod()
        {
            var ModuleList = _monthlyMonitoring.GetWorkActivityRecordsByPeriod();
            return Json(ModuleList, JsonRequestBehavior.AllowGet);
        }
    }
}