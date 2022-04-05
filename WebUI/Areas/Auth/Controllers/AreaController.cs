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
    public class AreaController : BaseController
    {
        private readonly AreaFacade _area;
        
        public AreaController(AreaFacade areaFacade)
        {
            this._area = areaFacade;
        }

        // GET: /Module/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAreas()
        {
            var ModuleList = _area.GetAreas();
            return Json(ModuleList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveArea(AreaDto areaDto)
        {
            try
            {
                _area.SaveArea(areaDto);
                return Json(new { Result = "OK", Record = "", Message = "District saved successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteArea(int id)
        {
            try
            {
                _area.DeleteArea(id);
                return Json(new { Result = "OK", Message = "District deleted successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public string GetAllCompanyListForGrid()
        {
            var CompanyList = _area.GetAllCompanyProfiles().Select(c => new { c.BnName, c.Id }).ToList();

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