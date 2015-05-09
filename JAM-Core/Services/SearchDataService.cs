using System.Collections.Generic;
using System.Linq;

using Dapper;
using JAM.Core.Interfaces;
using JAM.Core.Models;
using JetBrains.Annotations;

namespace JAM.Core.Services
{
    [UsedImplicitly]
    public class SearchDataService : ISearchDataService
    {
        private readonly IDatabaseConfigurationService _c;

        public SearchDataService(IDatabaseConfigurationService configurationService)
        {
            _c = configurationService;
        }

        public IEnumerable<SearchResult> SimplerSearch(SearchCriteria sc)
        {
            // no longer works with full site
            // "0xE6100000010C0200008031632A40FFFFFF3F1FDA4B40;"
            // todo fix 4 and 2
            const string sqlSimplerSearchSurvey = "DECLARE @here geography;"
                                               + "SELECT @here = Coordinates FROM PostalCodes WHERE PostalCode = @PostalCode; " 
                                               + "SELECT Surveys.SurveyId, Surveys.Name, Surveys.PostalCode, Surveys.City, Surveys.Birth, Surveys.Height, CAST(PostalCodes.Coordinates.STDistance(@here) AS int)/1000 AS Distance FROM Surveys "
                                               + "LEFT OUTER JOIN PostalCodes ON Surveys.PostalCode = PostalCodes.PostalCode "
                                               + "WHERE Surveys.IsDisabled = 0 AND Surveys.OriginId = @OriginId "
                                               + "AND Surveys.WhatSearchingForWhatId = CASE @WhatSearchingForWhatId WHEN 2 THEN 4 "
                                               + "WHEN 4 THEN 2 "
                                               + "ELSE Surveys.WhatSearchingForWhatId END "
                                               + "AND year(Surveys.Birth) > @StartYear "
                                               + "AND year(Surveys.Birth) < @StopYear "
                                               + "AND Surveys.SurveyId != @SelfSurveyId "
                                               + "ORDER BY PostalCodes.Coordinates.STDistance(@here)";
            using (var cn = _c.CreateConnection())
            {
                IEnumerable<SearchResult> srs = cn.Query<SearchResult>(sqlSimplerSearchSurvey, sc);
                return srs;
            }
        }

        public IEnumerable<SearchResult> SimpleSearch(SearchCriteria sc, Survey survey)
        {
            // todo fix 4 and 2
            const string sqlSimpleSearchSurvey = "SELECT SurveyId, Name, PostalCode, City, Birth, Height FROM Surveys "
                                               + "WHERE IsDisabled = 0 AND OriginId = @OriginId "
                                               + "AND WhatSearchingForWhatId = CASE @WhatSearchingForWhatId WHEN 2 THEN 4 "
                                               + "WHEN 4 THEN 2 "
                                               + "ELSE WhatSearchingForWhatId END "
                                               + "AND Height > @HeightMin AND Height < @HeightMax AND year(Birth) > @StartYear AND year(Birth) < @StopYear AND City LIKE @CityPattern";
            using (var cn = _c.CreateConnection())
            {
                sc.WhatSearchingForWhatId = survey.WhatSearchingForWhatId;
                IEnumerable<SearchResult> srs = cn.Query<SearchResult>(sqlSimpleSearchSurvey, sc).Where(x => x.SurveyId != survey.SurveyId);
                return srs;
            }
        }

        public IEnumerable<SearchResult> AdvancedSearch(SearchAdvCriteria sac, Survey survey)
        {
            const string sqlAdvSearchSurvey = "SELECT SurveyId, Name, City, Birth, Height FROM Surveys "
                                            + "WHERE IsDisabled = 0 AND OriginId = @OriginId "
                                            + "AND WhatSearchingForWhatsId = CASE @WhatSearchingForWhatId WHEN 2 THEN 4 "
                                            + "WHEN 4 THEN 2 "
                                            + "ELSE WhatSearchingForWhatId END "
                                            + "AND City LIKE @CityPattern";
            using (var cn = _c.CreateConnection())
            {
                sac.WhatSearchingForWhatId = survey.WhatSearchingForWhatId;
                IEnumerable<SearchResult> srs = cn.Query<SearchResult>(sqlAdvSearchSurvey, sac).Where(x => x.SurveyId != survey.SurveyId);
                return srs;
            }
        }
    }
}