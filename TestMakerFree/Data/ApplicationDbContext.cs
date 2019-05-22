using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMakerFree.Models;

namespace TestMakerFree.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "Customer", NormalizedName = "CUSTOMER" },
                new IdentityRole { Id = "3", Name = "Moderator", NormalizedName = "MODERATOR" });
            //{
            //    new { Id = 1, Name = "Admin", NormalizedName = "ADMIN"},
            //    new { Id = 1, Name = "Customer", NormalizedName = "CUSTOMER" },
            //    new { Id = 1, Name = "Moderator", NormalizedName = "MODERATOR" }
            //};
        }


        public DbSet<ProductModel> Products { get; set; }
    }
}
