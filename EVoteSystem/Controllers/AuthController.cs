using System.Linq;
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

        private readonly UserManager<ApplicationUser> _userManager;

       

        private readonly SignInManager<ApplicationUser> _userSignInManager;
        
        public AuthController(ILogger<AuthController> logger,
            SignInManager<ApplicationUser> userSignInManager,
            UserManager<ApplicationUser> userManager)
        {
            _userSignInManager = userSignInManager;
            _userManager = userManager;
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
                var result =
                    await _userSignInManager.PasswordSignInAsync(login.Username, login.Password, login.RememberMe,
                        false);
                if (result.Succeeded)
                {
                    _logger.LogInformation($"User: {login.Username} logged in");
                    var user = await _userManager.FindByNameAsync(login.Username);
                    var claims = await _userManager.GetClaimsAsync(user);
                    if (claims.Any(x => x.Type == "Fullname"))
                    {
                        await _userManager.ReplaceClaimAsync(user, claims.First(x => x.Type == "Fullname"),
                            new Claim("Fullname", user.Fullname));
                    }
                    else
                    {
                        await _userManager.AddClaimAsync(user, new Claim("Fullname", user.Fullname));
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            _logger.LogInformation("Invalid credential");
            ModelState.AddModelError("","خطا، نام‌کاربری یا کلمه‌عبور اشتباه می‌باشد.");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _userSignInManager.SignOutAsync();
            return Redirect("/");
        }
        
    }
}