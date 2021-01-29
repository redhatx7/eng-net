using System.Security.Claims;

namespace EVoteSystem.Extensions
{
    public static class FullnameClaimExtension
    {
        public static string GetFullname(this ClaimsPrincipal user)
        {
            var fullname = ((ClaimsIdentity) user.Identity)?.FindFirst("Fullname");
            if (fullname != null)
            {
                return fullname.Value;
            }
            return string.Empty;
        }
    }
}