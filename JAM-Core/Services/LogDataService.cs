using System;
using System.Collections.Generic;

using Dapper;

using JAM.Core.Interfaces;
using JAM.Core.Logic;
using JAM.Core.Models;
using JetBrains.Annotations;
using NLog;

namespace JAM.Core.Services
{
    [UsedImplicitly]
    public class LogDataService : ILogDataService
    {
        private readonly IDatabaseConfigurationService _c;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public LogDataService(IDatabaseConfigurationService configurationService)
        {
            _c = configurationService;
            _logger.Trace("Logger instantiated");
        }

        public void LogIpForLogin(string ip, string userName, bool success)
        {
            if (success)
            {
                _logger.Info("User, {0}, successfully logged in from {1}{2}", userName, Constants.IpPrefix, ip);
            }
            else
            {
                _logger.Info("User, {0}, failed to log in from {1}{2}", userName, Constants.IpPrefix, ip);
            }
        }

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }

        public void LogError(string message, Exception ex)
        {
            _logger.WarnException(message, ex);
        }

        //public async void LogIpForLogin(string ip, string userName, bool success)
        //{
        //    if (!ip.Equals("::1") && !ip.Equals("127.0.0.1"))
        //    {
        //        const string sqlInsertNewIpLog = "INSERT INTO IPLog (IP, UserName, Success) VALUES (@IP, @UserName, @Success)";

        //        using (var cn = _c.CreateConnection())
        //        {
        //            await cn.ExecuteAsync(sqlInsertNewIpLog, new { IP = ip, UserName = userName, Success = success });
        //        }
        //    }
        //}

        public IEnumerable<LogEntry> GetSortedLogEntries()
        {
            const string sqlGetIpLog = "SELECT * FROM NLog ORDER BY EventDateTime DESC";

            using (var cn = _c.CreateConnection())
            {
                return cn.Query<LogEntry>(sqlGetIpLog);
            }
        }

        public void DeleteOldLogEntries()
        {
            const string sqlDeleteOldIpLog = "DELETE FROM NLog WHERE EventDateTime < DATEADD(mm, -1, getdate())";

            using (var cn = _c.CreateConnection())
            {
                cn.ExecuteAsync(sqlDeleteOldIpLog);
            }
        }
    }
}