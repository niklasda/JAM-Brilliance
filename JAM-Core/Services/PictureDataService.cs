using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Dapper;
using JAM.Core.Interfaces;
using JAM.Core.Models;
using JetBrains.Annotations;

namespace JAM.Core.Services
{
    [UsedImplicitly]
    public class PictureDataService : IPictureDataService
    {
        protected readonly IDatabaseConfigurationService Config;
        protected readonly IDataStorageConfigurationService StorageConfig;

        public PictureDataService(IDatabaseConfigurationService configurationService, IDataStorageConfigurationService storageConfigurationService)
        {
            Config = configurationService;
            StorageConfig = storageConfigurationService;
        }

        public Picture GetMainPictureFor(int surveyId)
        {
            const string sqlGetMainPictureFor = "SELECT PictureId, SurveyId, IsApproved, PictureGuid, ContentType FROM Pictures WHERE IsMain = 1 AND SurveyId = @SurveyId";

            using (var cn = Config.CreateConnection())
            {
                if (surveyId > 0)
                {
                    var picture = cn.Query<Picture>(sqlGetMainPictureFor, new { surveyId }).FirstOrDefault();
                    picture = LoadFromAzure(picture);
                    return picture;
                }
            }

            return null;
        }

        public Picture GetPicture(int surveyId, int pictureId)
        {
            const string sqlGetPicture = "SELECT PictureId, SurveyId, IsApproved, PictureGuid, ContentType FROM Pictures WHERE PictureId = @PictureId AND SurveyId = @SurveyId";

            using (var cn = Config.CreateConnection())
            {
                if (pictureId > 0)
                {
                    var picture = cn.Query<Picture>(sqlGetPicture, new { surveyId, pictureId }).FirstOrDefault();
                    picture = LoadFromAzure(picture);
                    return picture;
                }
            }

            return null;
        }

        public IEnumerable<Picture> GetPictures(int surveyId)
        {
            const string sqlGetPictures = "SELECT SurveyId, PictureId, IsMain, IsApproved, ContentType, UploadDate FROM Pictures WHERE SurveyId = @SurveyId";

            using (var cn = Config.CreateConnection())
            {
                var pictures = cn.Query<Picture>(sqlGetPictures, new { SurveyId = surveyId });
                return pictures;
            }
        }

        public int SaveNewPicture(Picture picture)
        {
            const string sqlGetPictureCount = "SELECT COUNT(*) FROM Pictures WHERE SurveyId = @SurveyId";
            const string sqlGetPictureGuid = "SELECT PictureGuid FROM Pictures WHERE SurveyId = @SurveyId AND PictureId = @PictureId";
            const string sqlInsertMainPicture = "INSERT INTO Pictures (SurveyId, IsMain, IsApproved, ContentType) VALUES (@SurveyId, 0, 0, @ContentType); SELECT CAST(SCOPE_IDENTITY() AS int)";

            using (var cn = Config.CreateConnection())
            {
                int pictureCount = cn.Query<int>(sqlGetPictureCount, picture).FirstOrDefault();
                if (pictureCount < 5)
                {
                    picture.PictureId = cn.Query<int>(sqlInsertMainPicture, picture).Single();
                    picture.PictureGuid = cn.Query<Guid>(sqlGetPictureGuid, picture).Single();
                    SaveToAzure(picture);
                    return picture.PictureId;
                }
                else
                {
                    return 0;
                }
            }
        }

        public void SetMainPicture(int surveyId, int pictureId)
        {
            const string sqlPictureExists = "SELECT PictureId FROM Pictures WHERE IsMain = 0 AND SurveyId = @surveyId AND PictureId = @PictureId";
            const string sqlUnsetMainPicture = "UPDATE Pictures SET IsMain = 0 WHERE IsMain = 1 AND SurveyId = @surveyId";
            const string sqlSetMainPicture = "UPDATE Pictures SET IsMain = 1 WHERE IsMain = 0 AND SurveyId = @surveyId AND PictureId = @PictureId";

            using (var cn = Config.CreateConnection())
            {
                int existingId = cn.Query<int>(sqlPictureExists, new { SurveyId = surveyId, PictureId = pictureId }).FirstOrDefault();
                if (existingId > 0)
                {
                    cn.Execute(sqlUnsetMainPicture, new { SurveyId = surveyId });
                    cn.Execute(sqlSetMainPicture, new { SurveyId = surveyId, PictureId = pictureId });
                }
            }
        }

