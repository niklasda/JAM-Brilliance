using System.Collections.Generic;
using System.Linq;

using Dapper;

using JAM.Core.Interfaces;
using JAM.Core.Interfaces.Admin;
using JAM.Core.Models;
using JetBrains.Annotations;

namespace JAM.Core.Services.Admin
{
    [UsedImplicitly]
    public class PictureAdminDataService : PictureDataService, IPictureAdminDataService
    {
        public PictureAdminDataService(IDatabaseConfigurationService configurationService, IDataStorageConfigurationService storageConfigurationService)
            : base(configurationService, storageConfigurationService)
        {
        }

        public Picture GetPicture(int pictureId)
        {
            const string sqlGetPicture = "SELECT PictureId, SurveyId, PictureGuid, ContentType FROM Pictures WHERE PictureId = @PictureId";

            using (var cn = Config.CreateConnection())
            {
                if (pictureId > 0)
                {
                    var picture = cn.Query<Picture>(sqlGetPicture, new { pictureId }).FirstOrDefault();
                    picture = LoadFromAzure(picture);
                    return picture;
                }
            }

            return null;
        }

        public IEnumerable<Picture> GetUnapprovedPictures()
        {
            const string sqlGetNonApprovedPictures = "SELECT Surveys.Name, Pictures.SurveyId, PictureId, IsMain, IsApproved, ContentType, UploadDate FROM Pictures "
                                                     + "INNER JOIN Surveys ON Pictures.SurveyId = Surveys.SurveyId WHERE IsApproved = 0";

            using (var cn = Config.CreateConnection())
            {
                var pictures = cn.Query<Picture>(sqlGetNonApprovedPictures);
                return pictures;
            }
        }

        public bool ApprovePicture(int pictureId)
        {
            const string sqlUpdateApprovePicture = "UPDATE Pictures SET IsApproved = 1 WHERE IsApproved = 0 AND PictureId = @PictureId";
            using (var cn = Config.CreateConnection())
            {
                int nbrOfRows = cn.Execute(sqlUpdateApprovePicture, new { PictureId = pictureId });
                return nbrOfRows == 1;
            }
        }

        public int GetUnapprovedPictureCount()
        {
            const string sqlUnapprovedPictureCount = "SELECT COUNT(*) FROM Pictures WHERE IsApproved = 0";
            using (var cn = Config.CreateConnection())
            {
                int unapprovedPictureCount = cn.Query<int>(sqlUnapprovedPictureCount).SingleOrDefault();
                return unapprovedPictureCount;
            }
        }

        public bool RemoveUnapprovedPicture(int pictureId)
        {
            const string sqlDeletePicture = "DELETE FROM Pictures WHERE PictureId = @PictureId AND IsApproved = 0";
            using (var cn = Config.CreateConnection())
            {
                int nbrOfRows = cn.Execute(sqlDeletePicture, new { PictureId = pictureId });
                return nbrOfRows == 1;
            }
        }
    }
}