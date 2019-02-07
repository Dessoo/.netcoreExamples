using DataAccessDapper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessDapper.Repositories
{
    public interface IUserRepository
    {
        User GetByIds(Dictionary<string, string> Ids);

        void Delete(Dictionary<string, string> keyValues);

        void Add(User user);
    }
}
