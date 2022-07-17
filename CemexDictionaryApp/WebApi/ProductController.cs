using CemexDictionaryApp.Models;
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
    public class ProductController : ControllerBase
    {
        IProductRepository Product_Repository;
        public ProductController(IProductRepository _product_Repository)
        {
            Product_Repository = _product_Repository;
        }
        [HttpGet]
        public IActionResult getAll()
        {
            if (Product_Repository.GetAll_Active_Products() != null)
            {
                return Ok();
            }
            return BadRequest("No Products are found !");
        }
    }
}
