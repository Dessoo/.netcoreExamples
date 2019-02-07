using Dapper;
using DataAccessDapper.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DataAccessDapper.Core
{
    public class DapperRepositoryT<TEntity>
        : IDapperRepositoryT<TEntity> where TEntity : class
    {
        private readonly IDbConnection _connection;
        private readonly string _tableName;
        private readonly List<string> _keys;

        public DapperRepositoryT(IDbConnection connection, string tableName, List<string> keys)
        {
            this._connection = connection;
            this._connection.Open();

            this._tableName = tableName;
            this._keys = keys;
        }

        public TEntity GetByIds(Dictionary<string,string> keyValues)
        {
            string query = $"select * from [{this._tableName}] where {GetWhereClause()}";
            return this._connection.Query<TEntity>(query, GetKeys(keyValues)).FirstOrDefault();
        }

        public void Delete(Dictionary<string, string> keyValues)
        {
            string query = $"delete from [{this._tableName}] where {GetWhereClause()}";
            this._connection.Query(query, GetKeys(keyValues));
        }

        public void Add(TEntity entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"insert into [{this._tableName}] (");
            foreach (PropertyInfo info in entity.GetType().GetProperties())
            {

                var test = info.CustomAttributes.FirstOrDefault(f => f.AttributeType.FullName == "DataAccessDapper.Attributes.AutoIncrement");
                sb.Append(info.Name);
            }   
            
        }

        private string GetWhereClause()
        {
            string where = string.Empty;
            foreach (string key in this._keys)
            {
                where += $"{key} = @{key} AND";
            }

            return where.Substring(0, where.Length - 3);
        }

        private DynamicParameters GetKeys(Dictionary<string, string> keyValues)
        {
            var parameters = new DynamicParameters();
            foreach (KeyValuePair<string, string> entry in keyValues)
            {
                parameters.Add(entry.Key, entry.Value);         
            }

            return parameters;
        }
    }
}
