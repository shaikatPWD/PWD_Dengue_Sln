using DEL.Auth.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.DTO
{    
    public class AreaDto 
    { 
        public long? Id { get; set; }
        public string Name { get; set; }
        public string BnName { get; set; }        
        public int OrderID { get; set; }
        public long HrOfficeId { get; set; } = 0;
        public string OfficeName { get; set; }
        public IsShow IsShow { get; set; } = IsShow.Yes;
        public string IsShowName { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? EditDate { get; set; }
        public long? EditedBy { get; set; }
        public EntityStatus? Status { get; set; }
    }
}
