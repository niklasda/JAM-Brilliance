using System;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading;

using JAM.Core.Interfaces;
using JAM.Core.Logic;
using JAM.Core.Models;
using JetBrains.Annotations;

namespace JAM.Core.Services
{
    [UsedImplicitly]
    public class GeoService : IGeoService
    {
        private readonly IGeoDataService _geoDataService;

        public GeoService(IGeoDataService geoDataService)
        {
            _geoDataService = geoDataService;
        }

        public void LookupAndStoreGeoCoordinates(int surveyId, string postalCode, string city, string country)
        {
            //// string pCode1 = "22222, Lund, Sverige";
            //// string pCode2 = "2900, Hellerup, Danmark";
            //// string pCode2 = "W6 0AE, London, England";

            var countryCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            string pcodeCityCountry = string.Format("{0}, {1}, {2}", postalCode, city, country);

            var restRequestUri = new Uri(string.Format("http://dev.virtualearth.net/REST/v1/Locations?q={0}&c={1}&key={2}", pcodeCityCountry, countryCode, Constants.BingKey));

            ProcessResponse(restRequestUri, surveyId);
        }

        private void ProcessResponse(Uri uri, int surveyId)
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetStreamAsync(uri);

            response.ContinueWith(
                x =>
                {
                    var ser1 = new DataContractJsonSerializer(typeof(Response));
                    var r1 = ser1.ReadObject(response.Result) as Response;

                    if (r1 != null && r1.ResourceSets[0].EstimatedTotal > 0)
                    {
                        var lat = r1.ResourceSets[0].Resources[0].GeocodePoints[0].Coordinates[0];
                        var lon = r1.ResourceSets[0].Resources[0].GeocodePoints[0].Coordinates[1];

                        _geoDataService.StoreGeoCoordinates(surveyId, lat, lon);
                    }
                });
        }
    }
}