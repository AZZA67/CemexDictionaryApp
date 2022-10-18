using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task SendNotification(string token, string title, string body)
        {
            _ = await Messaging.SendAsync(CreateNotification(title, body, token));
        }
    }
}
