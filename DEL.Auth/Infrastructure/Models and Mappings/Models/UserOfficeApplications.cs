using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DEL.Auth.Infrastructure
{
    [Table("UserOfficeApplication")]
    public class UserOfficeApplication : Entity
    {
        //public long UserId { get; set; }
        //[ForeignKey("UserId")]
        //public virtual User User { get; set; }
        //public long OwnOfficeId { get; set; }
        //[ForeignKey("OwnOfficeId")]
        //public virtual OwnOffice OwnOffice { get; set; }
        //public long OwnOfficeBranchId { get; set; }
        //[ForeignKey("OwnOfficeBranchId")]
        //public virtual OwnOfficeBranch OwnOfficeBranch { get; set; }
        //public long ApplicationId { get; set; }
        //[ForeignKey("ApplicationId")]
        //public virtual Application Application { get; set; }
        public long UserId { get; set; }
        public long OfficeId { get; set; }
        public long ApplicationId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("OfficeId")]
        public virtual HrOffice Office { get; set; }
        [ForeignKey("ApplicationId")]
        public virtual Application Application { get; set; }
    }
}