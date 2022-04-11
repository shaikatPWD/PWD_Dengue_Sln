using DEL.Auth.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.DTO
{
    public class MonthlyMonitoringInfoDto
    {   
        public long? Id { get; set; }
        public long? OfficeId { get; set; }        
        public string Period { get; set; }
        public long? OfficeAssetId { get; set; }
        public string OfficeAssetName { get; set; }
        public IsComplete IsPondsCleanUp { get; set; } = IsComplete.NA;
        public string IsPondCleanUpName { get; set; }
        public IsComplete IsWastageCleanUp { get; set; } = IsComplete.NA;
        public string IsWastageCleanUpName { get; set; }
        public IsComplete IsMedicalCollegeCleanUp { get; set; } = IsComplete.NA;
        public string IsMedicalCollegeCleanUpName { get; set; }
        public IsComplete IsOfficeAndHouseholdCleanUp { get; set; } = IsComplete.NA;
        public string IsOfficeAndHouseholdCleanUpName { get; set; }
        public IsComplete IsStillWaterCleanUp { get; set; } = IsComplete.NA;
        public string IsStillWaterCleanUpName { get; set; }
        public IsComplete IsCuringWaterCleanUp { get; set; }
        public string IsCuringWaterCleanUpName { get; set; }
        public IsComplete IsUnderConstructionBuildingCleanUp { get; set; } = IsComplete.NA;
        public string IsUnderConstructionBuildingCleanUpName { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? EditDate { get; set; }
        public long? EditedBy { get; set; }
        public EntityStatus? Status { get; set; }
    }
}
