using BusinessLayer.Core;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class BaseController : Controller
    {
        private readonly ILogger _logger;

        public BaseController(ILogProvider logProvider)
        {
            this._logger = logProvider.CreateLogger<BaseController>();
        }

        public IActionResult Error()
        {
            Exception ex = HttpContext.Features.Get<IExceptionHandlerPathFeature>().Error;
            this._logger.LogCritical(ex, ex.Message);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
