using CemexDictionaryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Repositories
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        DBContext context;

        public ProductTypeRepository(DBContext _context)
        {
            context = _context;
        }

        public List<ProductType> GetAll()
        {
            List<ProductType> _productsTypes = context.productTypes.ToList();
            return _productsTypes;
        }

        public int Insert(ProductType productType)
        {
            context.productTypes.Add(productType);
            return context.SaveChanges();
        }

        public int Delete(int id)
        {
            ProductType productType = context.productTypes.FirstOrDefault(producttype => producttype.Id == id);
            context.Remove(productType);
            return context.SaveChanges();
        }
        public ProductType GetById(int id)
        {
            ProductType productType = context.productTypes.FirstOrDefault(producttype => producttype.Id == id);

            return productType;
        }

    }
}
