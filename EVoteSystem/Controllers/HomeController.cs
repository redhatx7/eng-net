using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EVoteSystem.Models;
using EVoteSystem.Repositories;
using EVoteSystem.Services;
using Microsoft.AspNetCore.Identity;

namespace EVoteSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISessionRepository _sessionRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IStudentRepository _studentRepository;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager,
            ISessionRepository sessionRepository,
            ICandidateRepository candidateRepository,
            IStudentRepository studentRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _studentRepository = studentRepository;
            _candidateRepository = candidateRepository;
            _sessionRepository = sessionRepository;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                
                var roles = await _userManager.GetRolesAsync(user);
                _logger.LogInformation("-----------------------");
                foreach (var item in roles)
                {
                    _logger.LogInformation(item);
                }
            }
            return View();
        }

        public async Task<IActionResult> MainPage()
        {
            ViewData["cCount"] = (await _candidateRepository.GetAll()).Count;
            ViewData["sCount"] = (await _studentRepository.GetAll()).Count;
            ViewData["oSess"] = (await _sessionRepository.GetActiveSessions()).Count;
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