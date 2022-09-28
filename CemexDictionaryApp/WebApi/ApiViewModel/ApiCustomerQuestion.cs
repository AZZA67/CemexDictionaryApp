using CemexDictionaryApp.Models;
using System;
using System.Collections.Generic;

namespace CemexDictionaryApp.WebApi.ApiViewModel
{
    public class ApiCustomerQuestion
    {
         //public int ID { get; set; }
        public string Text { get; set; }
        //[Required(ErrorMessage = "Answer Question Is required !")]
        public string Answer { get; set; }
        public string Status { get; set; }
        public DateTime SubmitTime { get; set; }
        public string Comment { get; set; }
        public QuestionCategory Category { get; set; }
        public List<string> ImagesPaths { get; set; }
        public List<string> VideoPaths { get; set; }
    }

    public class ApiCustomerQuestionMapping
    {
        public static List<ApiCustomerQuestion> Mapping(List<CustomerQuestions> Customer_questions)
        {
            if (Customer_questions != null)
            {
                List<ApiCustomerQuestion> _Customer_questions = new();
                foreach (var customer_question in Customer_questions)
                {
                    ApiCustomerQuestion _question = new()
                    {
                        //Id = question.ID,
                        Text = customer_question.Text,
                        Answer = customer_question.Answer,
                        Status = customer_question.Status,
                        SubmitTime = customer_question.SubmitTime,
                        Comment = customer_question.Comment,
                        Category = customer_question.Category,
                    };
                    foreach (var questionMedia in customer_question.QuestionMedia)
                    {
                        if (questionMedia.Type == "image")
                        {
                            _question.ImagesPaths.Add("/images/CustomerQuestions/" + questionMedia.Path);
                        }
                        else
                        {
                            _question.VideoPaths.Add(questionMedia.Path);
                        }
                    }

                  
                    _Customer_questions.Add(_question);
                }
                return _Customer_questions;
            }
            return null;
        }
    }


}
