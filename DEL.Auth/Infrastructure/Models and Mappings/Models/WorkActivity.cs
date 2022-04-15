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
        public IsComplete IsPondsCleanUp { get; set; } = IsComplete.NA;
        public IsComplete IsWastageCleanUp { get; set; } = IsComplete.NA;
        public IsComplete IsMedicalCollegeCleanUp { get; set; } = IsComplete.NA;
        public IsComplete IsOfficeAndHouseholdCleanUp { get; set; } = IsComplete.NA;
        public IsComplete IsStillWaterCleanUp { get; set; } = IsComplete.NA;
        public IsComplete IsCuringWaterCleanUp { get; set; } = IsComplete.NA;
        public IsComplete IsUnderConstructionBuildingCleanUp { get; set; } = IsComplete.NA;
        [ForeignKey("OfficeAssetId")]
        public virtual OfficeAssets OfficeAssets { get; set; }
    }
}