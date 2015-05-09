using System.ComponentModel.DataAnnotations;

namespace JAM.Brilliance.Models.ViewModels
{
    public class SearchAdvCriteriaViewModel
    {
        [StringLength(150, ErrorMessage = "{0} måste vara minst {2} tecken långt.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "Stad")]
        public string City { get; set; }
    }
}