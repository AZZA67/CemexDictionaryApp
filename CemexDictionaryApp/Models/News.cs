using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [RegularExpression(@"(Active|InActive)", ErrorMessage = "Status Must be Active or InActive")]
        public string Status { get; set; }
        public virtual List<NewsLog> NewsLogs { get; set; }

    }
}
