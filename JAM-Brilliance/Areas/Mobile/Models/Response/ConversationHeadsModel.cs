using System.Collections.Generic;
using JAM.Core.Models;

namespace JAM.Brilliance.Areas.Mobile.Models.Response
{
    public class ConversationHeadsModel : ResponseBase
    {
        public IEnumerable<ConversationHead> Heads { get; set; }
    }
}