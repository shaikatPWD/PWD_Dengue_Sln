﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.Infrastructure
{
    [Table("UserProxyRoleMenuTask")]
    public class UserProxyRoleMenuTask : Entity
    {
        public long RoleMenuId { get; set; }
        public long TaskId { get; set; }
        public bool Read { get; set; }
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        [ForeignKey("TaskId")]
        public virtual Task Task { get; set; }
        [ForeignKey("RoleMenuId"), InverseProperty("RoleMenuTasks")]
        public virtual UserProxyRoleMenu UserProxyRoleMenu { get; set; }
        public virtual RoleMenu RoleMenu { get; set; }
    }
}
