using CemexDictionaryApp.Models;
using CemexDictionaryApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Repositories
{
    public interface IProductRepository
    {
        int Delete(int id);
        List<Product> GetAll(PagenationParameters productParameters);
        List<Product> GetAll_Active_Products();
        Product GetById(int ProductId);
        Task<int> Insert(Product product);
        int ProductsCount();
        int Update(int id, Product product);
        string UploadedFile([Bind(new[] { "Id", "ProductImage" })] ProductViewModel ProductViewModel);
    }
}