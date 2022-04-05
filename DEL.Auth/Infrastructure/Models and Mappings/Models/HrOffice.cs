using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.Infrastructure
{
    [Table("HrOffice")]
    public class HrOffice : Entity
    {
        public HrOffice()
        {
            this.Areas = new List<Area>();
        }
        public string Name { get; set; }
        public string BnName { get; set; }
        public long? ZoneId { get; set; }
        public long? CircleId { get; set; }
        public long? DivisionId { get; set; }
        public string Phone { get; set; }
        public string PhoneResidence { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public IsShow IsShow { get; set; }
        [ForeignKey("ZoneId")]
        public virtual HrOffice Parent1 { get; set; }
        [ForeignKey("CircleId")]
        public virtual HrOffice Parent2 { get; set; }
        [ForeignKey("DivisionId")]
        public virtual HrOffice Parent3 { get; set; }
        public virtual ICollection<Area> Areas { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
