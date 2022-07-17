using CemexDictionaryApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CemexDictionaryApp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CemexDictionaryApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        DBContext context;
        private readonly IWebHostEnvironment hostEnvironment;

        public ProductRepository(DBContext _context, IWebHostEnvironment _hostEnvironment)
        {
            context = _context;
            hostEnvironment = _hostEnvironment;

        }
        public List<Product> GetAll_Active_Products()
        {

            List<Product> products = context.products.Include(Product => Product.productType).
                Where(product => product.Status == "Active").ToList();
            return products;
        }
  
        public List<Product> GetAll()
        {
            List<Product> products = context.products.
                ToList();
            return products;
        }

        public int ProductsCount()
        {
            return context.products.ToList().Count();
        }
        public string UploadedFile([Bind("Id", "ProductImage")] ProductViewModel ProductViewModel)
        {
            string uniqueFileName = null;

            if (ProductViewModel.ProductImage != null)
            {
                string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + ProductViewModel.ProductImage.FileName;
                string path = Path.Combine(uploadsFolder + @"\images\", uniqueFileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    ProductViewModel.ProductImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        public int Insert(Product product)
        {


            context.products.Add(product);
            return context.SaveChanges();
        }
        public int Update(int id, Product product)
        {
            Product OldProduct = context.products.Include(Product => Product.productType).
                FirstOrDefault(product => product.Id == id);
            if (OldProduct != null)
            {
                OldProduct.Name = product.Name;
                OldProduct.Image = product.Image;
                OldProduct.Status = product.Status;
                OldProduct.productType.Type = product.productType.Type;
                OldProduct.Description = product.Description;

                return context.SaveChanges();
            }
            return 0;
        }
        public int Delete(int id)
        {
            Product OldProduct = context.products.FirstOrDefault(product => product.Id == id);
            context.Remove(OldProduct);
            return context.SaveChanges();
        }
        public Product GetById(int ProductId)
        {
            Product Product = context.products.Include(Product => Product.productType).
                FirstOrDefault(product => product.Id == ProductId);
            return Product;
        }

    }
}

