using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using JAM.Core.Interfaces;
using JAM.Core.Interfaces.App;
using JAM.Core.Models;
using JAM.Core.Models.App;
using JetBrains.Annotations;

namespace JAM.Core.Services.App
{
    [UsedImplicitly]
    public class AccountTokenDataService : IAccountTokenDataService
    {
        private readonly IDataCache _dataCache;
        private readonly IDatabaseConfigurationService _configurationService;
        private readonly IAccountService _accountService;
        private readonly ISurveyDataService _surveyDataService;

        public AccountTokenDataService(IDataCache dataCache, IDatabaseConfigurationService configurationService, IAccountService accountService, ISurveyDataService surveyDataService)
        {
            _dataCache = dataCache;
            _configurationService = configurationService;
            _accountService = accountService;
            _surveyDataService = surveyDataService;
        }

        public UserTokenData IssueToken(string userName)
        {
            var token = Guid.Empty;

            const string sqlGetExistingToken = "SELECT Token FROM AppUserTokens WHERE UserName = @UserName AND Token = @Token";

            const string sqlInsertToken = "INSERT INTO AppUserTokens (UserName, Token, TokenIssueDate, TokenIssueIP, TokenIssueAppVersion, TokenLastUsedDate, TokenLastUsedIP, TokenLastUsedAppVersion) VALUES (@UserName, @Token, getdate(), @TokenIssueIP, @TokenIssueAppVersion, getdate(), @TokenLastUsedIP, @TokenLastUsedAppVersion)";

            const string sqlUpdateToken = "UPDATE AppUserTokens SET TokenLastUsedDate = getdate(), TokenLastUsedIP = @TokenLastUsedIP, TokenLastUsedAppVersion = @TokenLastUsedAppVersion WHERE UserName = @UserName AND Token = @Token";

            using (var cn = _configurationService.CreateConnection())
            {
                var utd = new UserTokenData();
                utd.UserName = userName;
                utd.Token = token;

                IEnumerable<Guid> sms = cn.Query<Guid>(sqlGetExistingToken, utd);

                if (sms.Any())
                {
                    utd.TokenLastUsedIP = "2.2.2.2";
                    utd.TokenLastUsedAppVersion = "Brilliance v2";
                    int nbrOfRows = cn.Execute(sqlUpdateToken, utd);
                }
                else
                {
                    utd.Token = Guid.NewGuid();
                    utd.TokenIssueIP = "1.1.1.1";
                    utd.TokenIssueAppVersion = "Brilliance v1";
                    utd.TokenLastUsedIP = "1.1.1.1";
                    utd.TokenLastUsedAppVersion = "Brilliance v1";
                    int nbrOfRows = cn.Execute(sqlInsertToken, utd);
                }

                return utd;
            }
        }

        public int GetSurveyIdForToken(Guid token)
        {
            const string sqlGetTokenUserName = "SELECT UserName FROM AppUserTokens WHERE Token = @Token";
            using (var cn = _configurationService.CreateConnection())
            {
                var userName = cn.Query<string>(sqlGetTokenUserName, new { Token = token }).SingleOrDefault();

                var email =_accountService.GetEmail(userName);

                var surveyId =_surveyDataService.GetSurveyId(email);
                return surveyId;
            } 
        }

        public bool IsTokenValid(Guid token)
        {
            const string sqlGetExistingToken = "SELECT Token FROM AppUserTokens WHERE Token = @Token";
            using (var cn = _configurationService.CreateConnection())
            {
                var utd = new UserTokenData();
                utd.UserName = "";
                utd.Token = token;

                IEnumerable<Guid> sms = cn.Query<Guid>(sqlGetExistingToken, utd);

                if (sms.Any())
                {
                    return true;
                }
         
                return false;
            }
        }

        public void RemoveToken(Guid token)
        {
            const string sqlDeleteExistingToken = "DELETE FROM AppUserTokens WHERE Token = @Token";
       //     Guid g;

         //   if (Guid.TryParse(token, out g))
           // {
                using (var cn = _configurationService.CreateConnection())
                {
                    int nbrOfRows = cn.Execute(sqlDeleteExistingToken, new { Token = token });
                }
            //}
        }
    }
}