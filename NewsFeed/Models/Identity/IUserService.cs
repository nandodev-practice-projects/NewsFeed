using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewsFeed.Models.Identity
{
    public interface IUserService
    {
        ClaimsPrincipal GetUser();
    }
}
