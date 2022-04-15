using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.Infrastructure
{
    [Table("OfficeAssets")]
    public class OfficeAssets : Entity
    {
        public string AssetName { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public string Period { get; set; }
        public long HrOfficeId { get; set; }
        [ForeignKey("HrOfficeId")]
        public virtual HrOffice HrOffice { get; set; }
        public ICollection<WorkActivity> Activities { get; set; }
    }
}
