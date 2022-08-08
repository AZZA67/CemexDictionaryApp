using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.ViewModels
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title Field Is Required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description Field Is Required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please choose News Image")]
        [Display(Name = "News Image")]
        public IFormFile NewsImage { get; set; }
    }
}
