using System;

namespace JAM.Core.Models
{
    public class Favourite
    {
        public int FavouriteId { get; set; }

        public Guid FavouriteGuid { get; set; }

        public int SelfSurveyId { get; set; }

        public int OtherSurveyId { get; set; }

        public string SelfName { get; set; }

        public string OtherName { get; set; }

        public bool IsDisabled { get; set; }

        public DateTime AddedDate { get; set; }
    }
}