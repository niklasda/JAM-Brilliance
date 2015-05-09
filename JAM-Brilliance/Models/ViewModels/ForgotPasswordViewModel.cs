using System.ComponentModel.DataAnnotations;

namespace JAM.Brilliance.Models.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "{0} måste anges")]
        [StringLength(256, ErrorMessage = "{0} får vara max {1} tecken långt.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Ogiltig {0}")]
        [Display(Name = "E-Post")]
        public string Email { get; set; }
    }
}