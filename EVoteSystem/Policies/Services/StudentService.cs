using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace EVoteSystem.Policies.Services
{
    public class StudentService : IStudentService
    {
        private readonly IHttpContextAccessor _httpContext;

        public StudentService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public LoggedInUserType GetLoggedInUserType(ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
            {
                return LoggedInUserType.Nothing;
            }
            if (_httpContext.HttpContext?.Request.Cookies["UserType"] == "Student")
            {
                return LoggedInUserType.Student;
            }
            else if(_httpContext.HttpContext?.Request.Cookies["UserType"] == "Candidate")
            {
                return LoggedInUserType.Candidate;
            }

            return LoggedInUserType.Nothing;
        }
    }
}