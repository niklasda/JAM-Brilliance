using System.Collections.Generic;
using JAM.Core.Models;

namespace JAM.Brilliance.Areas.Mobile.Models.Response
{
    public class MessageModel : ResponseBase
    {
        public SendMessage AddedMessage { get; set; }
    }
}