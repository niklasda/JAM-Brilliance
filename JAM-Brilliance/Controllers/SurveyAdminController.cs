using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.SessionState;

using JAM.Core.Interfaces.Admin;
using JAM.Core.Logic;
using JAM.Core.Models;

using PagedList;

namespace JAM.Brilliance.Controllers
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    [Authorize(Roles = MemberRoles.Administrator)]
    public class SurveyAdminController : Controller
    {
        private readonly ISurveyAdminDataService _surveyAdminDataService;

        public SurveyAdminController(ISurveyAdminDataService surveyAdminDataService)
        {
            _surveyAdminDataService = surveyAdminDataService;
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public ViewResult Start(string sortOrder, string searchString, int? page)
        {
            IEnumerable<Survey> surveys = _surveyAdminDataService.GetSurveys().Where(s => !s.IsDisabled);

            return IndexFiltered(surveys, sortOrder, searchString, page);
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public ViewResult StartMen(string sortOrder, string searchString, int? page)
        {
            var surveys = _surveyAdminDataService.GetSurveys().Where(s => s.IsMale && !s.IsDisabled && !s.IsInterviewed);

            return IndexFiltered(surveys, sortOrder, searchString, page);
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public ViewResult StartWomen(string sortOrder, string searchString, int? page)
        {
            var surveys = _surveyAdminDataService.GetSurveys().Where(s => !s.IsMale && !s.IsDisabled && !s.IsInterviewed);

            return IndexFiltered(surveys, sortOrder, searchString, page);
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public ViewResult StartAll(string sortOrder, string searchString, int? page)
        {
            var surveys = _surveyAdminDataService.GetSurveys().Select(s => s);

            return IndexFiltered(surveys, sortOrder, searchString, page);
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public ViewResult StartProspects(string sortOrder, string searchString, int? page)
        {
            var surveys = _surveyAdminDataService.GetSurveys().Where(s => s.IsInterviewed && !s.IsDisabled);

            return IndexFiltered(surveys, sortOrder, searchString, page);
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public RedirectToRouteResult Hide(int surveyId)
        {
            _surveyAdminDataService.HideSurvey(surveyId);

            return RedirectToAction("Start");
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public RedirectToRouteResult UnHide(int surveyId)
        {
            _surveyAdminDataService.UnhideSurvey(surveyId);

            return RedirectToAction("StartAll");
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public ViewResult StartOne(string emailFilter)
        {
            var surveys = _surveyAdminDataService.GetSurveys().Where(s => s.Email == emailFilter);

            var onePageOfSurveys = surveys.ToPagedList(1, Constants.PageSize);

            ViewBag.OnePageOfSurveys = onePageOfSurveys;

            return View("Start", onePageOfSurveys);
        }

        private ViewResult IndexFiltered(IEnumerable<Survey> surveys, string sortOrder, string searchString, int? page)
        {
            ViewBag.CurrentPageId = PageIds.DevPage;

            ViewBag.IdSortParm = string.IsNullOrEmpty(sortOrder) ? "IdDesc" : "Id";
            ViewBag.NameSortParm = sortOrder == "Name" ? "NameDesc" : "Name";
            ViewBag.CitySortParm = sortOrder == "City" ? "CityDesc" : "City";
            ViewBag.SearchString = searchString;

            var filteredSurveys = surveys;
            if (!string.IsNullOrEmpty(searchString))
            {
                filteredSurveys = filteredSurveys.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())
                                        || s.City.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "Name":
                    filteredSurveys = filteredSurveys.OrderBy(s => s.Name);
                    break;

                case "NameDesc":
                    filteredSurveys = filteredSurveys.OrderByDescending(s => s.Name);
                    break;

                case "City":
                    filteredSurveys = filteredSurveys.OrderBy(s => s.City);
                    break;

                case "CityDesc":
                    filteredSurveys = filteredSurveys.OrderByDescending(s => s.City);
                    break;

                case "Id":
                    filteredSurveys = filteredSurveys.OrderBy(s => s.SurveyId);
                    break;

                case "IdDesc":
                    filteredSurveys = filteredSurveys.OrderByDescending(s => s.SurveyId);
                    break;

                default:
                    filteredSurveys = filteredSurveys.OrderByDescending(s => s.SurveyId);
                    break;
            }

            var pageNumber = page ?? 1;
            IPagedList<Survey> onePageOfSurveys = filteredSurveys.ToPagedList(pageNumber, Constants.PageSize);

            ViewBag.OnePageOfSurveys = onePageOfSurveys;

            return View("Start", onePageOfSurveys);
        }
    }
}