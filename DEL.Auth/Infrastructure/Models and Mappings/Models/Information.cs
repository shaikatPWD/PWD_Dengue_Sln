using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.Infrastructure
{
    [Table("Information")]
    public class Information : Entity
    { 
        public string FullName { get; set; }
        public string Mobile { get; set; }        
        public string ComplainID { get; set; }        
        public int DhakaInOut { get; set; }        
        public long AreaID { get; set; }        
        public long? DistrictID { get; set; }        
        public string Location { get; set; }
        public string Remarks { get; set; }
        public ComplainStatus ComplainStatus { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }
        public int OrderID { get; set; }
        [ForeignKey("AreaID")]
        public virtual Area Area { get; set; }
        [ForeignKey("DistrictID")]
        public virtual District District { get; set; }
    }
}
