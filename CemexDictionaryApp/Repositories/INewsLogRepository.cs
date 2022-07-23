using CemexDictionaryApp.Models;
using System.Collections.Generic;

namespace CemexDictionaryApp.Repositories
{
    public interface INewsLogRepository
    {
        int Delete(int id);
        List<NewsLog> GetAll_Logs();
        List<NewsLog> GetByNewId(int NewsId);
        int Insert(NewsLog NewsLog);
    }
}