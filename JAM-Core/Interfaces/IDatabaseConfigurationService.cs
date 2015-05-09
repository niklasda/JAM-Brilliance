using System.Data;

namespace JAM.Core.Interfaces
{
    public interface IDatabaseConfigurationService
    {
        IDbConnection CreateConnection();
    }
}