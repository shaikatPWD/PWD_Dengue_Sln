using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.Infrastructure
{
    //[Table("WorkRecord")]
    //public class WorkRecord : Entity
    //{
    //    public long? OfficeId { get; set; }
    //    public long AssetId { get; set; }
    //    public virtual ICollection<WorkRecordDetails> WorkRecordDetails { get; set; }
    //    [ForeignKey("AssetId")]
    //    public virtual Assets Assets { get; set; }
    //}

    [Table("WorkRecordDetails")]
    public class WorkRecordDetails : Entity
    {
        //public long WorkRecordId { get; set; }
        public long? OfficeId { get; set; }
        public long? AssetId { get; set; }
        public string AssetBuildingName { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public IsComplete IsComplete { get; set; } = 0;
        public int OrderId { get; set; }
        [ForeignKey("OfficeId")]
        public virtual HrOffice HrOffice { get; set; }
        [ForeignKey("AssetId")]
        public virtual Assets Assets { get; set; }
        //[ForeignKey("WorkRecordId")]
        //public virtual WorkRecord WorkRecord { get; set; }
    }
}