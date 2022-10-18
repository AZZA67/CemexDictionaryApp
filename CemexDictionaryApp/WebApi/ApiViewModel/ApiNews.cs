using CemexDictionaryApp.Models;
using System.Collections.Generic;

namespace CemexDictionaryApp.WebApi.ApiViewModel
{
    public class ApiNews
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; } // URL
        public string Status { get; set; } // Active  1 
    }


    public class ApiNewsMapping
    {
        public static List<ApiNews> Mapping(List<News> news)
        {
            if (news != null)
            {
                List<ApiNews> _news = new();
                foreach (var item in news)
                {
                    ApiNews n = new()
                    {
                        Id = item.Id,
                        Title = item.Title,
                        Description = item.Description,
                        Image = string.Format(ServerConfig.ServerPath+ "/images/News/" + item.Image),
                        Status = item.Status
                    };
                    _news.Add(n);
                }
                return _news;
            }
            return null;
        }
    }
}
