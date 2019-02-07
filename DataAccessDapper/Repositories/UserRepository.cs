using DataAccessDapper.Core;
using DataAccessDapper.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccessDapper.Repositories
{
    public class UserRepository : DapperRepositoryT<User>, IUserRepository
    {
        public UserRepository(IDbConnection connection, string tableName, List<string> keys) : base(connection, tableName, keys)
        {

        }
    }
}
