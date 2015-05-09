using System.Collections.Generic;
using System.Linq;
using Dapper;
using JAM.Core.Interfaces;
using JAM.Core.Models;
using JetBrains.Annotations;

namespace JAM.Core.Services
{
    [UsedImplicitly]
    public class VisitorDataService : IVisitorDataService
    {
        private readonly IDatabaseConfigurationService _c;

        public VisitorDataService(IDatabaseConfigurationService configurationService)
        {
            _c = configurationService;
        }

        public IEnumerable<HistoryEntry> GetVisitors(int surveyId)
        {
            const string sqlGetVisitors = "SELECT History.*, Surveys.Name AS SelfName, Surveys.IsDisabled FROM History "
                                          + "INNER JOIN Surveys ON Surveys.SurveyId = History.SelfSurveyId WHERE OtherSurveyId = @OtherSurveyId";
            using (var cn = _c.CreateConnection())
            {
                var fms = cn.Query<HistoryEntry>(sqlGetVisitors, new { OtherSurveyId = surveyId });
                return fms;
            }
        }

        public int GetVisitorsCount(int surveyId)
        {
            const string sqlGetVisitorsCount = "SELECT COUNT(*) FROM History WHERE OtherSurveyId = @OtherSurveyId";
            using (var cn = _c.CreateConnection())
            {
                var visitorsCount = cn.Query<int>(sqlGetVisitorsCount, new { OtherSurveyId = surveyId }).SingleOrDefault();
                return visitorsCount;
            }
        }

        public void AddVisitor(HistoryEntry he)
        {
            const string sqlVisitorExists = "SELECT VisitationCount FROM History WHERE SelfSurveyId  = @SelfSurveyId AND OtherSurveyId  = @OtherSurveyId";
            const string sqlInsertVisitor = "INSERT INTO History (SelfSurveyId, OtherSurveyId, VisitationCount) VALUES (@SelfSurveyId, @OtherSurveyId, 1)";
            const string sqlUpdateVisitor = "UPDATE History SET LastVisitDate = getdate(), VisitationCount = @VisitationCount WHERE SelfSurveyId = @SelfSurveyId AND OtherSurveyId = @OtherSurveyId";

            if (he.OtherSurveyId != he.SelfSurveyId)
            {
                using (var cn = _c.CreateConnection())
                {
                    int visitationCount = cn.Query<int>(sqlVisitorExists, he).FirstOrDefault();
                    if (visitationCount == 0)
                    {
                        he.VisitationCount = 1;
                        cn.Execute(sqlInsertVisitor, he);
                    }
                    else
                    {
                        he.VisitationCount = visitationCount + 1;
                        cn.Execute(sqlUpdateVisitor, he);
                    }
                }
            }
        }

        public IEnumerable<HistoryEntry> GetVisits(int surveyId)
        {
            const string sqlGetVisits = "SELECT History.*, Surveys.Name AS OtherName, Surveys.IsDisabled FROM History "
                                        + "INNER JOIN Surveys ON Surveys.SurveyId = History.OtherSurveyId WHERE SelfSurveyId = @SelfSurveyId";
            using (var cn = _c.CreateConnection())
            {
                var fms = cn.Query<HistoryEntry>(sqlGetVisits, new { SelfSurveyId = surveyId });
                return fms;
            }
        }
    }
}