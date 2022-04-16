using DEL.Auth.Facade;
using WebUI.Controllers;
using System.Web.Mvc;
using DEL.Auth.DTO;

namespace WebUI.Areas.Auth.Controllers
{
    public class DpmsHomeController : BaseController
    {
        private readonly UserFacade _user = new UserFacade();
        private readonly InformationFacade _info = new InformationFacade();
        //readonly BasicDataFacade basicDataFacade = new BasicDataFacade();
        readonly MenuFacade menuFacade = new MenuFacade();
        public DpmsHomeController(InformationFacade info)
        {
            this._info = info;
        }
        public ActionResult Index()
        {
            ViewBag.Title = "DPMS";
            return View();
        }

        [HttpPost]
        public JsonResult SaveUpdateActions(InformationDto dto)
        {
            var result = _info.SaveFromClient(dto);
            return Json(result, JsonRequestBehavior.DenyGet);
        }
        public JsonResult GetAllAreas()
        {
            var areas = _info.GetAllAreas();

            return Json(areas, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllDistricts()
        {
            var districts = _info.GetAllDistricts();

            return Json(districts, JsonRequestBehavior.AllowGet);
        }
    }
}
