using CemexDictionaryApp.Models;
using System.Collections.Generic;

namespace CemexDictionaryApp.Repositories
{
    public interface IMediaRepository
    {
        List<Media> GetAllMediaByQuestionId(int _questionId);
        List<string> GetAll_uploaded_photos();
        int Insert(Media media);
    }
}