using CemexDictionaryApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using static System.Net.WebRequestMethods;
//using static System.Net.Mime.MediaTypeNames;

namespace CemexDictionaryApp.Repositories
{
    public class CustomerQuistionsRepository : ICustomerQuistionsRepository
    {
        DBContext context;
        private readonly IWebHostEnvironment hostEnvironment;
        public CustomerQuistionsRepository(DBContext _context, IWebHostEnvironment _hostEnvironment)
        {
            context = _context;
            hostEnvironment = _hostEnvironment;
        }
        public List<CustomerQuestions> GetAll()
        {
            List<CustomerQuestions> Questions = context.customer_Questions.
                Include(question => question.QuestionMedia).
                Include(question=>question.Category).
                 Include(question => question.User).
                ToList();
                return Questions;
        }
        public List<CustomerQuestions> GetAllByCategoryId(int _categoryId)
        {
            List<CustomerQuestions> Questions = context.customer_Questions
                .Include(question => question.QuestionMedia).
                 Include(question => question.Category).
                 Include(question => question.User).
               Where(question => question.CategoryId == _categoryId).ToList();
            return Questions;
        }

        public List<string> UploadFile(List<string> base64Images)
        {
            List<string> images = new List<string>();
            //long size = FormFile.Sum(f => f.Length);
            int count = 0;
            foreach (var base64image in base64Images)
            {
                if (base64image != null)
                {
                    string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath);
                    string imageName = Guid.NewGuid().ToString() + ".jpg";
                    images.Add(imageName);
                    string path = Path.Combine(uploadsFolder + @"\images\CustomerQuestions\", images[count]);
                    count++;
                    byte[] imageBytes = Convert.FromBase64String(base64image);
                    using (var img = Image.Load(imageBytes))
                    {
                        img.Save(path);
                    }
                }
            }
            return images;
        }
        public List<string> UploadImagesByAdmin(List<IFormFile> FormFile)
        {
            List<string> images = new List<string>();
            long size = FormFile.Sum(f => f.Length);
            int count = 0;
            foreach (var formFile in FormFile)
            {
                if (formFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath);
                    images.Add(Guid.NewGuid().ToString() + "_" + formFile.FileName);
                    string path = Path.Combine(uploadsFolder + @"\images\CustomerQuestions\CustomerQuestions_Answers", images[count]);
                    count++;
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        formFile.CopyTo(fileStream);
                    }
                }
            }
            return images;
        }
        public int Insert(CustomerQuestions question)
        {
            context.customer_Questions.Add(question);
            return context.SaveChanges();
        }

        public CustomerQuestions GetById(int QuestionId)
        {
            CustomerQuestions question = context.customer_Questions.
                
                Include(q => q.Category).
                 Include(question => question.User).
                 Include(question => question.QuestionMedia).
                 ThenInclude(questionmedia=>questionmedia.User).
                FirstOrDefault(question => question.ID == QuestionId);
            return question;
        }
        public int AnswerQuestion(int QuestionId, CustomerQuestions questionWithAnswer)
        {
            CustomerQuestions question = context.customer_Questions.
                Include(question => question.QuestionMedia).
                 Include(question => question.User).
            FirstOrDefault(question => question.ID == QuestionId);
            question.Answer = questionWithAnswer.Answer;
            question.Status = Question_Status.Answered.ToString();
            question.IsRead = true;
            question.AdminId = questionWithAnswer.AdminId;
            context.customer_Questions.Update(question);
            context.SaveChanges();

            return question.ID;
        }
        public int RejectQuestion(int QuestionId, string comment)
        {
            CustomerQuestions question = context.customer_Questions.
                Include(question => question.QuestionMedia).
                 Include(question => question.User).
            FirstOrDefault(question => question.ID == QuestionId);

            if (comment != null)
            {
                question.Comment = comment;
            }

            question.Status = Question_Status.Rejected.ToString();
            context.customer_Questions.Update(question);
            context.SaveChanges();
            return question.ID;
        }



        public List<CustomerQuestions> GetAllPendingQuestions()
        {
            List<CustomerQuestions> Pending_questions = context.customer_Questions.
                Include(question => question.QuestionMedia).
                 Include(question => question.User).
           Where(question => question.Status == "Pending" && question.IsRead == true).ToList();
            return Pending_questions;
        }

        public void change_IsRead_Property(int questionId)
        {
            CustomerQuestions question = context.customer_Questions.
                Include(question => question.QuestionMedia).
                 Include(question => question.User).
            FirstOrDefault(question => question.ID == questionId);
            question.IsRead = true;
            context.customer_Questions.Update(question);
            context.SaveChanges();
        }

        public List<CustomerQuestions> NotificationList()
        {
            List<CustomerQuestions> Pending_questions = context.customer_Questions.
                Include(question => question.QuestionMedia).
                Include(question=>question.User).
           Where(question => question.Status == "Pending" && question.IsRead == false).ToList();
         
                Pending_questions.Reverse();
            return Pending_questions;
        }


    }
}
