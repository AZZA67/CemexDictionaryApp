using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CemexDictionaryApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CemexDictionaryApp.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        readonly INewsRepository NewsRepository;
        readonly IQuestionCategoryRepository QuestionCategory;
        public HomeController(INewsRepository newsRepository, IQuestionCategoryRepository questionCategory)
        {
            NewsRepository = newsRepository;
            QuestionCategory = questionCategory;
        }

        [HttpGet("Data")]
        public IActionResult HomeData()
        {
            var _news = NewsRepository.ActiveNews();
            var _questioncategory = QuestionCategory.GetAll().Select(p => p.Name).ToList();
            
            if(_questioncategory!=null)
            return Ok(new { Flag = true, Message = ApiMessages.Done, QuestionCategories = _questioncategory, News = _news }); 
            else
                return BadRequest(new { Flag = false, Message = ApiMessages.EmptyNewsList, Data = 0 });
        }

    }
}
