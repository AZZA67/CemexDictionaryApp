using CemexDictionaryApp.Models;
using CemexDictionaryApp.Repositories;
using CemexDictionaryApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public QuestionController(IQuestionPerCategoryRepository _questionPerCategoryRepository,IMediaRepository _mediaRepository, UserManager<ApplicationUser> _userManager,IQuestionCategoryRepository _questionCategoryRepository, IQuestionRepository _questionRepository)
        {
            QuestionCategoryRepository = _questionCategoryRepository;
            QuestionRepository = _questionRepository;
            userManager = _userManager;
            mediaRepository = _mediaRepository;
            questionPerCategoryRepository = _questionPerCategoryRepository;
        }

      /*  private async Task<ApplicationUser> GetCurrentUserAsync() => await userManager.GetUserAsync(HttpContext.User);*/
        public IActionResult GetAll()
        {
            List<QuestionCategory> categories = QuestionCategoryRepository.GetAll();
            ViewData["Categories"] = categories;
            //List<Question> Questions = QuestionRepository.GetAll();
            return View();
        }


        [HttpGet]
        public IActionResult AddNewQuestion()
        {
            List<QuestionCategory> categories = QuestionCategoryRepository.GetAll();
            //ViewBag.Categories = new MultiSelectList(categories, "Id", "Name");
            List<Media> images = mediaRepository.GetAll_uploaded_photos();
          
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
        public void Add(IEnumerable<string>images)
        {
            //List<string> images = new List<string>();
            //images.Add(imagepath);
            TempData["selectedImages"] = images;
            }
            [HttpPost]
        public IActionResult AddNewQuestion(QuestionViewModel questionViewModel, List<IFormFile> photos,int[]categories_Ids)
        {
            if (ModelState.IsValid && ((photos.Count<=3 && TempData["selectedImages"]==null) ||
                (((IEnumerable<string>)TempData["selectedImages"]).Count()<=3 && photos.Count ==0)))
            {
                Question question = new Question();
                question.Text= questionViewModel.Text;
                question.Answer = questionViewModel.Answer;
                question.Tags = questionViewModel.Tags;
                question.SubmitTime = DateTime.Now; //askkk
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
                List<Media> images = mediaRepository.GetAll_uploaded_photos();
                ViewData["Images"] = images;
                return View("AddNewQuestion");
            }
        }


        public IActionResult Search( SearchViewModel search_viewmodel)
        {
            if (search_viewmodel.SearchKeyword != null)
            {
                var searchresult = QuestionRepository.Search(search_viewmodel.SearchKeyword, search_viewmodel.Selected_categories);
                ViewBag.searchresult = searchresult.ToList();
            }
            var categories = QuestionCategoryRepository.GetAll();
            ViewBag.cat = categories;
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in categories)
            {
                items.Add(new SelectListItem() { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.categories = items;

            return View("SearchResult");
        }
        public IActionResult Searchquestions(string SearchKeyword)
        {
            if (SearchKeyword != null)
            {
                var searchresult = QuestionRepository.Search(SearchKeyword, null);
                ViewBag.searchresult = searchresult.ToList();
            }
            var categories = QuestionCategoryRepository.GetAll();
            ViewBag.cat = categories;
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in categories)
            {
                items.Add(new SelectListItem() { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.cats = items;
            return View("SearchResult");
        }

    }
}
