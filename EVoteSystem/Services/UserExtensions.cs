using System.Security.Claims;
using System.Threading.Tasks;
using EVoteSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace EVoteSystem.Services
{
    public static class UserExtensions
    {
        public static async Task<string> GetProfileImage(this ClaimsPrincipal principal, UserManager<ApplicationUser> user)
        {
            if (principal.Identity.IsAuthenticated)
            {
                var currentUser = await user.GetUserAsync(principal);
                return currentUser.PersonalImagePath;
            }
            return string.Empty;
        }
        
        public static async Task<string> GetFullname(this ClaimsPrincipal principal, UserManager<ApplicationUser> user)
        {
            if (principal.Identity.IsAuthenticated)
            {
                var currentUser = await user.GetUserAsync(principal);
                return currentUser.Fullname;
            }
            return string.Empty;
        }
    }
}