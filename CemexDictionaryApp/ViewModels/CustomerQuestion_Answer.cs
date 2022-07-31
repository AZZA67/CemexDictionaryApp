using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.ViewModels
{
    public class CustomerQuestion_Answer
    {
        public string Answer { get; set; }
        public List<IFormFile> QuestionImage { get; set; }
        public List<string> Videos_URLs { get; set; }
        public List<string> existing_images { get; set; }
        public string AdminId { get; set; }

    }
}
