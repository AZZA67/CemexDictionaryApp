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
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if(model != null)
            {
                var _user = dbcontext.app_users.FirstOrDefault(user => user.PhoneNumber == model.Mobileno);
                if (_user != null)
                {
                    if (await userManager.CheckPasswordAsync(_user, model.Password))
                    {
                        var userRoles = await userManager.GetRolesAsync(_user);
                        await userManager.AddClaimAsync(_user, new Claim(ClaimTypes.NameIdentifier, _user.Id));
                        await userManager.AddClaimAsync(_user, new Claim(ClaimTypes.Name, _user.UserName));
                        await userManager.AddClaimAsync(_user, new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        var authClaims = new List<Claim>{
                        new Claim(ClaimTypes.Name, _user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.NameIdentifier, _user.Id)
                        };

                        foreach (var userRole in userRoles)
                        {
                            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                        }

                        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecrtKey"]));
                        return Ok(new { Flag = true, Message =ApiMessages.Done, Data = ApiUserMapping.Mapping(_user)});
                    }
                    return BadRequest(new { Flag = false, Message =ApiMessages.WrongPassword, Data = 0 });
                }
                return BadRequest(new { Flag = false, Message =ApiMessages.MobileNotExist, Data = 0 });
            }
            return BadRequest(new { Flag = false, Message =ApiMessages.EmptyObject, Data = 0 });
        }
       
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(ApiUser model)
        {
            if(model != null)
            {
                var _user = dbcontext.app_users.FirstOrDefault(user => user.PhoneNumber == model.Mobileno);
                if(_user == null)
                {
                    ApplicationUser _newUser = new()
                    {
                        SecurityStamp = Guid.NewGuid().ToString(),
                        UserName = model.Mobileno,
                        Zone = model.Zone,
                        State = model.State,
                        Category = model.Occupation,
                        Occupation = model.Occupation,
                        PhoneNumber = model.Mobileno,
                        Address = model.Address,
                        Name = model.Username,
                        Role = "User"
                    };

                    if (model.Email == null || string.IsNullOrEmpty(model.Email))
                        _newUser.Email = model.Mobileno + "@cemex.com";
                    else
                        _newUser.Email = model.Email;

                    var _createPass = await userManager.CreateAsync(_newUser, model.Password);
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
                        return Ok(new { Flag = true, Message =ApiMessages.ConfirmRegistration, Data = ApiUserMapping.Mapping(_newUser) });

                    // new { id = _newUser.Id, mobileNo = _newUser.PhoneNumber }

                    return BadRequest(new { Flag = false, Message =ApiMessages.RegistrationError, Data = 0 });
                }
                return BadRequest(new { Flag = false, Message =ApiMessages.MobileExist, Data = 0 });
            }
            return BadRequest(new { Flag = false, Message =ApiMessages.EmptyObject, Data = 0 });
        }

        [HttpPost]
        [Route("Profile")]
        public async Task<IActionResult> UserProfile([FromBody] ApiUser model)
        {
            if (model != null)
            {
                ApplicationUser _userModel = await userManager.FindByIdAsync(model.Id);
                if (_userModel != null)
                {
                      var _questions =   CustomerQuestion.QuestionStatusPerCustomer(model.Id);
                      return Ok(new { Flag = true, Message = ApiMessages.Done, User = ApiUserMapping.Mapping(_userModel) , Questions = _questions});
                }
                return BadRequest(new { Flag = false, Message = ApiMessages.UserNotExist, Data = 0 });
            }
            return BadRequest(new { Flag = false, Message = ApiMessages.EmptyObject, Data = 0 });
        }

        [HttpPost]
        [Route("Token")]
        public async Task<IActionResult> UserToken([FromBody] ApiUser model)
        {
            if (model != null)
            {
                ApplicationUser _user = await userManager.FindByIdAsync(model.Id);
                _user.Token = model.Token; 
                var _result  = await userManager.UpdateAsync(_user);

                if (_result.Succeeded)
                    return Ok(new { Flag = true, Message = ApiMessages.Done, UserId = _user.Id });
                return BadRequest(new { Flag = false, Message = ApiMessages.UserNotExist, Data = 0 });
            }
            return BadRequest(new { Flag = false, Message = ApiMessages.EmptyObject, Data = 0 });
        }
    }
}
