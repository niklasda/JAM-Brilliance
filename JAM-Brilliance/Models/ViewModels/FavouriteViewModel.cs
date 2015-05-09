using System;

namespace JAM.Brilliance.Models.ViewModels
{
    public class FavouriteViewModel
    {
        public int FavouriteId { get; set; }

        public int SelfSurveyId { get; set; }

        public int OtherSurveyId { get; set; }

        public string SelfName { get; set; }

        public string OtherName { get; set; }

        public bool IsDisabled { get; set; }

        public DateTime AddedDate { get; set; }
    }
}