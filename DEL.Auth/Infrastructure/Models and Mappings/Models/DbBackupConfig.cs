using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.Infrastructure
{
    [Table("DbBackupConfig")]
    public class DbBackupConfig : Entity
    {
        public string DBContextName { get; set; }
        public string DBName { get; set; }
        public string FilePrefix { get; set; }
        public string FileDirectory { get; set; }
    }
}
