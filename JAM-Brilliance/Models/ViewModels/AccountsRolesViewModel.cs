using System.Collections.Generic;

namespace JAM.Brilliance.Models.ViewModels
{
    public class AccountsRolesViewModel
    {
        public IEnumerable<AccountViewModel> Accounts { get; set; }

        public IEnumerable<RoleViewModel> Roles { get; set; }
    }
}