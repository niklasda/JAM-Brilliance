using System.Collections.Generic;
using JAM.Core.Models;

namespace JAM.Core.Interfaces.Admin
{
    public interface IAbuseAdminDataService : IAbuseDataService
    {
        IEnumerable<AbuseReport> GetAllAbuseReports();

        int GetAbuseReportsCount();

        bool DiscardAbuseReport(int abuseId);

        void DeleteOldHandledAbuse();
    }
}