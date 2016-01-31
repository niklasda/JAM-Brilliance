using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;

using AutoMapper;
using JAM.Core.Interfaces;
using JAM.Core.Interfaces.Admin;
using JAM.Core.Logic;
using JAM.Core.Models;
using JAM.Brilliance.Models.ViewModels;
using PagedList;

namespace JAM.Brilliance.Controllers
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class AccountAdminController : Controller
    {
        private readonly IAccountAdminService _accountAdminService;
        private readonly IEmailService _mailService;
        private readonly IEmailDataService _mailDataService;
        private readonly ILogDataService _logDataService;
        private readonly ISurveyAdminDataService _surveyAdminDataService;
        private readonly IMapper _mapper;

        public AccountAdminController(IAccountAdminService accountAdminService, IEmailService mailService, IEmailDataService mailDataService, ILogDataService logDataService, ISurveyAdminDataService surveyAdminDataService, IMapper mapper)
        {
            _accountAdminService = accountAdminService;
            _mailService = mailService;
            _mailDataService = mailDataService;
            _logDataService = logDataService;
            _surveyAdminDataService = surveyAdminDataService;
            _mapper = mapper;
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public RedirectToRouteResult AddRole(string userName, string roleName)
        {
            _accountAdminService.AddRoleToUser(userName, roleName);

            return RedirectToAction("Start");
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public ViewResult Start(int? page)
        {
            ViewBag.CurrentPageId = PageIds.DevPage;

            IList<RoleViewModel> rvms = new List<RoleViewModel>();
            string[] roles = Roles.GetAllRoles();
            foreach (var role in roles)
            {
                var rvm = new RoleViewModel();
                rvm.RoleName = role;
                rvm.Users = Roles.GetUsersInRole(role);
                rvms.Add(rvm);
            }

            MembershipUserCollection accounts = Membership.GetAllUsers();
            var ams = _mapper.Map<MembershipUserCollection, IList<Account>>(accounts);
            var avms = _mapper.Map<IList<Account>, IList<AccountViewModel>>(ams);
            var savms = avms.OrderBy(x => x.UserName);

            foreach (var avm in savms)
            {
                avm.Roles = Roles.GetRolesForUser(avm.UserName);
            }

            var pageNumber = page ?? 1;
            var onePageOfAccounts = savms.ToPagedList(pageNumber, Constants.PageSize);
            ViewBag.OnePageOfAccounts = onePageOfAccounts;

            var arvm = new AccountsRolesViewModel();
            arvm.Accounts = onePageOfAccounts;
            arvm.Roles = rvms;

            return View(arvm);
        }

        //[Authorize(Roles = MemberRoles.Administrator)]
        public RedirectToRouteResult Setup()
        {
            _accountAdminService.CreateAllRoles();
            _accountAdminService.SetupSpecialUser();

            return RedirectToAction("Start");
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public ViewResult StartOne(string emailfilter)
        {
            MembershipUserCollection accounts = Membership.FindUsersByEmail(emailfilter);
            var ams = _mapper.Map<MembershipUserCollection, IList<Account>>(accounts);
            var avms = _mapper.Map<IList<Account>, IList<AccountViewModel>>(ams);
            foreach (var avm in avms)
            {
                avm.Roles = Roles.GetRolesForUser(avm.UserName);
            }

            var onePageOfAccounts = avms.ToPagedList(1, Constants.PageSize);
            ViewBag.OnePageOfAccounts = onePageOfAccounts;

            var arvm = new AccountsRolesViewModel();
            arvm.Accounts = onePageOfAccounts;
            arvm.Roles = new List<RoleViewModel>();

            return View("Start", arvm);
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public RedirectToRouteResult ActivateUser(string userName)
        {
            _accountAdminService.MakeUserMember(userName);

            return RedirectToAction("Start");
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public RedirectToRouteResult BlockUser(string email)
        {
            _accountAdminService.MakeUserBlocked(email);

            return RedirectToAction("Start");
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public ViewResult ResetUserPassword(string userName)
        {
            var user = Membership.GetUser(userName);
            var password = user.ResetPassword();

            var cpm = new ChangePasswordViewModel();
            cpm.OldPassword = password;

            return View("NewPassword", cpm);
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public ActionResult ResendMail(string userName)
        {
            var user = Membership.GetUser(userName);
            if (user != null)
            {
                Guid guid = _mailDataService.GetVerificationGuid(user.Email);
                if (guid != Guid.Empty)
                {
                    string activationUrl = Url.Action("EmailVerification", "Account", new { uid = guid }, Request.Url.Scheme);

                    bool ok = _mailService.SendVerificationMail(user.UserName, user.Email, activationUrl);
                }
            }

            return RedirectToAction("Start");
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public ViewResult DeleteFromAccount(string userName)
        {
            MembershipUser user = Membership.GetUser(userName);

            var surveyId = _surveyAdminDataService.GetSurveyId(user.Email);
            var survey = _surveyAdminDataService.GetSurvey(surveyId);
            if (survey == null) // will not be null since id will be 0 if fail
            {
                survey = new Survey();
                survey.Name = userName;
                survey.PostalCode = "Ingen profil";
                survey.City = "Ingen profil";
            }

            var ssvm = _mapper.Map<ShortSurveyViewModel>(survey);
            ssvm.Note1 = user.Email;

            return View("Delete", ssvm);
        }

        [HttpPost]
        [Authorize(Roles = MemberRoles.Administrator)]
        public RedirectToRouteResult Delete(ShortSurveyViewModel ssvm)
        {
            if (ssvm.SurveyId >= 10)
            {
                _surveyAdminDataService.DeleteSurvey(ssvm.SurveyId);
            }

            string email = ssvm.Note1;
            var userName = Membership.GetUserNameByEmail(email);
            Membership.DeleteUser(userName, true);

            return RedirectToAction("Start");
        }

        [Authorize(Roles = MemberRoles.Administrator)]
        public ViewResult LogEntries(int? page)
        {
            ViewBag.CurrentPageId = PageIds.DevPage;

            IEnumerable<LogEntry> lems = _logDataService.GetSortedLogEntries();
            var levms = _mapper.Map<IEnumerable<LogEntry>, IList<LogEntryViewModel>>(lems);

            var pageNumber = page ?? 1;
            var onePageOfLogEntries = levms.ToPagedList(pageNumber, Constants.PageSize);
            ViewBag.OnePageOfLogEntries = onePageOfLogEntries;

            return View(onePageOfLogEntries);
        }
    }
}