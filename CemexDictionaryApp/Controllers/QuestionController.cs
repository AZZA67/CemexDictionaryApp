using CemexDictionaryApp.Models;
using CemexDictionaryApp.Repositories;
using CemexDictionaryApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace CemexDictionaryApp.Controllers
{
    public class QuestionController : Controller
    {
        IQuestionCategoryRepository QuestionCategoryRepository;
        IQuestionRepository QuestionRepository;
        private UserManager<ApplicationUser> userManager;
        //private SignInManager<ApplicationUser> signInManager;
        IMediaRepository mediaRepository;
        IQuestionPerCategoryRepository questionPerCategoryRepository;
        ICustomerQuestionMediaRepository CustomerQuestionMedia;

        ICustomerQuistionsRepository Customer_QuestionRepository;
        public QuestionController(IQuestionPerCategoryRepository _questionPerCategoryRepository,
            IMediaRepository _mediaRepository, UserManager<ApplicationUser> _userManager,
            IQuestionCategoryRepository _questionCategoryRepository,
            IQuestionRepository _questionRepository,
            ICustomerQuistionsRepository _customer_QuestionRepository,
             ICustomerQuestionMediaRepository _customerQuestionMedia
            )
        {
            CustomerQuestionMedia = _customerQuestionMedia;
            QuestionCategoryRepository = _questionCategoryRepository;
            QuestionRepository = _questionRepository;
            userManager = _userManager;
            mediaRepository = _mediaRepository;
            questionPerCategoryRepository = _questionPerCategoryRepository;
            Customer_QuestionRepository = _customer_QuestionRepository;
        }


        public IActionResult GetAll()
        {
            List<QuestionCategory> categories = QuestionCategoryRepository.GetAll();
            ViewData["Categories"] = categories;
            return View();
        }


        [HttpGet]
        public IActionResult AddNewQuestion()
        {
            List<QuestionCategory> categories = QuestionCategoryRepository.GetAll();
     
            List<string> images = mediaRepository.GetAll_uploaded_photos();
          
            ViewData["Images"] = images;
            ViewData["Categories"] = categories;
            return View();
        }
        public int OrderResult(Question _question, string Keyword)
        {
            return _question.Text.Split(" ", StringSplitOptions.RemoveEmptyEntries).
                Intersect(Keyword.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                , StringComparer.OrdinalIgnoreCase).Count()
                + _question.Text.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Intersect(Keyword.Split(" ", StringSplitOptions.RemoveEmptyEntries), StringComparer.OrdinalIgnoreCase).Count()
              + _question.Tags.Split(" ", StringSplitOptions.RemoveEmptyEntries)
              .Intersect(Keyword.Split(" ", StringSplitOptions.RemoveEmptyEntries), StringComparer.OrdinalIgnoreCase).Count();
        }

        public List<string> i=new List<string>();
        public void Add(IEnumerable<string> images)
        {
           
            TempData["selectedImages"] = images;
        }


        private static readonly List<string> tags = new List<string>();
        public void add_Tag(string  new_tag) 
        {
            tags.Add(new_tag);
        }

        public void remove_Tag(string removed_tag)
        {
            tags.Remove(removed_tag);
        }
        [HttpPost]
        public IActionResult AddNewQuestion(QuestionViewModel questionViewModel, List<IFormFile> photos,int[]categories_Ids)
        {
            int check = (TempData["selectedImages"] == null) ? 0 : ((IEnumerable<string>)TempData["selectedImages"]).Count();
            if (ModelState.IsValid && (check + photos?.Count() <=3) )
            {
                Question question = new Question();
                question.Text= questionViewModel.Text;
                question.Answer = questionViewModel.Answer;
                question.Tags = string.Join(",", tags);
                question.SubmitTime = DateTime.Now; 
                question.AdminId = userManager.GetUserId(HttpContext.User);
                question.TopQuestion = questionViewModel.TopQuestion;
                QuestionRepository.Insert(question);
                if (photos != null)
                {
                    List<string> images = QuestionRepository.UploadFile(photos);
                    foreach (var item in images)
                    {
                        Media media = new Media();
                        media.Path = item;
                        media.Type = MediaTypes.Image.ToString();
                        media.QuestionId = question.ID;
                        mediaRepository.Insert(media);
                    }
                }

                if (TempData["selectedImages"] != null)
                {
                    IEnumerable<string> existing_images = (IEnumerable<string>)TempData["selectedImages"];
                    foreach (var item in existing_images)
                    {
                        Media media = new Media();
                        media.Path = item;
                        media.Type = MediaTypes.Image.ToString();
                        media.QuestionId = question.ID;
                        mediaRepository.Insert(media);
                    }
                }
                if (questionViewModel.Videos_URLs != null)
                {
                    foreach (var item in questionViewModel.Videos_URLs)
                    {
                        Media media = new Media();
                        media.Path = item;
                        media.Type = MediaTypes.Video.ToString();
                        media.QuestionId = question.ID;
                        mediaRepository.Insert(media);
                    }
                }
                foreach (var item in categories_Ids)
                {
                    QuestionPerCategory questionPerCategory = new QuestionPerCategory();
                    questionPerCategory.CategoryId = item;
                    questionPerCategory.QuestionId = question.ID;
                    questionPerCategoryRepository.Insert(questionPerCategory);
                }
                return RedirectToAction("GetAll");
            }
            else
            {
                List<QuestionCategory> categories = QuestionCategoryRepository.GetAll();
                ViewData["Categories"] = categories;
                List<string> images = mediaRepository.GetAll_uploaded_photos();
                ViewData["Images"] = images;
                return View("AddNewQuestion");
            }
        }

        public IActionResult Search_question( SearchViewModel search_viewmodel)
        {
            if (search_viewmodel.SearchKeyword != null)
            {
                var searchresult = QuestionRepository.Search(search_viewmodel.SearchKeyword, search_viewmodel.Selected_categories);
                ViewBag.searchresult = searchresult.ToList();
                TempData["searchResult"] = searchresult.ToList();
            }
            var categories = QuestionCategoryRepository.GetAll();
            ViewBag.Categories = categories;
            return View("GetAll");
        }

        public IActionResult Searchquestions(string SearchKeyword)
       {
            if (SearchKeyword != null)
            {
                var searchresult = QuestionRepository.Search(SearchKeyword, null);
                ViewBag.searchresult = searchresult.ToList();
                TempData["searchResult"] = searchresult.ToList();
            }
            var categories = QuestionCategoryRepository.GetAll();
            ViewBag.Categories = categories;
            TempData["searchKeyWord"] = SearchKeyword;
            return View("GetAll");
        }

        public IActionResult Details(int QuestionId)
        {
            Question question = QuestionRepository.GetById(QuestionId);
            return PartialView("Details", question);
        }

        [HttpGet]
        public IActionResult AnswerQuestion(int questionId)
        {
            CustomerQuestions question = Customer_QuestionRepository.GetById(questionId);
              List<string> images = mediaRepository.GetAll_uploaded_photos();
            ViewData["Images"] = images;
            Customer_QuestionRepository.change_IsRead_Property(question.ID);

            return View( question);
        }
        [HttpGet]
        public void savecomment(string comment)
        {
            TempData["comment"] = comment;   
        }
        [HttpPost]
        public IActionResult AnswerQuestion(CustomerQuestions question,List<IFormFile>photos, string videoURL)
        {
            if (ModelState.IsValid)
            {
                question.AdminId= userManager.GetUserId(HttpContext.User);
                if (photos != null)
                {
                    List<string> images = Customer_QuestionRepository.UploadImagesByAdmin(photos);
                    foreach (var item in images)
                    {
                        CustomerQuestionMedia media = new CustomerQuestionMedia();
                        media.Path = item;
                        media.Type = MediaTypes.Image.ToString();
                        media.QuestionId = question.ID;
                        media.UserId= userManager.GetUserId(HttpContext.User);
                        CustomerQuestionMedia.Insert(media);
                    }
                }

                if (TempData["selectedImages"] != null)
                {
                    IEnumerable<string> existing_images = (IEnumerable<string>)TempData["selectedImages"];
                    foreach (var item in existing_images)
                    {
                        CustomerQuestionMedia media = new CustomerQuestionMedia();
                        media.Path = item;
                        media.Type = MediaTypes.Image.ToString();
                        media.QuestionId = question.ID;
                        media.UserId = userManager.GetUserId(HttpContext.User);
                        CustomerQuestionMedia.Insert(media);
                    }
                }

                if (videoURL != null)
                {
                        CustomerQuestionMedia media = new CustomerQuestionMedia();
                        media.Path = videoURL;
                        media.Type = MediaTypes.Video.ToString();
                        media.QuestionId = question.ID;
                        CustomerQuestionMedia.Insert(media);
                        
                }
                Customer_QuestionRepository.AnswerQuestion(question.ID, question);
                return RedirectToAction("HomePage", "Home");
            }
            return View("AnswerQuestion", question);
        }
        public IActionResult RejectQuestion(int questionId)
        {
            string comment="";
            if (TempData["comment"] !=null)
            {
                comment = TempData["comment"].ToString();
            }
           
            int x = Customer_QuestionRepository.RejectQuestion(questionId, comment);
            return RedirectToAction("HomePage","Home");
        }
        public IActionResult GetNotificationList(string listName)
        {
            
            List<CustomerQuestions> notifications = new List<CustomerQuestions>();
          if (listName=="Notification")
            {
                notifications = Customer_QuestionRepository.NotificationList();
      
            }
          
          else
            {

                notifications = Customer_QuestionRepository.GetAllPendingQuestions();

            }
            return View(notifications);
        }

        public ActionResult GetNotifications()
        {
            List<CustomerQuestions> notifications= Customer_QuestionRepository.NotificationList();
            
            return Json(notifications);
           


        }



    }
}

