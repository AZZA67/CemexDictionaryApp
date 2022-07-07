using CemexDictionaryApp.Models;
using CemexDictionaryApp.Repositories;
using CemexDictionaryApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository ProductRepository;
        IProductTypeRepository ProductTypeRepository;

        public ProductController(IProductRepository _productRepository,  DBContext _dbcontext, IProductTypeRepository _productTypeRepository)
        {
            ProductRepository = _productRepository;
            ProductTypeRepository = _productTypeRepository;
        }
        [HttpGet]
        public IActionResult GetAllProducts( int page_number)
        {

            PagenationParameters _pagenationParameters = new PagenationParameters();
            _pagenationParameters.PageNumber = page_number;

            List<Product> products = ProductRepository.GetAll(_pagenationParameters);
            return View(products);
        }
        [HttpGet]
        public IActionResult AddNewProduct()
        {
            List<ProductType> productTypes = ProductTypeRepository.GetAll();
            ViewData["ProductTypes"] = productTypes;
            return View();
        }
        public IActionResult Details(int productId)
        {
            Product product = ProductRepository.GetById(productId);
            return PartialView("Details", product);
        }
        public IActionResult ChangeProductStatusById(int productId)
        {

            Product product = ProductRepository.GetById(productId);
            //product.Status = !product.Status;
            if (product.Status == "Active")
                product.Status = "InActive";
            else
            {
                product.Status = "Active";
            }
            ProductRepository.Update( productId, product);
            return Json(product.Status);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProduct(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProductRepository.UploadedFile(model);

                Product product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    Status = "Active",
                    Image = uniqueFileName,
                    ProductTypeId = model.ProductTypeId,
                    Type = ProductTypeRepository.GetById(model.ProductTypeId).Type


                };
                await ProductRepository.Insert(product);
                //int productscount = ProductRepository.ProductsCount();
                //int x = productscount / 3;
                //if (productscount%3!=0)
                //{
                //    x = x + 1;
                //}
              
                    return RedirectToAction("GetAllProducts", "Product", new {@page_number=1 });
                //return RedirectToAction("GetAllProducts", "Product");
            }
            return View("AddNewProduct");
        }

    }
}
