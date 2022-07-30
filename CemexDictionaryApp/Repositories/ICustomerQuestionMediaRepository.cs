using CemexDictionaryApp.Models;
using System.Collections.Generic;

namespace CemexDictionaryApp.Repositories
{
    public interface ICustomerQuestionMediaRepository
    {
        List<CustomerQuestionMedia> GetAllMediaByQuestionId(int _questionId);
        List<CustomerQuestionMedia> GetAll_uploaded_photos();
        int Insert(CustomerQuestionMedia media);
    }
}