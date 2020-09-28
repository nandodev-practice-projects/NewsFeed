using NewsFeed.Models.Identity;
using System.Threading.Tasks;

namespace NewsFeed.Repositories
{
    public interface IUsersRepository
    {
        Task<ApplicationUser> GetUser(string userName);

    }
}
