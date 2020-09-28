using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewsFeed.Models;
using NewsFeed.Repositories;
using System.Collections.Generic;
using System.Diagnostics;

namespace NewsFeed.Controllers
{
    public class HomeController : Controller
    {
        protected ICategoriesRepository _categoriesRepository;

        public HomeController(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<Category> allCategories = _categoriesRepository.GetAllCategories();
            List<SelectListItem> _categories = new List<SelectListItem>();

            foreach (var category in allCategories)
            {
                _categories.Add(new SelectListItem { Value = category.CategoryID.ToString(), Text = category.Name });
            }

            ViewData["Categories"] = _categories;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

