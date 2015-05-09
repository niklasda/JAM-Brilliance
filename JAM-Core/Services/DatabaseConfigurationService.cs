using System.Data;
using System.Data.SqlClient;

using JAM.Core.Interfaces;
using JetBrains.Annotations;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;

namespace JAM.Core.Services
{
    [UsedImplicitly]
    public class DatabaseConfigurationService : IDatabaseConfigurationService
    {
        private readonly string _connString;

        public DatabaseConfigurationService(string connString)
        {
            _connString = connString;
        }

        public IDbConnection CreateConnection()
        {
            var cn = new SqlConnection(_connString);
            var c = new ProfiledDbConnection(cn, MiniProfiler.Current);
            return c;
        }
    }
}