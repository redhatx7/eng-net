using System.Security.Claims;
using System.Threading.Tasks;
using EVoteSystem.Models;
using EVoteSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace EVoteSystem.Controllers
{
    [Authorize]
    public class AuthController : Controller
    {
        private ILogger<AuthController> _logger;

        private readonly UserManager<Student> _studentUserManager;

        private readonly SignInManager<Student> _studentSignInManager;

        private readonly SignInManager<ApplicationAdmin> _adminSignInManager;
        
        public AuthController(ILogger<AuthController> logger,
            SignInManager<Student> studentSignInManager,
            SignInManager<ApplicationAdmin> adminSignInManager)
        {
            _studentSignInManager = studentSignInManager;
            _adminSignInManager = adminSignInManager;
            _logger = logger;
        }
        
        
        // Login GET
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");
            return View();
        }
        
        // Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl, LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                switch (login.LoginType)
                {
                    case LoginType.Candidate:
                    {
                        var result = await _studentSignInManager.PasswordSignInAsync(login.Username, login.Password,
                            login.RememberMe, false);
                        if (result.Succeeded)
                        {
                            HttpContext.Response.Cookies.Append("UserType", "Candidate");   
                            return RedirectToAction("Index", "Candidate");
                        }
                        break;
                    }
                    case LoginType.Student:
                    {
                        var result = await _studentSignInManager.PasswordSignInAsync(login.Username, login.Password,
                            login.RememberMe, false);
                        if (result.Succeeded)
                        {
                            HttpContext.Response.Cookies.Append("UserType", "Student");
                            return RedirectToAction("Index", "Student");
                        }
                        break;
                    }
                    case LoginType.Deputy:
                    {
                        var result = await _adminSignInManager.PasswordSignInAsync(login.Username, login.Password,
                            login.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Deputy");
                        }
                        break;
                    }
                    case LoginType.HeadMaster:
                    {
                        var result = await _adminSignInManager.PasswordSignInAsync(login.Username, login.Password,
                            login.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "HeadMaster");
                        }
                        break;
                    }
                    default: 
                        break;
                }
            }
            ModelState.AddModelError(null,"خطا، نام‌کاربری یا کلمه‌عبور اشتباه می‌باشد.");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            if (User.IsInRole("HeadMaster") || User.IsInRole("Deputy"))
            {
                await _adminSignInManager.SignOutAsync();
            }
            else
            {
                await _studentSignInManager.SignOutAsync();
            }

            return Redirect("/");
        }


        private async Task RefreshClaimAsync()
        {
            
        }
    }
}