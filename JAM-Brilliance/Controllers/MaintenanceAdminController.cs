using System.Web.Mvc;
using System.Web.SessionState;
using JAM.Core.Interfaces;
using JAM.Core.Interfaces.Admin;
using JAM.Core.Logic;

namespace JAM.Brilliance.Controllers
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    [Authorize(Roles = MemberRoles.Administrator)]
    public class MaintenanceAdminController : Controller
    {
        private readonly ILogDataService _logDataService;
        private readonly IAbuseAdminDataService _abuseAdminDataService;

        public MaintenanceAdminController(ILogDataService logDataService, IAbuseAdminDataService abuseAdminDataService)
        {
            _logDataService = logDataService;
            _abuseAdminDataService = abuseAdminDataService;
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public ActionResult Start()
        {
            return View();
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public ActionResult DeleteOldLogEntries()
        {
            _logDataService.DeleteOldLogEntries();

            return RedirectToAction("Start");
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public ActionResult DeleteOldAbuse()
        {
            _abuseAdminDataService.DeleteOldHandledAbuse();

            return RedirectToAction("Start");
        }
    }
}