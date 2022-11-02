using CemexDictionaryApp.Core;
using CemexDictionaryApp.Models;
using CemexDictionaryApp.Repositories;
using CemexDictionaryApp.WebApi.ApiModels;
using CemexDictionaryApp.WebApi.ApiViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CemexDictionaryApp.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INewsRepository NewsRepository;
        private readonly IProductRepository ProductRepository;
        private readonly INotificationRepo NotificationRepo;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly IQuestionRepository QuestionRepository;
        ICustomerQuistionsRepository CustomerQuestion;
        public NotificationController(INewsRepository newsRepository , INotificationRepo notificationRepo ,
        UserManager<ApplicationUser> userManager , IProductRepository productRepository , 
        IQuestionRepository questionRepository, ICustomerQuistionsRepository customerQuestion)
        {
            NewsRepository = newsRepository;
            NotificationRepo = notificationRepo;
            UserManager = userManager;
            ProductRepository = productRepository;
            QuestionRepository = questionRepository;
            CustomerQuestion = customerQuestion;
        }

        [HttpPost]
        [Route("NotificationList")]
        public async Task<IActionResult> Notification([FromBody] ApiUser model)
        {
            if (model != null)
            {
                ApplicationUser _user = await UserManager.FindByIdAsync(model.Id);
                if (_user != null)
                {
                    return Ok(new { Flag = true, Message = Messages.Done, Notification = ApiNotificationMapping.Mapping(NotificationRepo.NotificationList(model.Id))});
                }
                return BadRequest(new { Flag = false, Message = Messages.UserNotExist, Data = 0 });
            }
            return BadRequest(new { Flag = false, Message = Messages.EmptyObject, Data = 0 });
        }

        [HttpPost]
        [Route("NotificationDetails")]
        public IActionResult NotificationDetails([FromBody] ApiNotification model)
        {
            if (model != null)
            {
                if (!string.IsNullOrEmpty(model.Type) && !string.IsNullOrEmpty(model.ObjectId))
                {
                    var _result = (dynamic)null;

                    if (model.Type == NotificationType.News)
                        _result = ApiNewsMapping.MappingByObject(NewsRepository.GetById(Convert.ToInt32(model.ObjectId)));

                    if (model.Type == NotificationType.Products)
                        _result = ApiProductMapping.MappingByObject(ProductRepository.GetById(Convert.ToInt32(model.ObjectId)));

                    if (model.Type == NotificationType.SystemQuestion)
                        _result = ApiQuestionMapping.MappingByObject(QuestionRepository.GetById(Convert.ToInt32(model.ObjectId)));

                    if (model.Type == NotificationType.CustomerQuestion)
                        _result = ApiCustomerQuestionMapping.MappingByObject(CustomerQuestion.GetById(Convert.ToInt32(model.ObjectId)));

                    return Ok(new { Flag = true, Message = Messages.Done, Data = _result});  // anamous object 
                }
                return BadRequest(new { Flag = false, Message = Messages.UserNotExist, Data = 0 });
            }
            return BadRequest(new { Flag = false, Message = Messages.EmptyObject, Data = 0 });
        }
    }
}
