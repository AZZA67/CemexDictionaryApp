using CemexDictionaryApp.Core;
using CemexDictionaryApp.Repositories;
using CemexDictionaryApp.WebApi.ApiModels;
using CemexDictionaryApp.WebApi.ApiViewModel;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Firebase
{
    public class FirebaseNotificationService : IFirebaseNotificationService
    {
        private readonly FirebaseMessaging Messaging;
        public FirebaseNotificationService()
        {
            var app = FirebaseApp.DefaultInstance;
            if (app == null)
            {
                app = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("cemex-dictionary-firebase-adminsdk-uhil7-82af4c0e13.json").
                    CreateScoped("https://www.googleapis.com/auth/firebase.messaging")
                });
            }
            Messaging = FirebaseMessaging.GetMessaging(app);
        }

        public Message CreateNotification(string title, string notificationBody, string token)
        {
            var message = new Message()
            { 
                 
                Token = token,
                Notification = new Notification()
                {
                    Body = notificationBody,
                    Title = title,
                }
            };
            return message;
        }


        public Message UserNotification(Models.Notification notify)
        {
            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = notify.Title,
                    Body = notify.Message
                },
                Token = notify.Token,
                Data = new Dictionary<string, string>()
                 {
                     { "ObjectId", notify.ObjectId },
                     { "Type", notify.Type },
                     { "UserId", notify.UserId },
                 },
            };
            return message;
        }
        //public async Task SendNotification(string token, string title, string body)
        //{
        //    _ = await Messaging.SendAsync(CreateNotification(title, body, token));
        //}

        public async Task SendNotification(Message message)
        {
            _ = await Messaging.SendAsync(message);
        }

        public Message Topic(Models.Notification notify)
        {
            var topic = "all";
            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = notify.Title,
                    Body =notify.Message
                },
                Topic = topic,
                Data = new Dictionary<string, string>()
                 {
                     { "ObjectId", notify.ObjectId },
                     { "Type", notify.Type },
                     { "UserId", notify.UserId },
                 },
            };
            return message;
        }
    }
}
