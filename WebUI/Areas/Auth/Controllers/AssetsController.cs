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
    public class AssetsController : BaseController
    {
        private readonly AssetsFacade _assets;

        public AssetsController(AssetsFacade assetsFacade)
        {
            this._assets = assetsFacade;
        }

        // GET: /Module/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAssets()
        {
            var ModuleList = _assets.GetAssets(SessionHelper.UserProfile.SelectedOfficeId);
            return Json(ModuleList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveAssets(AssetsDto assetsDto)
        {
            try
            {
                _assets.SaveAssets(assetsDto);
                return Json(new { Result = "OK", Record = "", Message = "Resource/Equipment saved successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteAssets(int id)
        {
            try
            {
                _assets.DeleteAssets(id);
                return Json(new { Result = "OK", Message = "Resource/Equipment deleted successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public string GetAllCompanyListForGrid()
        {
            var CompanyList = _assets.GetAllCompanyProfiles().Select(c => new { c.BnName, c.Id }).ToList();

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