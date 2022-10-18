using CemexDictionaryApp.Repositories;
using CemexDictionaryApp.WebApi.ApiViewModel;
using Microsoft.AspNetCore.Mvc;
namespace CemexDictionaryApp.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        readonly INewsRepository NewsRepository;
        public NewsController(INewsRepository newsRepository)
        {
            NewsRepository = newsRepository;
        }

        /// <summary>
        /// List All Actie News
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public IActionResult NewsList()
        {
            var _result = NewsRepository.ActiveNews();
            if(_result != null && _result.Count>0)
                return Ok(new { Flag = true, Message =ApiMessages.Done, Data = ApiNewsMapping.Mapping(_result) });
            else
                return BadRequest(new { Flag = false, Message =ApiMessages.EmptyNewsList, Data = 0 });
        }
    }
}
