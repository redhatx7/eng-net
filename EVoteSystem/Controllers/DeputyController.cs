using Microsoft.AspNetCore.Mvc;

namespace EVoteSystem.Controllers
{
    public class DeputyController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}