using CemexDictionaryApp.Models;
using System.Collections.Generic;

namespace CemexDictionaryApp.Repositories
{
    public interface IProductLogRepository
    {
        int Delete(int id);
        List<ProductLog> GetAll_Logs();
        List<ProductLog> GetByProductId(int ProductId);
        int Insert(ProductLog productLog);
    }
}