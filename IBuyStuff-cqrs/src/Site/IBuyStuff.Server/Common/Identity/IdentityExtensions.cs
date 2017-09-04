using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace IBuyStuff.Server.Common.Identity
{
    public static class IdentityExtensions
    {
        public static string Email(this ClaimsIdentity identity)
        {
            return identity.Claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).SingleOrDefault();
        }

        public static string Avatar(this ClaimsIdentity identity)
        {
            return identity.Claims.Where(c => c.Type == IdentityHelpers.IBuyStuff_Avatar).Select(c => c.Value).SingleOrDefault();
        }

        public static ClaimsIdentity AsClaimsIdentity(this IIdentity identity)
        {
            return (ClaimsIdentity) identity;
        }
    }
}