using Microsoft.EntityFrameworkCore;
using NewsFeed.Models;
using NewsFeed.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsFeed.Repositories
{
    public class SubscriptionsRepository : ISubscriptionsRepository
    {
        public readonly ApplicationDbContext dbContext;

        public SubscriptionsRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public bool AddSubscription(int categoryID, string userName)
        {
            try
            {
                dbContext.Subscriptions.Add(new Subscription()
                {
                    CategoryID = categoryID,
                    Subscriber = userName,
                    Created = DateTime.Now
                });

                dbContext.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveSubscription(int categoryID, string userName)
        {
            try
            {
                dbContext.Subscriptions.Remove(dbContext.Subscriptions.Where(x => x.Subscriber == userName && x.CategoryID == categoryID).FirstOrDefault());
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<Subscription> GetSubscriptions(string userName)
        {
            return dbContext.Subscriptions.Include(c => c.category).ThenInclude(m => m.News).Where(x => x.Subscriber == userName).ToList();
        }

        public bool IsUserSubscribed(int categoryId, string userName)
        {
            return dbContext.Subscriptions.Any(x => x.CategoryID == categoryId && x.Subscriber == userName);
        }
    }
}
