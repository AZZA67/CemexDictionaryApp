using CemexDictionaryApp.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CemexDictionaryApp.Repositories
{
    public interface IQuestionRepository
    {
        List<Question> GetAll();
        List<QuestionPerCategory> GetAllByCategoryId(int _categoryId);
        Question GetById(int QuestionId);
        List<Question> GetTopTenQuestions();
        int Insert(Question question);
        int OrderResult(Question q, string Keyword);
        IEnumerable<Question> Search(string Keyword, int[] categories);
        List<string> UploadFile(List<IFormFile> FormFile);
        public int[] GetCategoriesId(List<string> categories);
    }
}