﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Jobs
    {
        [Key]
        public int JobId { get; set; }

        public string JobName { get; set; }
    }
}
