using DEL.Auth.DTO;
using DEL.Auth.Facade;
using WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Areas.Auth.Controllers
{
    public class ApplicationController : BaseController
    {
        readonly ApplicationFacade _application = new ApplicationFacade();
        //
        // GET: /Auth/Application/
        public ActionResult Index()
        {
            ViewBag.CompanyId = SessionHelper.UserProfile.SelectedOfficeId;
            //ViewBag.BranchId = SessionHelper.UserProfile.SelectedOwnOfficeBranchId;
            return View();
        }

        [HttpGet]
        public JsonResult GetAllApplications()
        {
            var response = _application.GetAllActiveApplication();
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveApplication(ApplicationDto application)
        {
            var response = _application.SaveApplication(application, SessionHelper.UserProfile.UserId);
            return Json(response);
        }
        [HttpPost]
        public JsonResult DeleteApplication(ApplicationDto application)
        {
            throw new NotImplementedException();
            //return Json("");
        }
	}
}
