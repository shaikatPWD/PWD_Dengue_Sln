using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.Infrastructure
{
    [Table("WorkActivity")]
    public class WorkActivity : Entity
    {
        //public long? OfficeId { get; set; }
        public DateTime Date { get; set; }
        public string Period { get; set; }
        public long? OfficeAssetId { get; set; }
        public IsComplete IsPondsCleanUp { get; set; } = 0;
        public IsComplete IsWastageCleanUp { get; set; } = 0;
        public IsComplete IsMedicalCollegeCleanUp { get; set; } = 0;
        public IsComplete IsOfficeAndHouseholdCleanUp { get; set; } = 0;
        public IsComplete IsStillWaterCleanUp { get; set; } = 0;
        public IsComplete IsCuringWaterCleanUp { get; set; } = 0;
        public IsComplete IsUnderConstructionBuildingCleanUp { get; set; } = 0;        
        [ForeignKey("OfficeAssetId")]
        public virtual OfficeAssets OfficeAssets { get; set; }
    }
}