using Microsoft.AspNetCore.Identity;

namespace NewsFeed.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

    }
}
