using DEL.Auth.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.DTO
{    
    public class HrOfficeDto
    { 
        public long? Id { get; set; }
        public string Name { get; set; }
        public string BnName { get; set; }
        public long? ZoneId { get; set; }
        public string ZoneName { get; set; }
        public long? CircleId { get; set; }
        public string CircleName { get; set; }
        public long? DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string Phone { get; set; }
        public string PhoneResidence { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public IsShow IsShow { get; set; } = IsShow.Yes;
        public string IsShowName { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? EditDate { get; set; }
        public long? EditedBy { get; set; }
        public EntityStatus? Status { get; set; }
    }
}
