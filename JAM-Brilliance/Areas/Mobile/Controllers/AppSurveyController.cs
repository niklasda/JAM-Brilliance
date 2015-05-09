using System;
using System.Web.Mvc;
using AutoMapper;
using JAM.Brilliance.Areas.Mobile.Attributes;
using JAM.Brilliance.Areas.Mobile.Models.Response;
using JAM.Brilliance.Models.ViewModels;
using JAM.Core.Interfaces;
using JAM.Core.Interfaces.App;

namespace JAM.Brilliance.Areas.Mobile.Controllers
{
    public class AppSurveyController : AppBaseController
    {
        private readonly ISurveyDataService _surveyDataService;

        public AppSurveyController(IAccountService accountService, IAccountTokenDataService accountTokenDataService, ISurveyDataService surveyDataService)
            : base(accountService, accountTokenDataService)
        {
            _surveyDataService = surveyDataService;
        }

        [HttpGet, ValidateToken]
        public JsonResult GetShortSurvey(int id)
        {
            int otherSurveyId = id;
            var selfSurveyId = AccountTokenDataService.GetSurveyIdForToken(Token);
            var survey = _surveyDataService.GetSurvey(otherSurveyId);
            var ssvm = Mapper.Map<ShortSurveyViewModel>(survey);

            var response = new SurveyModel { Success = true, Message = "OK", Survey = ssvm };
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        [HttpGet, ValidateToken]
        public JsonResult GetMyShortSurvey()
        {
            var selfSurveyId = AccountTokenDataService.GetSurveyIdForToken(Token);
            var survey = _surveyDataService.GetSurvey(selfSurveyId);
            var ssvm = Mapper.Map<ShortSurveyViewModel>(survey);

            var response = new SurveyModel { Success = true, Message = "OK", Survey = ssvm };
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
