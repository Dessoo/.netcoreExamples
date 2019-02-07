using BusinessLayer.BackgroundServices.Queue;
using BusinessLayer.Core;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace WebApplication.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ICacheService _mycache;
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public HomeController(ICacheService mycache, IUserService userService, ILogProvider logProvider) : base(logProvider)
        {
            //INJECTING LoggerFactory INTO MVC CONTROLLERS DOESNT WORK !!! .Net Core Bug fix ?? Build at 2.0
            this._mycache = mycache;
            this._userService = userService;
            logProvider.UseQueue = false;
            this._logger = logProvider.CreateLogger<HomeController>();
        }
        public IActionResult Index()
        {
            ViewData["UserCount"] = this._userService.GetAllUsersParallel().Count;
            ViewData["CacheTest"] = this._mycache.GetByKey(Infrastructure.Constants.keyCache);

            this._logger.LogDebug($"test message from Index Action");

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            this._mycache.SetCache(Infrastructure.Constants.keyCache, 888);
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            this._mycache.SetCache(Infrastructure.Constants.keyCache, 777);

            return View();
        }
    }
}
