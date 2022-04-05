using DEL.Auth.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.DTO
{
    public class RoleDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TaskDto> TaskList { get; set; }
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
