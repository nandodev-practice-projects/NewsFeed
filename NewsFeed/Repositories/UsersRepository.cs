using NewsFeed.Models.Data;
using NewsFeed.Models.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace NewsFeed.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        public readonly ApplicationDbContext dbContext;

        public UsersRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }


        public async Task<ApplicationUser> GetUser(string userName)
        {
            return dbContext.ApplicationUsers.FirstOrDefault(x => x.UserName.Equals(userName));
        }
    }
}
