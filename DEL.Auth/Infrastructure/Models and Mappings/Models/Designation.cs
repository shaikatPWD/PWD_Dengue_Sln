using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.Infrastructure
{
    [Table("Designation")]
    public class Designation : Entity
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public string Remarks { get; set; }
    }
}
