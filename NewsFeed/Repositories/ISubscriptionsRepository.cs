using NewsFeed.Models;
using System.Collections.Generic;

namespace NewsFeed.Repositories
{
    public interface ISubscriptionsRepository
    {
        bool AddSubscription(int categoryId, string userId);

        bool RemoveSubscription(int categoryId, string userId);

        bool IsUserSubscribed(int categoryId, string userName);

        IList<Subscription> GetSubscriptions(string userId);
    }
}
