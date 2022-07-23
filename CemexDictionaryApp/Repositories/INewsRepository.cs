using CemexDictionaryApp.Models;
using CemexDictionaryApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Repositories
{
    public interface INewsRepository
    {
        int Delete(int id);
        List<News> GetAll();
        List<News> GetAll_Active_Newss();
        News GetById(int NewsId);
        Task<int> Insert(News news);
        int NewssCount();
        int Update(int id, News news);
        string UploadedFile([Bind(new[] { "Id", "NewsImage" })] NewsViewModel NewsViewModel);
    }
}