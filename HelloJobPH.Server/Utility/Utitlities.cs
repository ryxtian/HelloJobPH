using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HelloJobPH.Server.Utility
{
    public static class Utilities
    {
        private static IHttpContextAccessor? _httpContextAccessor;

        // Method to set the HttpContextAccessor (called once from Program.cs)
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private static HttpContext? HttpContext => _httpContextAccessor?.HttpContext;

        // ✅ Get the current user ID from Claims
        public static int? GetUserId()
        {
            var userIdClaim = HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userIdClaim, out var userId))
                return userId;

            return null;
        }

        // ✅ Example: Get user email
        public static string? GetUserEmail()
        {
            return HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
        }

        // ✅ Example: Get username
        public static string? GetUsername()
        {
            return HttpContext?.User?.Identity?.Name;
        }
    }
}
