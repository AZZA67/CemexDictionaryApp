using Microsoft.AspNetCore.Identity;
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
        public virtual List<ProductLog> ProductLogs { get; set; }
    }
}
