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
    public class SubModuleController : BaseController
    {
        //
        // GET: /Submodule/
        private readonly MenuFacade _menu;

        public SubModuleController(MenuFacade menuFacade)
        {

            this._menu = menuFacade;
        }

        [HttpGet]
        public JsonResult GetSubModule(int moduleId=0)
        {
            var submodulelist = _menu.GetSubModules().ToList();
            return Json(submodulelist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SavaSubModule(SubModuleDto submoduledto)
        {
            try
            {
                _menu.SaveSubModule(submoduledto);
                return Json(new { Result = "OK", Message = "Submodule saved succesfully" },JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteSubModule(int id)
        {
            try
            {
                _menu.DeleteSubModule(id);
                return Json(new { Result = "OK", Message = "Submodule deleted successfully" });
            }
            catch (Exception ex)
            {

                return Json(new { Result = "ERROR", Message = ex.Message });
            }
            
        }

        public string GetCorrespondingModules()
        {
            List<ModuleDto> modules = _menu.GetModules(null);

            string strret = "<select>";
            foreach (ModuleDto item in modules)
            {
                strret += "<option value='" + item.Id + "'>" + item.Name + "</option>";
            }
            strret += "</select>";
            return strret;
        }
	}
}