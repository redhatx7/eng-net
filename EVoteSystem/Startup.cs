using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EVoteSystem.DatabaseContext;
using EVoteSystem.Models;
using EVoteSystem.Policies;
using EVoteSystem.Policies.Services;
using EVoteSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;

namespace EVoteSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddRouting(t => t.LowercaseUrls = true);
            
            services.AddDbContext<EVoteDbContext>();
            services.AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<EVoteDbContext>();

            services.AddIdentityCore<Student>()
                .AddDefaultTokenProviders()
                .AddRoles<IdentityRole<int>>()
                .AddSignInManager<SignInManager<Student>>()
                .AddEntityFrameworkStores<EVoteDbContext>();

            services.AddIdentityCore<ApplicationAdmin>()
                .AddDefaultTokenProviders()
                .AddSignInManager<SignInManager<ApplicationAdmin>>()
                .AddEntityFrameworkStores<EVoteDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 3;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/auth/login";
                options.LogoutPath = "/auth/logout";
                options.SlidingExpiration = true;
                options.AccessDeniedPath = "/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromDays(15);
            });
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("StudentLogin", policy =>
                {
                    policy.RequireUserType(LoggedInUserType.Student);
                });
                options.AddPolicy("CandidateLogin", policy =>
                {
                    policy.RequireUserType(LoggedInUserType.Candidate);
                });
            });

            services.AddHttpContextAccessor();
            
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IAuthorizationHandler, StudentRequirementHandler>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}