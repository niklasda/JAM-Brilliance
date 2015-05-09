using System.Collections.Generic;

namespace JAM.Brilliance.Models.ViewModels
{
    public class ConversationViewModel
    {
        public SendMessageViewModel OriginalMessage { get; set; }

        public IEnumerable<SendMessageViewModel> Messages { get; set; }

        //public IEnumerable<SendMessageViewModel> FromRecipient { get; set; }
    }
}