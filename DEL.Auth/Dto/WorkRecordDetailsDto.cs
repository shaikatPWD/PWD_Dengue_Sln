using DEL.Auth.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.DTO
{
    public class WorkRecordDetailsDto
    {   
        public long? Id { get; set; }
        public long? OfficeId { get; set; }
        public string OfficeName { get; set; }
        public long? AssetId { get; set; }
        public string AssetName { get; set; }
        public string AssetBuildingName { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string CompletionDatetxt { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public IsComplete IsComplete { get; set; } = 0;
        public string IsCompleteName { get; set; }        
        /// <summary>
        public string IsPondsCleanUptxt { get; set; }
        public string IsWastageCleanUptxt { get; set; }
        public string IsMedicalCollegeCleanUptxt { get; set; }
        public string IsOfficeAndHouseholdCleanUptxt { get; set; }
        public string IsStillWaterCleanUptxt { get; set; }
        public string IsCuringWaterCleanUptxt { get; set; }
        public string IsUnderConstructionBuildingCleanUptxt { get; set; }
        public string IsPondsCleanUpDatetxt { get; set; }
        public string IsWastageCleanUpDatetxt { get; set; }
        public string IsMedicalCollegeCleanUpDatetxt { get; set; }
        public string IsOfficeAndHouseholdCleanUpDatetxt { get; set; }
        public string IsStillWaterCleanUpDatetxt { get; set; }
        public string IsCuringWaterCleanUpDatetxt { get; set; }
        public string IsUnderConstructionBuildingCleanUpDatetxt { get; set; }
        /// </summary>
        public int OrderId { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? EditDate { get; set; }
        public long? EditedBy { get; set; }
        public EntityStatus? Status { get; set; }
    }

    public class DengueReportDto
    {
        public string OfficeName { get; set; }
        public string AssetBuildingName { get; set; }
        public string CompletionDate { get; set; }
        public IsComplete IsComplete { get; set; }
        public string IsCompleteName { get; set; }
    }
}
