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
        public string Category_Name { get; set; } //string of category names
        public List<string> ImagesPaths { get; set; } = new List<string>();
        //public List<string> VideoPaths { get; set; } = new List<string>();
        public List<string> Answer_ImagesPaths { get; set; } = new List<string>();
        public List<string> Answer_VideoPaths { get; set; } = new List<string>();

        //another list of answer videos
        //another list of answer images
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
                        Category_Name = customer_question.Category.Name_Ar,
                    };

                    foreach (var questionMedia in customer_question.QuestionMedia)
                    {
                        if (questionMedia.Type == "Image" && questionMedia.User.Role=="User")
                        {
                            _question.ImagesPaths.Add("/images/CustomerQuestions/" + questionMedia.Path);
                        }

                        else if(questionMedia.Type == "Video")
                        {
                            _question.Answer_VideoPaths.Add(questionMedia.Path);
                        }

                        else
                        {
                            _question.Answer_ImagesPaths.Add("/images/CustomerQuestions/CustomerQuestions_Answers/" + questionMedia.Path);
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
