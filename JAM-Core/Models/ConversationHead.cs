using System;

namespace JAM.Core.Models
{
    public class ConversationHead
    {
        // 1=messageReceived, 2=MessageSent, 3=favourite, 3=someones favourite
        public int Category { get; set; }

        public int OtherSurveyId { get; set; }

        public string OtherName { get; set; }

        public int NbrOf { get; set; }

        public int NbrOfUnread { get; set; }

        public bool IsDisabled { get; set; }

        public DateTime LastActionDate { get; set; }

        public string LastActionDateFormatted { get { return LastActionDate.ToString("dd-MMM hh:mm"); } }

    }
}