using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionary.Models
{
    public class SearchModel
    {
        [Required]
        public string SearchKeyword { get; set; }
        public int[] categories { get; set; }
    }
}
