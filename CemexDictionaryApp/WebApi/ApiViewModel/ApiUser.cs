using CemexDictionaryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.WebApi.ApiModels
{
    public class ApiUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public string Zone { get; set; }
        public string Address { get; set; }
        public string Mobileno { get; set; }
        public string Occupation { get; set; }
        public string Password { get; set; }
    }

       public class ApiUserMapping
    {
        public static ApiUser Mapping(ApplicationUser user)
        {
            if (user != null)
            {
                ApiUser _mappedUser = new()
                {
                    Id = user.Id,
                    Address = user.Address,
                    Email = user.Email,
                    Mobileno = user.PhoneNumber,
                    Occupation = user.Occupation,
                    State = user.State,
                    Username = user.Name,
                    Zone = user.Zone
                };
                return _mappedUser;
            }
            return null;
        }
    }
}
