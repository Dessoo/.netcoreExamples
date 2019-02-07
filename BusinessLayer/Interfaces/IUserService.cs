using BusinessLayer.DTO;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface IUserService
    {
        List<UserDTO> GetAllUsers();

        List<UserDTO> GetAllUsersParallel();

        void AddUser(UserDTO user);

        void UpdateUser(UserDTO user);

        void DeleteUser(UserDTO user);
    }
}
