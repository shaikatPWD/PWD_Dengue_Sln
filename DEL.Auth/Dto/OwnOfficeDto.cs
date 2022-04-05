using DEL.Auth.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.DTO
{
    public class OwnOfficeDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string RoutingNo { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string ContactPerson { get; set; }
        public string Logo { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? CreatedBy { get; set; }
        //public string CreatorIP { get; set; }
        //public string CreatorMac { get; set; }
        //public DateTime? AuthorizeDate { get; set; }
        //public long? AuthorizedBy { get; set; }
        //public string AuthorizerIP { get; set; }
        //public string AuthorizerMac { get; set; }
        public DateTime? EditDate { get; set; }
        public long? EditedBy { get; set; }
        //public string EditorIP { get; set; }
        //public string EditorMac { get; set; }
        public EntityStatus? Status { get; set; }
    }

    //public class MenuReportDto
    //{
    //    public long Id { get; set; }
    //    public long? ParentId { get; set; }
    //    public string MenuCode { get; set; }
    //    public string ModuleCode { get; set; }
    //    public string MenuName { get; set; }
    //    public bool Visible { get; set; }
    //    public int MenuSequence { get; set; }
    //    public string MenuType { get; set; }
    //    public string ImagePath { get; set; }
    //    public string Target { get; set; }
    //    public string ToolTip { get; set; }
    //    public string UserLevel { get; set; }
    //    public string OperationLevel { get; set; }
    //    public string florabank_ID { get; set; }
    //    public int Depth { get; set; }
    //    public bool Expanded { get; set; }
    //    public string NavigateUrl { get; set; }
    //    public string MenuNameIslamic { get; set; }
    //    public string IslamicYN { get; set; }
    //    public string ToolTipIslamic { get; set; }
    //    public string servertype { get; set; }
    //    public virtual List<MenuReportDto> Childs { get; set; }
    //}

    //public class DynamicMenuDto
    //{
    //    public DynamicMenuDto() { }
    //    public long Id { get; set; }
    //    public long? ParentId { get; set; }
    //    public string Data { get; set; }
    //    public string Url { get; set; }

    //    public DynamicMenuDto(DynamicMenuDto objMenu)
    //    {
    //        Data = objMenu.Data;
    //        Id = objMenu.Id;
    //        ParentId = objMenu.ParentId;
    //        Url = objMenu.Url;
    //    }

    //    public DynamicMenuDto(string name, long id, long? parentId, string url)
    //    {
    //        Data = name;
    //        Id = id;
    //        ParentId = (long)parentId;
    //        Url = url;
    //    }

    //}

    //public class DynamicRecursiveMenuDto
    //{
    //    public string data { get; set; }
    //    public long id { get; set; }
    //    public DynamicTreeMenuAttribute attr { get; set; }
    //    public DynamicMenuMetaData metadata { get; set; }
    //    public List<DynamicRecursiveMenuDto> children { get; set; }
    //}

    //public class DynamicTreeMenuAttribute
    //{
    //    public long id;
    //    public bool selected;
    //}
    //public class DynamicMenuMetaData
    //{
    //    public string href;
    //}

}
