using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.WebApi.ApiViewModel
{
    public class ApiQuestion
    {
        public int id { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionAnswer { get; set; }
        public List<string> QuestionImagesUrls { get; set; }  //URLS  
        public List<string> QuestionAnswerVideosUrls { get; set; }

        public List<string> QuestionAnswerImagesUrls { get; set; }
        public string QuestionDescription { get; set; }
        public string  Status { get; set; }
    }
}
