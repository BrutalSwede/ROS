using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ROS.Web.Models;

namespace ROS.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //builder.Entity<ApplicationUser>().HasMany(u => u.Boats).WithOne(b => b.Owner).HasForeignKey(u => u.OwnerId);

            //builder.Entity<ClubUser>().HasKey(u => new { u.ClubId, u.UserId });
        }

        public DbSet<Boat> Boat { get; set; }
        public DbSet<Club> Clubs { get; set; }
    }
}
