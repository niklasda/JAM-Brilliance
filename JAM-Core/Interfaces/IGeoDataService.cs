namespace JAM.Core.Interfaces
{
    public interface IGeoDataService
    {
        void StoreGeoCoordinates(int surveyId, double lat, double lon);
    }
}