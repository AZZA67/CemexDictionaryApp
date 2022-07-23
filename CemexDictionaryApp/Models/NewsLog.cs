using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Models
{
    public class NewsLog
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        [RegularExpression(@"(Active|InActive|Adding)", ErrorMessage = "Action Must be Active or InActive or Adding")]
        public string Action { get; set; }
        public virtual ApplicationUser User { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual News news { get; set; }
        [ForeignKey("news")]
        public int NewId { get; set; }
    }
}
