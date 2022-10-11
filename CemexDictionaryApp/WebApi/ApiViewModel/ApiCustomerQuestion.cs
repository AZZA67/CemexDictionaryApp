using CemexDictionaryApp.Models;
using System;
using System.Collections.Generic;

namespace CemexDictionaryApp.WebApi.ApiViewModel
{
    public class ApiCustomerQuestion
    {
        public int id { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionDescription { get; set; }
        public string QuestionAnswer { get; set; }
        public List<string> QuestionImagesUrls { get; set; } = new List<string>();
        //public List<string> VideoPaths { get; set; } = new List<string>();
        public List<string> QuestionAnswerImagesUrls { get; set; } = new List<string>();
        public List<string> QuestionAnswerVideosUrls { get; set; } = new List<string>();
        public string Status { get; set; }

        //public int ID { get; set; }
        //public string Text { get; set; }
        ////[Required(ErrorMessage = "Answer Question Is required !")]
        //public string Answer { get; set; }
        public DateTime SubmitTime { get; set; }
        public string Comment { get; set; }
        public string Category{ get; set; } //string of category names
        //public List<string> ImagesPaths { get; set; } = new List<string>();
        ////public List<string> VideoPaths { get; set; } = new List<string>();
        //public List<string> Answer_ImagesPaths { get; set; } = new List<string>();
        //public List<string> Answer_VideoPaths { get; set; } = new List<string>();

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
                        QuestionTitle = customer_question.Text,
                        QuestionAnswer = customer_question.Answer,
                        Status = customer_question.Status,
                        SubmitTime = customer_question.SubmitTime,
                        Comment = customer_question.Comment,
                        Category = customer_question.Category.Name_Ar,
                    };

                    foreach (var questionMedia in customer_question.QuestionMedia)
                    {
                        if (questionMedia.Type == "Image" && questionMedia.User.Role=="User")
                        {
                            _question.QuestionImagesUrls.Add("/images/CustomerQuestions/" + questionMedia.Path);
                        }

                        else if(questionMedia.Type == "Video")
                        {
                            _question.QuestionAnswerVideosUrls.Add(questionMedia.Path);
                        }

                        else
                        {
                            _question.QuestionAnswerImagesUrls.Add("/images/CustomerQuestions/CustomerQuestions_Answers/" + questionMedia.Path);
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
