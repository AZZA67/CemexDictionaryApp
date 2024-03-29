﻿using CemexDictionaryApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Repositories
{
    public class MediaRepository : IMediaRepository
    {
        DBContext context;
        private readonly IWebHostEnvironment hostEnvironment;
        public MediaRepository(DBContext _context, IWebHostEnvironment _hostEnvironment)
        {
            context = _context;
            hostEnvironment = _hostEnvironment;
        }

        public List<Media> GetAllMediaByQuestionId(int _questionId)
        {
            List<Media> media = context.QuestionMedia.
            Where(media => media.QuestionId == _questionId)
            .ToList();
            return media;
        }
        public int Insert(Media media)
        {
            context.QuestionMedia.Add(media);
            return context.SaveChanges();
        }
        public List<string> GetAll_uploaded_photos()
        {
            List<string> images = context.QuestionMedia.
                Where(media => media.Type ==
                MediaTypes.Image.ToString()).
                Select(m=>m.Path)
                .Distinct()
                .ToList();
            return images;
        }

    }
}
