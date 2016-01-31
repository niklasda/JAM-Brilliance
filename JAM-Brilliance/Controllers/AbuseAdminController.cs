using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.SessionState;

using AutoMapper;
using JAM.Core.Interfaces.Admin;
using JAM.Core.Logic;
using JAM.Core.Models;
using JAM.Brilliance.Models.ViewModels;
using PagedList;

namespace JAM.Brilliance.Controllers
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    [Authorize(Roles = MemberRoles.Administrator)]
    public class AbuseAdminController : Controller
    {
        private readonly IAbuseAdminDataService _abuseAdminDataService;
        private readonly IMapper _mapper;

        public AbuseAdminController(IAbuseAdminDataService abuseAdminDataService, IMapper mapper)
        {
            _abuseAdminDataService = abuseAdminDataService;
            _mapper = mapper;
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public ViewResult Start(int? page)
        {
            ViewBag.CurrentPageId = PageIds.DevPage;

            IEnumerable<AbuseReport> reports = _abuseAdminDataService.GetAllAbuseReports();
            var rvms = _mapper.Map<IEnumerable<AbuseReport>, IList<AbuseReportViewModel>>(reports);
            var srvms = rvms.OrderByDescending(x => x.ReportDate);

            var pageNumber = page ?? 1;
            var onePageOfAbuse = srvms.ToPagedList(pageNumber, Constants.PageSize);
            ViewBag.OnePageOfAbuse = onePageOfAbuse;

            return View(onePageOfAbuse);
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public RedirectToRouteResult BlockUser()
        {
            return RedirectToAction("Start");
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public RedirectToRouteResult DiscardAbuse(int abuseId)
        {
            _abuseAdminDataService.DiscardAbuseReport(abuseId);

            return RedirectToAction("Start");
        }
    }
}