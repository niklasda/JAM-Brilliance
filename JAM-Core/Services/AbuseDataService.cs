using System.Linq;

using Dapper;
using JAM.Core.Interfaces;
using JAM.Core.Models;
using JetBrains.Annotations;

namespace JAM.Core.Services
{
    [UsedImplicitly]
    public class AbuseDataService : IAbuseDataService
    {
        protected readonly IDatabaseConfigurationService Config;

        public AbuseDataService(IDatabaseConfigurationService configurationService)
        {
            Config = configurationService;
        }

        public void ReportSurveyAbuse(AbuseReport arm)
        {
            const string sqlAbuseReportExists = "SELECT AbuseId FROM Abuse WHERE IsHandled = 0 AND SelfSurveyId  = @SelfSurveyId AND OtherSurveyId = @OtherSurveyId AND MessageId IS NULL AND PictureId IS NULL";
            const string sqlInsertAbuseReport = "INSERT INTO Abuse (SelfSurveyId, OtherSurveyId) VALUES (@SelfSurveyId, @OtherSurveyId)";
            using (var cn = Config.CreateConnection())
            {
                int abuseId = cn.Query<int>(sqlAbuseReportExists, arm).FirstOrDefault();
                if (abuseId == 0)
                {
                    cn.Execute(sqlInsertAbuseReport, arm);
                }
            }
        }

        public void ReportMessageAbuse(AbuseReport arm)
        {
            const string sqlAbuseReportExists = "SELECT AbuseId FROM Abuse WHERE IsHandled = 0 AND SelfSurveyId  = @SelfSurveyId AND OtherSurveyId = @OtherSurveyId AND MessageId = @MessageId AND PictureId IS NULL";
            const string sqlInsertAbuseReport = "INSERT INTO Abuse (SelfSurveyId, OtherSurveyId, MessageId) VALUES (@SelfSurveyId, @OtherSurveyId, @MessageId)";
            using (var cn = Config.CreateConnection())
            {
                int abuseId = cn.Query<int>(sqlAbuseReportExists, arm).FirstOrDefault();
                if (abuseId == 0)
                {
                    cn.Execute(sqlInsertAbuseReport, arm);
                }
            }
        }

        public void ReportPictureAbuse(AbuseReport arm)
        {
            const string sqlAbuseReportExists = "SELECT AbuseId FROM Abuse WHERE IsHandled = 0 AND SelfSurveyId  = @SelfSurveyId AND OtherSurveyId = @OtherSurveyId AND PictureId = @PictureId AND MessageId IS NULL";
            const string sqlInsertAbuseReport = "INSERT INTO Abuse (SelfSurveyId, OtherSurveyId, PictureId) VALUES (@SelfSurveyId, @OtherSurveyId, @PictureId)";
            using (var cn = Config.CreateConnection())
            {
                int abuseId = cn.Query<int>(sqlAbuseReportExists, arm).FirstOrDefault();
                if (abuseId == 0)
                {
                    cn.Execute(sqlInsertAbuseReport, arm);
                }
            }
        }
    }
}