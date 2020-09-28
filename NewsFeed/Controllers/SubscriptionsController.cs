using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsFeed.Models.Identity;
using NewsFeed.Repositories;
using System;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewsFeed.Controllers
{
    public class SubscriptionsController : Controller
    {
        protected ICategoriesRepository _categoriesRepository;
        protected IUsersRepository _usersRepository;
        protected ISubscriptionsRepository _subscriptionsRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public SubscriptionsController(
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

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> Subscribe(int categoryID)
        {
            try
            {
                var _userName = User.Identity.Name;

                _subscriptionsRepository.AddSubscription(categoryID, _userName);

                Response.StatusCode = (int)HttpStatusCode.OK;

                return Json(new
                {
                    success = true,
                    statusCode = Response.StatusCode,
                    responseText = "Subscription completed",
                });
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                return Json(new
                {
                    success = true,
                    statusCode = Response.StatusCode,
                    responseText = "Subscription not completed",
                });
            }
        }

        public async Task<JsonResult> Unsubscribe(int categoryID)
        {
            try
            {
                var _userName = User.Identity.Name;

                _subscriptionsRepository.RemoveSubscription(categoryID, _userName);

                Response.StatusCode = (int)HttpStatusCode.OK;

                return Json(new
                {
                    success = true,
                    statusCode = Response.StatusCode,
                    responseText = "Unsubscription completed",
                });
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                return Json(new
                {
                    success = true,
                    statusCode = Response.StatusCode,
                    responseText = "Unsubscription not completed",
                });
            }
        }
    }
}
