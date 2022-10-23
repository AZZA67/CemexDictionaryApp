using CemexDictionaryApp.Models;
using CemexDictionaryApp.Repositories;
using CemexDictionaryApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
namespace CemexDictionaryApp.Controllers
{
    public class NewsController : Controller
    {
        INewsRepository NewsRepository;
        INewsLogRepository NewsLogRepository;
        public NewsController(INewsRepository _newsRepository, INewsLogRepository _newsLogRepository)
        {
            NewsRepository = _newsRepository;
            NewsLogRepository = _newsLogRepository;
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetAllNews()
        {
          
            List <News> news = NewsRepository.GetAll();
            return View(news);
        }
        [Authorize]
        [HttpGet]
        public IActionResult AddNewNews()
        {
            return PartialView();
        }
        [Authorize]
        public IActionResult Details(int NewsId)
        {
          News news = NewsRepository.GetById(NewsId);
            return PartialView("Details", news);
        }
        public IActionResult ChangeNewsStatusById(int NewsId)
        {
           News news = NewsRepository.GetById(NewsId);
          
            if (news.Status == "Active")
                news.Status = "InActive";
            else
                news.Status = "Active";

            NewsRepository.Update(NewsId, news);
            NewsLog _newId = new()
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                DateTime = DateTime.Now,
                Action = news.Status,
                NewId = news.Id
            };
            NewsLogRepository.Insert(_newId);
            return Json(news.Status);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddNewNews(NewsViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = NewsRepository.UploadedFile(model);
                News news = new()
                {
                    Title = model.Title,
                    Description = model.Description,
                    Status = "Active",
                    Image = uniqueFileName,
                };
                await NewsRepository.Insert(news);
                NewsLog _newslog = new()
                {
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    DateTime = DateTime.Now,
                    Action = "Adding",
                    NewId = news.Id
                };
                TempData["Success"] = "true";
                NewsLogRepository.Insert(_newslog);
            
                return RedirectToAction("GetAllNews", "News");
            
            }
            return View("AddNewNews");
        }
    }
}
