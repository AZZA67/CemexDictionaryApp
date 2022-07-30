using CemexDictionaryApp.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Repositories
{
    public class CustomerQuestionMediaRepository : ICustomerQuestionMediaRepository
    {

        DBContext context;
        private readonly IWebHostEnvironment hostEnvironment;

        public CustomerQuestionMediaRepository(DBContext _context, IWebHostEnvironment _hostEnvironment)
        {
            context = _context;
            hostEnvironment = _hostEnvironment;

        }



        public List<CustomerQuestionMedia> GetAllMediaByQuestionId(int _questionId)
        {
            List<CustomerQuestionMedia> media = context.CustomerQuestionMedias.Where(media => media.QuestionId == _questionId)
                .ToList();
            return media;
        }
        public int Insert(CustomerQuestionMedia media)
        {
            context.CustomerQuestionMedias.Add(media);
            return context.SaveChanges();
        }

        public List<CustomerQuestionMedia> GetAll_uploaded_photos()
        {
            List<CustomerQuestionMedia> images = context.CustomerQuestionMedias.Where(media => media.Type == MediaTypes.Image.ToString()).ToList();

            return images;
        }

    }
}
