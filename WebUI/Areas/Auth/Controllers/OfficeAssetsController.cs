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
    public class OfficeAssetsController : BaseController
    {
        private readonly OfficeAssetsFacade _officeAssets;

        public OfficeAssetsController(OfficeAssetsFacade officeAssetsFacade)
        {
            this._officeAssets = officeAssetsFacade;
        }

        // GET: /Module/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetOfficeAssets()
        {
            var ModuleList = _officeAssets.GetOfficeAssets(SessionHelper.UserProfile.SelectedOfficeId);
            return Json(ModuleList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveOfficeAssets(OfficeAssetsDto officeAssetsDto)
        {
            try
            {
                _officeAssets.SaveOfficeAssets(officeAssetsDto, SessionHelper.UserProfile.SelectedOfficeId);
                return Json(new { Result = "OK", Record = "", Message = "Resource/Equipment saved successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteOfficeAssets(int id)
        {
            try
            {
                _officeAssets.DeleteOfficeAssets(id);
                return Json(new { Result = "OK", Message = "Resource/Equipment deleted successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public string GetAllCompanyListForGrid()
        {
            var CompanyList = _officeAssets.GetAllCompanyProfiles().Select(c => new { c.BnName, c.Id }).ToList();

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