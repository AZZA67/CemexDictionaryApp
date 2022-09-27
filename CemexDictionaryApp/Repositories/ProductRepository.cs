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
        readonly DBContext Context;
        private readonly IWebHostEnvironment HostEnvironment;

        public ProductRepository(DBContext context, IWebHostEnvironment hostEnvironment)
        {
            Context = context;
            HostEnvironment = hostEnvironment;
        }
        public List<Product> ActiveProducts()
        {
            List<Product> _productsList = Context.products.Include(Product => Product.productType).
                Where(product => product.Status == "Active").ToList();
            return _productsList;
        }
  
        public List<Product> GetAll()
        {
            List<Product> products = Context.products.
                ToList();
            return products;
        }

        public int ProductsCount()
        {
            return Context.products.ToList().Count();
        }
        public string UploadedFile([Bind("Id", "ProductImage")] ProductViewModel ProductViewModel)
        {
            string uniqueFileName = null;

            if (ProductViewModel.ProductImage != null)
            {
                string uploadsFolder = Path.Combine(HostEnvironment.WebRootPath);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + ProductViewModel.ProductImage.FileName;
                string path = Path.Combine(uploadsFolder + @"\images\Products\", uniqueFileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    ProductViewModel.ProductImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        public int Insert(Product product)
        {
            Context.products.Add(product);
            return Context.SaveChanges();
        }
        public int Update(int id, Product product)
        {
            Product OldProduct = Context.products.Include(Product => Product.productType).
                FirstOrDefault(product => product.Id == id);
            if (OldProduct != null)
            {
                OldProduct.Name = product.Name;
                OldProduct.Image = product.Image;
                OldProduct.Status = product.Status;
                OldProduct.productType.Type = product.productType.Type;
                OldProduct.Description = product.Description;

                return Context.SaveChanges();
            }
            return 0;
        }
        public int Delete(int id)
        {
            Product OldProduct = Context.products.FirstOrDefault(product => product.Id == id);
            Context.Remove(OldProduct);
            return Context.SaveChanges();
        }
        public Product GetById(int ProductId)
        {
            Product Product = Context.products.Include(Product => Product.productType).
                FirstOrDefault(product => product.Id == ProductId);
            return Product;
        }

    }
}

