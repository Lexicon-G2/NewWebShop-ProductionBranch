using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Threading.Tasks;
using WebAppForWebshop.Data;

namespace WebAppForWebshop.Models
{
    public class dbSeedUserRoles
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public dbSeedUserRoles(ApplicationDbContext context)
        {
            _context = context;
            //_userManager = userMrg;
            //var ccontext = serviceProvider.GetService<ApplicationDbContext>();

        }
        public async void SeedAdminUser()
        {

            var user = new ApplicationUser
            {
                UserName = "superadmin@admin",
                NormalizedUserName = "SUPERADMIN@ADMIN",
                Email = "superadmin@admin",
                NormalizedEmail = "SUPERADMIN@ADMIN",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = "Admin",
                LastName = "Admin",
                Address = "AdminGatan 2"
            };

            var roleStore = new RoleStore<IdentityRole>(_context);

            if (!_context.Roles.Any(r => r.Name == "Admin"))
            {
                await roleStore.CreateAsync(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
            }
            if (!_context.Roles.Any(r => r.Name == "Customer"))
            {
                await roleStore.CreateAsync(new IdentityRole { Name = "Customer", NormalizedName = "CUSTOMER" });
            }

            if (!_context.Users.Any(u => u.UserName == user.UserName))
        {
            var password = new PasswordHasher<ApplicationUser>();
var hashed = password.HashPassword(user, "1234");
user.PasswordHash = hashed;
            var userStore = new UserStore<ApplicationUser>(_context);
await userStore.CreateAsync(user);

                //string userId = user.Id;
                //ApplicationUser useradmin = await _userManager.FindByIdAsync(userId);
                //if(useradmin != null) {
                //IdentityResult result = await _userManager.AddToRoleAsync(user, "SuperAdmin");

            
                //var result = _userManager.AddToRoleAsync(user, "Admin");
                //}
                //if (result.Succeeded)
                //    await _userManager.AddToRoleAsync(user, "SuperAdmin");

                //await _userManager.AddToRoleAsync(user, "Admin");
            }

          //await  AssignRoles(user.Email, "SuperAdmin", _context);

            await  _context.SaveChangesAsync();
    }
        //public static async Task<IdentityResult> AssignRoles(string email, string role, )
        //{
        //    UserManager<ApplicationUser> _userManager = UserManager<ApplicationUser>(_context);
        //    ApplicationUser user = await _userManager.FindByEmailAsync(email);
        //    var result = await _userManager.AddToRoleAsync(user, role);

        //    return result;
        //}
    }
}
