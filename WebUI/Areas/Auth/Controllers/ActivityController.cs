using DEL.Auth.DTO;
using DEL.Auth.Facade;
using System.Collections.Generic;
using WebUI.Controllers;
using System.Web.Mvc;

namespace WebUI.Areas.Auth.Controllers
{
    public class ActivityController : BaseController
    {
        private readonly MonthlyMonitoringInfoFacade _monthlyMonitoring;
        private readonly EnumFacade _enumFacade = new EnumFacade();
        private string _period = string.Empty;

        public ActivityController(MonthlyMonitoringInfoFacade monthlyMonitoring)
        {
            this._monthlyMonitoring = monthlyMonitoring;
        }        
        public ActionResult ActivitiesEntry()
        {
            
            return View();
        }
        public ActionResult ActivitiesDetails()
        {

            return View();
        }
        [HttpPost]
        public JsonResult SaveWorkActivityforInstallation(WorkActivityDto dto)
        {
            var result = _monthlyMonitoring.SaveWorkActivityforInstallation(dto, SessionHelper.UserProfile.UserId);
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

        public JsonResult LoadWorkActivityByInstallationIdPeriod(long officeAssetId, string period)
        {
            var ModuleList = _monthlyMonitoring.GetWorkActivityRecordsByInstallationPeriod(officeAssetId, period);
            return Json(ModuleList, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult LoadWorkActivityByPeriod(string period)
        //{
        //    var ModuleList = _monthlyMonitoring.GetWorkActivityRecordsByPeriod(period);
        //    return Json(ModuleList, JsonRequestBehavior.AllowGet);
        //}
    }
}