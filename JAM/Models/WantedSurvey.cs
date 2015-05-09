using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JAM.Models
{
    [Table("WantedSurveys")]
    public class WantedSurvey
    {
        [Key]
        public int WantedSurveyId { get; set; }

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

        // [Range(25, 75, ErrorMessage = "{0} måste vara mellan {1} och {2}")]
        public int AgeMin { get; set; }

        [Display(Name = "Max. ålder")]

        // [Range(25, 75, ErrorMessage = "{0} måste vara mellan {1} och {2}")]
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