using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAppForWebshop.Data;
using WebAppForWebshop.Models;

namespace WebAppForWebshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly  ApplicationDbContext db;
        private UserManager<ApplicationUser> userManager;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, UserManager<ApplicationUser> userMrg)
        {
            _logger = logger;
            this.db = db;
            userManager = userMrg;
        }

        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await userManager.FindByEmailAsync("SUPERADMIN@ADMIN");
            if(user != null)
            {
               var result = await userManager.AddToRoleAsync(user, "Admin");
            }
            await db.SaveChangesAsync();
            return View("Index");
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        //public IActionResult Products()
        //{
           
        //    return View();
        //}
        [Authorize]
        public IActionResult User()
        {//LIST ALL USERS
            ApplicationUser model = new ApplicationUser();
            
            model.userList = db.Users.ToList();
            return View("User", model);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(string username, string pword, string fname, string lname, string adr)
        {
            var newUser = new ApplicationUser()
            {
                UserName = username,
                NormalizedUserName = username.ToUpper(),
                Email = username,
                NormalizedEmail = username.ToUpper(),
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = fname,
                LastName = lname,
                Address = adr
            };

            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(newUser, pword);
            newUser.PasswordHash = hashed;
            var userStore = new UserStore<ApplicationUser>(db);
            await userStore.CreateAsync(newUser);
            await db.SaveChangesAsync();
            ApplicationUser model = new ApplicationUser();
            model.userList = db.Users.ToList();
            return View("User", model);
        }

        public async Task<IActionResult> ManageUser(string id)
        {
            
                var user = await db.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            
            return View("manageUser", user);
        }

        [HttpPost, ActionName("UpdateUser")]
        public async Task<IActionResult> UpdateUser(string uid, string fname, string lname, string adr)
        {
            var userStore = new UserStore<ApplicationUser>(db);
            var user = (ApplicationUser)await db.Users.Where(x => x.Id == uid).FirstOrDefaultAsync();
            user.FirstName = fname;
            user.LastName = lname;
            user.Address = adr;
            db.Users.Update(user);
            db.SaveChanges();
            await userStore.UpdateAsync(user);
            await db.SaveChangesAsync();
            ViewBag.Test = fname + " ---" + lname + " ---" + adr + " ---" + uid;
         
            return View("manageUser", user);
        }

        

        public IActionResult RemoveUser(string id)
        {//REMOVE SPECIFIC USER
            db.Users.RemoveRange(db.Users.Where(x => x.Id == id));
            
            db.SaveChanges();
            
            return RedirectToAction("User");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
