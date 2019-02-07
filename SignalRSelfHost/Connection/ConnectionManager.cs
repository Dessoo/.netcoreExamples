using BusinessLayer.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SignalRSelfHost.Connection
{
    public class ConnectionManager : IConnectionManager
    {
        private ConcurrentDictionary<string, Dictionary<string, AuthEntity>> connections;

        private const string token = "this!is!test!Token";

        private const string exceptionMessage = "Token doesn't exist,please make sure your token is up to date!";

        private readonly ILogger<ConnectionManager> _logger;

        public ConnectionManager(ILogProvider logService)
        {
            this._logger = logService.CreateLogger<ConnectionManager>();
            connections = new ConcurrentDictionary<string, Dictionary<string, AuthEntity>>();
        }
        
        public void AddConnection(string hubName, string connectionId, AuthEntity authEntity)
        {
            try
            {
                this._logger.LogDebug($"Client with connectionId {connectionId} open chanel to hub {hubName}");

                if (!token.Equals(authEntity.AuthToken))
                {
                    throw new UnauthorizedAccessException(exceptionMessage);
                }

                if (connections != null)
                {
                    Dictionary<string, AuthEntity> entityList;
                    bool check = connections.TryGetValue(hubName, out entityList);
                    if (check)
                    {
                        entityList[connectionId] = authEntity;
                        connections.TryAdd(hubName, entityList);
                    }
                    else
                    {
                        Dictionary<string, AuthEntity> newList =
                                            new Dictionary<string, AuthEntity>();

                        newList.Add(connectionId, authEntity);
                        connections.TryAdd(hubName, newList);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                if (Environment.UserInteractive)
                {
                    throw ex;
                }
            }
        }

        public void DeleteConnection(string hubName, string connectionId)
        {
            try
            {
                Dictionary<string, AuthEntity> entityList;
                bool check = connections.TryGetValue(hubName, out entityList);
                if (check)
                {
                    entityList.Remove(connectionId);
                    connections.TryAdd(hubName, entityList);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                if (Environment.UserInteractive)
                {
                    throw ex;
                }     
            }
        }

        public List<string> GetActiveConnectionForHub(string hubName)
        {
            try
            {
                Dictionary<string, AuthEntity> entityList;
                bool check = connections.TryGetValue(hubName, out entityList);
                if (check)
                {
                    return entityList.Select(s => s.Key).ToList();
                }              
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                if (Environment.UserInteractive)
                {
                    throw ex;
                }
            }

            return null;
        }

        public List<string> GetActiveConnectionForUserPerHub(string hubName, string email)
        {
            try
            {
                Dictionary<string, AuthEntity> entityList;
                bool check = connections.TryGetValue(hubName, out entityList);
                if (check)
                {
                    return entityList.Where(w => w.Value.Email.Equals(email)).Select(s => s.Key).ToList();
                }            
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                if (Environment.UserInteractive)
                {
                    throw ex;
                }
            }

            return null;
        }
    }
}
