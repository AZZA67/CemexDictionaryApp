using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Models
{
    public class QuestionCategory
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Field Is Required")]
      
        public string Name_En { get; set; }
        [Required(ErrorMessage = "خانة الأسم مطلوبة ")]
        public string Name_Ar { get; set; }

        public virtual List<QuestionPerCategory> Question_category { get; set; }
    }
}
