using DEL.Auth.Facade;
using WebUI.Controllers;
using System.Web.Mvc;

namespace WebUI.Areas.Auth.Controllers
{
    public class DpmsHomeController : BaseController
    {
        private readonly UserFacade _user = new UserFacade();
        //readonly BasicDataFacade basicDataFacade = new BasicDataFacade();
        readonly MenuFacade menuFacade = new MenuFacade();
        public ActionResult Index()
        {
            ViewBag.Title = "DPMS";
            return View();
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
        
    }
}
