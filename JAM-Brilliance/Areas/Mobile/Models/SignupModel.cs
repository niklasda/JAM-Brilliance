using System;

namespace JAM.Brilliance.Areas.Mobile.Models
{
    public class SignupModel
    {
        public bool AmMan { get; set; }

        public bool AmWoman { get; set; }

        public bool WantMan { get; set; }

        public bool WantWoman { get; set; }

        public string PostalCode { get; set; }
        
        public string Country { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}