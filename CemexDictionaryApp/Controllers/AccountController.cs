using CemexDictionaryApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CemexDictionaryApp.Models;
using Microsoft.AspNetCore.Authorization;
namespace CemexDictionaryApp.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private RoleManager<IdentityRole> roleManager;
        DBContext dbcontext;
        public AccountController(UserManager<ApplicationUser> _userManager
        , SignInManager<ApplicationUser> _signInManager
        , RoleManager<IdentityRole> _roleManager
        , DBContext _dbContext)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            roleManager = _roleManager;
            dbcontext = _dbContext;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var UserModel =
                await userManager.FindByEmailAsync(user.Email);



                if (UserModel != null)
                {
                   var result =
                    await signInManager.PasswordSignInAsync
                    (UserModel, user.Password, user.RememberMe, false);
                    //Check user role to add Logging Record As Reciptionist
                   
                    return RedirectToAction("HomePage", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }

                }
            
            return View(user);

        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Name,
                    Email = model.Email,
                    PasswordHash=model.Password,
                    Role="Admin"
                };
                var result = await userManager.CreateAsync(user,user.PasswordHash);
                if (await roleManager.RoleExistsAsync("Admin") == false)
                {
                    IdentityRole role = new IdentityRole();
                    role.Name = "Admin";
                    await roleManager.CreateAsync(role);
                    await userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("HomePage", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }
    }
}
