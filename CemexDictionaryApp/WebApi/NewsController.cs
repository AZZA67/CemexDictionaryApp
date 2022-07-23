using CemexDictionaryApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CemexDictionaryApp.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        INewsRepository News_Repository;
        public NewsController(INewsRepository _news_Repository)
        {
            News_Repository = _news_Repository;
        }
        [HttpGet("getAll")]
        public IActionResult getAll()
        {
            if (News_Repository.GetAll_Active_Newss() != null)
            {
                return Ok(News_Repository.GetAll_Active_Newss());
            }
            return BadRequest("No News are found !");
        }
    }
}
