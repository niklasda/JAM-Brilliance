using System;
using System.Net;

namespace JAM.Core.Models
{
    public class SendMessage
    {
        public int MessageId { get; set; }

        public Guid MessageGuid { get; set; }

        public int FromSurveyId { get; set; }

        public string FromName { get; set; }

        public bool IsFromDisabled { get; set; }

        public string ToName { get; set; }

        public bool IsToDisabled { get; set; }

        public int ToSurveyId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime SendDate { get; set; }

        public string SendDateFormatted { get { return SendDate.ToString("dd-MMM hh:mm"); } }

        public DateTime ReadDate { get; set; }

        public string Body { get; set; }

        public string BodyAsHtml
        {
            get { return WebUtility.HtmlEncode(Body).Replace("\n","<br>"); }
        }
    }
}