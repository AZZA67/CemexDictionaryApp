using CemexDictionaryApp.Models;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Repositories
{
    public class NotificationRepo : INotificationRepo
    {
        DBContext Context;
        private readonly IWebHostEnvironment HostEnvironment;

        public NotificationRepo(DBContext context, IWebHostEnvironment hostEnvironment)
        {
            Context = context;
            HostEnvironment = hostEnvironment;
        }

        public Task<int> Add(Notification notification)
        {
            Context.Notifications.Add(notification);
            return Context.SaveChangesAsync();
        }

        public List<Notification> NotificationList(string userId)
        {
            if(!string.IsNullOrEmpty(userId))
            {
                return Context.Notifications.Where(n => n.UserId.Contains(userId) || n.UserId.Contains("All")).OrderByDescending(n => n.Id).Take(10).ToList();
            }
            return null;
        }
    }
}
