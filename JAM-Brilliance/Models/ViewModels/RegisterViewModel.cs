using System.ComponentModel.DataAnnotations;

using JAM.Core.Attributes;

namespace JAM.Brilliance.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "{0} måste anges")]
        [StringLength(50, ErrorMessage = "{0} måste vara mellan {2} och {1} tecken långt.", MinimumLength = 4)]
        [Display(Name = "Användarnamn")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Du måste ange {0}")]
        [Display(Name = "Riktigt namn")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} måste anges")]
        [StringLength(256, ErrorMessage = "{0} får vara max {1} tecken långt.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Ogiltig {0}")]
        [Display(Name = "E-Post (konfidentiellt)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} måste anges")]
        [StringLength(100, ErrorMessage = "{0} måste vara mellan {2} och {1} tecken långt.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bekräfta lösenord")]
        [Compare("Password", ErrorMessage = "Lösenorden du angivit är inte samma.")]
        public string ConfirmPassword { get; set; }

        [MustBeTrue(ErrorMessage = "Du måste accepter användarvillkoren")]
        [Display(Name = "Användarvillkor")]
        public bool UserTermsAgreedTo { get; set; }
    }
}