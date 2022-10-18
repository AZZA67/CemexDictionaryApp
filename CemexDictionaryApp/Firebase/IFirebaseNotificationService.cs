using FirebaseAdmin.Messaging;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Firebase
{
    public interface IFirebaseNotificationService
    {
        public Message CreateNotification(string title, string notificationBody, string token);
        Task SendNotification(string token, string title, string body);
    }
}
