using DEL.Auth.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.DTO
{    
    public class InformationDto
    { 
        public long? Id { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string ComplainID { get; set; }
        public int DhakaInOut { get; set; }
        public long AreaID { get; set; }
        public string AreaName { get; set; }
        public long? DistrictID { get; set; }
        public string DistrictName { get; set; }
        public string Location { get; set; }
        public string Remarks { get; set; }
        public ComplainStatus ComplainStatus { get; set; }
        public string StatusName { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
        public int OrderID { get; set; }        
        public DateTime? CreateDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? EditDate { get; set; }
        public long? EditedBy { get; set; }
        public EntityStatus? Status { get; set; }
    }
}
