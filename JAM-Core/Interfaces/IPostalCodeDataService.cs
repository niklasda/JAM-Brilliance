using JAM.Core.Models;

namespace JAM.Core.Interfaces
{
    public interface IPostalCodeDataService
    {
        PostalCodeInfo GetPostalCodeInfo(string postalCode);
    }
}