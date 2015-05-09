using JAM.Core.Models;

namespace JAM.Core.Interfaces
{
    public interface ISurveyDataService
    {
        Survey GetCurrentUserSurvey();

        Survey GetSurvey(int surveyId);

        int GetCurrentUserSurveyId();

        int GetSurveyId(string email);

        WantedSurvey GetCurrentUserWantedSurvey();

        WantedSurvey GetWantedSurvey(int surveyId);

        bool HideSurvey(int surveyId);

        bool UnhideSurvey(int surveyId);

        bool SavePage1(Survey partSurvey);

        bool SavePage2(Survey partSurvey);

        bool SavePage3(Survey partSurvey);

        bool SavePage4(Survey partSurvey);

        bool SavePage5(Survey partSurvey);

        bool SavePage6(Survey partSurvey, WantedSurvey wantedSurvey);
    }
}