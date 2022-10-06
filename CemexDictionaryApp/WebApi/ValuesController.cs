using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CemexDictionaryApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CemexDictionaryApp.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly INewsRepository NewsRepository;
        readonly IQuestionRepository Questions;
        public ValuesController(INewsRepository newsRepository , IQuestionRepository questions)
        {
            NewsRepository = newsRepository;
        }


        [HttpPost("Test")]
        public IActionResult Post()
        {
            var _questionText = HttpContext.Request.Form["QuestionText"];
            var _questionDesc = HttpContext.Request.Form["QuestionDesc"];
            var _questionCategory = HttpContext.Request.Form["QuestionCateory"];

            var ImageOne = HttpContext.Request.Form.Files[0];
            var ImageTwo = HttpContext.Request.Form.Files[1].FileName;


            //Questions.UploadFile(HttpContext.Request.Form.Files);
            //var x = HttpContext.Request.Form.Keys.ToList();
            //var user = HttpContext.Request.Form["islam"];
            //var name = HttpContext.Request.Form.Files.Count;
            //var ddd = HttpContext.Request.Form.Files[0].FileName;

            //foreach (var key in HttpContext.Request.Form.Keys)
            //{
            //    var val = HttpContext.Request.Form[key];

            //    //process the form data
            //}

            return Ok(new { QuestionText = _questionText , QuestionDesc = _questionDesc , QuestionCateory = _questionCategory , Image1 = ImageOne  , Image2 =ImageTwo });
        }
    }
}
