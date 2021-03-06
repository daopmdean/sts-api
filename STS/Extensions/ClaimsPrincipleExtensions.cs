using System;
using System.Security.Claims;

namespace STS.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static string GetRoleName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Role)?.Value;
        }

        public static string GetBrandId(this ClaimsPrincipal user)
        {
            return user.FindFirst("brandId")?.Value;
        }

        public static string GetStoreId(this ClaimsPrincipal user)
        {
            return user.FindFirst("storeId")?.Value;
        }
    }
}
