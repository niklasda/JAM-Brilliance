namespace JAM.Core.Models
{
    public class WantedSurvey
    {
        public int WantedSurveyId { get; set; }

        public int SurveyId { get; set; }

        public bool Is150 { get; set; }

        public bool Is160 { get; set; }

        public bool Is170 { get; set; }

        public bool Is180 { get; set; }

        public bool Is190 { get; set; }

        public int AgeMin { get; set; }

        public int AgeMax { get; set; }

        public bool IsBodySlim { get; set; }

        public bool IsBodyAverage { get; set; }

        public bool IsBodyAthletic { get; set; }

        public bool IsBodyHeavy { get; set; }

        public bool IsBodyChubby { get; set; }

        public bool IsHobbySport { get; set; }

        public bool IsHobbyMusic { get; set; }

        public bool IsHobbyCulture { get; set; }

        public bool IsHobbyNature { get; set; }

        public bool IsHobbyFriends { get; set; }

        public bool IsHobbyTech { get; set; }
    }
}