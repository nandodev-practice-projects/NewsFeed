using NewsFeed.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsFeed.Repositories
{
    public interface ICategoriesRepository
    {
        public IEnumerable<Category> GetAllCategories();

        Task<Category> GetCategory(int categoryId);

    }
}
