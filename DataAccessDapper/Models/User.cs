using System.Collections.Generic;
using DataAccessDapper.Attributes;

namespace DataAccessDapper.Models
{
    public partial class User
    {
        [AutoIncrement(true)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> GetPrimaryKeys()
        {
            return new List<string>() { "id" };
        }
    }
}
