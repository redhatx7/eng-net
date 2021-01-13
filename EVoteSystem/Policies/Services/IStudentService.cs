using System.Security.Claims;

namespace EVoteSystem.Policies.Services
{
    public interface IStudentService
    {
        LoggedInUserType GetLoggedInUserType(ClaimsPrincipal user);
    }
}