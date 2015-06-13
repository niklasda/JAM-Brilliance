using System;
using System.Collections.Generic;
using System.Web.Security;
using JAM.Core.Models;

namespace JAM.Core.Interfaces
{
    public interface IAccountService
    {
        bool IsUserMember(string userName);

        bool IsUserPending(string userName);

        bool IsUserApproved(string userName);

        bool IsUserLockedOut(string userName);

        void MakeUserMember(string userName);

        void MakeUserMobileApp(string userName);

        void MakeUserPending(string userName);

        void MakeUserBlocked(string email);

        string GetCurrentUserEmail();

        string GetUserName(string email);

        string GetEmail(string userName);

        void DeactivateCurrentAccount();

        IEnumerable<Account> GetOnlineUsers(ISurveyDataService surveyDataService);

        void SetUserComment(MembershipUser user, string cultureCode, string realName);

        void UpdateCurrentUserCommentCultureCode(string cultureCode);

        string GetCurrentUserCommentCultureCode();

        string GetCurrentUserCommentRealName();

        int GetOnlineUsersCount();

        bool IsUserAdmin(string userName);

        DateTime GetCurrentUserLastActivity(string email);

        string[] GetCurrentUserRoles();
        bool IsUserMobileApp(string userName);
    }
}