namespace JAM.Core.Interfaces
{
    public interface IGeoService
    {
        void LookupAndStoreGeoCoordinates(int surveyId, string postalCode, string city, string country);
    }
}