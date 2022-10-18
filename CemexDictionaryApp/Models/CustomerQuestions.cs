using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Models
{

    public enum Question_Status
    {
        Pending,
        Answered,
        Rejected
    }
    public class CustomerQuestions
    {
        public int ID { get; set; }
        public string Text { get; set; }
        //[Required(ErrorMessage = "Answer Question Is required !")]
        public string Answer { get; set; }
        public string Status { get; set; }
        public virtual ApplicationUser Admin { get; set; }
        [ForeignKey("Admin")]
        public string AdminId { get; set; }
        public virtual ApplicationUser User { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public DateTime SubmitTime { get; set; }
        public bool IsRead { get; set; }
        public string Comment { get; set; }
        public virtual QuestionCategory Category { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual List<CustomerQuestionMedia> QuestionMedia { get; set; }
        public string Description { get; set; }
    }
}
