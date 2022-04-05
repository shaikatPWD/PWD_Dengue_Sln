using DEL.Auth.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.DTO
{    
    public class EnumDto
    {
        public long? Id { get; set; }
        public string Name { get; set; }
    }
}
