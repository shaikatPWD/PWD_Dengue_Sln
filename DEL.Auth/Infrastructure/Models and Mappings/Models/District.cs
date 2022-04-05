using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.Infrastructure
{
    [Table("District")]
    public class District : Entity
    { 
        public string Name { get; set; }
        public string BnName { get; set; }        
        public int OrderID { get; set; }
        public IsShow IsShow { get; set; }
    }
}
