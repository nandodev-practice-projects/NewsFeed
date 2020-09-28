using NewsFeed.Models;
using NewsFeed.Models.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsFeed.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        public readonly ApplicationDbContext dbContext;

        public CategoriesRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return dbContext.Categories.ToList();
        }

        public async Task<Category> GetCategory(int categoryId)
        {
            return dbContext.Categories.Find(categoryId);
        }
    }
}
