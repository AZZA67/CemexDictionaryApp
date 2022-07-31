using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Models
{
    public enum CustomerMediaTypes
    {
        Image,
        Video
    }

    public class CustomerQuestionMedia
    {
        public virtual ApplicationUser User { get; set; } //user or admin 
        [ForeignKey("User")]
        public string UserId { get; set; }
        public int Id { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public virtual CustomerQuestions question { get; set; }
        [ForeignKey("question")]
        public int QuestionId { get; set; }

    }
}

