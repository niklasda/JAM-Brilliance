using System.ComponentModel.DataAnnotations;

namespace JAM.Brilliance.Models.ViewModels
{
    public class SurveyPage5ViewModel : SurveyViewModelBase
    {
        [Display(Name = "Astrologi/New age", GroupName = "Intressen")]
        public bool IsHobbyNewAge { get; set; }

        [Display(Name = "Bilar/motorcyklar")]
        public bool IsHobbyCars { get; set; }

        [Display(Name = "Dans")]
        public bool IsHobbyDance { get; set; }

        [Display(Name = "Djur")]
        public bool IsHobbyAnimals { get; set; }

        [Display(Name = "Film/bio")]
        public bool IsHobbyMovies { get; set; }

        [Display(Name = "Fiske/jakt")]
        public bool IsHobbyHunting { get; set; }

        [Display(Name = "Heminredning")]
        public bool IsHobbyInterior { get; set; }

        [Display(Name = "Fotografering")]
        public bool IsHobbyPhoto { get; set; }

        [Display(Name = "Friluftsliv")]
        public bool IsHobbyOutdoors { get; set; }

        [Display(Name = "Konst")]
        public bool IsHobbyArt { get; set; }

        [Display(Name = "Spela kort/brädspel")]
        public bool IsHobbyGame { get; set; }

        [Display(Name = "Matlagning")]
        public bool IsHobbyCooking { get; set; }

        [Display(Name = "Böcker/läsning")]
        public bool IsHobbyReading { get; set; }

        [Display(Name = "Litteratur/historia")]
        public bool IsHobbyHistory { get; set; }

        [Display(Name = "Måla")]
        public bool IsHobbyPainting { get; set; }

        [Display(Name = "Muséer/utställningar")]
        public bool IsHobbyMuseum { get; set; }

        [Display(Name = "Musik")]
        public bool IsHobbyMusic { get; set; }

        [Display(Name = "Politik")]
        public bool IsHobbyPolitics { get; set; }

        [Display(Name = "Resor")]
        public bool IsHobbyTravel { get; set; }

        [Display(Name = "Rollspel")]
        public bool IsHobbyRolePlaying { get; set; }

        [Display(Name = "Shopping")]
        public bool IsHobbyShopping { get; set; }

        [Display(Name = "Sjunga/spela instrument")]
        public bool IsHobbySinging { get; set; }

        [Display(Name = "Skriva")]
        public bool IsHobbyWriting { get; set; }

        [Display(Name = "Spel")]
        public bool IsHobbyGaming { get; set; }

        [Display(Name = "Träning")]
        public bool IsHobbyWorkout { get; set; }

        [Display(Name = "Teater")]
        public bool IsHobbyDrama { get; set; }

        [Display(Name = "Teckna")]
        public bool IsHobbyDrawing { get; set; }

        [Display(Name = "Trädgårdsarbeta")]
        public bool IsHobbyGardening { get; set; }

        [Display(Name = "TV/dataspel")]
        public bool IsHobbyTv { get; set; }

        [Display(Name = "Föreningsverksamhet")]
        public bool IsHobbyCircles { get; set; }

        [Display(Name = "Vandring")]
        public bool IsHobbyHiking { get; set; }

        [Display(Name = "Vinprovning")]
        public bool IsHobbyWine { get; set; }

        [Display(Name = "Mycket aktiviteter", GroupName = "Hur ser en helg ut?")]
        public bool IsWeekendActive { get; set; }

        [Display(Name = "Återhämtning/lugn")]
        public bool IsWeekendChill { get; set; }

        [Display(Name = "Spontan")]
        public bool IsWeekendRandom { get; set; }

        [Display(Name = "Välplanerad")]
        public bool IsWeekendPlanned { get; set; }

        [Display(Name = "Aerobics/dans", GroupName = "Idrottsintressen")]
        public bool IsSportDance { get; set; }

        [Display(Name = "Bollsport")]
        public bool IsSportBalls { get; set; }

        [Display(Name = "Kampsport")]
        public bool IsSportMartial { get; set; }

        [Display(Name = "Fitness/gym")]
        public bool IsSportFitness { get; set; }

        [Display(Name = "Äventyr")]
        public bool IsSportAdventure { get; set; }

        [Display(Name = "Golf")]
        public bool IsSportGolf { get; set; }

        [Display(Name = "Löpning")]
        public bool IsSportRunning { get; set; }

        [Display(Name = "Ridning/hästar")]
        public bool IsSportRiding { get; set; }

        [Display(Name = "Segling/båtsport")]
        public bool IsSportSailing { get; set; }

        [Display(Name = "Skateboard")]
        public bool IsSportSkating { get; set; }

        [Display(Name = "Vindsurfing")]
        public bool IsSportSurfing { get; set; }

        [Display(Name = "Yoga/meditation")]
        public bool IsSportYoga { get; set; }

        [Display(Name = "Racketsport")]
        public bool IsSportRaquet { get; set; }

        [Display(Name = "Frukostdejt", GroupName = "Din favoritdejt")]
        public bool IsDateBreakfast { get; set; }

        [Display(Name = "Lunchdejt")]
        public bool IsDateLunch { get; set; }

        [Display(Name = "Romantisk middag")]
        public bool IsDateDinner { get; set; }

        [Display(Name = "Bio")]
        public bool IsDateMovie { get; set; }

        [Display(Name = "Casual matbit på en bar")]
        public bool IsDateBarSnack { get; set; }

        [Display(Name = "Drink")]
        public bool IsDateDrinks { get; set; }

        [Display(Name = "Picnic")]
        public bool IsDatePicnic { get; set; }

        [Display(Name = "Middag i hemmet")]
        public bool IsDateDinnerHome { get; set; }

        [Display(Name = "Fika")]
        public bool IsDateFika { get; set; }

        [Display(Name = "Mycket viktigt", GroupName = "Vikten av utseende")]
        public bool IsLooksImportant { get; set; }

        [Display(Name = "Mindre viktigt")]
        public bool IsLooksLessImportant { get; set; }

        [Display(Name = "Vill göra det mesta tillsammans", GroupName = "Min syn på en kärleksrelation")]
        public bool IsRelationTogether { get; set; }

        [Display(Name = "Värdesätter att man har 'ett eget liv' trots att man är i relation")]
        public bool IsRelationOwnTime { get; set; }

        [Display(Name = "Föredrar äktenskap")]
        public bool IsRelationMarriage { get; set; }

        [Display(Name = "Föredrar samboskap")]
        public bool IsRelationLiving { get; set; }

        [Display(Name = "Föredrar särboskap")]
        public bool IsRelationNotLiving { get; set; }
    }
}