using BusinessLayer.BackgroundServices.Queue;
using BusinessLayer.DTO;
using BusinessLayer.Enums;
using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.XmlProvider;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.Core
{
    public class Logger<TCategoryName> : ILogger<TCategoryName> where TCategoryName : class
    {
        private readonly IEventLogRepository _eventLogRepository;
        private readonly HostInformationDTO _hostInformation;
        private readonly LoggerSettingsDTO _loggerSettings;
        private readonly IXmlDataProvider<EventLog> _xmlEventLogRepository;
        private readonly IBackgroundTaskQueue _queue;
        private readonly bool _useQueue;

        public Logger(IEventLogRepository eventLogRepository,
                                IXmlDataProvider<EventLog> xmlEventLogRepository,
                                HostInformationDTO hostInformation,
                                LoggerSettingsDTO loggerSettings,
                                IBackgroundTaskQueue queue,
                                bool useQueue)
        {
            this._eventLogRepository = eventLogRepository;
            this._hostInformation = hostInformation;
            this._loggerSettings = loggerSettings;
            this._xmlEventLogRepository = xmlEventLogRepository;
            this._queue = queue;
            this._useQueue = useQueue;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            try
            {
                if (!this.IsEnabled(logLevel))
                {
                    return;//stop logging 
                }

                if (formatter == null)
                {
                    throw new ArgumentNullException(nameof(formatter));
                }

                var message = formatter(state, exception);
                if (string.IsNullOrEmpty(message))
                {
                    return;
                }

                if (exception != null)
                {
                    message += "\n" + exception.ToString();
                }

                int? maxMessageLength = GetMaxMessageLength();
                message = maxMessageLength != null && message.Length > maxMessageLength ? message.Substring(0, (int)maxMessageLength) : message;

                EventLog logEntity = new EventLog()
                {
                    Message = message,
                    CreatedDateTime = DateTime.UtcNow,
                    LogLevel = logLevel.ToString(),
                    Logger = this.ToString(),
                    EventId = eventId.Id,
                    Host = _hostInformation.Name,
                    Server = _hostInformation.Server
                };

                switch (this._loggerSettings.StorageType)
                {
                    case LoggerStorageTypes.Database:
                        if (this._useQueue)
                        {
                            this._queue.QueueBackgroundWorkItem(async token =>
                            {
                                this._eventLogRepository.Add(logEntity);
                                await Task.CompletedTask;
                            });
                        }
                        else
                        {
                            this._eventLogRepository.Add(logEntity);
                        }             
                        break;

                    case LoggerStorageTypes.Xml:
                        if (this._useQueue)
                        {
                            this._queue.QueueBackgroundWorkItem(async token =>
                            {
                                this._xmlEventLogRepository.Add(logEntity);
                                await Task.CompletedTask;
                            });
                        }
                        else
                        {
                            this._xmlEventLogRepository.Add(logEntity);
                        }   
                        break;

                    default:
                        if (this._useQueue)
                        {
                            this._queue.QueueBackgroundWorkItem(async token =>
                            {
                                this._eventLogRepository.Add(logEntity);
                                await Task.CompletedTask;
                            });
                        }
                        else
                        {
                            this._eventLogRepository.Add(logEntity);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        private int? GetMaxMessageLength()
        {
            int? maxLength = null;
            PropertyInfo[] props = typeof(EventLog).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    MaxLengthAttribute maxLengthAttr = attr as MaxLengthAttribute;
                    if (maxLengthAttr != null && prop.Name.Equals("Message"))
                    {
                        maxLength = maxLengthAttr.Length;
                    }
                }
            }

            return maxLength;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            bool enabled = _loggerSettings.TurnOn;
            if (!enabled) { return false; }
            enabled = _loggerSettings.LogLevel.Contains(logLevel.ToString());

            return enabled;
        }
    }
}
