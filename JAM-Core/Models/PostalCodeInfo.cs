using System.Linq;

namespace JAM.Core.Models
{
    public class PostalCodeInfo
    {
        public int PostalCodeId { get; set; }
        
        public string PostalCode { get; set; }
        
        public string City { get; set; }
        
        public string county { get; set; }
        
        public double lat { get; set; }
        
        public double lng { get; set; }
    }
}