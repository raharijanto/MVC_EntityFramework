﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DTSMCC_WebApp.Models
{
    public class Employees
    {
        [Key]
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public Jobs Jobs { get; set; }
        [ForeignKey("Jobs")]
        public int IdJob { get; set; }
    }
}
