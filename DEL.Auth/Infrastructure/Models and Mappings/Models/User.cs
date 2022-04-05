using DEL.Auth.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.Infrastructure
{

    [Table("User")]
    public class User : Entity
    {
        public User()
        {
            Roles = new List<Role>();
            Menus = new List<Menu>();
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        //public string IMEI { get; set; }
        public long? EmployeeId { get; set; }
        public bool IsActive { get; set; }
        //[ForeignKey("EmployeeId")]
        //public virtual Employee Employee { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<UserOfficeApplication> UserOfficeApplication { get; set; }
        //public User()
        //{
        //    Roles = new List<Role>();
        //    Menus = new List<Menu>();
        //}
        //public string UserName { get; set; }
        //public string Password { get; set; }
        ////public string IMEI { get; set; }
        //public long? EmployeeId { get; set; }
        //[ForeignKey("EmployeeId")]
        //public virtual Employee Employee { get; set; }
        //public string EmpPcIp { get; set; }
        //public string EmpPcMac { get; set; }
        //public bool IsActive { get; set; }
        //public bool? IsSelfAuthorizer { get; set; }
        //public bool? PasswordExpired { get; set; }
        //public DateTime? PasswordExpireDate { get; set; }
        //public bool? PasswordLocked { get; set; }
        //public virtual ICollection<Role> Roles { get; set; }
        //public virtual ICollection<Menu> Menus { get; set; }
        //public virtual ICollection<UserOfficeApplication> UserCompanyApplications { get; set; }
    }
}
