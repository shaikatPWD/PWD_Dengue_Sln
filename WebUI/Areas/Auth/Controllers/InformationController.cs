using DEL.Auth.DTO;
using DEL.Auth.Facade;
using WebUI.Controllers;
using System.Web.Mvc;

namespace WebUI.Areas.Auth.Controllers
{
    public class InformationController : BaseController
    {
        private readonly InformationFacade _Information;
        private readonly EnumFacade _enumFacade = new EnumFacade();        
        public InformationController(InformationFacade ifoFacade)
        {
            this._Information = ifoFacade;
        }
        public ActionResult ActionEntry()
        {
            return View();
        }        
        public ActionResult Dashboard(string sortOrder, string searchString, int page = 1)
        {
            ViewBag.SearchString = searchString;
            ViewBag.CurrentSort = sortOrder;
            var model = _Information.InofrmationList(10, page, searchString, SessionHelper.UserProfile.SelectedOfficeId);
            return View(model);
        }                
        [HttpPost]
        public JsonResult SaveUpdateActions(InformationDto dto)
        {
            var result = _Information.SaveUpdateActions(dto, SessionHelper.UserProfile.UserId);
            return Json(result, JsonRequestBehavior.DenyGet);
        }
        [HttpGet]
        public JsonResult GetPendingObs()
        {
            var pendingObs = _Information.GetPendingObs(SessionHelper.UserProfile.SelectedOfficeId);
            return Json(pendingObs, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetInProgressObs()
        {
            var pendingObs = _Information.GetInProgressObs(SessionHelper.UserProfile.SelectedOfficeId);
            return Json(pendingObs, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCompletedObs()
        {
            var pendingObs = _Information.GetCompletedObs(SessionHelper.UserProfile.SelectedOfficeId);
            return Json(pendingObs, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadInformation(long id)
        {
            var result = _Information.LoadInformation(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStatus()
        {
            var allJobType = _enumFacade.GetStatus();
            return Json(allJobType, JsonRequestBehavior.AllowGet);
        }
    }
}