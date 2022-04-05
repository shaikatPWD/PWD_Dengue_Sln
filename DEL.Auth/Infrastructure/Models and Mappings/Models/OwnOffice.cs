using System.ComponentModel.DataAnnotations.Schema;

namespace DEL.Auth.Infrastructure
{
    [Table("OwnOffice")]
    public class OwnOffice : Entity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string RoutingNo { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string ContactPerson { get; set; }
        public string Logo { get; set; }
    }

    [Table("OwnOfficeBranch")]
    public class OwnOfficeBranch : Entity
    {
        public long? OwnOfficeId { get; set; }
        [ForeignKey("OwnOfficeId")]
        public virtual OwnOffice OwnOffice { get; set; }
        public BranchType BranchType { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string RoutingNo { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string ContactPerson { get; set; }
    }
}
