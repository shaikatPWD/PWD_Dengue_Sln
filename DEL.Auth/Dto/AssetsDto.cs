using DEL.Auth.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.DTO
{    
    public class AssetsDto
    { 
        public long? Id { get; set; }
        public string AssetType { get; set; }
        public string AssetTypeFull { get; set; }
        public int OrderId { get; set; }
        public List<WorkRecordDetailsDto> WorkRecordDetails { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? EditDate { get; set; }
        public long? EditedBy { get; set; }
        public EntityStatus? Status { get; set; }
    }
}
