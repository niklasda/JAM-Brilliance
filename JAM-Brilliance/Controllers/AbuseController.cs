using System.Web.Mvc;
using System.Web.SessionState;

using JAM.Core.Interfaces;
using JAM.Core.Logic;
using JAM.Core.Models;

namespace JAM.Brilliance.Controllers
{
    [SessionState(SessionStateBehavior.Required)]
    public class AbuseController : Controller
    {
        private readonly IAbuseDataService _abuseDataService;
        private readonly ISurveyDataService _surveyDataService;

        public AbuseController(IAbuseDataService abuseDataService, ISurveyDataService surveyDataService)
        {
            _abuseDataService = abuseDataService;
            _surveyDataService = surveyDataService;
        }

        [Authorize(Roles = MemberRoles.Member)]
        public RedirectToRouteResult ReportSurvey(int fromSurveyId)
        {
            int surveyId = _surveyDataService.GetCurrentUserSurveyId();

            var arm = new AbuseReport();
            arm.OtherSurveyId = fromSurveyId;
            arm.SelfSurveyId = surveyId;

            _abuseDataService.ReportSurveyAbuse(arm);
            return RedirectToAction("MyShortDetails", "Survey");
        }

        [Authorize(Roles = MemberRoles.Member)]
        public RedirectToRouteResult ReportMessage(int messageId, int fromSurveyId)
        {
            int surveyId = _surveyDataService.GetCurrentUserSurveyId();

            var arm = new AbuseReport();
            arm.OtherSurveyId = fromSurveyId;
            arm.SelfSurveyId = surveyId;
            arm.MessageId = messageId;

            _abuseDataService.ReportMessageAbuse(arm);
            return RedirectToAction("MyShortDetails", "Survey");
        }

        [Authorize(Roles = MemberRoles.Member)]
        public RedirectToRouteResult ReportPicture(int pictureId, int ownerSurveyId)
        {
            int surveyId = _surveyDataService.GetCurrentUserSurveyId();

            var arm = new AbuseReport();
            arm.OtherSurveyId = ownerSurveyId;
            arm.SelfSurveyId = surveyId;
            arm.PictureId = pictureId;

            _abuseDataService.ReportPictureAbuse(arm);
            return RedirectToAction("MyShortDetails", "Survey");
        }
    }
}