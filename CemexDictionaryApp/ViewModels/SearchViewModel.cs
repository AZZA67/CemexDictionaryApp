﻿using System;
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
        public List<string> SearchCategories { get; set; }
        public int[] SelectedCategories { get; set; }
    }
}