using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.Infrastructure
{
    [Table("OfficeDivision")]
    public class OfficeDivision : Entity
    {        
        public string Name { get; set; }
        public string DisplayName { get; set; }        
        public int OrderID { get; set; }
        public virtual ICollection<District> Districts { get; set; }
    }
}
