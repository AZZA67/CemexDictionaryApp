using CemexDictionaryApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Repositories
{
    public class ProductLogRepository : IProductLogRepository
    {



        DBContext context;
        private readonly IWebHostEnvironment hostEnvironment;
        public ProductLogRepository(DBContext _context, IWebHostEnvironment _hostEnvironment)
        {
            context = _context;
            hostEnvironment = _hostEnvironment;
        }
        public List<ProductLog> GetAll_Logs()
        {
            List<ProductLog> productLogs = context.ProductsLog.AsSplitQuery().
                Include(ProductLog => ProductLog.User).
                Include(productlog => productlog.Product).ToList();
            return productLogs;
        }

        public int Insert(ProductLog productLog)
        {
            context.ProductsLog.Add(productLog);
            return context.SaveChanges();
        }
        //public int Update(int id, Product product)
        //{
        //    Product OldProduct = context.products.Include(Product => Product.productType).
        //        FirstOrDefault(product => product.Id == id);
        //    if (OldProduct != null)
        //    {
        //        OldProduct.Name = product.Name;
        //        OldProduct.Image = product.Image;
        //        OldProduct.Status = product.Status;
        //        OldProduct.productType.Type = product.productType.Type;
        //        OldProduct.Description = product.Description;
        //        return context.SaveChanges();
        //    }
        //    return 0;
        //}
        public int Delete(int id)
        {
            ProductLog productLog = context.ProductsLog.FirstOrDefault(productLog => productLog.Id == id);
            context.Remove(productLog);
            return context.SaveChanges();
        }
        public List<ProductLog> GetByProductId(int ProductId)
        {
            List<ProductLog> Productslog = context.ProductsLog.Where(product => product.ProductId == ProductId)
                .Include(Product => Product.User)
                .Include(ProductLog => ProductLog.Product).ToList();

            return Productslog;
        }
    }
}
