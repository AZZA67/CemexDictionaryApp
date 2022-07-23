using CemexDictionaryApp.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Repositories
{
    public class QuestionCategoryRepository : IQuestionCategoryRepository
    {


        DBContext context;
        private readonly IWebHostEnvironment hostEnvironment;
        public QuestionCategoryRepository(DBContext _context, IWebHostEnvironment _hostEnvironment)
        {
            context = _context;
            hostEnvironment = _hostEnvironment;
        }
        public List<QuestionCategory> GetAll()
        {
            List<QuestionCategory> QuestionCategories = context.QuestionCategories.ToList();
            return QuestionCategories;
        }
        public int Insert(QuestionCategory QuestionCategory)
        {
            context.QuestionCategories.Add(QuestionCategory);
            return context.SaveChanges();
        }
        public int Update(int id, QuestionCategory New_QuestionCategory)
        {
            QuestionCategory Old_QuestionCategory = context.QuestionCategories.FirstOrDefault(Question_Category => Question_Category.Id == id);
            if (Old_QuestionCategory != null)
            {
                Old_QuestionCategory.Name = New_QuestionCategory.Name;
                Old_QuestionCategory.الأسم = New_QuestionCategory.الأسم;
                return context.SaveChanges();
            }
            return 0;
        }
        public int Delete(int id)
        {
            QuestionCategory Old_QuestionCategory = context.QuestionCategories.FirstOrDefault(Question_Category => Question_Category.Id == id);
            context.Remove(Old_QuestionCategory);
            return context.SaveChanges();
        }


    }
}
