using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Models
{
    public class Zone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual State State { get; set; }
        [ForeignKey("State")]
        public int StateId { get; set; }
    }
}
