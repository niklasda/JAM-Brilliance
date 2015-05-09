using System.Web.Mvc;
using System.Web.SessionState;
using JAM.Core.Interfaces;

namespace JAM.Brilliance.Controllers
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class ErrorController : Controller
    {
        private readonly IDiagnosticsService _diagnosticsService;

        public ErrorController(IDiagnosticsService diagnosticsService)
        {
            _diagnosticsService = diagnosticsService;
        }

        public ViewResult Throttled()
        {
            return View();
        }

        public ViewResult NotFound()
        {
            return View();
        }

        public ViewResult Start(string aspxerrorpath)
        {
            if (!_diagnosticsService.IsDatabaseOk())
            {
                return View("DatabaseFail");
            }

            if (!_diagnosticsService.IsAuthenticationOk())
            {
                return View("AuthenticationFail");
            }

            return View("Error");
        }
    }
}