using CemexDictionaryApp.Models;
using System.Collections.Generic;

namespace CemexDictionaryApp.Repositories
{
    public interface IQuestionCategoryRepository
    {
        int Delete(int id);
        List<QuestionCategory> GetAll();
        int Insert(QuestionCategory QuestionCategory);
        int Update(int id, QuestionCategory New_QuestionCategory);
        string GetCategoryNameEnById(int Categoryid);
    }
}