﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Role { get; set; }
        public string State { get; set; }
        public string Zone { get; set; }
        public string Category { get; set; }
        public string Occupation { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public virtual List<ProductLog> ProductLogs { get; set; }
    }
}
