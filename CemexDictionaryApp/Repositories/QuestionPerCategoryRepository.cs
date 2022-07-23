using CemexDictionaryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Repositories
{
    public class QuestionPerCategoryRepository : IQuestionPerCategoryRepository
    {
        DBContext context;


        public QuestionPerCategoryRepository(DBContext _context)
        {
            context = _context;
        }

        public int Insert(QuestionPerCategory _questionPerCategory)
        {
            context.questionPerCategories.Add(_questionPerCategory);
            return context.SaveChanges();
        }


    }
}
