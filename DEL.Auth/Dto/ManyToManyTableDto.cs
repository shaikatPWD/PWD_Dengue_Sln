﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEL.Auth.Dto
{
    public class ManyToManyTableDto
    {
        public bool IsLeftTable { get; set; }
        public long Id { get; set; }
        public string Description { get; set; }
    }
}
