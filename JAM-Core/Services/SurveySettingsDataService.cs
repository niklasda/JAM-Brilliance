using System.Linq;

using Dapper;
using JAM.Core.Interfaces;
using JAM.Core.Models;
using JetBrains.Annotations;

namespace JAM.Core.Services
{
    [UsedImplicitly]
    public class SurveySettingsDataService : ISurveySettingsDataService
    {
        protected readonly IDatabaseConfigurationService Config;
        private readonly IUserProfile _userProfile;
        private readonly IAccountService _accountService;

        public SurveySettingsDataService(IAccountService accountService, IUserProfile userProfile, IDatabaseConfigurationService configurationService)
        {
            _accountService = accountService;
            _userProfile = userProfile;
            Config = configurationService;
        }

        public SurveySettings GetSurveySettings(int surveyId)
        {
            const string sqlSurveySettings = "SELECT SearchAgeMin, SearchAgeMax, SearchDistanceMax FROM SurveySettings WHERE SurveyId = @SurveyId";

            using (var cn = Config.CreateConnection())
            {
                var surveySettings = cn.Query<SurveySettings>(sqlSurveySettings, new { SurveyId = surveyId }).FirstOrDefault();
                //_userProfile.SetSurveyId(email, surveyId);
                return surveySettings;
            }
        }

    }
}