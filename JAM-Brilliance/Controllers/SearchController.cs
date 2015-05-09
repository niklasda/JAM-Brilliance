using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.SessionState;

using AutoMapper;

using JAM.Core.Interfaces;
using JAM.Core.Logic;
using JAM.Core.Models;
using JAM.Brilliance.Models.ViewModels;

using PagedList;

namespace JAM.Brilliance.Controllers
{
    [SessionState(SessionStateBehavior.Required)]
    public class SearchController : Controller
    {
        private readonly ISearchDataService _searchDataService;
        private readonly ISurveyDataService _surveyDataService;
        private readonly IDiagnosticsService _diagnosticsService;

        public SearchController(ISearchDataService searchDataService, ISurveyDataService surveyDataService, IDiagnosticsService diagnosticsService)
        {
            _searchDataService = searchDataService;
            _surveyDataService = surveyDataService;
            _diagnosticsService = diagnosticsService;
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ViewResult Search()
        {
            ViewBag.CurrentPageId = PageIds.Search;

            _diagnosticsService.ThrowUnlessSurveyComplete();

            var ss = new SearchCriteriaViewModel();
            return View(ss);
        }

        [HttpPost]
        [Authorize(Roles = MemberRoles.Member)]
        [ValidateAntiForgeryToken]
        public ActionResult Search(SearchCriteriaViewModel scvm)
        {
            ViewBag.CurrentPageId = PageIds.Search;

            if (ModelState.IsValid)
            {
                var sc = Mapper.Map<SearchCriteria>(scvm);
                var survey = _surveyDataService.GetCurrentUserSurvey();

                IEnumerable<SearchResult> srs = _searchDataService.SimpleSearch(sc, survey);

                var srvms = Mapper.Map<IEnumerable<SearchResult>, IList<SearchResultViewModel>>(srs);
                if (srvms.Count > 0)
                {
                    TempData[Constants.SearchResultTempDataName] = srvms;
                    return RedirectToAction("SearchResult");
                }
            }

            ViewBag.CurrentPageId = PageIds.Search;
            return View(scvm);
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ViewResult SearchResult(int? page)
        {
            ViewBag.CurrentPageId = PageIds.Search;

            var srvms = (List<SearchResultViewModel>)TempData[Constants.SearchResultTempDataName];
            if (srvms != null)
            {
                TempData.Keep(Constants.SearchResultTempDataName);
            }
            else
            {
                srvms = new List<SearchResultViewModel>();
            }

            var pageNumber = page ?? 1;
            var onePageOfSearchResults = srvms.ToPagedList(pageNumber, Constants.PageSize);
            ViewBag.OnePageOfSearchResults = onePageOfSearchResults;

            return View(onePageOfSearchResults);
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ViewResult SearchAdv()
        {
            ViewBag.CurrentPageId = PageIds.Search;

            var ssa = new SearchAdvCriteriaViewModel();

            return View(ssa);
        }

        [HttpPost]
        [Authorize(Roles = MemberRoles.Member)]
        [ValidateAntiForgeryToken]
        public ViewResult SearchAdv(SearchAdvCriteriaViewModel sacvm)
        {
            if (ModelState.IsValid)
            {
                var sac = Mapper.Map<SearchAdvCriteria>(sacvm);
                var survey = _surveyDataService.GetCurrentUserSurvey();

                IEnumerable<SearchResult> srs = _searchDataService.AdvancedSearch(sac, survey);

                var srvms = Mapper.Map<IEnumerable<SearchResult>, IList<SearchResultViewModel>>(srs);

                return View("SearchResult", srvms);
            }

            ViewBag.CurrentPageId = PageIds.Search;
            return View(sacvm);
        }
    }
}