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
        private readonly ISurveySettingsDataService _surveySettingsDataService;

        public AppSurveyController(IAccountService accountService, IAccountTokenDataService accountTokenDataService, ISurveyDataService surveyDataService, ISurveySettingsDataService surveySettingsDataService)
            : base(accountService, accountTokenDataService)
        {
            _surveyDataService = surveyDataService;
            _surveySettingsDataService = surveySettingsDataService;
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

        [HttpGet, ValidateToken]
        public JsonResult GetSurveySettings()
        {
            var selfSurveyId = AccountTokenDataService.GetSurveyIdForToken(Token);
            var surveySettings = _surveySettingsDataService.GetSurveySettings(selfSurveyId);
            var ssvm = Mapper.Map<SurveySettingsViewModel>(surveySettings);
            if (ssvm != null)
            {
                var survey = _surveyDataService.GetSurvey(selfSurveyId);
                var ssvm2 = Mapper.Map<ShortSurveyViewModel>(survey);
                ssvm = new SurveySettingsViewModel();
                ssvm.SearchAgeMin = ssvm2.Age - 18 ?? 25;
                ssvm.SearchAgeMax = ssvm2.Age + 18 ?? 50;

                ssvm.SearchAgeMin = Math.Max(ssvm.SearchAgeMin, 18);
                ssvm.SearchAgeMax = Math.Min(ssvm.SearchAgeMax, ssvm.SearchAgeMin + 1);

                ssvm.SearchDistanceMax = 50;
            }

            var response = new SurveySettingsModel { Success = true, Message = "OK", Settings = ssvm };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost, ValidateToken]
        //public JsonResult SetSurveySettings(SurveySettingsModel model)
        //{
        //    var selfSurveyId = AccountTokenDataService.GetSurveyIdForToken(Token);
        //    var ok = _surveySettingsDataService.SetSurveySettings(model);
        //    var ssvm = Mapper.Map<SurveySettingsViewModel>(surveySettings);
        //    if (ssvm != null)
        //    {
        //        var survey = _surveyDataService.GetSurvey(selfSurveyId);
        //        var ssvm2 = Mapper.Map<ShortSurveyViewModel>(survey);
        //        ssvm = new SurveySettingsViewModel();
        //        ssvm.SearchAgeMin = ssvm2.Age - 18 ?? 25;
        //        ssvm.SearchAgeMax = ssvm2.Age + 18 ?? 50;

        //        ssvm.SearchAgeMin = Math.Max(ssvm.SearchAgeMin, 18);
        //        ssvm.SearchAgeMax = Math.Min(ssvm.SearchAgeMax, ssvm.SearchAgeMin + 1);

        //        ssvm.SearchDistanceMax = 50;
        //    }

        //    var response = new SurveySettingsModel { Success = true, Message = "OK", Settings = ssvm };
        //    return Json(response, JsonRequestBehavior.AllowGet);
        //}
    }
}
