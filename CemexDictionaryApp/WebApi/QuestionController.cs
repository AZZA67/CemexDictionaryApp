using CemexDictionaryApp.Core;
using CemexDictionaryApp.DTO;
using CemexDictionaryApp.Hubs;
using CemexDictionaryApp.Models;
using CemexDictionaryApp.Repositories;
using CemexDictionaryApp.ViewModels;
using CemexDictionaryApp.WebApi.ApiModels;
using CemexDictionaryApp.WebApi.ApiViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private UserManager<ApplicationUser> UserManager;
        IQuestionRepository QuestionRepository;
        IQuestionCategoryRepository QuestionCategory;
        ICustomerQuestionMediaRepository CustomerMedia;
        ICustomerQuistionsRepository CustomerQuestion;
        IHubContext<NotificationHub> HubContext;

        public QuestionController(UserManager<ApplicationUser> userManager, IQuestionRepository questionRepository,
            IQuestionCategoryRepository questionCategory, ICustomerQuestionMediaRepository customerMedia,
            ICustomerQuistionsRepository customerQuestion, IHubContext<NotificationHub> hubContext)
        {
            UserManager = userManager;
            HubContext = hubContext;
            CustomerQuestion = customerQuestion;
            CustomerMedia = customerMedia;
            QuestionRepository = questionRepository;
            QuestionCategory = questionCategory;
        }

        [HttpPost("Search")]
        public IActionResult Search(SearchViewModel searchModel)
        {
            if (searchModel.SearchKeyword != null)
            {
                if(searchModel.SearchCategories != null && searchModel.SearchCategories.Count>0)
                    searchModel.SelectedCategories = QuestionRepository.GetCategoriesId(searchModel.SearchCategories);
                
                var _searchResult = QuestionRepository.Search(searchModel.SearchKeyword,searchModel.SelectedCategories);
                return Ok(new
                {
                    Flag = true,
                    Message = "Done",
                    questions = ApiQuestionMapping.Mapping(_searchResult.ToList())
                });
            }
            return BadRequest(new { Flag = false, Message = Messages.EmptySearchText, Data = 0 });
        }

        [HttpPost("PostQuestion")]
        public async Task<IActionResult> AddQuestion([FromForm] QuestionModel model)
        {
            if (HttpContext.Request.Form["QuestionText"].ToString() != null && HttpContext.Request.Form["Category"] != 0)
            {
                List<string> categories = new();
                categories.Add(HttpContext.Request.Form["Category"].ToString());
                int[] CategoryId = QuestionRepository.GetCategoriesId(categories);

                ApplicationUser _user = await UserManager.FindByIdAsync(HttpContext.Request.Form["UserId"].ToString());
                if (_user != null)
                {
                    CustomerQuestions _customerQuestion = new()
                    {
                        Text = HttpContext.Request.Form["QuestionText"].ToString(),
                        CategoryId = CategoryId[0],
                        Description = HttpContext.Request.Form["Description"].ToString(),
                        Status = Question_Status.Pending.ToString(),
                        SubmitTime = DateTime.Now,
                        UserId = HttpContext.Request.Form["UserId"].ToString(),
                        User = _user
                    };
                    CustomerQuestion.Insert(_customerQuestion);

                    //Images
                    if (HttpContext.Request.Form.Files != null)
                    {
                        List<string> Images = CustomerQuestion.UploadImagesByUser((List<IFormFile>)HttpContext.Request.Form.Files);
                        foreach (var item in Images)
                        {
                            CustomerQuestionMedia media = new();
                            media.Path = item;
                            media.Type = MediaTypes.Image.ToString();
                            media.QuestionId = _customerQuestion.ID;
                            media.UserId = HttpContext.Request.Form["UserId"].ToString();
                            CustomerMedia.Insert(media);
                        }
                    }

                    string jsonObject = JsonConvert.SerializeObject(_customerQuestion, new JsonSerializerSettings()
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                        Formatting = Formatting.Indented
                    });

                    await HubContext.Clients.All.SendAsync("ReciveQuestions", jsonObject);
                    return Ok(new { Flag = true, Message = Messages.QuestionPosted, QuestionID = _customerQuestion.ID  });
                }
                return BadRequest(new { Flag = false, Message = Messages.UserNotExist, Data = 0 });
            }
            return BadRequest(new { Flag = false, Message = Messages.EmptyObject, Data = 0 });
        }

        [HttpPost("AddQuestion")]
        public async Task<IActionResult> AddQuestionnAsync(QuestionModel questionModel)
        {
            if (questionModel.Text != null && questionModel.CategoryId != 0)
            {
                ApplicationUser UserModel = await UserManager.FindByIdAsync(questionModel.UserId);
                CustomerQuestions customerQuestion = new();
                customerQuestion.Text = questionModel.Text;
                customerQuestion.CategoryId = questionModel.CategoryId;
                customerQuestion.Status = Question_Status.Pending.ToString();
                customerQuestion.SubmitTime = DateTime.Now;
                customerQuestion.UserId = questionModel.UserId;
                customerQuestion.User = UserModel;
                CustomerQuestion.Insert(customerQuestion);

                if (questionModel.QuestionImage != null)
                {
                    List<string> Images = CustomerQuestion.UploadFile(questionModel.QuestionImage);
                    foreach (var item in Images)
                    {
                        CustomerQuestionMedia media = new CustomerQuestionMedia();
                        media.Path = item;
                        media.Type = MediaTypes.Image.ToString();
                        media.QuestionId = customerQuestion.ID;
                        media.UserId = questionModel.UserId;
                        CustomerMedia.Insert(media);
                    }
                }

                string jsonObject = JsonConvert.SerializeObject(customerQuestion, new JsonSerializerSettings()
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    Formatting = Formatting.Indented
                });

                //   JsonConvert.SerializeObject(question);

                await HubContext.Clients.All.SendAsync("ReciveQuestions", jsonObject);
                return Ok(customerQuestion);
            }
            else
            {
                return BadRequest("please add a valid question ");
            }
        }

        [HttpGet("GetTopTenQuestions")]
        public IActionResult GetTopTenQuestions()
        {
            if (QuestionRepository.GetTopTenQuestions() != null)
            {
                return Ok(new
                {
                    Flag = true,
                    Message = "Done",
                    Data = ApiQuestionMapping.Mapping(QuestionRepository.GetTopTenQuestions())
                });
            }
            return BadRequest(new { Flag = false, Message = "Error,there are no top questions", Data = 0 });
        }

        [HttpPost("CustomerQuestions")]
        public IActionResult GetCustomerQuestionsById(ApiUser user)
        {
            var _customerQuestions = CustomerQuestion.GetAllQuestionsByCustomerId(user.Id);
            if (_customerQuestions != null   && _customerQuestions.Count > 0)
            {
                return Ok(new
                {
                    Flag = true,
                    Message = "Done",
                    Questions = ApiCustomerQuestionMapping.Mapping(_customerQuestions)
                });
            }
            return BadRequest(new { Flag = false, Message = Messages.CustomerEmptyQuestons, Questions = 0 });
        }
    }
}
