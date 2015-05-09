using System;

namespace JAM.Brilliance.Models.ViewModels
{
    public class HistoryEntryViewModel
    {
        public int SelfSurveyId { get; set; }

        public string SelfName { get; set; }

        public int OtherSurveyId { get; set; }

        public string OtherName { get; set; }

        public bool IsDisabled { get; set; }

        public DateTime LastVisitDate { get; set; }

        public int VisitationCount { get; set; }
    }
}