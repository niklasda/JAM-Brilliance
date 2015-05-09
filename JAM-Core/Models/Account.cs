using System;

namespace JAM.Core.Models
{
    public class Account
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public bool IsApproved { get; set; }

        public bool IsLockedOut { get; set; }

        public bool IsOnline { get; set; }

        public DateTime LastActivityDate { get; set; }

        public string[] Roles { get; set; }

        public int SurveyId { get; set; }

        public Point GeoLocation { get; set; }
    }
}