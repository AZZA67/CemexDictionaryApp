using CemexDictionaryApp.Repositories;
using CemexDictionaryApp.WebApi.ApiModels;
using Microsoft.AspNetCore.Mvc;
namespace CemexDictionaryApp.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly IProductRepository ProductRepository;
        public ProductController(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }
    
        /// <summary>
        /// List All Active Products
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public IActionResult Products()
        {
            var _result = ProductRepository.ActiveProducts();
            if (_result != null && _result.Count>0)
                return Ok(new { Flag = true, Message = ApiMessages.Done, Data = ApiProductMapping.Mapping(_result)});
            else
                return BadRequest(new { Flag = false, Message =ApiMessages.EmptyProductList, Data = 0 });
        }
    }
}
