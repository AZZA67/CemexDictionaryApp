﻿using CemexDictionaryApp.Models;
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
                Include(q => q.Admin).
                  Include(q => q.Question_category).
                ThenInclude(qc => qc.category).
                ToList();
            return Questions;
        }
        bool FilterExpression(Question _question, string Keyword)
        {
            return _question.Text.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                       .Intersect(Keyword.Split(" ", StringSplitOptions.RemoveEmptyEntries),
                       StringComparer.OrdinalIgnoreCase).Count() > 0 ||
                       _question.Answer.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                      .Intersect(Keyword.Split(" ", StringSplitOptions.RemoveEmptyEntries),
                      StringComparer.OrdinalIgnoreCase).Count() > 0 ||
                      _question.Tags.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Intersect(Keyword.Split(" ", StringSplitOptions.RemoveEmptyEntries),
                    StringComparer.OrdinalIgnoreCase).Count() > 0 ||
                    _question.Text.Contains(Keyword, StringComparison.OrdinalIgnoreCase)
                    || _question.Answer.Contains(Keyword, StringComparison.OrdinalIgnoreCase)
                    || _question.Tags.Contains(Keyword, StringComparison.OrdinalIgnoreCase);

        }
        public int OrderResult(Question q, string Keyword)
        {
            return q.Text.Split(" ", StringSplitOptions.RemoveEmptyEntries).
                Intersect(Keyword.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                , StringComparer.OrdinalIgnoreCase).Count()
                + q.Answer.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Intersect(Keyword.Split(" ", StringSplitOptions.RemoveEmptyEntries), StringComparer.OrdinalIgnoreCase).Count()
              + q.Tags.Split(" ", StringSplitOptions.RemoveEmptyEntries)
              .Intersect(Keyword.Split(" ", StringSplitOptions.RemoveEmptyEntries), StringComparer.OrdinalIgnoreCase).Count();
        }

        public int[] GetCategoriesId(List<string> categories)
        {
            if (categories != null && categories.Count > 0)
            {
                int[] _categoryId = new int[categories.Count];
                for (int i = 0; i < categories.Count; i++)
                {
                    var _id = context.QuestionCategories.Where(t => t.Name_Ar.Contains(categories[i])).FirstOrDefault();
                    if(_id != null)
                        _categoryId[i] = _id.Id;
                }
                return _categoryId;
            }
            return null;
        }

        public IEnumerable<Question> Search(string Keyword, int[] categories)
        {
            if (categories != null)
            {
                string[] keywords = Keyword.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                var QuestionsWithCategories = context.Questions
                    .Where(q => q.Question_category.
                    Any(c => categories.Contains(c.CategoryId))).Include(q => q.QuestionMedia).
                    Include(q => q.Admin).
                     Include(q => q.Question_category).
                ThenInclude(qc => qc.category);
                var FilteredQuestionwithcategories = QuestionsWithCategories.ToList()
                    .Where(q => FilterExpression(q, Keyword)).OrderByDescending(q => OrderResult(q, Keyword));
                if (FilteredQuestionwithcategories.Count() > 0)
                {
                    return FilteredQuestionwithcategories;
                }
                return QuestionsWithCategories;
            }
            else
            {

                var result = context.Questions
                       .Include(q => q.QuestionMedia).
                    Include(q => q.Admin).
                     Include(q => q.Question_category).
                ThenInclude(qc => qc.category).ToList().
                    Where(q => FilterExpression(q, Keyword))
                    .OrderByDescending(q => OrderResult(q, Keyword));
                return result;

            }
        }

        public List<QuestionPerCategory> GetAllByCategoryId(int _categoryId)
        {
            List<QuestionPerCategory> Questions = context.questionPerCategories
                .Where(questionpercategory => questionpercategory.CategoryId == _categoryId)
                .Include(questionpercategory => questionpercategory.question).
                ThenInclude(question => question.QuestionMedia).ToList();
            return Questions;
        }

        public List<string> UploadFile(List<IFormFile> FormFile)
        {
            List<string> images = new();
            long size = FormFile.Sum(f => f.Length);
            int count = 0;
            foreach (var formFile in FormFile)
            {
                if (formFile.Length > 0)
                {
                  //  string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath);
                    images.Add(Guid.NewGuid().ToString() + "_" + formFile.FileName);
                    string path = Path.Combine(ServerConfig.ImagePath + @"\images\Questions\", images[count]);
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
                 Include(q => q.Admin).
                Include(q => q.Question_category).
                ThenInclude(qc => qc.category).
                FirstOrDefault(question => question.ID == QuestionId);
            return question;
        }

        public List<Question> GetTopTenQuestions()
        {
            List<Question> Questions = context.Questions.
                Where(question => question.TopQuestion == true).
                Include(question => question.QuestionMedia).
                Include(q => q.Admin).
                  Include(q => q.Question_category).
                ThenInclude(qc => qc.category).ToList()
                .AsEnumerable()
              .TakeLast(10).ToList();
            return Questions;
        }

        public List<Question> GetMostNewestQuestions()
        {
            List<Question> Questions = context.Questions.
         OrderBy(question => question.SubmitTime).
                Include(question => question.QuestionMedia).
                Include(q => q.Admin).
                  Include(q => q.Question_category).
                ThenInclude(qc => qc.category)
              .ToList()
                .AsEnumerable()
              .TakeLast(5).Reverse().ToList();

            return Questions;
        }

    }
}
