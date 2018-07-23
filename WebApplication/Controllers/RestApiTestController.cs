using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Core;
using BusinessLayer.DTO;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [Produces("application/json")]
    public class RestApiTestController : BaseController
    {
        private readonly IUserService _userService;

        public RestApiTestController(IUserService userService, ILogProvider logProvider) : base(logProvider)
        {
            this._userService = userService;
        }

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns> List of User Entity</returns>
        [Route("api/RestApiTest/GetAllUsers")]
        [HttpGet]
        public List<UserDTO> GetAllUsers()
        {
            return this._userService.GetAllUsers().Take(10).ToList();  
        }
    }
}