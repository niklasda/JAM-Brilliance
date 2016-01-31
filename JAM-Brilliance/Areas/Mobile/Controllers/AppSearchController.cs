using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IMapper _mapper;
        public AppSearchController(IAccountService accountService, IAccountTokenDataService accountTokenDataService, ISearchDataService searchDataService, ISurveyDataService surveyDataService, IMapper mapper)
            : base(accountService, accountTokenDataService)
        {
            _searchDataService = searchDataService;
            _surveyDataService = surveyDataService;
            _mapper = mapper;
        }

        [HttpGet, ValidateToken]
        public JsonResult Search(string postalCode)
        {
            //int otherSurveyId = id;
            var selfSurveyId = AccountTokenDataService.GetSurveyIdForToken(Token);
            var survey = _surveyDataService.GetSurvey(selfSurveyId);
            var ssvm = _mapper.Map<ShortSurveyViewModel>(survey);

            SearchCriteria sc = new SearchCriteria();
            sc.AgeMin = Math.Max(ssvm.Age - 10 ?? 30, 20);
            sc.AgeMax = Math.Max(ssvm.Age + 10 ?? 31, 20);
            sc.SelfSurveyId = selfSurveyId;
            sc.PostalCode = ssvm.PostalCode;
            sc.City = ssvm.City;
            sc.WhatSearchingForWhatId = survey.WhatSearchingForWhatId;

            IEnumerable<SearchResult> surveysFound = _searchDataService.SimplerSearch(sc);

            IList<SearchResultViewModel> srvm = _mapper.Map<IEnumerable<SearchResult>, IList<SearchResultViewModel>>(surveysFound);

            int nbrOfExistingHits = 0;
            if (int.TryParse(postalCode, out nbrOfExistingHits))
            {
                srvm = srvm.Skip(nbrOfExistingHits).Take(1).ToList();
            }

            var response = new SearchResultModel { Success = true, Message = "OK", SearchResults = srvm };
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
