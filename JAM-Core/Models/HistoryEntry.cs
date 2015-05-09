using System;

namespace JAM.Core.Models
{
    public class HistoryEntry
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