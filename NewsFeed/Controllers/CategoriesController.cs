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
    public class CategoriesController : Controller
    {
        protected ICategoriesRepository _categoriesRepository;
        protected IUsersRepository _usersRepository;
        protected ISubscriptionsRepository _subscriptionsRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CategoriesController(
           UserManager<ApplicationUser> userManager,
           ICategoriesRepository categoriesRepository,
           IUsersRepository usersRepository,
           ISubscriptionsRepository subscriptionsRepository)
        {
            _userManager = userManager;
            _categoriesRepository = categoriesRepository;
            _usersRepository = usersRepository;
            _subscriptionsRepository = subscriptionsRepository;
        }

        public async Task<IActionResult> GetAll()
        {
            try
            {
                var _userName = User.Identity.Name;

                var _categories = _categoriesRepository.GetAllCategories();

                var categoriesList = _categories.Select(x => new CategoriesViewModel()
                {
                    CategoryID = x.CategoryID,
                    Name = x.Name,
                    CurrentUserSubscribed = _subscriptionsRepository.IsUserSubscribed(x.CategoryID, _userName)
                }).ToList();

                return PartialView("_Categories", categoriesList);

            }
            catch (Exception)
            {
                return PartialView("_Categories", new List<CategoriesViewModel>());
            }
        }
    }
}
