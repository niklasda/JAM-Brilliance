using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

using JAM.Core.Interfaces;
using JAM.Core.Logic;
using JAM.Brilliance.Models.ViewModels;

namespace JAM.Brilliance.Controllers
{
    [SessionState(SessionStateBehavior.Required)]
    public class HomeController : Controller
    {
        private readonly ISurveyDataService _surveyDataService;
        private readonly IAccountService _accountService;

        public HomeController(ISurveyDataService surveyDataService, IAccountService accountService, ILogDataService logService)
        {
            _surveyDataService = surveyDataService;
            _accountService = accountService;

            logService.LogInfo("Home");
        }

        public ViewResult Start()
        {
            ViewBag.CurrentPageId = PageIds.Start;
            return View();
        }

        //public ViewResult About()
        //{
        //    ViewBag.CurrentPageId = PageIds.About;
        //    return View();
        //}

        [Authorize(Roles = MemberRoles.Member)]
        public ActionResult MyActions()
        {
            ViewBag.CurrentPageId = PageIds.MyPage;

            var survey = _surveyDataService.GetCurrentUserSurvey();

            var mpvm = new MyPageViewModel();
            mpvm.IsSurveyComplete = survey.SurveyId > 0;
            mpvm.IsDisabled = survey.IsDisabled;
            mpvm.IsMember = true;
            mpvm.IsSearchigForMale = survey.IsSearchigForMale;

            return View(mpvm);
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public ActionResult DevPage()
        {
            ViewBag.CurrentPageId = PageIds.DevPage;

            var surveyId = _surveyDataService.GetCurrentUserSurveyId();
            return View(surveyId);
        }

        public RedirectToRouteResult Contact()
        {
            ViewBag.CurrentPageId = PageIds.Contact;
            return RedirectToAction("SendMessageToUs", "Message");
        }

        //public ViewResult Coaching()
        //{
        //    ViewBag.CurrentPageId = PageIds.Coaching;
        //    return View();
        //}

        //public ViewResult Sharpen()
        //{
        //    ViewBag.CurrentPageId = PageIds.Sharpen;
        //    return View();
        //}

        //public ViewResult Terms()
        //{
        //    ViewBag.CurrentPageId = PageIds.Register;
        //    return View();
        //}

        //public RedirectToRouteResult Danish()
        //{
        //    const string CultureCode = "da";
        //    HttpContext.Response.Cookies.Set(new HttpCookie(Constants.CultureCookieName, CultureCode));
        //    _accountService.UpdateCurrentUserCommentCultureCode(CultureCode);

        //    return StartPage();
        //}

        public RedirectToRouteResult StartPage()
        {
            if (User.IsInRole(MemberRoles.Member))
            {
                return RedirectToAction("MyShortDetails", "Survey");
            }

            return RedirectToAction("Start");
        }

        //public RedirectToRouteResult Swedish()
        //{
        //    const string CultureCode = "sv";
        //    HttpContext.Response.Cookies.Set(new HttpCookie(Constants.CultureCookieName, CultureCode));
        //    _accountService.UpdateCurrentUserCommentCultureCode(CultureCode);

        //    return StartPage();
        //}

        //public RedirectToRouteResult English()
        //{
        //    const string CultureCode = "en";
        //    HttpContext.Response.Cookies.Set(new HttpCookie(Constants.CultureCookieName, CultureCode));
        //    _accountService.UpdateCurrentUserCommentCultureCode(CultureCode);

        //    return StartPage();
        //}

        //public RedirectToRouteResult Arabic()
        //{
        //    const string CultureCode = "ar";
        //    HttpContext.Response.Cookies.Set(new HttpCookie(Constants.CultureCookieName, CultureCode));
        //    _accountService.UpdateCurrentUserCommentCultureCode(CultureCode);

        //    return StartPage();
        //}

        public RedirectResult Matchmaking()
        {
            return Redirect("http://www.jamatchmaking.com");
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public RedirectResult GotoElmah()
        {
            return Redirect("~/elmah");
        }

        [ChildActionOnly]
        public PartialViewResult Menu(PageIds? pageId)
        {
            var roles = _accountService.GetCurrentUserRoles();
            
            var mpvm = new MyPageViewModel();
            mpvm.CurrentPageId = pageId.HasValue ? pageId.Value : PageIds.DevPage;
            mpvm.IsMember = roles.Contains(MemberRoles.Member);
            mpvm.IsAdmin = roles.Contains(MemberRoles.Administrator);

            return PartialView(mpvm);
        }

        [ChildActionOnly]
        [Authorize(Roles = MemberRoles.Administrator)]
        public PartialViewResult ToolbarAdmin()
        {
            var survey = _surveyDataService.GetCurrentUserSurvey();
            var mpvm = new MyPageViewModel();
            mpvm.IsSurveyComplete = survey.SurveyId > 0;
            mpvm.IsDisabled = survey.IsDisabled;
            mpvm.IsMember = true;
            mpvm.IsAdmin = true;
            mpvm.IsSearchigForMale = survey.IsSearchigForMale;

            return PartialView(mpvm);
        }

        [ChildActionOnly]
        [Authorize(Roles = MemberRoles.Member)]
        public ActionResult ToolbarUser()
        {
            var survey = _surveyDataService.GetCurrentUserSurvey();
            var mpvm = new MyPageViewModel();
            mpvm.IsSurveyComplete = survey.SurveyId > 0;
            mpvm.IsDisabled = survey.IsDisabled;
            mpvm.IsMember = true;
            mpvm.IsSearchigForMale = survey.IsSearchigForMale;

            if (mpvm.IsSurveyComplete)
            {
                return PartialView(mpvm);
            }
            else
            {
                return new EmptyResult();
            }
        }

        [ChildActionOnly]
        [Authorize(Roles = MemberRoles.Member)]
        public ActionResult ProfileMenu()
        {
            var survey = _surveyDataService.GetCurrentUserSurvey();
            var mpvm = new MyPageViewModel();
            mpvm.IsSurveyComplete = survey.SurveyId > 0;
            mpvm.IsDisabled = survey.IsDisabled;
            mpvm.IsSearchigForMale = survey.IsSearchigForMale;
            mpvm.IsMember = true;

            return PartialView(mpvm);
        }
    }
}