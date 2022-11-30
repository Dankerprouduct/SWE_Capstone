﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
    }
}
