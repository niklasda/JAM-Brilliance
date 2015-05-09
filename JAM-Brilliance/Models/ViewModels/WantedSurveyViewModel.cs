using System.ComponentModel.DataAnnotations;

namespace JAM.Brilliance.Models.ViewModels
{
    public class WantedSurveyViewModel : SurveyViewModelBase
    {
        [Display(Name = "150 cm - 160 cm", GroupName = "Längd")]
        public bool Is150 { get; set; }

        [Display(Name = "160 cm - 170 cm")]
        public bool Is160 { get; set; }

        [Display(Name = "170 cm - 180 cm")]
        public bool Is170 { get; set; }

        [Display(Name = "180 cm - 190 cm")]
        public bool Is180 { get; set; }

        [Display(Name = "190 cm ->")]
        public bool Is190 { get; set; }

        [Display(Name = "Min. ålder", GroupName = "Ålder")]
        public int AgeMin { get; set; }

        [Display(Name = "Max. ålder")]
        public int AgeMax { get; set; }

        [Display(Name = "Slank", GroupName = "Kroppstyp")]
        public bool IsBodySlim { get; set; }

        [Display(Name = "Ganska genomsnittlig")]
        public bool IsBodyAverage { get; set; }

        [Display(Name = "Atletisk/muskulös")]
        public bool IsBodyAthletic { get; set; }

        [Display(Name = "Kraftigt byggt")]
        public bool IsBodyHeavy { get; set; }

        [Display(Name = "Överviktig")]
        public bool IsBodyChubby { get; set; }

        [Display(Name = "Sport", GroupName = "Intressen")]
        public bool IsHobbySport { get; set; }

        [Display(Name = "Musik")]
        public bool IsHobbyMusic { get; set; }

        [Display(Name = "Kultur")]
        public bool IsHobbyCulture { get; set; }

        [Display(Name = "Natur")]
        public bool IsHobbyNature { get; set; }

        [Display(Name = "Vänner/socialt umgänge")]
        public bool IsHobbyFriends { get; set; }

        [Display(Name = "Teknik")]
        public bool IsHobbyTech { get; set; }
    }
}