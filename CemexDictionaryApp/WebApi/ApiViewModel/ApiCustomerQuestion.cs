using CemexDictionaryApp.Models;
using System;
using System.Collections.Generic;

namespace CemexDictionaryApp.WebApi.ApiViewModel
{
    public class ApiCustomerQuestion
    {
        public int Id { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionAnswer { get; set; }
        public List<string> QuestionImagesUrls { get; set; } = new List<string>();
        public List<string> QuestionAnswerImagesUrls { get; set; } = new List<string>();
        public List<string> QuestionAnswerVideosUrls { get; set; } = new List<string>();
        public string Status { get; set; }
        public string QuestionDescription { get; set; }
        public DateTime SubmitTime { get; set; }
        public string Comment { get; set; }
        public string Category{ get; set; }
    }

    public class ApiCustomerQuestionMapping
    {
        public static List<ApiCustomerQuestion> Mapping(List<CustomerQuestions> customerQuestions)
        {
            if (customerQuestions != null)
            {
                List<ApiCustomerQuestion> _questionList = new();
                foreach (var question in customerQuestions)
                {
                    ApiCustomerQuestion _apiCustomerQuestion = new()
                    {
                        Id = question.ID,
                        QuestionTitle = question.Text,
                        QuestionAnswer = question.Answer,
                        Status = question.Status,
                        SubmitTime = question.SubmitTime,
                        Comment = question.Comment,
                        Category = question.Category.Name_Ar,
                        QuestionDescription = question.Description
                    };

                    foreach (var questionMedia in question.QuestionMedia)
                    {
                        if (questionMedia.Type == "Image" && questionMedia.User.Role=="User")
                            _apiCustomerQuestion.QuestionImagesUrls.Add(ServerConfig.ServerPath + "/images/CustomerQuestions/" + questionMedia.Path);
                        else if(questionMedia.Type == "Video")
                            _apiCustomerQuestion.QuestionAnswerVideosUrls.Add(questionMedia.Path);
                        else
                            _apiCustomerQuestion.QuestionAnswerImagesUrls.Add(ServerConfig.ServerPath  + "/images/CustomerQuestions/CustomerQuestions_Answers/" + questionMedia.Path);
                    }
                    _questionList.Add(_apiCustomerQuestion);
                }
                return _questionList;
            }
            return null;
        }

        public static ApiCustomerQuestion MappingByObject(CustomerQuestions question)
        {
            if (question != null)
            {
                ApiCustomerQuestion _apiCustomerQuestion = new()
                {
                    Id = question.ID,
                    QuestionTitle = question.Text,
                    QuestionAnswer = question.Answer,
                    Status = question.Status,
                    SubmitTime = question.SubmitTime,
                    Comment = question.Comment,
                    Category = question.Category.Name_Ar,
                    QuestionDescription = question.Description
                };

                foreach (var questionMedia in question.QuestionMedia)
                {
                    if (questionMedia.Type == "Image" && questionMedia.User.Role == "User")
                        _apiCustomerQuestion.QuestionImagesUrls.Add(ServerConfig.ServerPath + "/images/CustomerQuestions/" + questionMedia.Path);
                    else if (questionMedia.Type == "Video")
                        _apiCustomerQuestion.QuestionAnswerVideosUrls.Add(questionMedia.Path);
                    else
                        _apiCustomerQuestion.QuestionAnswerImagesUrls.Add(ServerConfig.ServerPath + "/images/CustomerQuestions/CustomerQuestions_Answers/" + questionMedia.Path);
                }
                return _apiCustomerQuestion;
            }
            return null;
        }
    }
}
