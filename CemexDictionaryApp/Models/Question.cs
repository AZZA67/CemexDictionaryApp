
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Models
{
    public class Question
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public string Answer { get; set; }
        public string Tags { get; set; }
        public virtual ApplicationUser Admin { get; set; }
        [ForeignKey("Admin")]
        public string AdminId { get; set; }
        public DateTime SubmitTime { get; set; }
        public bool TopQuestion { get; set; }
        public virtual List<Media> QuestionMedia { get; set; }
        public virtual List<QuestionPerCategory> Question_category { get; set; }

    }
}
