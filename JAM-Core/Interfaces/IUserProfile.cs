namespace JAM.Core.Interfaces
{
    public interface IUserProfile
    {
        int GetCurrentSurveyId();

        int GetSurveyId(string email);

        void SetSurveyId(string email, int surveyId);
    }
}