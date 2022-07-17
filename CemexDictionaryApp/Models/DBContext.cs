using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.Models
{
    public class DBContext: IdentityDbContext<ApplicationUser>
    {
        private readonly DbContextOptions _options;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DBContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _options = options;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<Product> products { get; set; }
        public DbSet<ProductType> productTypes { get; set; }
        public DbSet<ProductLog> ProductsLog { get; set; }
        public DbSet<News> news { get; set; }
        public DbSet<NewsLog> NewsLog { get; set; }
        public DbSet<QuestionCategory> QuestionCategories { get; set; }

    }
}
