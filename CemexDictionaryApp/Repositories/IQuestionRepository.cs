using CemexDictionaryApp.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CemexDictionaryApp.Repositories
{
    public interface IQuestionRepository
    {
        List<Question> GetAll();
        Question GetById(int QuestionId);
        int Insert(Question question);
        List<string> UploadFile(List<IFormFile> FormFile);
    }
}