﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignement_3.DAL.Models
{
    internal class Department
    {
        public int Id { get; set; }
        public string Code { get; set; }    
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }
            
    }
}
