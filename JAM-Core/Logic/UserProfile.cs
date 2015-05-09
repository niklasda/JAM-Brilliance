using System.Web;
using System.Web.Security;

using JAM.Core.Interfaces;

namespace JAM.Core.Logic
{
    public class UserProfile : IUserProfile
    {
        public int GetCurrentSurveyId()
        {
            var email = Membership.GetUser().Email;

            var o = HttpContext.Current.Session[GetSessionKey(email)];
            return o != null ? (int)o : 0;
        }

        public int GetSurveyId(string email)
        {
            var o = HttpContext.Current.Session[GetSessionKey(email)];
            return o != null ? (int)o : 0;
        }

        public void SetSurveyId(string email, int surveyId)
        {
            HttpContext.Current.Session[GetSessionKey(email)] = surveyId;
        }

        private string GetSessionKey(string email)
        {
            return string.Format("{0}_{1}", Constants.SurveyIdSessionName, email);
        }
    }
}