using System;

namespace JAM.Brilliance.Areas.Mobile.Attributes
{
    public class ValidateTokenAttribute : Attribute
    {
        public bool FromUrl { get; set; }
    }
}