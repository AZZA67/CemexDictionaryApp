using CemexDictionaryApp.DTO;
using CemexDictionaryApp.Models;
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
        //EntityContext context;
        DBContext dbcontext;
        private readonly SignInManager<ApplicationUser> signinmanager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserController(IHttpContextAccessor httpContextAccessor, SignInManager<ApplicationUser> signinmanager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, DBContext _dbcontext)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            this.dbcontext = _dbcontext;
            this.signinmanager = signinmanager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);
                await userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id));
                await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.UserName));
                await userManager.AddClaimAsync(user, new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Id)
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecrtKey"]));

              
                //getuser(authClaims);
                return Ok(new
                {
                   user,
                    authClaims = authClaims[2].Value
                });
            }
            return Unauthorized();
        }
       //return object have user data
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterModel userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userExists = await userManager.FindByNameAsync(userDTO.Username);
            if (userExists != null)
                return BadRequest(error: "User already exists!");
            ApplicationUser user = new ApplicationUser()
            {
                Email = userDTO.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = userDTO.Username,
             
                Role = "User"

            };
            var result = await userManager.CreateAsync(user, userDTO.Password);
            if (await roleManager.RoleExistsAsync("User") == false)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                await roleManager.CreateAsync(role);
                await userManager.AddToRoleAsync(user, "User");
            }
            else
            {
                await userManager.AddToRoleAsync(user, "User");
            }

            if (!result.Succeeded)
                return BadRequest(result.Errors.FirstOrDefault().Description);
            return Ok(user);
        }




    }
}
