using CemexDictionaryApp.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CemexDictionaryApp.Repositories
{
    public interface ICustomerQuistionsRepository
    {
        int AnswerQuestion(int QuestionId, CustomerQuestions questionWithAnswer);
        List<CustomerQuestions> GetAll();
        List<CustomerQuestions> GetAllByCategoryId(int _categoryId);
        CustomerQuestions GetById(int QuestionId);
        int Insert(CustomerQuestions question);
        int RejectQuestion(int QuestionId, string comment);
        List<string> UploadFile(List<string> base64Images);
        List<string> UploadImagesByAdmin(List<IFormFile> FormFile);
    }
}