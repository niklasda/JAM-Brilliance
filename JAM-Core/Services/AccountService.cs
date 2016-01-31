using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Security;
using AutoMapper;
using JAM.Core.Interfaces;
using JAM.Core.Logic;
using JAM.Core.Models;
using JetBrains.Annotations;

namespace JAM.Core.Services
{
    [UsedImplicitly]
    public class AccountService : IAccountService
    {
        private readonly IDataCache _dataCache;
        private readonly IMapper _mapper;

        public AccountService(IDataCache dataCache, IMapper mapper)
        {
            _dataCache = dataCache;
            _mapper = mapper;
        }

        public string GetCurrentUserEmail()
        {
            var user = Membership.GetUser();
            return user != null ? user.Email : "";
        }

        public void SetUserComment(MembershipUser user, string cultureCode, string realName)
        {
            user.Comment = string.Format("{0}{1}{2}", cultureCode, Constants.CommentSeparator, realName);
            Membership.UpdateUser(user);
        }

        public void UpdateCurrentUserCommentCultureCode(string cultureCode)
        {
            var user = Membership.GetUser();
            if (user != null)
            {
                var name = GetCurrentUserCommentRealName();

                SetUserComment(user, cultureCode, name);
            }
        }

        public string GetCurrentUserCommentCultureCode()
        {
            string cultureCode = Constants.DefaultCountryCode;

            var user = Membership.GetUser();
            if (user != null && !string.IsNullOrEmpty(user.Comment))
            {
                var parts = user.Comment.Split(new[] { Constants.CommentSeparator }, StringSplitOptions.None);
                if (parts.Length == 2 || parts.Length == 1)
                {
                    cultureCode = parts[0];
                }
            }

            cultureCode = VerifyCultureCode(cultureCode);

            return cultureCode;
        }

        public string GetCurrentUserCommentRealName()
        {
            var user = Membership.GetUser();
            if (user != null && !string.IsNullOrEmpty(user.Comment))
            {
                var parts = user.Comment.Split(new[] {Constants.CommentSeparator}, StringSplitOptions.None);
                if (parts.Length == 2)
                {
                    return parts[1];
                }

                return user.Comment;
            }

            return string.Empty;
        }

        public string GetUserName(string email)
        {
            return Membership.GetUserNameByEmail(email);
        }

        public string GetEmail(string userName)
        {
            var user = Membership.GetUser(userName);
            return user.Email;
        }

        public void DeactivateCurrentAccount()
        {
            var user = Membership.GetUser();
            user.IsApproved = false;
            Membership.UpdateUser(user);
            FormsAuthentication.SignOut();
        }

        public IEnumerable<Account> GetOnlineUsers(ISurveyDataService surveyDataService)
        {
            MembershipUserCollection accounts = Membership.GetAllUsers();
            IList<Account> ams = _mapper.Map<MembershipUserCollection, IList<Account>>(accounts);

            var onlineUsers = ams.Where(x => x.IsOnline).ToList();

            foreach (var user in onlineUsers)
            {
                var surveyId = surveyDataService.GetSurveyId(user.Email);
                user.SurveyId = surveyId;

                if (surveyId > 0)
                {
                    var survey = surveyDataService.GetSurvey(surveyId);

                    user.UserName = survey.Name;
                    user.GeoLocation = new Point { Coordinates = new[] { survey.Lat, survey.Long } };
                }
            }

            return onlineUsers.Where(x => x.SurveyId > 0);
        }

        public int GetOnlineUsersCount()
        {
            int onlineUsersCount = Membership.GetNumberOfUsersOnline();
            return onlineUsersCount;
        }

        public bool IsUserMember(string userName)
        {
            return Roles.IsUserInRole(userName, MemberRoles.Member);
        }

        public bool IsUserMobileApp(string userName)
        {
            return Roles.IsUserInRole(userName, MemberRoles.MobileApp);
        }

        public bool IsUserPending(string userName)
        {
            return Roles.IsUserInRole(userName, MemberRoles.Pending);
        }

        public bool IsUserAdmin(string userName)
        {
            return Roles.IsUserInRole(userName, MemberRoles.Administrator);
        }

        public DateTime GetCurrentUserLastActivity(string email)
        {
            var userName = Membership.GetUserNameByEmail(email);
            if (!string.IsNullOrEmpty(userName))
            {
                var user = Membership.GetUser(userName);
                return user.LastActivityDate;
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        public string[] GetCurrentUserRoles()
        {
            var roles = Roles.GetRolesForUser();
            return roles;
        }

        public bool IsUserApproved(string userName)
        {
            var user = Membership.GetUser(userName);
            if (user != null && !user.IsApproved)
            {
                return false;
            }

            return true;
        }

        public bool IsUserLockedOut(string userName)
        {
            var user = Membership.GetUser(userName);
            if (user != null && user.IsLockedOut)
            {
                return true;
            }

            return false;
        }

        public void MakeUserMobileApp(string userName)
        {
            if (!IsSuperUser(userName))
            {
                RemoveFromAllRoles(userName);

                if (!Roles.IsUserInRole(userName, MemberRoles.MobileApp))
                {
                    Roles.AddUserToRole(userName, MemberRoles.MobileApp);
                }
            }
        }

        public void MakeUserPending(string userName)
        {
            if (!IsSuperUser(userName))
            {
                RemoveFromAllRoles(userName);

                if (!Roles.IsUserInRole(userName, MemberRoles.Pending))
                {
                    Roles.AddUserToRole(userName, MemberRoles.Pending);
                }
            }
        }

        public void MakeUserMember(string userName)
        {
            if (!IsSuperUser(userName))
            {
                RemoveFromAllRoles(userName);

                var u = Membership.GetUser(userName);

                if (!u.IsApproved)
                {
                    u.IsApproved = true;
                    Membership.UpdateUser(u);
                }

                if (u.IsLockedOut)
                {
                    u.UnlockUser();
                }

                if (!Roles.IsUserInRole(userName, MemberRoles.Member))
                {
                    Roles.AddUserToRole(userName, MemberRoles.Member);
                }
            }
        }

        public void MakeUserBlocked(string email)
        {
            var userName = Membership.GetUserNameByEmail(email);
            if (!string.IsNullOrEmpty(userName) && !IsSuperUser(userName))
            {
                RemoveFromAllRoles(userName);

                if (!Roles.IsUserInRole(userName, MemberRoles.Blocked))
                {
                    Roles.AddUserToRole(userName, MemberRoles.Blocked);
                }
            }
        }

        private string VerifyCultureCode(string cultureCode)
        {
            try
            {
                var culture = CultureInfo.GetCultureInfo(cultureCode);
                return culture.TwoLetterISOLanguageName;
            }
            catch (Exception)
            {
                return Constants.DefaultCountryCode;
            }
        }

        private bool IsSuperUser(string userName)
        {
            return userName.Trim().ToLower().Equals(_dataCache.DevAdminSurveyName);
        }

        private void RemoveFromAllRoles(string userName)
        {
            foreach (var memberRole in MemberRoles.GetAllMemberRoles())
            {
                if (Roles.IsUserInRole(userName, memberRole))
                {
                    Roles.RemoveUserFromRole(userName, memberRole);
                }
            }
        }
    }
}