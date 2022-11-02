using CemexDictionaryApp.Core;
using CemexDictionaryApp.Firebase;
using CemexDictionaryApp.Models;
using CemexDictionaryApp.Repositories;
using CemexDictionaryApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace CemexDictionaryApp.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository ProductRepository;
        IProductTypeRepository ProductTypeRepository;
        IProductLogRepository ProductLogRepository;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private RoleManager<IdentityRole> roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        readonly INotificationRepo NotificationRepo;
        readonly IFirebaseNotificationService FirebaseNotificationService;

        public ProductController(UserManager<ApplicationUser> _userManager
        , SignInManager<ApplicationUser> _signInManager
        , RoleManager<IdentityRole> _roleManager,IProductRepository _productRepository,
            IProductTypeRepository _productTypeRepository, 
            IProductLogRepository _productLogRepository, IHttpContextAccessor httpContextAccessor ,
             INotificationRepo notificationRepo, IFirebaseNotificationService firebaseNotificationService)
        {
            ProductRepository = _productRepository;
            ProductTypeRepository = _productTypeRepository;
            ProductLogRepository = _productLogRepository;
            signInManager = _signInManager;
            userManager = _userManager;
            roleManager = _roleManager;
            _httpContextAccessor = httpContextAccessor;
            NotificationRepo = notificationRepo;
            FirebaseNotificationService = firebaseNotificationService;
        }

        private async Task<ApplicationUser> GetCurrentUserAsync() => await userManager.GetUserAsync(HttpContext.User);

        [Authorize]
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            List<Product> products = ProductRepository.GetAll();
            return View(products);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddNewProduct()
        {
            List<ProductType> productTypes = ProductTypeRepository.GetAll();
            ViewData["ProductTypes"] = productTypes;
            return PartialView();
        }

        public IActionResult Details(int productId)
        {
            Product product = ProductRepository.GetById(productId);
            return PartialView("Details", product);
        }
       
        public async Task<IActionResult> ChangeProductStatusByIdAsync(int productId)
        {
            Product product = ProductRepository.GetById(productId);
            if (product.Status == "Active")
                product.Status = "InActive";
            else
                product.Status = "Active";

            ProductRepository.Update( productId, product);
            var user = await GetCurrentUserAsync();
            ProductLog _productlog = new ProductLog
            {
                UserId = userManager.GetUserId(HttpContext.User),
                DateTime = DateTime.Now,
                Action = product.Status,
                ProductId = product.Id
            };
            ProductLogRepository.Insert(_productlog);
            return Json(product.Status);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddNewProductAsync(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProductRepository.UploadedFile(model);

                Product product = new()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Status = "Active",
                    Image = uniqueFileName,
                    ProductTypeId = model.ProductTypeId,
                    Type = ProductTypeRepository.GetById(model.ProductTypeId).Type
                };

                _ = await GetCurrentUserAsync();

                ProductRepository.Insert(product);
                TempData["Success"] = "true";

                ProductLog _productlog = new()
                {
                    UserId = userManager.GetUserId(HttpContext.User),
                    DateTime = DateTime.Now,
                    Action = "Adding",
                    ProductId = product.Id
                };

                ProductLogRepository.Insert(_productlog);

                //Notify
                Notification _notify = new()
                {
                    Title = Messages.ProductTitle.ToString(),
                    Message = Messages.ProductMessage.ToString(),
                    Type = NotificationType.Products.ToString(),
                    ObjectId = product.Id.ToString(),
                    SubmitDate = DateTime.Now.ToString(),
                    UserId = NotificationType.All.ToString()
                };

                await NotificationRepo.Add(_notify);
                await FirebaseNotificationService.SendNotification(FirebaseNotificationService.Topic(_notify));

                return RedirectToAction("GetAllProducts", "Product");
            }

            List<ProductType> productTypes = ProductTypeRepository.GetAll();
            ViewData["ProductTypes"] = productTypes;
            return View("AddNewProduct");
        }
    }
}
