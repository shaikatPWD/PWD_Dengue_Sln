using DEL.Auth.DTO;
using DEL.Auth.Facade;
using WebUI.Controllers;
using WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
//using DEL.IPDC.DTO;
using DEL.Auth.Util;

namespace WebUI.Areas.Auth.Controllers
{
    public class LoginController : BaseController
    {
        private readonly LoginFacade _loginFacade;
        //private readonly object HttpResponseBuilder;

        public LoginController(LoginFacade loginFacade)
        {
            this._loginFacade = loginFacade;
        }
        //
        // GET: /Login/
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult GetKey()
        {
            var resObj = new { Key = KeyVault.GetKey() };
            return Json(resObj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Login(LogOnDto model)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                LogOnDto logOnDto = new LogOnDto();
                logOnDto.UserName = model.UserName;
                logOnDto.Password = KeyVault.DecryptPassword(model.Key, model.PasswordHex);
                logOnDto.RememberMe = model.RememberMe;
                var login = _loginFacade.DoLogin(logOnDto);
                if (login.UserId > 0)
                {
                    SessionHelper.UserProfile = login;

                    //var _employee = new Finix.IPDC.Facade.EmployeeFacade();
                    UserFacade _user = new UserFacade();
                    var empId = _user.GetEmployeeIdByUserId(login.UserId);
                    var roleIdList = new List<long>();
                    if (login.Roles != null && login.Roles.Count > 0)
                        roleIdList = login.Roles.Select(r => r.Id).ToList();//_employee.GetEmpRoleIdList(empId);
                    var empAuthData = _loginFacade.RoleAssignmentByRoleIdList(roleIdList, SessionHelper.UserProfile.IsAdmin);
                    if (empAuthData != null)
                    {
                        SessionHelper.UserProfile.Roles = empAuthData.Roles;
                        SessionHelper.UserProfile.Tasks = empAuthData.Tasks;
                    }


                    return RedirectToAction("ChooseApplication");
                    //return Redirect(rUrl);

                }
                ViewBag.ErrMsg = "Incorrect User Name or Password";
                return View();
            }
            catch (Exception ex)
            {
                var logOnDto = new LogOnDto();
                logOnDto.ErrMessage = ex.Message;
                ViewBag.ErrMsg = ex.Message;
                return View(logOnDto);
            }

        }

