using System;

namespace JAM.Brilliance.Areas.Mobile.Models.Response
{
    public abstract class ResponseBase
    {
        public bool Success { get; set; }

        public Guid Token { get; set; }

        public string Message { get; set; }
    }
}