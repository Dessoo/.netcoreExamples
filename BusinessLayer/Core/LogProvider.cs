using BusinessLayer.BackgroundServices.Queue;
using BusinessLayer.DTO;
using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.XmlProvider;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace BusinessLayer.Core
{
    /// <summary>
    /// DUPLICATE this class for other storage providers like XML
    /// </summary>
    public class LogProvider : ILogProvider
    {
        private readonly IEventLogRepository _eventLogRepository;
        private readonly HostInformationDTO _hostInformation;
        private readonly LoggerSettingsDTO _loggerSettings;
        private readonly IXmlDataProvider<EventLog> _xmlEventLogRepository;

        public LogProvider(IEventLogRepository eventLogRepository,
                                        IXmlDataProvider<EventLog> xmlEventLogRepository,
                                        HostInformationDTO hostInformation,
                                        LoggerSettingsDTO loggerSettings)
        {
            this._eventLogRepository = eventLogRepository;
            this._hostInformation = hostInformation;
            this._loggerSettings = loggerSettings;
            this._xmlEventLogRepository = xmlEventLogRepository;
        }

        public ILogger<TCategoryName> CreateLogger<TCategoryName>() where TCategoryName : class
        {
            ILogger<TCategoryName> logger = new Logger<TCategoryName>(this._eventLogRepository, this._xmlEventLogRepository, this._hostInformation, this._loggerSettings);
            return logger;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
