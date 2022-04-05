using DEL.Auth.DTO;
using DEL.Auth.Facade;
using WebUI.Controllers;
using System.Web.Mvc;

namespace WebUI.Areas.Auth.Controllers
{
    public class WorkrecordController : BaseController
    {
        private readonly WorkRecordFacade _workRecord;
        private readonly EnumFacade _enumFacade = new EnumFacade();    
        
        public WorkrecordController(WorkRecordFacade workRecord)
        {
            this._workRecord = workRecord;
        }        
        public ActionResult WorkrecordEntry()
        {
            return View();
        }
        public ActionResult WorkrecordDetailsEntry()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveWorkRecord(WorkRecordDetailsDto dto)
        {
            var result = _workRecord.SaveWorkRecord(dto, SessionHelper.UserProfile.UserId, SessionHelper.UserProfile.SelectedOfficeId);
            return Json(result, JsonRequestBehavior.DenyGet);
        }
        [HttpGet]        
        public JsonResult LoadWorkRecord(long id)
        {
            var result = _workRecord.LoadWorkRecord(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadWorkRecordByAssetId(long assetId)
        {
            var ModuleList = _workRecord.GetWorkRecords((long)SessionHelper.UserProfile.SelectedOfficeId, assetId);
            return Json(ModuleList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAssetNamebyAssetId(long assetId)
        {
            var ModuleList = _workRecord.GetAssetNamebyAssetId(assetId);
            return Json(ModuleList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetIsComplete()
        {
            var allJobType = _enumFacade.GetIscomplete();
            return Json(allJobType, JsonRequestBehavior.AllowGet);
        }
    }
}