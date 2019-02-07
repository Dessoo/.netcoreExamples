using BusinessLayer.Core;
using BusinessLayer.DTO;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ServiceInstallTestAppMVCEndPoint.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<TestController> _logger;

        public TestController(IUserService userService, ILogProvider logProvider)
        {
            this._userService = userService;
            this._logger = logProvider.CreateLogger<TestController>();
        }

        // GET api/Test/GetAllUsers
        [HttpGet("GetAllUsers")]
        public ActionResult<UserDTO> GetAllUsers()
        {
            this._logger.LogDebug($"Invoke selfhost api, method {nameof(this.GetAllUsers)}");
            return Ok(this._userService.GetAllUsers());
        }

        [HttpPost("PostAddUser")]
        public void PostAddUser(UserDTO user)
        {
            this._logger.LogDebug($"Invoke selfhost api, method {nameof(this.PostAddUser)}");
            this._userService.AddUser(user);
        }
    }
}
