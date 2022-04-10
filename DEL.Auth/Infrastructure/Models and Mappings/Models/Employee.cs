using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.Infrastructure
{
    [Table("Employee")]
    public class Employee : Entity
    {
        public long OfficeId { get; set; }        
        //public long OwnOfficeBranchId { get; set; }
        //[ForeignKey("OwnOfficeBranchId")]
        //public virtual OwnOfficeBranch OwnOfficeBranch { get; set; }
        //public string EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public long DesignationId { get; set; }
        [ForeignKey("DesignationId")]
        public virtual Designation Designation { get; set; }
        public string PerPhone { get; set; }
        public string OfficePhone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Remarks { get; set; }
        [ForeignKey("OfficeId")]
        public virtual HrOffice Office { get; set; }
    }
}
