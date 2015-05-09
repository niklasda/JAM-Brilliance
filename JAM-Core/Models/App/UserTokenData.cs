using System;

namespace JAM.Core.Models.App
{
    public class UserTokenData
    {
        public string UserName { get; set; }

        public Guid Token { get; set; }

        public DateTime TokenIssueDate { get; set; }

        public string TokenIssueIP { get; set; }

        public string TokenIssueAppVersion { get; set; }

        public DateTime TokenLastUsedDate { get; set; }

        public string TokenLastUsedIP { get; set; }

        public string TokenLastUsedAppVersion { get; set; }

    }
}
