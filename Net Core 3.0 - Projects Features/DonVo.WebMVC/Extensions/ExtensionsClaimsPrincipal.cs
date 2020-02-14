using System.Security.Claims;

namespace DonVo.WebMVC.Extensions
{
    public static class ExtensionsClaimsPrincipal
    {
        public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Identity.Name;
        }
    }
}
