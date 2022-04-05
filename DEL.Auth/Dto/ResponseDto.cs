using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.DTO
{
    public class ResponseDto
    {
        public ResponseDto()
        {
            Success = false;
            Message = "";
        }
        public long? Id { get; set; }
        public bool? Success { get; set; }
        public string Message { get; set; }
    }
}
