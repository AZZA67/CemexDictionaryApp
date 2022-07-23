using CemexDictionaryApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        DBContext context;
        private readonly IWebHostEnvironment hostEnvironment;

        public QuestionRepository(DBContext _context, IWebHostEnvironment _hostEnvironment)
        {
            context = _context;
            hostEnvironment = _hostEnvironment;

        }

        public List<Question> GetAll()
        {
            List<Question> Questions = context.Questions.Include(question => question.QuestionMedia).
                ToList();
            return Questions;
        }

     



        public List<Question> GetAllByCategoryId(int _categoryId)
        {
            List<Question> Questions = context.Questions.
                Include(Question => Question.Question_category).
                Where(Question_category => Question_category.ID == _categoryId)
                .Include(Question => Question.Question_category).
                Include(question => question.QuestionMedia)
                .ToList();
            return Questions;
        }
        public List<string> UploadFile(List<IFormFile> FormFile)
        {
            List<string> images = new List<string>();
            long size = FormFile.Sum(f => f.Length);
            int count = 0;
            foreach (var formFile in FormFile)
            {
                if (formFile.Length > 0)
                {

                    string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath);
                    images.Add( Guid.NewGuid().ToString() + "_" + formFile.FileName);
                    string path = Path.Combine(uploadsFolder + @"\images\Questions\", images[count]);
                    count++;
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        formFile.CopyTo(fileStream);
                    }
                }
            }

            return images;

        }
        public int Insert(Question question)
        {
            context.Questions.Add(question);
            return context.SaveChanges();
        }    
        public Question GetById(int QuestionId)
        {
            Question question = context.Questions.Include(question => question.QuestionMedia).
                FirstOrDefault(question => question.ID == QuestionId);
            return question;
        }
    }
}
