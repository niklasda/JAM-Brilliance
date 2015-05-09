using System.ComponentModel.DataAnnotations;

namespace JAM.Brilliance.Models.ViewModels
{
    public class LogOnViewModel
    {
        [Required(ErrorMessage = "{0} måste anges")]
        [Display(Name = "Användarnamn")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} måste anges")]
        [DataType(DataType.Password)]
        [Display(Name = "Lösenord")]
        public string Password { get; set; }

        [Display(Name = "Kom ihåg mig?")]
        public bool RememberMe { get; set; }
    }
}