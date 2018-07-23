using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{
    public class UserRepository : EFRepository<User>, IUserRepository
    {
        public UserRepository(TestContext context)
            : base(context)
        {
            //inject other repositories instance here 
        }

        public List<User> GetAllUsers()
        {
            return this.DbSet.ToList();
        }

        public override void Add(User entity)
        {
            //custome add
        }
    }
}
