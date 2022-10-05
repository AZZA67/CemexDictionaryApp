using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CemexDictionaryApp.Repositories;
using CemexDictionaryApp.WebApi.ApiModels;
using CemexDictionaryApp.WebApi.ApiViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CemexDictionaryApp.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        readonly INewsRepository NewsRepository;
        readonly IQuestionCategoryRepository QuestionCategory;
        public HomeController(INewsRepository newsRepository, IQuestionCategoryRepository questionCategory)
        {
            NewsRepository = newsRepository;
            QuestionCategory = questionCategory;
        }

        [HttpGet("Data")]
        public IActionResult HomeData()
        {
            var _news = NewsRepository.ActiveNews();
            var _questioncategory = QuestionCategory.GetAll().Select(p =>p.Name).ToList();


            List<ApiQuestion> questionLst = new();
            List<string> q1images = new();
            q1images.Add("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT0Ef0FQt-E-gPZcG53ldNtLbAsEDefSgd3gA&usqp=CAU");
            q1images.Add("https://static.theprint.in/wp-content/uploads/2020/08/press-2333329_1280-e1596518357425.jpg?compress=true&quality=80&w=800&dpr=1.0");
            q1images.Add("https://www.lamborghini.com/sites/it-en/files/DAM/lamborghini/facelift_2019/model_detail/menu/09_09/menu_huracan_evo_rwd.jpg");

            List<string> q1videos = new();
            q1videos.Add("https://www.youtube.com/watch?v=V9n11cDpdIw");

            ApiQuestion q1 = new()
            {
                id = 1,
                QuestionTitle = "ما هو الفرق بين الاسمنت والخرسانة؟",
                QuestionAnswer = "على الرغم من كلمتي اسمنت و خرسانة غالبا ما تستخدما للدلالة على نفس المادة،  فإن الاسمنت هو في الواقع أحد مكونات الخرسانة. و هي في الأساس خليط من الركام و عجينة لاصقة. الركام كلمة تطلق على الرمل والحصمة مجتمعين، و العجينة اللاصقة فيما بين مكونات الركام تنتج عن خلط الماء بالاسمنت البورتلاندي،",
                QuestionImagesUrls = q1images,
                QuestionAnswerVideosUrls = q1videos
            };

            List<string> q2images = new() ;
            List<string> q2videos = new();

            q2images.Add("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT0Ef0FQt-E-gPZcG53ldNtLbAsEDefSgd3gA&usqp=CAU");
            q2images.Add("https://static.theprint.in/wp-content/uploads/2020/08/press-2333329_1280-e1596518357425.jpg?compress=true&quality=80&w=800&dpr=1.0");
            q2videos.Add("https://www.youtube.com/watch?v=_cOU_SaV87g");

            ApiQuestion q2 = new()
            {
                id = 2,
                QuestionTitle = "ماذا يعني اسمنت 42.5؟",
                QuestionAnswer = @"وفقاً للمواصفات الأردنية و العالمية للاسمنت، يجب على كل نوع من الاسمنت توفير الحد الأدنى من قوة تحمل الضغط على عمر 28 يوماً. ويتم تصنيف قوة أسمنت إلى 3 فئات :
فئة القوة 52.5: يجب أن لا تقل قوة تحمل الضغط الذي يوفره هذا الصنف من الإسمنت عن 52.5 ميغا باسكال على عمر 28 يوماً.
فئة القوة 42.5 : يجب أن لا تقل قوة تحمل الضغط الذي يوفره هذا الصنف من الإسمنت عن 42.5 ميغا باسكال على عمر 28 يوماً.
32.5 فئة القوة يجب أن لا تقل قوة تحمل الضغط الذي يوفره هذا الصنف من الإسمنت عن 32.5 ميغا باسكال على عمر 28 يوماً
تلك هي أصناف القوة الوحيدة المسموح بها في المواصفة القياسية ، مما يعني أن لا يسمح لفئات قوة أخرى بأن تستعمل لأغراض البناء",
                QuestionImagesUrls = q2images,
                QuestionAnswerVideosUrls = q2videos
            };


            ApiQuestion q3 = new()
            {
                id = 3,
                QuestionTitle = "ما تأثير الجو الحار أو البارد على عملية صب الخرسانة؟",
                QuestionAnswer = @"درجات الحرارة المرتفعة أو المنخفضة جداً تصعب عملية إيناع الخرسانة بالشكل الصحيح. في الأيام الحارة، يتم فقدان الكثير من المياه عن طريق التبخر من الخرسانة المصبوبة حديثا. أما إذا كانت درجة الحرارة منخفضة جداً و قريبة من درجة تجمد الماء، فإن ذلك يؤدي إلى إبطاء أو توقف عملية تصلب الخرسانة تقريباً، وبالتالي لا تعطي الخرسانة القوة المطلوبة ولا توفر قوة مقاومة للعوامل البيئية بالشكل الكافي.",
                QuestionImagesUrls = null,
                QuestionAnswerVideosUrls = null
            };


            questionLst.Add(q1);
            questionLst.Add(q2);
            questionLst.Add(q3);

            if (_questioncategory != null)
                return Ok(new { Flag = true, Message = ApiMessages.Done, QuestionCategories = _questioncategory, News = _news , TopQuestions =  questionLst });
            else
                return BadRequest(new { Flag = false, Message = ApiMessages.EmptyNewsList, Data = 0 });
        }


        [HttpPost("CustomerQuestions")]
        public IActionResult CustomerQuestions([FromBody] ApiUser user)
        {
            var id = user.Id;

            List <ApiQuestion> questionLst = new();
            List<string> q1images = new();
            q1images.Add("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT0Ef0FQt-E-gPZcG53ldNtLbAsEDefSgd3gA&usqp=CAU");
            q1images.Add("https://static.theprint.in/wp-content/uploads/2020/08/press-2333329_1280-e1596518357425.jpg?compress=true&quality=80&w=800&dpr=1.0");
            q1images.Add("https://www.lamborghini.com/sites/it-en/files/DAM/lamborghini/facelift_2019/model_detail/menu/09_09/menu_huracan_evo_rwd.jpg");

            List<string> q1videos = new();
            q1videos.Add("https://www.youtube.com/watch?v=V9n11cDpdIw");

            ApiQuestion q1 = new()
            {
                id = 1,
                QuestionTitle = "ما هو الفرق بين الاسمنت والخرسانة؟",
                QuestionAnswer = "على الرغم من كلمتي اسمنت و خرسانة غالبا ما تستخدما للدلالة على نفس المادة،  فإن الاسمنت هو في الواقع أحد مكونات الخرسانة. و هي في الأساس خليط من الركام و عجينة لاصقة. الركام كلمة تطلق على الرمل والحصمة مجتمعين، و العجينة اللاصقة فيما بين مكونات الركام تنتج عن خلط الماء بالاسمنت البورتلاندي،",
                QuestionImagesUrls = null,
                QuestionAnswerVideosUrls = q1videos,
                QuestionAnswerImagesUrls = q1images,
                QuestionDescription = "ما هو الفرق بين الاسمنت والخرسانة؟",
                Status = "Answered",
            };

            List<string> q2images = new();
            List<string> q2videos = new();

            q2images.Add("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT0Ef0FQt-E-gPZcG53ldNtLbAsEDefSgd3gA&usqp=CAU");
            q2images.Add("https://static.theprint.in/wp-content/uploads/2020/08/press-2333329_1280-e1596518357425.jpg?compress=true&quality=80&w=800&dpr=1.0");
            q2videos.Add("https://www.youtube.com/watch?v=_cOU_SaV87g");

            ApiQuestion q2 = new()
            {
                id = 2,
                QuestionTitle = "ماذا يعني اسمنت 42.5؟",
                QuestionAnswer = @"وفقاً للمواصفات الأردنية و العالمية للاسمنت، يجب على كل نوع من الاسمنت توفير الحد الأدنى من قوة تحمل الضغط على عمر 28 يوماً. ويتم تصنيف قوة أسمنت إلى 3 فئات :
فئة القوة 52.5: يجب أن لا تقل قوة تحمل الضغط الذي يوفره هذا الصنف من الإسمنت عن 52.5 ميغا باسكال على عمر 28 يوماً.
فئة القوة 42.5 : يجب أن لا تقل قوة تحمل الضغط الذي يوفره هذا الصنف من الإسمنت عن 42.5 ميغا باسكال على عمر 28 يوماً.
32.5 فئة القوة يجب أن لا تقل قوة تحمل الضغط الذي يوفره هذا الصنف من الإسمنت عن 32.5 ميغا باسكال على عمر 28 يوماً
تلك هي أصناف القوة الوحيدة المسموح بها في المواصفة القياسية ، مما يعني أن لا يسمح لفئات قوة أخرى بأن تستعمل لأغراض البناء",
                QuestionImagesUrls = q2images,
                QuestionAnswerVideosUrls = q2videos,
               QuestionAnswerImagesUrls = q1images,
                QuestionDescription = "ما هو الفرق بين الاسمنت والخرسانة؟",
                Status = "Answered",
            };


            ApiQuestion q3 = new()
            {
                id = 3,
                QuestionTitle = "ما تأثير الجو الحار أو البارد على عملية صب الخرسانة؟",
                QuestionAnswer = null, 
                QuestionImagesUrls = q1images,
                QuestionAnswerVideosUrls = null,
                QuestionAnswerImagesUrls = null,
                QuestionDescription = "ما هو الفرق بين الاسمنت والخرسانة؟",
                Status = "Pending",
            };


            ApiQuestion q4 = new()
            {
                id = 4,
                QuestionTitle = "لماذا تظهر التشققات في الخرسانة؟",
                 QuestionAnswer = null,
                QuestionImagesUrls = null,
                QuestionAnswerVideosUrls = null,
                QuestionAnswerImagesUrls = q2images,
                QuestionDescription = "ما هو الفرق بين الاسمنت والخرسانة؟",
                Status = "Rejected",
            };

            questionLst.Add(q1);
            questionLst.Add(q2);
            questionLst.Add(q3);
            questionLst.Add(q4);

            return Ok(new { Flag = true, Message = ApiMessages.Done,  result = questionLst });
        }
    }
}
