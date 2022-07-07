using CemexDictionaryApp.Models;
using System.Collections.Generic;

namespace CemexDictionaryApp.Repositories
{
    public interface IProductTypeRepository
    {
        int Delete(int id);
        List<ProductType> GetAll();
        ProductType GetById(int id);
        int Insert(ProductType productType);
    }
}