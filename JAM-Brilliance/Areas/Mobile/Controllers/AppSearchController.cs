using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using JAM.Brilliance.Areas.Mobile.Attributes;
using JAM.Brilliance.Areas.Mobile.Models.Response;
using JAM.Brilliance.Models.ViewModels;
using JAM.Core.Interfaces;
using JAM.Core.Interfaces.App;
using JAM.Core.Models;

namespace JAM.Brilliance.Areas.Mobile.Controllers
{
    public class AppSearchController : AppBaseController
    {
        private readonly ISearchDataService _searchDataService;
        private readonly ISurveyDataService _surveyDataService;

        public AppSearchController(IAccountService accountService, IAccountTokenDataService accountTokenDataService, ISearchDataService searchDataService, ISurveyDataService surveyDataService)
            : base(accountService, accountTokenDataService)
        {
            _searchDataService = searchDataService;
            _surveyDataService = surveyDataService;
        }

        [HttpGet, ValidateToken]
        public JsonResult Search(string postalCode)
        {
            //int otherSurveyId = id;
            var selfSurveyId = AccountTokenDataService.GetSurveyIdForToken(Token);
            var survey = _surveyDataService.GetSurvey(selfSurveyId);
            var ssvm = Mapper.Map<ShortSurveyViewModel>(survey);

            SearchCriteria sc = new SearchCriteria();
            sc.AgeMin = Math.Max(ssvm.Age - 10 ?? 30, 20);
            sc.AgeMax = Math.Max(ssvm.Age + 10 ?? 31, 20);
            sc.SelfSurveyId = selfSurveyId;
            sc.PostalCode = ssvm.PostalCode;
            sc.City = ssvm.City;
            sc.WhatSearchingForWhatId = survey.WhatSearchingForWhatId;

            IEnumerable<SearchResult> surveysFound = _searchDataService.SimplerSearch(sc);

            IList<SearchResultViewModel> srvm = Mapper.Map<IEnumerable<SearchResult>, IList<SearchResultViewModel>>(surveysFound);

            var response = new SearchResultModel { Success = true, Message = "OK", SearchResults = srvm };
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
