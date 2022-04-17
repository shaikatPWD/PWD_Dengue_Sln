using DEL.Auth.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.DTO
{
    public class UserDto
    {
        public long? Id { get; set; }
        public long? UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public long? EmployeeId { get; set; }
        public string EmpolyeeName { get; set; }
        public string FullName { get; set; }
        public string EmpPcIp { get; set; }
        public string EmpPcMac { get; set; }
        public bool IsActive { get; set; }
        public bool IsSelfAuthorizer { get; set; }
        public bool PasswordExpired { get; set; }
        public DateTime PasswordExpireDate { get; set; }
        public bool PasswordLocked { get; set; }
        public long? OwnOfficeId { get; set; }
        public long? OwnOfficeBranchId { get; set; }
        public IEnumerable<UserPermissionDto> UserPermissions { get; set; }
        public List<UserOfficeApplicationDto> UserCompanyApplications { get; set; }
        public IEnumerable<ModuleDto> Modules { get; set; }
        public IEnumerable<SubModuleDto> SubModules { get; set; }
        public string IMEI { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? EditDate { get; set; }
        public long? EditedBy { get; set; }
        public EntityStatus? Status { get; set; }
    }
}
