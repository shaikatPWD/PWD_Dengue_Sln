using DEL.Auth.Infrastructure;
using System;

namespace DEL.Auth.DTO
{
    public class DesignationDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string Remarks { get; set; }
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
}
