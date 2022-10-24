using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.DTO
{
    public class QuestionModel
    {
        public string UserId { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; } 

        public List<string> QuestionImage { get; set; }
    }
}
