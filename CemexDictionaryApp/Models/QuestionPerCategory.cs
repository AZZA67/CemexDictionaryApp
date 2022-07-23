
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Models
{
    public class QuestionPerCategory
    {

       


        public int QuestionId { get; set; }
        public Question question { get; set; }

        public int CategoryId { get; set; }
        public QuestionCategory category { get; set; }

     

    }
}
