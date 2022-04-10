using DEL.Auth.DTO;
using DEL.Auth.Facade;
using DEL.Auth.Util;
using WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Areas.Auth.Controllers
{
    public class ResourcesController : BaseController
    {
        private readonly ResourcesFacade _resources;

        public ResourcesController(ResourcesFacade resourcesFacade)
        {
            this._resources = resourcesFacade;
        }

        // GET: /Module/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetResources()
        {
            var ModuleList = _resources.GetResources(SessionHelper.UserProfile.SelectedOfficeId);
            return Json(ModuleList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveResources(ResourcesDto resourcesDto)
        {
            try
            {
                _resources.SaveResources(resourcesDto, SessionHelper.UserProfile.SelectedOfficeId);
                return Json(new { Result = "OK", Record = "", Message = "Resource/Equipment saved successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteResources(int id)
        {
            try
            {
                _resources.DeleteResources(id);
                return Json(new { Result = "OK", Message = "Resource/Equipment deleted successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public string GetAllCompanyListForGrid()
        {
            var CompanyList = _resources.GetAllCompanyProfiles().Select(c => new { c.BnName, c.Id }).ToList();

            string strret = "<select>";
            foreach (var item in CompanyList)
            {
                strret += "<option value='" + item.Id + "'>" + item.BnName + "</option>";
            }
            strret += "</select>";
            return strret;
        }
    }
}