namespace JAM.Core.Interfaces.Admin
{
    public interface IAccountAdminService : IAccountService
    {
        void CreateAllRoles();

        void SetupSpecialUser();
        void AddRoleToUser(string userName, string roleName);
    }
}