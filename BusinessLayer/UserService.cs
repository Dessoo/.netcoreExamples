using BusinessLayer.Core;
using BusinessLayer.DTO;
using BusinessLayer.Interfaces;
using DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogProvider logProvider)
        {
            this._userRepository = userRepository;
            this._logger = logProvider.CreateLogger<UserService>();
        }

        public List<UserDTO> GetAllUsers()
        {
            this._logger.LogDebug($"this is test debug message from {nameof(this.GetAllUsers)} method");
            return this._userRepository.GetAllUsers().Select(s => new UserDTO() {
                    Id =  s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName}).ToList();
        }
    }
}