        [HttpGet]
        public ActionResult ChooseApplication()
        {
            var temp = new { AccessPermissions = SessionHelper.UserProfile.UserCompanyApplications };
            ViewBag.UserInfo = temp;
            if (SessionHelper.UserProfile.UserCompanyApplications.Count == 1
                && SessionHelper.UserProfile.UserCompanyApplications.FirstOrDefault() != null
                && SessionHelper.UserProfile.UserCompanyApplications.FirstOrDefault().Applications.Count == 1)
            {
                var CompanyId = SessionHelper.UserProfile.UserCompanyApplications.FirstOrDefault().OfficeId;
                //var BranchId = SessionHelper.UserProfile.UserCompanyApplications.FirstOrDefault().OwnOfficeBranchId;
                var ApplicationId = SessionHelper.UserProfile.UserCompanyApplications.FirstOrDefault().Applications.FirstOrDefault().Id;
                var previousSession = SessionHelper.UserProfile;

                //var _employee = new Finix.IPDC.Facade.EmployeeFacade();
                UserFacade _user = new UserFacade();
                var empId = _user.GetEmployeeIdByUserId(SessionHelper.UserProfile.UserId);
                var roleIdList = new List<long>();//_employee.GetEmpRoleIdList(empId);
                if (SessionHelper.UserProfile.Roles != null && SessionHelper.UserProfile.Roles.Count > 0)
                    roleIdList = SessionHelper.UserProfile.Roles.Select(r => r.Id).ToList();
                //var empAuthData = _loginFacade.RoleAssignmentByRoleIdList(roleIdList);

                SessionHelper.UserProfile = _loginFacade.ChooseApplication(SessionHelper.UserProfile.UserId, (long)CompanyId, (long)ApplicationId, roleIdList);
                //SessionHelper.UserProfile.IsAdmin = true;
                //SessionHelper.UserProfile.SelectedOwnOfficeId = CompanyId;
                SessionHelper.UserProfile.SelectedOfficeId = CompanyId;
                SessionHelper.UserProfile.SelectedApplicationId = ApplicationId;
                SessionHelper.UserProfile.UserCompanyApplications = previousSession.UserCompanyApplications;
                SessionHelper.UserProfile.Tasks = previousSession.Tasks;
                SessionHelper.UserProfile.Roles = previousSession.Roles;
                return RedirectToAction("Index", "Home", new { area = "Auth" });
                //return RedirectToActionPermanent("ChooseApplication", "Login",
                //    new
                //    {
                //        CompanyId = SessionHelper.UserProfile.UserCompanyApplications.FirstOrDefault().CompanyProfileId,
                //        ApplicationId = SessionHelper.UserProfile.UserCompanyApplications.FirstOrDefault().Applications.FirstOrDefault().Id
                //    });
            }
            return View();
        }
        [HttpPost]
        public ActionResult ChooseApplication(long CompanyId, long ApplicationId)
        {
            var previousSession = SessionHelper.UserProfile;

            //var _employee = new Finix.IPDC.Facade.EmployeeFacade();
            UserFacade _user = new UserFacade();
            var empId = _user.GetEmployeeIdByUserId(SessionHelper.UserProfile.UserId);
            var roleIdList = new List<long>();//_employee.GetEmpRoleIdList(empId);
            if (SessionHelper.UserProfile.Roles != null && SessionHelper.UserProfile.Roles.Count > 0)
                roleIdList = SessionHelper.UserProfile.Roles.Select(r => r.Id).ToList();

            SessionHelper.UserProfile = _loginFacade.ChooseApplication(SessionHelper.UserProfile.UserId, CompanyId, ApplicationId, roleIdList);
            //SessionHelper.UserProfile.IsAdmin = true;
            SessionHelper.UserProfile.SelectedOfficeId = CompanyId;
            //SessionHelper.UserProfile.SelectedOwnOfficeBranchId = CompanyId;
            SessionHelper.UserProfile.SelectedApplicationId = ApplicationId;
            SessionHelper.UserProfile.UserCompanyApplications = previousSession.UserCompanyApplications;
            SessionHelper.UserProfile.Tasks = previousSession.Tasks;
            SessionHelper.UserProfile.Roles = previousSession.Roles;
            return RedirectToAction("Index", "Home", new { area = "Auth" });
            //return View();
        }
        public JsonResult GetMenuData()
        {
            var up = SessionHelper.UserProfile;
            if (up != null)
            {

                var data = up.Modules.OrderBy(x => x.Sl)
                     .Select(x => new
                     {
                         Id = x.Id,
                         Name = x.Name,
                         SubModules = up.SubModules
                         .Where(y => y.ModuleId == x.Id).OrderBy(y => y.Sl)
                         .Select(y => new
                         {
                             Id = y.Id,
                             Name = y.Name,
                             ColSpan = y.ColSpan,
                             Menus = up.Menus
                             .Where(z => z.SubModuleId == y.Id).OrderBy(z => z.Sl)
                             .Select(z => new
                             {
                                 Id = z.Id,
                                 Name = z.Name,
                                 Url = z.Url
                             }).ToList()
                         }).ToList(),

                     }).ToList();

                return Json(new { Modules = data }, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
        [HttpPost]
        public JsonResult ChangePassword(ChangePasswordDto dto)
        {
            dto.UserId = SessionHelper.UserProfile.UserId;
            var result = _loginFacade.ChangePassword(dto);
            return Json(result, JsonRequestBehavior.DenyGet);
        }
        //[HttpPost]
        //public JsonResult UploadPicture(PersonDto dto)
        //{
        //    var userId = SessionHelper.UserProfile.UserId;
        //    var _employee = new DEL.IPDC.Facade.EmployeeFacade();
        //    var result = _employee.UploadPicture(dto, userId);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
    }
}
