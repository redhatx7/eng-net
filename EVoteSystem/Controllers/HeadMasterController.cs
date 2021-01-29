using System.Linq;
using System.Threading.Tasks;
using EVoteSystem.Models;
using EVoteSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EVoteSystem.Controllers
{
    [Authorize(Roles = ValidRoles.HeadMaster)]
    public class HeadMasterController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public HeadMasterController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        
        // GET
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return Json(new { name = User.Identity.Name, isAuth = User.Identity.IsAuthenticated, user = user });
        }
    }
}