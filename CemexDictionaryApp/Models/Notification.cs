using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }
        public string ObjectId { get; set; } //Hold Id based on the notifcation type (news / product / question)
        public string Token { get; set; }
        public string SubmitDate { get; set; }
    }
}
