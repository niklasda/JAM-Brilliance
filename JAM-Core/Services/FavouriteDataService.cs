using System.Collections.Generic;
using System.Linq;
using Dapper;
using JAM.Core.Interfaces;
using JAM.Core.Models;
using JetBrains.Annotations;

namespace JAM.Core.Services
{
    [UsedImplicitly]
    public class FavouriteDataService : IFavouriteDataService
    {
        private readonly IDatabaseConfigurationService _c;

        public FavouriteDataService(IDatabaseConfigurationService configurationService)
        {
            _c = configurationService;
        }

        public IEnumerable<Favourite> GetFavourites(int surveyId)
        {
            const string sqlGetFavourites = "SELECT Favourites.*, Surveys.Name AS OtherName, Surveys.IsDisabled FROM Favourites "
                                            + "INNER JOIN Surveys ON Surveys.SurveyId = Favourites.OtherSurveyId WHERE SelfSurveyId = @SelfSurveyId";
            using (var cn = _c.CreateConnection())
            {
                var fms = cn.Query<Favourite>(sqlGetFavourites, new { SelfSurveyId = surveyId });
                return fms;
            }
        }

        public IEnumerable<Favourite> GetFans(int surveyId)
        {
            const string sqlGetFans = "SELECT Favourites.*, Surveys.Name AS SelfName, Surveys.IsDisabled  FROM Favourites "
                                      + "INNER JOIN Surveys ON Surveys.SurveyId = Favourites.SelfSurveyId WHERE OtherSurveyId = @OtherSurveyId";
            using (var cn = _c.CreateConnection())
            {
                var fms = cn.Query<Favourite>(sqlGetFans, new { OtherSurveyId = surveyId });
                return fms;
            }
        }

        public int GetFansCount(int surveyId)
        {
            const string sqlGetFansCount = "SELECT COUNT(*) FROM Favourites WHERE OtherSurveyId = @OtherSurveyId";
            using (var cn = _c.CreateConnection())
            {
                var fansCount = cn.Query<int>(sqlGetFansCount, new { OtherSurveyId = surveyId }).SingleOrDefault();
                return fansCount;
            }
        }

        public bool AddFavourite(Favourite favourite)
        {
            const string sqlFavouriteExists = "SELECT FavouriteId FROM Favourites WHERE SelfSurveyId  = @SelfSurveyId AND OtherSurveyId  = @OtherSurveyId";
            const string sqlInsertFavourite = "INSERT INTO Favourites (SelfSurveyId, OtherSurveyId) VALUES (@SelfSurveyId, @OtherSurveyId)";
            using (var cn = _c.CreateConnection())
            {
                int favouriteId = cn.Query<int>(sqlFavouriteExists, favourite).FirstOrDefault();
                if (favouriteId == 0)
                {
                    int nbrOfRows = cn.Execute(sqlInsertFavourite, favourite);
                    return nbrOfRows == 1;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool RemoveFavourite(Favourite favourite)
        {
            const string sqlDeleteFavourite = "DELETE FROM Favourites WHERE FavouriteId = @FavouriteId AND SelfSurveyId = @SelfSurveyId";
            using (var cn = _c.CreateConnection())
            {
                int nbrOfRows = cn.Execute(sqlDeleteFavourite, favourite);
                return nbrOfRows == 1;
            }
        }
    }
}