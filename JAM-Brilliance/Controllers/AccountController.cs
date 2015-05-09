using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;
using AutoMapper;
using JAM.Core.Attributes;
using JAM.Core.Interfaces;
using JAM.Core.Logic;
using JAM.Core.Models;
using JAM.Brilliance.Models.ViewModels;
using PagedList;

namespace JAM.Brilliance.Controllers
{
    [SessionState(SessionStateBehavior.Required)]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ISurveyDataService _surveyDataService;
        private readonly IEmailService _mailService;
        private readonly IEmailDataService _mailDataService;
        private readonly ILogDataService _logDataService;
        private readonly IDiagnosticsService _diagnosticsService;

        public AccountController(ISurveyDataService surveyDataService, IAccountService accountService, IEmailService mailService, IEmailDataService mailDataService, ILogDataService logDataService, IDiagnosticsService diagnosticsService)
        {
            _accountService = accountService;
            _surveyDataService = surveyDataService;
            _mailService = mailService;
            _mailDataService = mailDataService;
            _logDataService = logDataService;
            _diagnosticsService = diagnosticsService;
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ViewResult OnlineUsers(int? page)
        {
            ViewBag.CurrentPageId = PageIds.MyPage;

            _diagnosticsService.ThrowUnlessSurveyComplete();

            IEnumerable<AccountViewModel> usersOnline = GetSortedOnlineUsers();

            var pageNumber = page ?? 1;
            var onePageOfOnlineUsers = usersOnline.ToPagedList(pageNumber, Constants.PageSize);
            ViewBag.OnePageOfOnlineUsers = onePageOfOnlineUsers;

            return View(onePageOfOnlineUsers);
        }

        public ViewResult LogOn()
        {
            ViewBag.CurrentPageId = PageIds.Login;
            return View();
        }

        public ViewResult Blocked()
        {
            ViewBag.CurrentPageId = PageIds.Login;
            return View();
        }

        public ViewResult LockedOut()
        {
            ViewBag.CurrentPageId = PageIds.Login;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Throttle(Name = "LogonThrottle")]
        public ActionResult LogOn(LogOnViewModel model, string returnUrl)
        {
            ViewBag.CurrentPageId = PageIds.Login;

            string ip = Request.UserHostAddress;
            bool databaseOk = _diagnosticsService.IsDatabaseOk();

            if (ModelState.IsValid && databaseOk)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    if (_accountService.IsUserMember(model.UserName))
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                        _logDataService.LogIpForLogin(ip, model.UserName, true);

                        return RedirectToAction("MyShortDetails", "Survey");
                    }
                    else if (_accountService.IsUserPending(model.UserName))
                    {
                        _logDataService.LogIpForLogin(ip, model.UserName, false);
                        return RedirectToAction("Pending");
                    }
                    else
                    {
                        _logDataService.LogIpForLogin(ip, model.UserName, false);
                        return RedirectToAction("Blocked");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Användarnamnet eller lösenordet var felaktig angivet.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Okänt fel, eller databasproblem");
            }

            // If we got this far, something failed, redisplay form
            Task.Run(() => _logDataService.LogIpForLogin(ip, model.UserName, false));
            return View(model);
        }

        public ActionResult LogOff()
        {
            Session.RemoveAll();
            FormsAuthentication.SignOut();

            return RedirectToAction("Start", "Home");
        }

        public ViewResult Register()
        {
            ViewBag.CurrentPageId = PageIds.Register;
            return View();
        }

        public ViewResult Pending()
        {
            ViewBag.CurrentPageId = PageIds.Register;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Throttle(Name = "RegisterThrottle")]
        public ActionResult Register(RegisterViewModel model)
        {
            ViewBag.CurrentPageId = PageIds.Register;
            bool databaseOk = _diagnosticsService.IsDatabaseOk();

            if (ModelState.IsValid && databaseOk)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                var user = Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, /*isApproved:*/ false, out createStatus);

