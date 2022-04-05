using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DEL.Auth.Infrastructure;

namespace DEL.Auth.DTO
{
    public class FormDto
    {
        public long Id { get; set; }
        public long MenuId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int? Sl { get; set; }
        public bool PdfRadExist { get; set; }
        public bool CsvRadExist { get; set; }
        public bool ExcelExpRadExist { get; set; }
        public bool ExlDataRadExist { get; set; }
        public bool SubmitButtonExit { get; set; }
        public bool PreRequisities { get; set; }
        public List<long> RemovedFormFieldsDetails { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? EditDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? EditedBy { get; set; }
        public EntityStatus? Status { get; set; }
        public List<FormFieldsDetailsDto> FormFieldsDetails { get; set; }
    }
    
}
