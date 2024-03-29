﻿using CemexDictionaryApp.Core;
using CemexDictionaryApp.Repositories;
using CemexDictionaryApp.WebApi.ApiViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CemexDictionaryApp.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        readonly INewsRepository NewsRepository;
        readonly IQuestionCategoryRepository QuestionCategory;
        IQuestionRepository QuestionRepository;
        public HomeController(INewsRepository newsRepository, IQuestionCategoryRepository questionCategory, IQuestionRepository questionRepository)
        {
            NewsRepository = newsRepository;
            QuestionCategory = questionCategory;
            QuestionRepository = questionRepository;
        }

        [HttpGet("Data")]
        public IActionResult HomeData()
        {
            var _news = NewsRepository.ActiveNews();
            var _questionCategory = QuestionCategory.GetAll().Select(p => p.Name_Ar).ToList();

            if (_questionCategory != null)
                return Ok(new { 
                    Flag = true, 
                    Message = Messages.Done, 
                    QuestionCategories = _questionCategory, 
                    News = ApiNewsMapping.Mapping(_news), 
                    TopQuestions= ApiQuestionMapping.Mapping(QuestionRepository.GetTopTenQuestions()) 
                });
            else
                return BadRequest(new { Flag = false, Message = Messages.EmptyNewsList, Data = 0 });
        }
    }
}
