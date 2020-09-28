using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NewsFeed.Models;
using NewsFeed.Models.Data;
using NewsFeed.Models.Identity;
using NewsFeed.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsFeed.Repositories
{
    public class NewsRepository : INewsRepository
    {
        
        private Func<ApplicationDbContext> _contextFactory;
        public IUserService userService;

        public NewsRepository(Func<ApplicationDbContext> context, IUserService _userService)
        {
            _contextFactory = context;
            userService = _userService;
        }

        public IEnumerable<New> News => GetNewsBySubscription();

        public async Task<IList<New>> GetAllNews()
        {
            using (var dbContext = _contextFactory.Invoke())
            {
                return await dbContext.News.OrderByDescending(x => x.Created).ToListAsync();
            }
        }

        public async Task<IList<New>> GetNewsByCategory(int categoryId)
        {
            using (var dbContext = _contextFactory.Invoke())
            {
                return await dbContext.News.Where(x => x.CategoryID == categoryId).ToListAsync();
            }
        }

        public IList<New> GetNewsBySubscription()
        {
            var _user = userService.GetUser();
            IList<New> _subscriptionNews = new List<New>();

            if (_user != null)
            {
                using (var dbContext = _contextFactory.Invoke())
                {
                    _subscriptionNews = dbContext.Subscriptions.Include(c => c.category).ThenInclude(m => m.News).Where(x => x.Subscriber == _user.Identity.Name).ToList()
                    .SelectMany(n => n.category.News).OrderByDescending(w => w.Created).ToList();
                }
            }

            return _subscriptionNews;
        }

        public async Task<IList<New>> SearchNews(string text)
        {
            try
            {
                using (var dbContext = _contextFactory.Invoke())
                {
                    return await dbContext.News.Where(x => x.Title.Contains(text)).OrderByDescending(x => x.Created).ToListAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task AddNew(NewsViewModel _new)
        {
            try
            {
                var _NewAdd = new New()
                {
                    Title = _new.Title,
                    Content = _new.Content,
                    AuthorUserName = userService.GetUser().Identity.Name,
                    Created = DateTime.Now,
                    CategoryID = Convert.ToInt32(_new.CategoryID)
                };

                using (var dbContext = _contextFactory.Invoke())
                {
                    dbContext.News.Add(_NewAdd);
                    dbContext.SaveChanges();
                }

                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
