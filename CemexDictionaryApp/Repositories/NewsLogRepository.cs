using CemexDictionaryApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace CemexDictionaryApp.Repositories
{
    public class NewsLogRepository : INewsLogRepository
    {
        DBContext context;
        private readonly IWebHostEnvironment hostEnvironment;
        public NewsLogRepository(DBContext _context, IWebHostEnvironment _hostEnvironment)
        {
            context = _context;
            hostEnvironment = _hostEnvironment;
        }
        public List<NewsLog> GetAll_Logs()
        {
            List<NewsLog> productLogs = context.NewsLog.AsSplitQuery().
                Include(NewsLog => NewsLog.User).
                Include(NewsLog => NewsLog.news).ToList();
            return productLogs;
        }
        public int Insert(NewsLog NewLog)
        {
            context.NewsLog.Add(NewLog);
            return context.SaveChanges();
        }
        public int Delete(int id)
        {
            NewsLog newsLog = context.NewsLog.FirstOrDefault(NewsLog => NewsLog.Id == id);
            context.Remove(newsLog);
            return context.SaveChanges();
        }
        public List<NewsLog> GetByNewId(int NewsId)
        {
            List<NewsLog> Newslog = context.NewsLog.Where(newslog => newslog.NewId == NewsId)
                .Include(News => News.User)
                .Include(NewsLog => NewsLog.news).ToList();
            return Newslog;
        }

    }
}
