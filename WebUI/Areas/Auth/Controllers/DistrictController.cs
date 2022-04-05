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
    public class DistrictController : BaseController
    {
        private readonly DistrictFacade _district;
        public DistrictController(DistrictFacade districtFacade)
        {
            this._district = districtFacade;
        }

        // GET: /Module/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetDistricts()
        {
            var ModuleList = _district.GetDistricts();
            return Json(ModuleList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveDistrict(DistrictDto districtDto)
        {
            try
            {
                _district.SaveDistrict(districtDto);
                return Json(new { Result = "OK", Record = "", Message = "District saved successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteDistrict(int id)
        {
            try
            {
                _district.DeleteDistrict(id);
                return Json(new { Result = "OK", Message = "District deleted successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
	}
}