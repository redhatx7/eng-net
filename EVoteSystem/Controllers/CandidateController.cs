using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EVoteSystem.Controllers
{
    [Authorize(Roles = "Candidate", Policy = "CandidateLogin")]
    public class CandidateController : Controller
    {
        public IActionResult Index()
        {
            return Json("Hello This is candidate");
        }
    }
}