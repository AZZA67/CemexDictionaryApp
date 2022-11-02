using FirebaseAdmin.Messaging;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Firebase
{
    public interface IFirebaseNotificationService
    {
        Message CreateNotification(string title, string notificationBody, string token);
        Message Topic(Models.Notification notify);
        //  Task SendNotification(string token, string title, string body);
        Task SendNotification(Message message);
        Message UserNotification(Models.Notification notify);
    }
}
