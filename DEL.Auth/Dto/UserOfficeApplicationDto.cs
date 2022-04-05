using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DEL.Auth.Infrastructure;

namespace DEL.Auth.DTO
{
    public class UserCompanyApplicationDto
    {

        public long? UserId { get; set; }
        public string UserName { get; set; }
        public long? OfficeId { get; set; }
        public string OfficeName { get; set; }
        public List<ApplicationDto> Applications { get; set; }

        //public long? UserId { get; set; }
        //public string UserName { get; set; }
        //public long? OwnOfficeId { get; set; }
        //public string OwnOfficeName { get; set; }
        //public long? OwnOfficeBranchId { get; set; }
        //public string OwnOfficeBranchName { get; set; }
        //public List<ApplicationDto> Applications { get; set; }
    }

    public class UserOfficeApplicationDto
    {
        public long? Id { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public long OfficeId { get; set; }
        public string OfficeName { get; set; }
        public long ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? EditDate { get; set; }
        public long? EditedBy { get; set; }
        public EntityStatus Status { get; set; }
        //public long? Id { get; set; }
        //public long UserId { get; set; }
        //public string UserName { get; set; }
        //public long OwnOfficeId { get; set; }
        //public string OwnOfficeIdName { get; set; }
        //public long OwnOfficeBranchId { get; set; }
        //public string OwnOfficeIdBranchOfficeName { get; set; }
        //public long ApplicationId { get; set; }
        //public string ApplicationName { get; set; }
        //public DateTime? CreateDate { get; set; }
        //public long? CreatedBy { get; set; }
        ////public string CreatorIP { get; set; }
        ////public string CreatorMac { get; set; }
        ////public DateTime? AuthorizeDate { get; set; }
        ////public long? AuthorizedBy { get; set; }
        ////public string AuthorizerIP { get; set; }
        ////public string AuthorizerMac { get; set; }
        //public DateTime? EditDate { get; set; }
        //public long? EditedBy { get; set; }
        ////public string EditorIP { get; set; }
        ////public string EditorMac { get; set; }
        //public EntityStatus? Status { get; set; }
    }
}