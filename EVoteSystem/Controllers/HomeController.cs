using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EVoteSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace EVoteSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<Student> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<Student> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation(User.IsInRole("Candidate").ToString());
            if (User.Identity.IsAuthenticated)
            {
                var claims = User.Claims.ToArray();
                
                foreach (var claim in claims)
                {
                    _logger.LogInformation($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");    
                }
                
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}