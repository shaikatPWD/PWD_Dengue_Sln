using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEL.Auth.Infrastructure;

namespace DEL.Auth.DTO
{
    public class FormFieldsDetailsDto
    {
        public long Id { get; set; }
        public long FormId { get; set; }
        public string TagName { get; set; }
        public string AttrId { get; set; }
        public string AttrName { get; set; }
        public string AttrType { get; set; }
        public string AttrCaption { get; set; }
        public string AttrPlaceholder { get; set; }
        public string AttrTitle { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? EditDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? EditedBy { get; set; }
        public EntityStatus? Status { get; set; }
    }    
}
