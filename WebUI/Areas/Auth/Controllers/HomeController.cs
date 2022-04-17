using DEL.Auth.Facade;
using DEL.Auth.DTO;
//using DEL.IPDC.Facade;
using DEL.Auth.Infrastructure;
using WebUI.Areas.Auth.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DEL.Auth.Service;
//using DEL.IPDC.Util;

namespace WebUI.Areas.Auth.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserFacade _user = new UserFacade();
        //readonly BasicDataFacade basicDataFacade = new BasicDataFacade();
        readonly MenuFacade menuFacade = new MenuFacade();
        public ActionResult Index()
        {
            ViewBag.Title = "DEL- Solutions";
            return View();
        }
        public ActionResult Menu(int smId)
        {
            ViewBag.smId = smId;
            return View();
        }
        //public ActionResult Menu()//(int smId)
        //{
        //    //ViewBag.smId = smId;
        //    return PartialView("Menu");
        //}
        //public ActionResult Home()
        //{
        //    return View("Home");
        //}
        public ActionResult Welcome()
        {
            return PartialView("Welcome");
        }
        public ActionResult AuthError()
        {
            ViewBag.Message = "An authorization error occured.";
            return View();
        }

        public ActionResult Error()
        {
            ViewBag.Message = "An unexpected error occured.";
            return View();
        }


        public JsonResult GetMenus(int smId = 0, string _search = "false", string nd = "1462793528262", int rows = 10000, int page = 1, int sidx = 1, string sord = "asc")
        {
            var menus = SessionHelper.UserProfile.Menus.Where(m => m.SubModuleId == smId);//menuFacade.GetMenus(smId);
            var data = menus.OrderBy(m => m.Sl).Select(m => new List<string> { m.Id.ToString(), m.DisplayName, m.Url }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetModuleSubModules()
        {
            //var _employee = new EmployeeFacade();
            //var moduleSubModuleList = new MenuWebModel().GetModuleAndSubModules();
            //var moduleSubModuleList = new MenuWebModel().GetModuleAndSubModulesAndMenu();
            var moduleSubModuleList = new MenuWebModel().GetOnlyMenus();
            var UserName = SessionHelper.UserProfile.FullName;
            var UserId = SessionHelper.UserProfile.UserId;
            //var employeeId = _user.GetEmployeeIdByUserId(UserId);
            //var roleIdList = new List<long>();//_employee.GetEmpRoleIdList(empId);
            //if (SessionHelper.UserProfile.Roles != null && SessionHelper.UserProfile.Roles.Count > 0)
            //  roleIdList = SessionHelper.UserProfile.Roles.Select(r => r.Id).ToList();
            //var employee = _employee.GetEmployeeByEmployeeId(employeeId);
            //var Profilepicture = Path.GetFileName(employee.Photo);
            var data = new
            {
                UserName,
                moduleSubModuleList,
                //Profilepicture
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[HttpGet]
        //public JsonResult GetAllMenus()//long? RoleId
        //{
        //  var _menu = new MenuFacade();
        //  var data = _menu.GetAllMenus();
        //  return Json(data, JsonRequestBehavior.AllowGet);
        //}

        //[HttpGet]
        //public JsonResult GetDynamicTree()
        //{
        //  var flatObjects = new List<DynamicMenuDto>();
        //  var result1 = menuFacade.GetMenuAll();
        //  try
        //  {
        //    foreach (var item in result1)
        //    {
        //      flatObjects.Add(new DynamicMenuDto(
        //          item.Id > 0 ? item.MenuName : "", item.Id,
        //          item.ParentId != null ? item.ParentId : 0,
        //          item.NavigateUrl != null ? item.NavigateUrl : ""));
        //    }
        //  }
        //  catch (Exception ex)
        //  {
        //  }
        //  var recursiveObjects = menuFacade.FillDynamicRecursiveMenu(flatObjects, (long)flatObjects[0].ParentId);
        //  return new JsonResult { Data = recursiveObjects, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}
    }
}
