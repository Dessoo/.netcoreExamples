using BusinessLayer.DTO;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface IUserService
    {
        List<UserDTO> GetAllUsers();
    }
}
