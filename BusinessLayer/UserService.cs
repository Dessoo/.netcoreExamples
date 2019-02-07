using BusinessLayer.BackgroundServices.Queue;
using BusinessLayer.Core;
using BusinessLayer.DTO;
using BusinessLayer.Interfaces;
using DataAccess.Interfaces;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Models;

namespace BusinessLayer
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly IBackgroundTaskQueue _queue;

        public UserService(IUserRepository userRepository, ILogProvider logProvider, IBackgroundTaskQueue queue)
        {
            this._userRepository = userRepository;
            this._logger = logProvider.CreateLogger<UserService>();
            this._queue = queue;
        }

        public List<UserDTO> GetAllUsers()
        {
            this._logger.LogDebug("test from user service");

            return this._userRepository.GetAllUsers().Select(s => new UserDTO()
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName
            }).ToList();
        }

        public List<UserDTO> GetAllUsersParallel()
        {
            this._logger.LogDebug("test from user service");

            return this._userRepository.GetAllUsersParallel().Select(s => new UserDTO()
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName
            }).ToList();
        }

        public void AddUser(UserDTO user)
        {
            this._userRepository.Add(new User(){ FirstName = user.FirstName, LastName = user.LastName });
        }

        public void UpdateUser(UserDTO user)
        {
            var userEntity = this._userRepository.GetById(user.Id);
            userEntity.FirstName = user.FirstName;
            userEntity.LastName = user.LastName;

            this._userRepository.Update(userEntity);
        }

        public void DeleteUser(UserDTO user)
        {
            var userEntity = this._userRepository.GetById(user.Id);
            this._userRepository.Delete(userEntity);
        }
    }
}
