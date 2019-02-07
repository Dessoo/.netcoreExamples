using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessDapper.Core
{
    public interface IDapperRepositoryT<TEntity> where TEntity : class
    {
        TEntity GetByIds(Dictionary<string, string> Ids);

        void Delete(Dictionary<string, string> keyValues);
    }
}
