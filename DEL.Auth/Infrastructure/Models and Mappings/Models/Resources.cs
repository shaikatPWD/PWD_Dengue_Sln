using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.Infrastructure
{
    [Table("Resources")]
    public class Resources : Entity
    { 
        public string ResourceName { get; set; }
        public string Quantity { get; set; }        
        public int OrderID { get; set; }        
        public long HrOfficeId { get; set; }
        [ForeignKey("HrOfficeId")]
        public virtual HrOffice HrOffice { get; set; }
    }
}
