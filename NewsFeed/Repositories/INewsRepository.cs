using NewsFeed.Models;
using NewsFeed.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsFeed.Repositories
{
    public interface INewsRepository
    {
        IEnumerable<New> News { get; }

        Task<IList<New>> GetAllNews();

        Task<IList<New>> GetNewsByCategory(int categoryID);

        IList<New> GetNewsBySubscription();

        Task<IList<New>> SearchNews(string text);

        Task AddNew(NewsViewModel _new);

    }
}
