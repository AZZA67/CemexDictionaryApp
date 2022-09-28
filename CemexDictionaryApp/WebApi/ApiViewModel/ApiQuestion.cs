using CemexDictionaryApp.Models;
using CemexDictionaryApp.WebApi.ApiModels;
using System;
using System.Collections.Generic;

namespace CemexDictionaryApp.WebApi.ApiViewModel
{
    public class ApiQuestion
    {
        //public int ID { get; set; }
        public string Text { get; set; }
        public string Answer { get; set; }
        public string Tags { get; set; }
     
        public DateTime SubmitTime { get; set; }
        public bool TopQuestion { get; set; }
        public List<string> ImagesPaths = new List<string>();
        public List<string> VideoPaths = new List<string>();
        public  List<QuestionCategory> Question_category= new List<QuestionCategory>();

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
                        //Id = question.ID,
                        Text = question.Text,
                        Answer = question.Answer,
                        Tags = question.Tags,
                        SubmitTime = question.SubmitTime,
                        TopQuestion = question.TopQuestion,
                    };

                    foreach(var questionMedia in question.QuestionMedia)
                    {
                        if(questionMedia.Type=="Image")
                        {
                            _question.ImagesPaths.Add("/images/Questions/"+questionMedia.Path); 
                        }
                        else
                        {
                            _question.VideoPaths.Add( questionMedia.Path);
                        }
                    }
                    foreach (var questionCategory in question.Question_category)
                    {
                        _question.Question_category.Add(questionCategory.category);
                    }
                        _questions.Add(_question);



                }
                return _questions;
            }
            return null;
        }
    }


}
