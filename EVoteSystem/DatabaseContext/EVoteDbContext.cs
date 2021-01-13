using System;
using System.Collections.Generic;
using EVoteSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EVoteSystem.DatabaseContext
{
    public class EVoteDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlite(@"Data Source=evote.db");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Seed Initial Data to database
            builder.Entity<IdentityRole<int>>().HasData(new List<IdentityRole<int>>()
            {
                new IdentityRole<int>()
                {
                    Id =  1,
                    Name = "HeadMaster",
                    NormalizedName = "HEADMASTER"
                },
                new IdentityRole<int>()
                {
                    Id = 2,
                    Name = "Deputy",
                    NormalizedName = "DEPUTY"
                },
                new IdentityRole<int>()
                {
                    Id = 3,
                    Name = "Student",
                    NormalizedName = "STUDENT"
                },
                new IdentityRole<int>()
                {
                    Id = 4,
                    Name = "Candidate",
                    NormalizedName = "CANDIDATE",
                }
                
            });
            var adminHasher = new PasswordHasher<ApplicationAdmin>();
            var studentHasher = new PasswordHasher<Student>();
            
            builder.Entity<ApplicationAdmin>().HasData(new List<ApplicationAdmin>()
            {
                new ApplicationAdmin()
                {
                    Id = 1,
                    Email = "admin@admin.com",
                    Name   = "John",
                    Surename = "Doe",
                    NormalizedEmail = "admin@admin.com".ToUpper(),
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    NationalCode = "1440000000",
                    PasswordHash = adminHasher.HashPassword(null, "admin@123"),
                    SecurityStamp = Guid.NewGuid().ToString("D")
                }
            });

            builder.Entity<Student>().HasData(new List<Student>()
            {
                new Student()
                {
                    Id = 2,
                    Email = "student@test.com",
                    NormalizedEmail = "STUDENT@TEST.COM",
                    Name = "John",
                    Surename = "Wick",
                    EmailConfirmed = true,
                    UserName = "student1",
                    NormalizedUserName = "STUDENT1",
                    Grade =  Grade.Twelfth,
                    NationalCode = "1450000000",
                    PasswordHash =  studentHasher.HashPassword(null, "admin@123"),
                    SecurityStamp = Guid.NewGuid().ToString("D")
                }
            });

            builder.Entity<IdentityUserRole<int>>().HasData(new List<IdentityUserRole<int>>()
            {
                new IdentityUserRole<int>()
                {
                    UserId = 2,
                    RoleId = 3
                },
                new IdentityUserRole<int>()
                {
                    UserId = 2,
                    RoleId = 4,
                }
            });

        }

        public DbSet<Vote> Votes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ApplicationAdmin> Admins { get; set; }
        public DbSet<VoteSession> VoteSessions { get; set; }

    }
}