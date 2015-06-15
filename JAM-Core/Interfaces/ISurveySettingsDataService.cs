using JAM.Core.Models;

namespace JAM.Core.Interfaces
{
    public interface ISurveySettingsDataService
    {
        SurveySettings GetSurveySettings(int surveyId);
    }
}