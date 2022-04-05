using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.Infrastructure
{
    [Table("Area")]
    public class Area : Entity
    { 
        public string Name { get; set; }
        public string BnName { get; set; }        
        public int OrderID { get; set; }        
        public long HrOfficeId { get; set; }
        public IsShow IsShow { get; set; } = IsShow.Yes;

        [ForeignKey("HrOfficeId")]
        public virtual HrOffice HrOffice { get; set; }
    }
}
