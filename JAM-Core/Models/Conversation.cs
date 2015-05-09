using System.Collections.Generic;
using System.Linq;

namespace JAM.Core.Models
{
    public class Conversation
    {
        public SendMessage OriginalMessage { get; set; }

        public IEnumerable<SendMessage> Messages { get; set; }

        public string OtherSurveyName { get; set; }
    }
}