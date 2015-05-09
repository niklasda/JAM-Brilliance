using System;
using JAM.Core.Models.App;

namespace JAM.Core.Interfaces.App
{
    public interface IAccountTokenDataService
    {
        UserTokenData IssueToken(string userName);

        int GetSurveyIdForToken(Guid token);
        
        bool IsTokenValid(Guid token);

        void RemoveToken(Guid token);
    }
}