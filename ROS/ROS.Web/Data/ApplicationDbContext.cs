﻿using System;
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

            builder.Entity<Regatta>().HasOne(r => r.HostingClub).WithMany(c => c.Regattas).OnDelete(DeleteBehavior.SetNull);
        }

        public DbSet<Boat> Boats { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<Regatta> Regattas { get; set; }
        public DbSet<RegattaRegistration> RegattaRegistration { get; set; }
        public DbSet<ClubApplication> ClubApplications { get; set; }
        public DbSet<ROS.Web.Models.ApplicationUser> ApplicationUser { get; set; }
    }
}
