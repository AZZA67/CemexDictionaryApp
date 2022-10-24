﻿using CemexDictionaryApp.Core;
using CemexDictionaryApp.Models;
using CemexDictionaryApp.Repositories;
using CemexDictionaryApp.ViewModels;
using CemexDictionaryApp.WebApi;
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
        readonly INewsRepository NewsRepository;
        readonly INewsLogRepository NewsLogRepository;
        readonly INotificationRepo NotificationRepo;
        public NewsController(INewsRepository _newsRepository, INewsLogRepository _newsLogRepository , INotificationRepo notificationRepo)
        {
            NewsRepository = _newsRepository;
            NewsLogRepository = _newsLogRepository;
            NotificationRepo = notificationRepo;
        }

        [HttpGet]
        public IActionResult GetAllNews()
        {  
            List <News> news = NewsRepository.GetAll();
            return View(news);
        }

        [HttpGet]
        public IActionResult AddNewNews()
        {
            return PartialView();
        }

        public IActionResult Details(int NewsId)
        {
          News news = NewsRepository.GetById(NewsId);
            return PartialView("Details", news);
        }

        public IActionResult ChangeNewsStatusById(int NewsId)
        {
           News news = NewsRepository.GetById(NewsId);
            //product.Status = !product.Status;
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

                //Notifcation
                Notification _notifcation = new()
                {
                    Title = Messages.NewsTitle,
                     Message = Messages.NewsMessage,
                    UserId = "All",
                    ObjectId = news.Id.ToString(),
                    Type = NotificationType.News,
                    SubmitDate = DateTime.Now.ToString()
                };
                await NotificationRepo.Add(_notifcation);


                return RedirectToAction("GetAllNews", "News");
            }
            return View("AddNewNews");
        }
    }
}
