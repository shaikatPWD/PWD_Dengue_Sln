using DEL.Auth.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.DTO
{    
    public class WorkRecordDto
    { 
        public long? Id { get; set; }
        public long? OfficeId { get; set; }
        public long? AssetId { get; set; }
        public AssetsDto Assets { get; set; }
        public string AssetName { get; set; }
        public List<WorkRecordDetails> WorkRecordDetails { get; set; }
        public List<long> RemoveWorkRecordDetails { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? EditDate { get; set; }
        public long? EditedBy { get; set; }
        public EntityStatus? Status { get; set; }
    }
}
