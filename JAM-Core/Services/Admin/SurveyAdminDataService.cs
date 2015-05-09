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
    public class SurveyAdminDataService : SurveyDataService, ISurveyAdminDataService
    {
        public SurveyAdminDataService(IAccountService accountService, IUserProfile userProfile, IDatabaseConfigurationService configurationService)
            : base(accountService, userProfile, configurationService)
        {
        }

        public IEnumerable<Survey> GetSurveys()
        {
            const string sqlGetSurveys = "SELECT * FROM Surveys";

            using (var cn = Config.CreateConnection())
            {
                var surveys = cn.Query<Survey>(sqlGetSurveys);
                return surveys.AsQueryable();
            }
        }

        public bool DeleteSurvey(int surveyId)
        {
            const string sqlDeleteSurveyAbuse = "DELETE FROM Abuse WHERE SelfSurveyId = @SurveyId OR OtherSurveyId = @SurveyId";
            const string sqlDeleteSurveyFavourites = "DELETE FROM Favourites WHERE SelfSurveyId = @SurveyId OR OtherSurveyId = @SurveyId";
            const string sqlDeleteSurveyHistory = "DELETE FROM History WHERE SelfSurveyId = @SurveyId OR OtherSurveyId = @SurveyId";
            const string sqlDeleteSurveyMessages = "DELETE FROM Messages WHERE ToSurveyId = @SurveyId OR FromSurveyId = @SurveyId";
            const string sqlDeleteSurveyGeo = "DELETE FROM GeoInfo WHERE SurveyId = @SurveyId";

            const string sqlDeleteSurvey = "DELETE FROM Surveys WHERE SurveyId = @SurveyId";
            const string sqlDeleteWantedSurvey = "DELETE FROM WantedSurveys WHERE WantedSurveyId IN (SELECT WantedSurveyId FROM Surveys WHERE SurveyId = @SurveyId)";
            const string sqlDeleteSurveyPicture = "DELETE FROM Pictures WHERE SurveyId = @SurveyId";

            using (var cn = Config.CreateConnection())
            {
                cn.Execute(sqlDeleteSurveyAbuse, new { SurveyId = surveyId });
                cn.Execute(sqlDeleteSurveyFavourites, new { SurveyId = surveyId });
                cn.Execute(sqlDeleteSurveyHistory, new { SurveyId = surveyId });
                cn.Execute(sqlDeleteSurveyMessages, new { SurveyId = surveyId });
                cn.Execute(sqlDeleteSurveyGeo, new { SurveyId = surveyId });

                cn.Execute(sqlDeleteSurveyPicture, new { SurveyId = surveyId });
                int nbrOfRows4 = cn.Execute(sqlDeleteSurvey, new { SurveyId = surveyId });
                cn.Execute(sqlDeleteWantedSurvey, new { SurveyId = surveyId });

                return nbrOfRows4 == 1;
            }
        }
    }
}