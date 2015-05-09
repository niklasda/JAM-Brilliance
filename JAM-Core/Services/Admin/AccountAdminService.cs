using System.Web.Security;

using JAM.Core.Interfaces;
using JAM.Core.Interfaces.Admin;
using JAM.Core.Logic;
using JetBrains.Annotations;

namespace JAM.Core.Services.Admin
{
    [UsedImplicitly]
    public class AccountAdminService : AccountService, IAccountAdminService
    {
        private readonly IDataCache _dataCache;

        public AccountAdminService(IDataCache dataCache) : base(dataCache)
        {
            _dataCache = dataCache;
        }

        public void CreateAllRoles()
        {
            if (!Roles.RoleExists(MemberRoles.Administrator))
            {
                Roles.CreateRole(MemberRoles.Administrator);
            }

            if (!Roles.RoleExists(MemberRoles.Member))
            {
                Roles.CreateRole(MemberRoles.Member);
            }

            if (!Roles.RoleExists(MemberRoles.MobileApp))
            {
                Roles.CreateRole(MemberRoles.MobileApp);
            }

            if (!Roles.RoleExists(MemberRoles.Pending))
            {
                Roles.CreateRole(MemberRoles.Pending);
            }

            if (!Roles.RoleExists(MemberRoles.Blocked))
            {
                Roles.CreateRole(MemberRoles.Blocked);
            }
        }

        public void SetupSpecialUser()
        {
            if (!Roles.IsUserInRole(_dataCache.DevAdminSurveyName, MemberRoles.Administrator))
            {
                Roles.AddUserToRole(_dataCache.DevAdminSurveyName, MemberRoles.Administrator);
            }

            if (!Roles.IsUserInRole(_dataCache.AdminSurveyName, MemberRoles.Administrator))
            {
                Roles.AddUserToRole(_dataCache.AdminSurveyName, MemberRoles.Administrator);
            }

            if (!Roles.IsUserInRole(_dataCache.DevAdminSurveyName, MemberRoles.Member))
            {
                Roles.AddUserToRole(_dataCache.DevAdminSurveyName, MemberRoles.Member);
            }

            if (!Roles.IsUserInRole(_dataCache.AdminSurveyName, MemberRoles.Member))
            {
                Roles.AddUserToRole(_dataCache.AdminSurveyName, MemberRoles.Member);
            }
        }
    }
}