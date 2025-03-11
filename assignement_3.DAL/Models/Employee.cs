﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignement_3.DAL.Models
{
    public class Employee:BaseEntity
    {


        public int? Age { get; set; }

        public string Email { get; set; }


        public string Address { get; set; }

        public string Phone { get; set; }

        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime HiringDate { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
