using System.ComponentModel.DataAnnotations;
using JAM.Brilliance.App_GlobalResources.Survey;

namespace JAM.Brilliance.Models.ViewModels
{
    public class SearchCriteriaViewModel
    {
        [StringLength(150, ErrorMessage = "{0} måste vara minst {2} tecken långt.", MinimumLength = 2)]
        [Display(ResourceType = typeof(SurveyTexts), Name = "Signup_City")]
        public string City { get; set; }

        [Display(Name = "Ålder")]
        public int AgeMin { get; set; }

        public int AgeMax { get; set; }

        [Display(Name = "Längd")]
        public int HeightMin { get; set; }

        public int HeightMax { get; set; }

        [Display(Name = "Har Barn")]
        public bool HasKids { get; set; }
    }
}