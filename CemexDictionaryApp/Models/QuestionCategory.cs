using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Models
{
    public class QuestionCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string الأسم { get; set; }

        public virtual List<QuestionPerCategory> Question_category { get; set; }
    }
}
