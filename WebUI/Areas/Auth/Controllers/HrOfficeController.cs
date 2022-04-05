//using DEL.Accounts.Facade;
using DEL.Auth.DTO;
using DEL.Auth.Facade;
using WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DEL.Auth.Infrastructure;
using DEL.Auth.Util;

namespace WebUI.Areas.Auth.Controllers
{
    public class HrOfficeController : BaseController
    {
        private readonly HrOfficeFacade _CompanyProfileFacade;

        public HrOfficeController(HrOfficeFacade hrOfficeFacade)
        {
            this._CompanyProfileFacade = hrOfficeFacade;
        }
        [HttpGet]
        public JsonResult GetAllActiveCompanies()
        {
            var CompanyList = _CompanyProfileFacade.GetAllActiveCompanyProfiles();
            return Json(CompanyList, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAllCompanyList()
        {
            var CompanyList = _CompanyProfileFacade.GetAllCompanyProfiles().Select(c => new { c.Name, c.Id }).ToList();
            return Json(CompanyList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveCompanyProfile(HrOfficeDto Company)
        {
            var response = _CompanyProfileFacade.SaveCompanyProfile(Company, SessionHelper.UserProfile.UserId);
            return Json(response);
        }
        [HttpPost]
        public JsonResult DeleteHrOffice(int id)
        {
            try
            {
                _CompanyProfileFacade.DeleteHrOffice(id);
                return Json(new { Result = "OK", Message = "Office deleted successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        //
        // GET: /CompanyProfile/
        public ActionResult Index()
        {
            ViewBag.OfficeId = SessionHelper.UserProfile.SelectedOfficeId;
            //ViewBag.Branch = SessionHelper.UserProfile.SelectedOwnOfficeBranchId;
            return View();
        }

        public ActionResult ClosingDate()
        {
            return View();
        }
        
        public string GetAllCompanyListForGrid()
        {
            var CompanyList = _CompanyProfileFacade.GetAllCompanyProfiles().Select(c => new { c.BnName, c.Id }).ToList();

            string strret = "<select>";
            foreach (var item in CompanyList)
            {
                strret += "<option value='" + item.Id + "'>" + item.BnName + "</option>";
            }
            strret += "</select>";
            return strret;
        }
        public string GetCompanyProfile()
        {
            List<KeyValuePair<int, string>> leaveApps = UiUtil.EnumToKeyVal<CompanyType>();
            string strret = "<select>";
            foreach (KeyValuePair<int, string> item in leaveApps)
            {
                strret += "<option value='" + item.Key + "'>" + item.Value + "</option>";
            }
            strret += "</select>";
            return strret;
        }

    }
}
