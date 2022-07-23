using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CemexDictionaryApp.Models;
using CemexDictionaryApp.Repositories;

namespace CemexDictionaryApp.Controllers
{
    public class QuestionCategoryController : Controller
    {
        IQuestionCategoryRepository QuestionCategoryRepository;
      public  QuestionCategoryController(IQuestionCategoryRepository _questionCategoryRepository)
        {
            QuestionCategoryRepository = _questionCategoryRepository;
        }
        public IActionResult GetAll()
        {
            List<QuestionCategory> questionCategories = QuestionCategoryRepository.GetAll();
            return View(questionCategories);
        }
        [HttpGet]
        public IActionResult AddNewQuestionCategory()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult AddNewQuestionCategory(QuestionCategory questionCtegory)
        {

            if(ModelState.IsValid)
            {
                QuestionCategory _questionCategory = new QuestionCategory();
                _questionCategory.Name = questionCtegory.Name;
                _questionCategory.الأسم = questionCtegory.الأسم;
                QuestionCategoryRepository.Insert(_questionCategory);
                TempData["Success"] = "true";
                return RedirectToAction("GetAll");
            }
            return View("AddNewQuestionCategory");
        }

    }
}
