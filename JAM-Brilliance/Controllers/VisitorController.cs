using System.Collections.Generic;
using System.Linq;
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
    public class VisitorController : Controller
    {
        private readonly ISurveyDataService _surveyDataService;
        private readonly IVisitorDataService _visitorDataService;
        private readonly IDiagnosticsService _diagnosticsService;

        public VisitorController(ISurveyDataService surveyDataService, IVisitorDataService visitorDataService, IDiagnosticsService diagnosticsService)
        {
            _surveyDataService = surveyDataService;
            _visitorDataService = visitorDataService;
            _diagnosticsService = diagnosticsService;
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ViewResult MyVisitors(int? page)
        {
            ViewBag.CurrentPageId = PageIds.MyPage;

            _diagnosticsService.ThrowUnlessSurveyComplete();

            var svvms = GetMySortedVisitors();

            var pageNumber = page ?? 1;
            var onePageOfVisitors = svvms.ToPagedList(pageNumber, Constants.PageSize);
            ViewBag.OnePageOfVisitors = onePageOfVisitors;

            return View(onePageOfVisitors);
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ViewResult MyVisits(int? page)
        {
            ViewBag.CurrentPageId = PageIds.MyPage;

            _diagnosticsService.ThrowUnlessSurveyComplete();

            var svvms = GetMySortedVisits();

            var pageNumber = page ?? 1;
            var onePageOfVisits = svvms.ToPagedList(pageNumber, Constants.PageSize);
            ViewBag.OnePageOfVisits = onePageOfVisits;

            return View(onePageOfVisits);
        }

        private IEnumerable<HistoryEntryViewModel> GetMySortedVisitors()
        {
            int surveyId = _surveyDataService.GetCurrentUserSurveyId();

            IEnumerable<HistoryEntry> hms = _visitorDataService.GetVisitors(surveyId);
            var hvms = Mapper.Map<IEnumerable<HistoryEntry>, IList<HistoryEntryViewModel>>(hms);

            IEnumerable<HistoryEntryViewModel> smvms = hvms.OrderByDescending(x => x.LastVisitDate);
            return smvms;
        }

        private IEnumerable<HistoryEntryViewModel> GetMySortedVisits()
        {
            int surveyId = _surveyDataService.GetCurrentUserSurveyId();

            IEnumerable<HistoryEntry> hms = _visitorDataService.GetVisits(surveyId);
            var hvms = Mapper.Map<IEnumerable<HistoryEntry>, IList<HistoryEntryViewModel>>(hms);

            IEnumerable<HistoryEntryViewModel> smvms = hvms.OrderByDescending(x => x.LastVisitDate);
            return smvms;
        }
    }
}