                if (createStatus == MembershipCreateStatus.Success && user != null)
                {
                    var cultureCookie = HttpContext.Response.Cookies.Get(Constants.CultureCookieName);
                    string cultureCode = Constants.DefaultCountryCode;
                    if (cultureCookie != null)
                    {
                        cultureCode = cultureCookie.Value;
                    }

                    _accountService.SetUserComment(user, cultureCode, model.Name);

                    _accountService.MakeUserPending(model.UserName);

                    var guid = Guid.NewGuid();
                    guid = _mailDataService.StoreVerificationGuid(model.Email, guid);
                    string activationUrl = Url.Action("EmailVerification", "Account", new { uid = guid }, Request.Url.Scheme);
                    bool ok = _mailService.SendVerificationMail(model.UserName, model.Email, activationUrl);

                    return RedirectToAction("Pending");
                }
                else
                {
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// Called from Email
        /// </summary>
        /// <param name="uid">guid for registered user</param>
        /// <returns>The view</returns>
        public ViewResult EmailVerification(string uid)
        {
            Guid guid;
            if (Guid.TryParse(uid, out guid))
            {
                string email = _mailDataService.VerifyEmail(guid);

                if (!string.IsNullOrEmpty(email))
                {
                    string userName = _accountService.GetUserName(email);
                    _accountService.MakeUserMember(userName);

                    return View("VerificationSuccess");
                }
            }

            return View("VerificationFailed");
        }

        /// <summary>
        /// Called from Email
        /// </summary>
        /// <param name="uid">guid for registered user</param>
        /// <returns></returns>
        public ActionResult EmailReminder(string uid)
        {
            Guid guid;
            if (Guid.TryParse(uid, out guid))
            {
                string email = _mailDataService.RemindPasswordEmail(guid);

                if (!string.IsNullOrEmpty(email))
                {
                    string userName = _accountService.GetUserName(email);
                    var user = Membership.GetUser(userName);
                    var password = user.ResetPassword();

                    var fpvm = new ChangeNewPasswordViewModel();
                    fpvm.OldPassword = password;
                    fpvm.ReminderGuid = guid;
                    fpvm.Email = email;

                    TempData.Add("NewPasswordChange", fpvm);
                    return RedirectToAction("ChangeNewPassword");
                }
            }

            return View("ReminderFailed");
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ViewResult ChangePassword()
        {
            ViewBag.CurrentPageId = PageIds.MyPage;

            return View();
        }

        public ViewResult ChangeNewPassword()
        {
            var cpm = TempData["NewPasswordChange"] as ChangeNewPasswordViewModel;

            return View(cpm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeNewPassword(ChangeNewPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded = false;
                try
                {
                    string email = _mailDataService.RemindPasswordEmail(model.ReminderGuid);
                    if (email.Trim().ToLower().Equals(model.Email.Trim().ToLower()))
                    {
                        string userName = _accountService.GetUserName(email);

                        MembershipUser currentUser = Membership.GetUser(userName);
                        changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                    }
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangeNewPasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = MemberRoles.Member)]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, userIsOnline: true);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ViewResult ChangeNewPasswordSuccess()
        {
            return View();
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ViewResult ChangePasswordSuccess()
        {
            ViewBag.CurrentPageId = PageIds.MyPage;
            return View();
        }

        [Authorize(Roles = MemberRoles.Member)]
        public ActionResult Deactivate()
        {
            int surveyId = _surveyDataService.GetCurrentUserSurveyId();

            _accountService.DeactivateCurrentAccount();
            _surveyDataService.HideSurvey(surveyId);

            return RedirectToAction("Start", "Home");
        }

        public ViewResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Throttle(Name = "RemindPasswordThrottle")]
        public ViewResult ForgotPassword(ForgotPasswordViewModel fpvm)
        {
            if (ModelState.IsValid)
            {
                var guid = Guid.NewGuid();
                guid = _mailDataService.StoreReminderGuid(fpvm.Email, guid);
                string activationUrl = Url.Action("EmailReminder", "Account", new { uid = guid }, Request.Url.Scheme);
                _mailService.SendReminderMail(fpvm.Email, activationUrl);

                return View("ReminderSent");
            }

            return View();
        }

        private IEnumerable<AccountViewModel> GetSortedOnlineUsers()
        {
            IEnumerable<Account> usersOnline = _accountService.GetOnlineUsers(_surveyDataService);
            usersOnline = usersOnline.OrderBy(x => x.UserName);
            var avms = Mapper.Map<IEnumerable<Account>, IList<AccountViewModel>>(usersOnline);

            return avms;
        }

        private string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }
}