        public void DeletePicture(int surveyId, int pictureId)
        {
            const string sqlPictureDelete = "DELETE FROM Pictures WHERE SurveyId = @surveyId AND PictureId = @PictureId";
            const string sqlGetPictureGuid = "SELECT PictureGuid FROM Pictures WHERE SurveyId = @SurveyId AND PictureId = @PictureId";

            using (var cn = Config.CreateConnection())
            {
                var pic = new Picture();
                pic.SurveyId = surveyId;
                pic.PictureId = pictureId;
                pic.PictureGuid = cn.Query<Guid>(sqlGetPictureGuid, pic).Single();

                cn.Execute(sqlPictureDelete, new { SurveyId = surveyId, PictureId = pictureId });
                DeleteFromAzure(pic);
            }
        }

        public int GetPictureId(int surveyId, int idx)
        {
            const string sqlGetOtherPicturesIdsFor = "SELECT PictureId FROM Pictures WHERE IsMain = 0 AND SurveyId = @SurveyId";
            const string sqlGetMainPicturesIdsFor = "SELECT PictureId FROM Pictures WHERE IsMain = 1 AND SurveyId = @SurveyId";

            using (var cn = Config.CreateConnection())
            {
                if (surveyId > 0 && idx > 0)
                {
                    var ids = cn.Query<int>(sqlGetOtherPicturesIdsFor, new { surveyId }).ToList();
                    if (ids.Count >= idx)
                    {
                        var pictureId = ids.Skip(idx - 1).First();
                        return pictureId;
                    }
                }
                else if (surveyId > 0 && idx == 0)
                {
                    var ids = cn.Query<int>(sqlGetMainPicturesIdsFor, new { surveyId }).ToList();
                    if (ids.Count == 1)
                    {
                        var pictureId = ids.First();
                        return pictureId;
                    }
                }
            }

            return 0;
        }

        public Picture LoadBytesIntoPicture(HttpPostedFileBase pictureFile)
        {
            if (pictureFile != null && pictureFile.ContentLength > 0)
            {
                // todo extract rules
                int length = pictureFile.ContentLength;
                string ftype = pictureFile.ContentType;
                if (length > 50 && length < 500000 && ftype != null
                    && (ftype.Equals("image/jpeg") || ftype.Equals("image/png")))
                {
                    var imageBytes = new byte[length];
                    pictureFile.InputStream.Read(imageBytes, 0, length);

                    var picture = new Picture();
                    picture.ContentType = ftype;
                    picture.ThePicture = imageBytes;

                    return picture;
                }
            }

            return null;
        }

        public int SaveMainPicture(Picture picture)
        {
            const string sqlMainPictureExists = "SELECT PictureId FROM Pictures WHERE IsMain = 1 AND SurveyId = @surveyId";
            const string sqlGetPictureGuid = "SELECT PictureGuid FROM Pictures WHERE SurveyId = @SurveyId AND PictureId = @PictureId";
            const string sqlInsertMainPicture = "INSERT INTO Pictures (SurveyId, IsMain, IsApproved, ContentType) VALUES (@SurveyId, 1, 0, @ContentType); SELECT CAST(SCOPE_IDENTITY() AS int)";

            using (var cn = Config.CreateConnection())
            {
                int mainPictureId = cn.Query<int>(sqlMainPictureExists, picture).FirstOrDefault();
                if (mainPictureId == 0)
                {
                    picture.PictureId = cn.Query<int>(sqlInsertMainPicture, picture).Single();
                    picture.PictureGuid = cn.Query<Guid>(sqlGetPictureGuid, picture).Single();
                    SaveToAzure(picture);
                    return picture.PictureId;
                }
                else
                {
                    picture.PictureId = SaveNewPicture(picture);
                    return picture.PictureId;
                }
            }
        }

        protected Picture LoadFromAzure(Picture picture)
        {
            if (picture != null)
            {
                return StorageConfig.DownloadPicture(picture);
            }

            return null;
        }

        private void SaveToAzure(Picture picture)
        {
            if (picture != null)
            {
                StorageConfig.UploadPicture(picture);
            }
        }

        private void DeleteFromAzure(Picture picture)
        {
            if (picture != null)
            {
                StorageConfig.DeletePicture(picture);
            }
        }
    }
}