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
    public class CompanyProfileController : BaseController
    {
        private readonly CompanyProfileFacade _CompanyProfileFacade;

        public CompanyProfileController(CompanyProfileFacade CompanyProfileFacade)
        {
            this._CompanyProfileFacade = CompanyProfileFacade;
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
        public JsonResult SaveCompanyProfile(OfficeProfileDto Company)
        {
            var response = _CompanyProfileFacade.SaveCompanyProfile(Company, SessionHelper.UserProfile.UserId);
            return Json(response);
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
        [HttpGet]
        public JsonResult UpdateClosingDate(long? CompanyProfileId)
        {

            //var _accounts = new AccountsFacade();
            var closingDate = _CompanyProfileFacade.DateToday(CompanyProfileId);
            //var productionCostPerKg = _production.CalculateCostPerKG(closingDate);
            //_accounts.ArchieveVouchers(closingDate);

            //var _reportAccount = new ReportAccountFacade();
            //_reportAccount.GetInventoryAmount(DateTime.Now.AddYears(-1), closingDate);

            var cp = _CompanyProfileFacade.UpdateClosingDate();
            return Json(cp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DateToday(long? CompanyProfileId)
        {
            var cp = _CompanyProfileFacade.DateToday(CompanyProfileId);
            return Json(cp, JsonRequestBehavior.AllowGet);
        }
        /*sabiha*/
        //public JsonResult GetUpdateBillNo()
        //{
        //    var cp = _CompanyProfileFacade.GetUpdateBillNo();
        //    return Json(cp, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult GetUpdateVoucherNo()
        //{
        //    var cp = _CompanyProfileFacade.GetUpdateVoucherNo();
        //    return Json(cp, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult GetUpdateSpecificVoucherNo(string type)
        //{
        //    string voucherNo = "";// = _CompanyProfileFacade.GetUpdateCDvNo();
        //    switch (type)
        //    {
        //        case "CDV":
        //            voucherNo = _CompanyProfileFacade.GetUpdateCDvNo();
        //            break;
        //        case "BDV":
        //            voucherNo = _CompanyProfileFacade.GetUpdateBDvNo();
        //            break;
        //        case "CCV":
        //            voucherNo = _CompanyProfileFacade.GetUpdateCCvNo();
        //            break;
        //        case "BCV":
        //            voucherNo = _CompanyProfileFacade.GetUpdateBCvNo();
        //            break;
        //        case "JV":
        //            voucherNo = _CompanyProfileFacade.GetUpdateJvNo();
        //            break;
        //        default:
        //            break;
        //        //return Json(voucherNo, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(voucherNo, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetInvoiceNo()
        //{
        //    var cp = _CompanyProfileFacade.GetUpdateInvoiceNo();
        //    return Json(cp, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetChalanNo()
        {
            var cp = _CompanyProfileFacade.GetUpdatedChalanNo();
            return Json(cp, JsonRequestBehavior.AllowGet);
        }
        /*Sabiha Modified 18.10.2016*/
        public string GetAllCompanyListForGrid()
        {
            var CompanyList = _CompanyProfileFacade.GetAllCompanyProfiles().Select(c => new { c.Name, c.Id }).ToList();

            string strret = "<select>";
            foreach (var item in CompanyList)
            {
                strret += "<option value='" + item.Id + "'>" + item.Name + "</option>";
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
