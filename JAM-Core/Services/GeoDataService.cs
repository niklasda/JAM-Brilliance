using System.Data.SqlTypes;
using System.Globalization;

using Dapper;

using JAM.Core.Interfaces;
using JetBrains.Annotations;
using Microsoft.SqlServer.Types;

namespace JAM.Core.Services
{
    [UsedImplicitly]
    public class GeoDataService : IGeoDataService
    {
        private readonly IDatabaseConfigurationService _c;

        public GeoDataService(IDatabaseConfigurationService configurationService)
        {
            _c = configurationService;
        }

        public async void StoreGeoCoordinates(int surveyId, double lat, double lon)
        {
            var point = string.Format("POINT({0} {1})", lat.ToString(CultureInfo.CreateSpecificCulture("en-US")), lon.ToString(CultureInfo.CreateSpecificCulture("en-US")));
            var coordinates = SqlGeography.STPointFromText(new SqlChars(new SqlString(point)), 4326);

            const string sqlDeleteOldCoordinates = "DELETE FROM GeoInfo WHERE SurveyId = @SurveyId";
            const string sqlInsertNewCoordinates = "INSERT INTO GeoInfo (SurveyId, Coordinates) VALUES (@SurveyId, @Coordinates)";

            using (var cn = _c.CreateConnection())
            {
                cn.Execute(sqlDeleteOldCoordinates, new { SurveyId = surveyId });

                await cn.ExecuteAsync(sqlInsertNewCoordinates, new { SurveyId = surveyId, Coordinates = coordinates });
            }
        }
    }
}