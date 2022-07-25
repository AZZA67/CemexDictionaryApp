using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.ViewModels
{
    public class SearchViewModel
    {
        [Required]
        public string SearchKeyword { get; set; }
        public int[] Selected_categories { get; set; }
    }
}
