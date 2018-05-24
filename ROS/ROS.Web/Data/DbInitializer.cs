using Microsoft.AspNetCore.Identity;
using ROS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Web.Data
{
    public class DbInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public DbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async void Initialize()
        {

            // This makes sure the database is created when the application is started.
            _context.Database.EnsureCreated();

            if (!_context.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
            }

            if (!_context.Users.Any())
            {
                var user = new ApplicationUser
                {
                    Email = "admin@default.com",
                    UserName = "admin@default.com",
                    FirstName = "Admin",
                    LastName = "Adminsson",
                    PhoneNumber = "070-555-000000",
                    Address = "127.0.0.1"
                };

                IdentityResult result =
                    await _userManager.CreateAsync(user, "Password-1"); // This is super safe, don't worry.

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
