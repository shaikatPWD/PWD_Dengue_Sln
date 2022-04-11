using DEL.Auth.DTO;
using DEL.Auth.Facade;
using System.Collections.Generic;
using WebUI.Controllers;
using System.Web.Mvc;

namespace WebUI.Areas.Auth.Controllers
{
    public class MonthlyMonitoringController : BaseController
    {
        private readonly MonthlyMonitoringInfoFacade _monthlyMonitoring;
        private readonly EnumFacade _enumFacade = new EnumFacade();    
        
        public MonthlyMonitoringController(MonthlyMonitoringInfoFacade monthlyMonitoring)
        {
            this._monthlyMonitoring = monthlyMonitoring;
        }        
        public ActionResult MonthlyMonitoringInfoEntry()
        {
            return View();
        }
        //public ActionResult WorkrecordDetailsEntry()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public JsonResult SaveMonthlyMonitoringInfo(List<MonthlyMonitoringInfoDto> dto)
        //{
        //    var result = _monthlyMonitoring.SaveMonthlyMonitoringInfo(dto, SessionHelper.UserProfile.UserId, SessionHelper.UserProfile.SelectedOfficeId);
        //    return Json(result, JsonRequestBehavior.DenyGet);
        //}
        [HttpGet]        
        public JsonResult LoadMonthlyMonitorinInfoByOffice()
        {
            var result = _monthlyMonitoring.LoadMonthlyMonitorinInfoByOffice(SessionHelper.UserProfile.SelectedOfficeId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetIsComplete()
        {
            var allJobType = _enumFacade.GetIscomplete();
            return Json(allJobType, JsonRequestBehavior.AllowGet);
        }
    }
}