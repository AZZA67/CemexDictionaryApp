using CemexDictionaryApp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.ViewModels
{
    public class QuestionViewModel
    {
        [Required(ErrorMessage = "Question Text Is Required")]
        public string Text { get; set; }
        [Required(ErrorMessage = "Answer Text Is Required")]
        public string Answer { get; set; }
  
        public string Tags { get; set; } //Ask about it again 
        public bool TopQuestion { get; set; }

        public List<IFormFile> QuestionImage { get; set; }
        public List<string> Videos_URLs { get; set; }
        public List<string> existing_images { get; set; }
       //public  string[] existing_images = new string[1000];
      /*  public string check { get; set; }*/ //Ask about it again 
        //[Required(ErrorMessage = "Please Select category")]
        public List<QuestionCategory> QuestionCategories { get; set; }
    }
}
