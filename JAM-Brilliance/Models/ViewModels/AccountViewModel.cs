using System;
using System.ComponentModel.DataAnnotations;

using JAM.Core.Models;

namespace JAM.Brilliance.Models.ViewModels
{
    public class AccountViewModel
    {
        [Required(ErrorMessage = "{0} måste anges")]
        [StringLength(50, ErrorMessage = "{0} måste vara mellan {2} och {1} tecken långt.", MinimumLength = 4)]
        [Display(Name = "Användarnamn")]
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