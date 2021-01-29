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
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    Birthday = new DateTime(2001,1,1)
                },
                new ApplicationAdmin()
                {
                    Id = 2,
                    Email = "deputy@admin.com",
                    Name   = "علی",
                    Surename = "علی‌زاده",
                    NormalizedEmail = "deputy@admin.com".ToUpper(),
                    UserName = "admin2",
                    NormalizedUserName = "admin2".ToUpper(),
                    NationalCode = "1440000001",
                    PasswordHash = adminHasher.HashPassword(null, "admin@123"),
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    Birthday = new DateTime(2002,12,11)
                },
            });

            builder.Entity<Student>().HasData(new List<Student>()
            {
                new Student()
                {
                    Id = 3,
                    Email = "student1@test.com",
                    NormalizedEmail = "STUDENT1@TEST.COM",
                    Name = "علی",
                    Surename = "محمدی",
                    EmailConfirmed = true,
                    UserName = "student1",
                    NormalizedUserName = "STUDENT1",
                    Grade =  Grade.Twelfth,
                    NationalCode = "1450000000",
                    PasswordHash =  studentHasher.HashPassword(null, "admin@123"),
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    Birthday = new DateTime(2005,12,11)
                },
                new Student()
                {
                    Id = 4,
                    Email = "student2@test.com",
                    NormalizedEmail = "STUDENT2@TEST.COM",
                    Name = "امیر",
                    Surename = "رضاپور",
                    EmailConfirmed = true,
                    UserName = "student2",
                    NormalizedUserName = "STUDENT2",
                    Grade =  Grade.Eleventh,
                    NationalCode = "1450000001",
                    PasswordHash =  studentHasher.HashPassword(null, "admin@123"),
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    Birthday = new DateTime(2005,12,11)
                },
                new Student()
                {
                    Id = 5,
                    Email = "student3@test.com",
                    NormalizedEmail = "STUDENT3@TEST.COM",
                    Name = "بهرام",
                    Surename = "بهرامی",
                    EmailConfirmed = true,
                    UserName = "student3",
                    NormalizedUserName = "STUDENT3",
                    Grade =  Grade.Seventh,
                    NationalCode = "1450000000",
                    PasswordHash =  studentHasher.HashPassword(null, "admin@123"),
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    Birthday = new DateTime(2005,12,11)
                },
                new Student()
                {
                    Id = 6,
                    Email = "student4@test.com",
                    NormalizedEmail = "STUDENT4@TEST.COM",
                    Name = "آریا",
                    Surename = "آریاپور",
                    EmailConfirmed = true,
                    UserName = "student4",
                    NormalizedUserName = "STUDENT4",
                    Grade =  Grade.Twelfth,
                    NationalCode = "1450000000",
                    PasswordHash =  studentHasher.HashPassword(null, "admin@123"),
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    Birthday = new DateTime(2005,12,11)
                },
                new Student()
                {
                    Id = 7,
                    Email = "student5@test.com",
                    NormalizedEmail = "STUDENT5@TEST.COM",
                    Name = "امیرحسین",
                    Surename = "حسین‌پور",
                    EmailConfirmed = true,
                    UserName = "student5",
                    NormalizedUserName = "STUDENT5",
                    Grade =  Grade.Twelfth,
                    NationalCode = "1450000000",
                    PasswordHash =  studentHasher.HashPassword(null, "admin@123"),
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    Birthday = new DateTime(2005,12,11)
                },
                new Student()
                {
                    Id = 8,
                    Email = "student6@test.com",
                    NormalizedEmail = "STUDENT6@TEST.COM",
                    Name = "هانیه",
                    Surename = "باقرپور",
                    EmailConfirmed = true,
                    UserName = "student6",
                    NormalizedUserName = "STUDENT6",
                    Grade =  Grade.Twelfth,
                    NationalCode = "1450000000",
                    PasswordHash =  studentHasher.HashPassword(null, "admin@123"),
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    Birthday = new DateTime(2005,12,11)
                },
               
                
            });

            builder.Entity<IdentityUserRole<int>>().HasData(new List<IdentityUserRole<int>>()
            {
                new IdentityUserRole<int>()
                {
                    UserId = 3,
                    RoleId = 3
                },
                new IdentityUserRole<int>()
                {
                    UserId = 3,
                    RoleId = 4,
                },
                new IdentityUserRole<int>()
                {
                    UserId = 4,
                    RoleId = 3
                },
                new IdentityUserRole<int>()
                {
                    UserId = 4,
                    RoleId = 4
                },
                new IdentityUserRole<int>()
                {
                    UserId = 1,
                    RoleId = 1
                },
                new IdentityUserRole<int>()
                {
                    UserId = 2,
                    RoleId = 2
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