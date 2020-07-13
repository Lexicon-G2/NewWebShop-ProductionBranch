using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppForWebshop.Models;

namespace WebAppForWebshop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Products> Products { get; set; }
        //public DbSet<Customers> Customers { get; set; }
        public DbSet<Orders> Orders { get; set; }

     DbSet<ApplicationUser> Users { get; set; }
     public  DbSet<OrderReference> OrderReferences { get; set; }

        public DbSet<CartItem> ShoppingCartItems { get; set; }
        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(new List<IdentityRole>
{
    new IdentityRole {
        Id = "1",
        Name = "Head Admin",
        NormalizedName = "HEAD ADMIN"
    },
    new IdentityRole {
        Id = "2",
        Name = "Staff",
        NormalizedName = "STAFF"
    },
});
            //var hasher = new PasswordHasher<ApplicationUser>();
            //modelBuilder.Entity<ApplicationUser>().HasData(
            //    new ApplicationUser
            //    {
            //        Id = "1", // primary key
            //        UserName = "admin",
            //        NormalizedUserName = "ADMIN",
            //        PasswordHash = hasher.HashPassword(null, "1234"),
            //        FirstName = "Jonathan",
            //        LastName = "haha",
            //        Address = "gatan 2"
            //    },
            //    new ApplicationUser
            //    {
            //        Id = "2", // primary key
            //        UserName = "shoeb@shoeb.com",
            //        NormalizedUserName = "SHOEB@SHOEB.COM",
            //        PasswordHash = hasher.HashPassword(null, "1234"),
            //        FirstName = "Shoeb",
            //        LastName = "Shoeb",
            //        Address = "ShoebGatan 99"
            //    }
            //);
            
            //modelBuilder.Entity<AspNetUserRoles>().HasData(
            //    new AspNetUserRoles
            //    {
            //        RoleId = 1, // for admin username
            //        UserId = 1  // for admin role
            //    },
            //    new AspNetUserRoles
            //    {
            //        RoleId = 1, // for staff username
            //        UserId = 2  // for staff role
            //    }
            //);
        }

        #endregion

    }
}
