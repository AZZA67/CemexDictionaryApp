using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Models
{
    public enum MediaTypes
    {
        Image,
        Video
    }
    public class Media
    {
    
        public int Id { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public virtual Question question { get; set; }
        [ForeignKey("question")]
        public int QuestionId { get; set; }
    }
}
