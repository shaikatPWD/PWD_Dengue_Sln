using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.Infrastructure
{
    [Table("MetroArea")]
    public class MetroArea : Entity
    {
        public MetroArea()
        {
            this.Areas = new List<Area>();
        }
        public string Name { get; set; }
        public string DisplayName { get; set; }        
        public int OrderID { get; set; }
        public virtual ICollection<Area> Areas { get; set; }
    }
}
