using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.Infrastructure
{
    [Table("Assets")]
    public class Assets : Entity
    {
        public string AssetType { get; set; }
        public string AssetTypeFull { get; set; }
        public int OrderId { get; set; }
        public virtual ICollection<WorkRecordDetails> WorkRecordDetails { get; set; }
    }
}
