using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsFeed.Models.Identity;
using NewsFeed.Models.ViewModels;
using NewsFeed.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsFeed.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        protected INewsRepository _newsRepository;
        protected ICategoriesRepository _categoriesRepository;
        protected IUsersRepository _usersRepository;
        protected ISubscriptionsRepository _subscriptionsRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public NewsController(
           UserManager<ApplicationUser> userManager,
           INewsRepository newsRepository,
           ICategoriesRepository categoriesRepository,
           IUsersRepository usersRepository,
           ISubscriptionsRepository subscriptionsRepository)
        {
            _userManager = userManager;
            _newsRepository = newsRepository;
            _categoriesRepository = categoriesRepository;
            _usersRepository = usersRepository;
            _subscriptionsRepository = subscriptionsRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllNews()
        {
            var newsList = _newsRepository.GetAllNews().Result.Select(x => new NewsViewModel()
            {
                NewID = x.NewID,
                Title = x.Title,
                Content = x.Content,
                AuthorName = _usersRepository.GetUser(x.AuthorUserName).Result.FullName,
                Created = x.Created,
                Category = _categoriesRepository.GetCategory(x.CategoryID).Result.Name
            }).ToList();

            return PartialView("_Feed", newsList);
        }

        public IActionResult GetSubscriptionsNews()
        {
            try
            {
                var _userName = User.Identity.Name;

                var _subscriptionNews = _subscriptionsRepository.GetSubscriptions(_userName).ToList()
                    .SelectMany(n => n.category.News).OrderByDescending(w => w.Created).ToList();

                var newsList = _subscriptionNews.Select(x => new NewsViewModel()
                {
                    NewID = x.NewID,
                    Title = x.Title,
                    Content = x.Content,
                    AuthorName = _usersRepository.GetUser(x.AuthorUserName).Result.FullName,
                    Created = x.Created,
                    Category = _categoriesRepository.GetCategory(x.CategoryID).Result.Name
                }).ToList();

                return PartialView("_Feed", newsList);

            }
            catch (Exception ex)
            {
                return PartialView("_Feed", new List<NewsViewModel>());

            }
        }

        public async Task<IActionResult> Search(string searchText)
        {
            try
            {
                var newsList = _newsRepository.SearchNews(searchText).Result.Select(x => new NewsViewModel()
                {
                    NewID = x.NewID,
                    Title = x.Title,
                    Content = x.Content,
                    AuthorName = _usersRepository.GetUser(x.AuthorUserName).Result.FullName,
                    Created = x.Created,
                    Category = _categoriesRepository.GetCategory(x.CategoryID).Result.Name
                }).ToList();

                return PartialView("_Feed", newsList);

            }
            catch (Exception)
            {
                return Content("The search was not completed");
            }
        }

        [Authorize(Roles = "Publisher")]
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNews([FromBody] NewsViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_FormResult", new ResultViewModel()
                    {
                        Success = false,
                        Message = "The news couldn't be added"
                    });
                }

                await _newsRepository.AddNew(model);

                return PartialView("_FormResult", new ResultViewModel()
                {
                    Success = true,
                    Message = "The news has been added"
                });
            }
            catch (Exception ex)
            {
                return PartialView("_FormResult", new ResultViewModel()
                {
                    Success = false,
                    Message = "The news couldn't be added. Internal error"
                });

            }
        }
    }
}