using CemexDictionaryApp.Models;
using CemexDictionaryApp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Repositories
{
    public class NewsRepository : INewsRepository
    {
        DBContext context;
        private readonly IWebHostEnvironment hostEnvironment;
        public NewsRepository(DBContext _context, IWebHostEnvironment _hostEnvironment)
        {
            context = _context;
            hostEnvironment = _hostEnvironment;
        }
        public List<News> ActiveNews()
        {
            List<News> _newsList = context.news.
                Where(New => New.Status == "Active").ToList();
            return _newsList;
        }
        public List<News> GetAll()
        {
            List<News> news = context.news.ToList();
            return news;
        }
        public int NewssCount()
        {
            return context.news.ToList().Count();
        }
        public string UploadedFile([Bind("Id", "NewsImage")] NewsViewModel NewsViewModel)
        {
            string uniqueFileName = null;

            if (NewsViewModel.NewsImage != null)
            {
                //string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + NewsViewModel.NewsImage.FileName;
                string path = Path.Combine(ServerConfig.ImagePath + @"\images\News\", uniqueFileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    NewsViewModel.NewsImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        public Task<int> Insert(News news)
        {
            context.news.Add(news);
            return context.SaveChangesAsync();
        }
        public int Update(int id, News news)
        {
            News OldNews = context.news.
                FirstOrDefault(news => news.Id == id);
            if (OldNews != null)
            {
                OldNews.Title = news.Title;
                OldNews.Image = news.Image;
                OldNews.Status = news.Status;
                OldNews.Description = news.Description;
                return context.SaveChanges();
            }
            return 0;
        }
        public int Delete(int id)
        {
            News OldNews = context.news.FirstOrDefault(news => news.Id == id);
            context.Remove(OldNews);
            return context.SaveChanges();
        }
        public News GetById(int NewsId)
        {
            News News = context.news.
                FirstOrDefault(news => news.Id == NewsId);
            return News;
        }

    }
}
