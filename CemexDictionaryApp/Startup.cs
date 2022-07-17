using CemexDictionaryApp.Models;
using CemexDictionaryApp.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CemexDictionaryApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            string connectionString = Configuration.GetConnectionString("Cemex_Dictionary");
            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();
            services.AddDbContext<DBContext>(c => c.UseSqlServer(connectionString));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IProductLogRepository, ProductLogRepository>();
            services.AddScoped<IQuestionCategoryRepository,QuestionCategoryRepository>();
            services.AddScoped<INewsLogRepository, NewsLogRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<DBContext>();
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Login",
                    pattern: "{controller=Account}/{action=Login}/{Id?}");
            
            
                    endpoints.MapControllerRoute(name: "default",
             pattern: "{controller=Home}/{action=HpmePage}/{id?}");
                
            }
            );
        }
    }
}




