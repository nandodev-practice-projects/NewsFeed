using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewsFeed.Models.Identity
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor accessor;

        public UserService(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public ClaimsPrincipal GetUser()
        {
            return accessor?.HttpContext?.User as ClaimsPrincipal;
        }
    }
}
