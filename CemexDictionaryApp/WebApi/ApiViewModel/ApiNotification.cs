using CemexDictionaryApp.Models;
using CemexDictionaryApp.WebApi.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.WebApi.ApiViewModel
{
    public class ApiNotification
    {
        //public int Id { get; set; }
        public string UserId { get; set; }
        public string ObjectId { get; set; } // id
        public string Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string SubmitDate { get; set; }
        //public ApiNews News { get; set; }
        //public ApiProduct Product { get; set; }
        //public ApiQuestion Question { get; set; }
        //public ApiCustomerQuestion CustomerQuestion { get; set; }
    }


    public class ApiNotificationMapping
    {
        public static List<ApiNotification> Mapping(List<Notification> notifications)
        {
            if (notifications != null)
            {
                List<ApiNotification> _apiNotifcations = new();
                foreach (var item in notifications)
                {
                    ApiNotification _model = new()
                    {
                        ObjectId = item.ObjectId,
                        Message = item.Message,
                        Title = item.Title,
                        Type = item.Type,
                        SubmitDate = item.SubmitDate
                    };
                    _apiNotifcations.Add(_model);
                }
                return _apiNotifcations;
            }
            return null;
        }
    }
}
