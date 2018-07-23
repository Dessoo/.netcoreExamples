using Microsoft.Extensions.Logging;

namespace BusinessLayer.Core
{
    public class BaseBusinessService<T> : IBaseBusinessService<T> where T : class
    {
        //private readonly ILoggerFactory _loggerFactory;
        ////abstract ??
        //public BaseBusinessService(ILoggerFactory loggerFactory)
        //{
        //    this._loggerFactory = loggerFactory;
        //}

        //public ILoggerFactory LoggerFactory()
        //{
        //    return this._loggerFactory;
        //}
    }
}
