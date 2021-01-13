using EVoteSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EVoteSystem.Controllers
{
    [Authorize(Roles = "Student", Policy = "StudentLogin")]
    public class StudentController : Controller
    {
        private ILogger<StudentController> _logger;

        private StudentRepository _studentRepository;

        public StudentController(ILogger<StudentController> logger, StudentRepository studentRepository)
        {
            _logger = logger;
            _studentRepository = studentRepository;
        }
        
        public IActionResult Index()
        {
            return Json("Hello this is student");
        }
    }
}