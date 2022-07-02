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

        private DBContext dbcontext;
        public AccountController(UserManager<ApplicationUser> _userManager
        , SignInManager<ApplicationUser> _signInManager
        , RoleManager<IdentityRole> _roleManager
        , DBContext _DBContext)
        {
            this.signInManager = _signInManager;
            this.userManager = _userManager;
            this.roleManager = _roleManager;
            this.dbcontext = _DBContext;
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
                };

                var result = await userManager.CreateAsync(user,user.PasswordHash);

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
