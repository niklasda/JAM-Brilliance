using System.Collections.Generic;

using JAM.Core.Models;

namespace JAM.Core.Interfaces.Admin
{
    public interface ISurveyAdminDataService : ISurveyDataService
    {
        IEnumerable<Survey> GetSurveys();

        bool DeleteSurvey(int surveyId);
    }
}