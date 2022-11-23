using System.Security.Claims;

namespace Common.Extensions
{
    public static class ExtensionsMethods
    {
        /// <summary>
        /// User ID
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string getUserId(this ClaimsPrincipal user)
        {
            if(!user.Identity.IsAuthenticated) 
                return null;

            ClaimsPrincipal currentUser = user;
            return currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;    
        }
    }
}