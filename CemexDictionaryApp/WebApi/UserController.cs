using CemexDictionaryApp.DTO;
using CemexDictionaryApp.Models;
using CemexDictionaryApp.Repositories;
using CemexDictionaryApp.WebApi.ApiModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CemexDictionaryApp.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly DBContext dbcontext;
        private readonly SignInManager<ApplicationUser> signinmanager;
        private readonly IHttpContextAccessor httpContextAccessor;
        ICustomerQuistionsRepository CustomerQuestion;

        public UserController(IHttpContextAccessor httpContextAccessor, SignInManager<ApplicationUser> signinmanager, UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, IConfiguration configuration, DBContext _dbcontext, ICustomerQuistionsRepository customerQuestion)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            this.dbcontext = _dbcontext;
            this.signinmanager = signinmanager;
            CustomerQuestion = customerQuestion;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            if(login !=null)
            {
                var _checkUser = dbcontext.app_users.FirstOrDefault(user => user.PhoneNumber == login.Mobileno);
                if (_checkUser != null)
                {
                    if (await userManager.CheckPasswordAsync(_checkUser, login.Password))
                    {
                        var userRoles = await userManager.GetRolesAsync(_checkUser);
                        await userManager.AddClaimAsync(_checkUser, new Claim(ClaimTypes.NameIdentifier, _checkUser.Id));
                        await userManager.AddClaimAsync(_checkUser, new Claim(ClaimTypes.Name, _checkUser.UserName));
                        await userManager.AddClaimAsync(_checkUser, new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        var authClaims = new List<Claim>{
                        new Claim(ClaimTypes.Name, _checkUser.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.NameIdentifier, _checkUser.Id)
                        };

                        foreach (var userRole in userRoles)
                        {
                            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                        }

                        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecrtKey"]));
                        return Ok(new { Flag = true, Message =ApiMessages.Done, Data = new { id = _checkUser.Id, mobileNo = _checkUser.PhoneNumber } });
                    }
                    return BadRequest(new { Flag = false, Message =ApiMessages.WrongPassword, Data = 0 });
                }
                return BadRequest(new { Flag = false, Message =ApiMessages.MobileNotExist, Data = 0 });
            }
            return BadRequest(new { Flag = false, Message =ApiMessages.EmptyObject, Data = 0 });
        }
       //return object have user data
       
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(ApiUser registerUser)
        {
            if(registerUser != null)
            {
                var _check = dbcontext.app_users.FirstOrDefault(user => user.PhoneNumber == registerUser.Mobileno);
                if(_check == null)
                {
                    ApplicationUser _newUser = new()
                    {
                        SecurityStamp = Guid.NewGuid().ToString(),
                        UserName = registerUser.Mobileno,
                        Zone = registerUser.Zone,
                        State = registerUser.State,
                        Category = registerUser.Occupation,
                        Occupation = registerUser.Occupation,
                        PhoneNumber = registerUser.Mobileno,
                        Role = "User",
                        Address = registerUser.Address,
                        Name = registerUser.Username
                    };

                    if (registerUser.Email == null || string.IsNullOrEmpty(registerUser.Email))
                        _newUser.Email = registerUser.Mobileno + "@cemex.com";
                    else
                        _newUser.Email = registerUser.Email;

                    var _createPass = await userManager.CreateAsync(_newUser, registerUser.Password);
                    if (await roleManager.RoleExistsAsync("User") == false)
                    {
                        IdentityRole role = new();
                        role.Name = "User";
                        await roleManager.CreateAsync(role);
                        await userManager.AddToRoleAsync(_newUser, "User");
                    }
                    else
                        await userManager.AddToRoleAsync(_newUser, "User");

                    if (_createPass.Succeeded)
                        return Ok(new { Flag = true, Message =ApiMessages.ConfirmRegistration, Data = new { id = _newUser.Id, mobileNo = _newUser.PhoneNumber } });

                    return BadRequest(new { Flag = false, Message =ApiMessages.RegistrationError, Data = 0 });
                }
                return BadRequest(new { Flag = false, Message =ApiMessages.MobileExist, Data = 0 });
            }
            return BadRequest(new { Flag = false, Message =ApiMessages.EmptyObject, Data = 0 });
        }

        [HttpPost]
        [Route("Profile")]
        public async Task<IActionResult> UserProfile([FromBody] ApiUser user)
        {
            if (user != null)
            {
                //ApplicationUser  _checkUser = dbcontext.app_users.FirstOrDefault(u => u.Id.Contains(user.Id));
                ApplicationUser _userModel = await userManager.FindByIdAsync(user.Id);

                if (_userModel != null)
                {
                      var _questions =   CustomerQuestion.QuestionStatusPerCustomer(user.Id);
                      return Ok(new { Flag = true, Message = ApiMessages.Done, User = ApiUserMapping.Mapping(_userModel) , Questions = _questions});
                }
                return BadRequest(new { Flag = false, Message = ApiMessages.UserNotExist, Data = 0 });
            }
            return BadRequest(new { Flag = false, Message = ApiMessages.EmptyObject, Data = 0 });
        }
    }
}
