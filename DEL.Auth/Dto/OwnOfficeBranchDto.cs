using DEL.Auth.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.DTO
{
    public class OwnOfficeBranchDto
    {
        public long Id { get; set; }
        public long? OwnOfficeId { get; set; }
        public string OwnOfficeName { get; set; }
        public BranchType? BranchType { get; set; }
        public string BranchTypeName { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string RoutingNo { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string ContactPerson { get; set; }
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
