using CemexDictionaryApp.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CemexDictionaryApp.Repositories
{
    public interface ICustomerQuistionsRepository
    {
        int AnswerQuestion(int QuestionId, CustomerQuestions questionWithAnswer);
        void change_IsRead_Property(int questionId);
        List<CustomerQuestions> GetAll();
        List<CustomerQuestions> GetAllByCategoryId(int _categoryId);
        List<CustomerQuestions> GetAllPendingQuestions();
        CustomerQuestions GetById(int QuestionId);
        int Insert(CustomerQuestions question);
        List<CustomerQuestions> NotificationList();
        int RejectQuestion(int QuestionId, string comment);
        List<string> UploadFile(List<string> base64Images);
        List<string> UploadImagesByAdmin(List<IFormFile> FormFile);
        Dictionary<string, int> QuestionStatusPerCustomer(string userId);
        List<CustomerQuestions> GetAllQuestionsByCustomerId(string CustomerId);
        List<string> UploadImagesByUser(List<IFormFile> FormFile);
    }
}