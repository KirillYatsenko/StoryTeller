using System.Security.Claims;
using System.Security.Principal;

namespace StoryTeller.Common
{
    public static class UserExtensions
    {
        public static string GetStoryTellerName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("StoryTellerName");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}