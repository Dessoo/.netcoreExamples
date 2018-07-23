using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.XmlProvider
{
    public interface IXmlDataProvider<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
    }
}
