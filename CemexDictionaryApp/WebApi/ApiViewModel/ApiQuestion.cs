using CemexDictionaryApp.Models;
using System;
using System.Collections.Generic;

namespace CemexDictionaryApp.WebApi.ApiViewModel
{
    public class ApiQuestion
    {
        public int Id { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionAnswer { get; set; }
        public string Tags { get; set; }
        public DateTime SubmitTime { get; set; }
        public bool TopQuestion { get; set; }
        public List<string> QuestionImagesUrls { get; set; } = new List<string>();
        public List<string> QuestionAnswerVideosUrls { get; set; } = new List<string>();
        public  List<string> QuestionCategory { get; set; } = new List<string>();
    }

    public class ApiQuestionMapping
    {
        public static List<ApiQuestion> Mapping(List<Question> questions)
        {
            if (questions != null)
            {
                List<ApiQuestion> _questions = new();
                foreach (var question in questions)
                {
                    ApiQuestion _question = new()
                    {
                        Id = question.ID,
                        QuestionTitle = question.Text,
                        QuestionAnswer = question.Answer,
                        Tags = question.Tags,
                        SubmitTime = question.SubmitTime,
                        TopQuestion = question.TopQuestion,
                    };

                    foreach(var questionMedia in question.QuestionMedia)
                    {
                        if(questionMedia.Type=="Image")
                            _question.QuestionImagesUrls.Add(ServerConfig.ServerPath+ "/images/Questions/" + questionMedia.Path); 
                        else
                            _question.QuestionAnswerVideosUrls.Add( questionMedia.Path);
                    }

                    foreach (var questionCategory in question.Question_category)
                    {
                        _question.QuestionCategory.Add(questionCategory.category.Name_Ar);
                    }
                        _questions.Add(_question);
                }
                return _questions;
            }
            return null;
        }
    }
}
