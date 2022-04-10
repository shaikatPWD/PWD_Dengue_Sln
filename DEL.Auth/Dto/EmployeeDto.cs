using DEL.Auth.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.DTO
{
    public class EmployeeDto
    {
        public long? Id { get; set; }
        public long OfficeId { get; set; }
        public string OfficeName { get; set; }
        //public long OwnOfficeBranchId { get; set; }
        //public string OwnOfficeBranchName { get; set; }
        //public string EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public long DesignationId { get; set; }
        public string DesignationName { get; set; }
        public string PerPhone { get; set; }
        public string OfficePhone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Remarks { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? EditDate { get; set; }
        public long? EditedBy { get; set; }
        public EntityStatus? Status { get; set; }
    }
}
