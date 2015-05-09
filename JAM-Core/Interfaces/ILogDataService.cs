using System;
using System.Collections.Generic;

using JAM.Core.Models;

namespace JAM.Core.Interfaces
{
    public interface ILogDataService
    {
        void LogIpForLogin(string ip, string userName, bool success);

        IEnumerable<LogEntry> GetSortedLogEntries();

        void DeleteOldLogEntries();
        void LogInfo(string message);
        void LogError(string message, Exception ex);
    }
}