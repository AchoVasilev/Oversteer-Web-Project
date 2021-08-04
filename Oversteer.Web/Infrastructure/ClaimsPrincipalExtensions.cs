namespace Oversteer.Web.Infrastructure
{
    using System.Security.Claims;

    using Oversteer.Web.Data.Constants;

    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(WebConstants.AdministratorRoleName);
    }
}
