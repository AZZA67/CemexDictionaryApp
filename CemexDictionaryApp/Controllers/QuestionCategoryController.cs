using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CemexDictionaryApp.Models;
using CemexDictionaryApp.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace CemexDictionaryApp.Controllers
{
    public class QuestionCategoryController : Controller
    {
        IQuestionCategoryRepository QuestionCategoryRepository;
      public  QuestionCategoryController(IQuestionCategoryRepository _questionCategoryRepository)
        {
            QuestionCategoryRepository = _questionCategoryRepository;
        }
        [Authorize]
        public IActionResult GetAll()
        {
            List<QuestionCategory> questionCategories = QuestionCategoryRepository.GetAll();
            return View(questionCategories);
        }
        [Authorize]
        [HttpGet]
        public IActionResult AddNewQuestionCategory()
        {
            return PartialView();
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddNewQuestionCategory(QuestionCategory questionCtegory)
        {

            if(ModelState.IsValid)
            {
                QuestionCategory _questionCategory = new QuestionCategory();
                _questionCategory.Name_En = questionCtegory.Name_En;
                _questionCategory.Name_Ar = questionCtegory.Name_Ar;
                QuestionCategoryRepository.Insert(_questionCategory);
                TempData["Success"] = "true";
                return RedirectToAction("GetAll");
            }
            return View("AddNewQuestionCategory");
        }

    }
}
