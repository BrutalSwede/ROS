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
        public static async void Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // This makes sure the database is created when the application is started.
            context.Database.EnsureCreated();

            if (!context.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                await roleManager.CreateAsync(new IdentityRole { Name = "User" });
            }

            if (!context.Users.Any())
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
                    await userManager.CreateAsync(user, "Password-1"); // This is super safe, don't worry.

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
