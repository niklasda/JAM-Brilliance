using System.Collections.Generic;

namespace JAM.Dating.Logic
{
    public static class MemberRoles
    {
        public const string Administrator = "Administrator";

        public const string Member = "Member";

        public const string Pending = "Pending";

        public const string Blocked = "Blocked";

        public static IEnumerable<string> GetAllMemberRoles()
        {
            return new[] { Administrator, Member, Pending, Blocked };
        }
    }
}