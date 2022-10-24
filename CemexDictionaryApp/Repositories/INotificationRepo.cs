using CemexDictionaryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Repositories
{
    public interface INotificationRepo
    {
        Task<int> Add(Notification notification);
        List<Notification> NotificationList(string userId);
    }
}
