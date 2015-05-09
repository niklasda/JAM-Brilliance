using System.Collections.Generic;
using System.Linq;

using Dapper;

using JAM.Core.Interfaces;
using JAM.Core.Interfaces.Admin;
using JAM.Core.Models;
using JetBrains.Annotations;

namespace JAM.Core.Services.Admin
{
    [UsedImplicitly]
    public class AbuseAdminDataService : AbuseDataService, IAbuseAdminDataService
    {
        public AbuseAdminDataService(IDatabaseConfigurationService configurationService)
            : base(configurationService)
        {
        }

        public IEnumerable<AbuseReport> GetAllAbuseReports()
        {
            const string sqlGetAbuseReports = "SELECT Abuse.*, OtherSurvey.Name AS OtherName, OtherSurvey.Email AS OtherEmail, SelfSurvey.Name AS SelfName FROM Abuse "
                                              + "INNER JOIN Surveys AS OtherSurvey ON OtherSurvey.SurveyId = Abuse.OtherSurveyId "
                                              + "INNER JOIN Surveys AS SelfSurvey ON SelfSurvey.SurveyId = Abuse.SelfSurveyId WHERE IsHandled = 0";
            using (var cn = Config.CreateConnection())
            {
                var fms = cn.Query<AbuseReport>(sqlGetAbuseReports);

                return fms;
            }
        }

        public int GetAbuseReportsCount()
        {
            const string sqlUnapprovedPictureCount = "SELECT COUNT(*) FROM Abuse WHERE IsHandled = 0";
            using (var cn = Config.CreateConnection())
            {
                int unapprovedPictureCount = cn.Query<int>(sqlUnapprovedPictureCount).SingleOrDefault();
                return unapprovedPictureCount;
            }
        }

        public bool DiscardAbuseReport(int abuseId)
        {
            const string sqlUpdateHandleAbuse = "UPDATE Abuse SET IsHandled = 1 WHERE AbuseId = @AbuseId AND IsHandled = 0";
            using (var cn = Config.CreateConnection())
            {
                int nbrOfRows = cn.Execute(sqlUpdateHandleAbuse, new { AbuseId = abuseId });
                return nbrOfRows == 1;
            }
        }

        public void DeleteOldHandledAbuse()
        {
            const string sqlDeleteOldHandledAbuse = "DELETE FROM Abuse WHERE IsHandled = 1 AND ReportDate < DATEADD(mm, -1, getdate())";
            using (var cn = Config.CreateConnection())
            {
                cn.Execute(sqlDeleteOldHandledAbuse);
            }
        }
    }
}