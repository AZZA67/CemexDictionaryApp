
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
        private UserManager<ApplicationUser> userManager;
        IQuestionRepository Question_Repository;
        IQuestionCategoryRepository Question_Category;
        ICustomerQuestionMediaRepository Customer_Media;
        ICustomerQuistionsRepository customer_Question;
        IHubContext<NotificationHub> hubContext;

        public QuestionController(UserManager<ApplicationUser> _userManager,  IQuestionRepository _question_Repository,
            IQuestionCategoryRepository _question_Category, ICustomerQuestionMediaRepository _customer_Media,
            ICustomerQuistionsRepository _customer_Question, IHubContext<NotificationHub> _hubContext
            )
        {
            userManager = _userManager;
            hubContext = _hubContext;
             customer_Question = _customer_Question;
            Customer_Media = _customer_Media;
            Question_Repository = _question_Repository;
            Question_Category = _question_Category;
        }

        [HttpPost("Search")]
        public IActionResult Search(SearchViewModel searchModel)
        {
            if (searchModel.SearchKeyword != null)
            {
                var searchresult = Question_Repository.Search(searchModel.SearchKeyword,
                    searchModel.Selected_categories);
                return Ok(new
                {
                    Flag = true,
                    Message = "Done",
                    questions = ApiQuestionMapping.Mapping(searchresult.ToList())
                });
            }
            return BadRequest(new { Flag = false, Message = "Error_No Search Keyword", Data = 0 });
        }

       [HttpPost("GetCategories")]
        public IActionResult GetCategories()
        {
            if (Question_Category.GetAll() != null)
            {
              
                return Ok(Question_Category.GetAll());
            }
            return BadRequest("There are No Categories");
        }


        //var _questionText = HttpContext.Request.Form["QuestionText"];
        //var _questionDesc = HttpContext.Request.Form["QuestionDesc"];
        //var _questionCategory = HttpContext.Request.Form["QuestionCateory"];
        //var ImageOne = HttpContext.Request.Form.Files[0];
        //var ImageTwo = HttpContext.Request.Form.Files[1].FileName;


        [HttpPost("AddQuestion_formdata")]
        public async Task<IActionResult> AddQuestion([FromForm] QuestionModel questionModel)
        {
            if (HttpContext.Request.Form["QuestionText"].ToString() != null && HttpContext.Request.Form["CategoryId"] != 0)
            {
                ApplicationUser UserModel =
              await userManager.FindByIdAsync(HttpContext.Request.Form["UserId"].ToString());
                CustomerQuestions customerQuestion = new CustomerQuestions();
                customerQuestion.Text = HttpContext.Request.Form["QuestionText"].ToString();
                customerQuestion.CategoryId = Convert.ToInt32(HttpContext.Request.Form["CategoryId"]);
                customerQuestion.Status = Question_Status.Pending.ToString();
                customerQuestion.SubmitTime = DateTime.Now;
                customerQuestion.UserId = HttpContext.Request.Form["UserId"].ToString();
                customerQuestion.User = UserModel;
               
                customer_Question.Insert(customerQuestion);

                if (HttpContext.Request.Form.Files != null)
                {

                    List<string> Images = customer_Question.UploadImagesByUser((List<IFormFile>)HttpContext.Request.Form.Files);
                    foreach (var item in Images)
                    {
                        CustomerQuestionMedia media = new CustomerQuestionMedia();
                        media.Path = item;
                        media.Type = MediaTypes.Image.ToString();
                        media.QuestionId = customerQuestion.ID;
                        media.UserId = HttpContext.Request.Form["UserId"].ToString();
                        Customer_Media.Insert(media);

                    }
                }



                string jsonObject = JsonConvert.SerializeObject(customerQuestion, new JsonSerializerSettings()
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    Formatting = Formatting.Indented
                });

                //   JsonConvert.SerializeObject(question);

                await hubContext.Clients.All.SendAsync("ReciveQuestions", jsonObject);
                return Ok(customerQuestion);
            }
            else
            {
                return BadRequest("please add a valid question ");
            }

        }






        [HttpPost("AddQuestion")]
        public async Task<IActionResult> AddQuestionnAsync(QuestionModel questionModel)
        {
            if (questionModel.Text !=null && questionModel.CategoryId !=0)
            {
                ApplicationUser UserModel =
              await userManager.FindByIdAsync(questionModel.UserId);
                CustomerQuestions customerQuestion = new CustomerQuestions();
                customerQuestion.Text = questionModel.Text;
                customerQuestion.CategoryId = questionModel.CategoryId;
                customerQuestion.Status = Question_Status.Pending.ToString();
                customerQuestion.SubmitTime = DateTime.Now;
                customerQuestion.UserId = questionModel.UserId;
                customerQuestion.User = UserModel;
              
                customer_Question.Insert(customerQuestion);
              
                if (questionModel.QuestionImage != null)
                {
                    List<string> Images = customer_Question.UploadFile(questionModel.QuestionImage);
                    foreach (var item in Images)
                    {
                        CustomerQuestionMedia media = new CustomerQuestionMedia();
                        media.Path = item;
                        media.Type = MediaTypes.Image.ToString();
                        media.QuestionId = customerQuestion.ID;
                        media.UserId = questionModel.UserId;
                        Customer_Media.Insert(media);

                    }
                }

                string jsonObject = JsonConvert.SerializeObject(customerQuestion, new JsonSerializerSettings()
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    Formatting = Formatting.Indented
                });

                //   JsonConvert.SerializeObject(question);

               await hubContext.Clients.All.SendAsync("ReciveQuestions", jsonObject);
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
            if (Question_Repository.GetTopTenQuestions() != null)
            {
                return Ok(new { Flag = true, Message ="Done",
                    Data = ApiQuestionMapping.Mapping(Question_Repository.GetTopTenQuestions()) });
            }
            return BadRequest(new { Flag = false, Message ="Error,there are no top questions" , Data = 0 });
        }

        [HttpPost("CustomerQuestions")]
        public IActionResult GetCustomerQuestionsById(ApiUser user)
        {
            if (customer_Question.GetAllQuestionsByCustomerId(user.Id).Count != 0)
            {
                return Ok(new
                {
                    Flag = true,
                    Message = "Done",
                    Questions = ApiCustomerQuestionMapping.Mapping(customer_Question.GetAllQuestionsByCustomerId(user.Id))
                });
            }
            return BadRequest(new { Flag = false, Message = "Error,there are no questions posted by this user", Questions = 0 });
        }
    }
    }
