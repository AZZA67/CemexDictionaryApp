using CemexDictionaryApp.Core;
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
            var _products = ProductRepository.ActiveProducts();
            if (_products != null && _products.Count>0)
                return Ok(new { 
                    Flag = true, 
                    Message = Messages.Done, 
                    Data = ApiProductMapping.Mapping(_products)
                });
            else
                return BadRequest(new { Flag = false, Message = Messages.EmptyProductList, Data = 0 });
        }
    }
}
