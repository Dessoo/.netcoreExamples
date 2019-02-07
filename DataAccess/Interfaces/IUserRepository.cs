using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();

        List<User> GetAllUsersParallel();

        void Add(User user);

        User GetById(int id);

        void Update(User user);

        void Delete(User user);
    }
}
