using System;

namespace JAM.Brilliance.Models.ViewModels
{
    public class AbuseReportViewModel
    {
        public int AbuseId { get; set; }

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