using System;

namespace JAM.Core.Models
{
    public class AbuseReport
    {
        public int AbuseId { get; set; }

        public Guid AbuseGuid { get; set; }

        public int SelfSurveyId { get; set; }

        public string SelfName { get; set; }

        public int OtherSurveyId { get; set; }

        public string OtherName { get; set; }

        public string OtherEmail { get; set; }

        public int MessageId { get; set; }

        public int PictureId { get; set; }

        public DateTime ReportDate { get; set; }
    }
}