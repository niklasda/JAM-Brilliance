using System.Data;
using System.Web.Security;
using JAM.Core.Exceptions;
using JAM.Core.Interfaces;
using JetBrains.Annotations;

namespace JAM.Core.Services
{
    [UsedImplicitly]
    public class DiagnosticsService : IDiagnosticsService
    {
        private readonly ISurveyDataService _surveyDataService;
        private readonly IUserProfile _userProfile;
        private readonly IDataCache _dataCache;
        private readonly IDatabaseConfigurationService _c;

        public DiagnosticsService(ISurveyDataService surveyDataService, IUserProfile userProfile, IDataCache dataCache, IDatabaseConfigurationService configurationService)
        {
            _surveyDataService = surveyDataService;
            _userProfile = userProfile;
            _dataCache = dataCache;
            _c = configurationService;
        }

        public bool IsDatabaseOk()
        {
            try
            {
                return TryConnect();
            }
            catch
            {
                return false;
            }
        }

        public bool IsAuthenticationOk()
        {
            try
            {
                return TryAuthenticate();
            }
            catch
            {
                return false;
            }
        }

        public void ThrowUnlessSurveyComplete()
        {
            var surveyId = _userProfile.GetCurrentSurveyId();
            if (surveyId == 0)
            {
                surveyId = _surveyDataService.GetCurrentUserSurveyId();
                if (surveyId == 0)
                {
                    throw new SurveyIncompleteException();
                }
            }
        }

        private bool TryAuthenticate()
        {
            var user = Membership.FindUsersByName(_dataCache.DevAdminSurveyName);
            return user != null;
        }

        private bool TryConnect()
        {
            using (var cn = _c.CreateConnection())
            {
                cn.Open();
                return cn.State == ConnectionState.Open;
            }
        }
    }
}