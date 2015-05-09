using System.Collections.Generic;
using System.Web;

using JAM.Core.Models;

namespace JAM.Core.Interfaces
{
    public interface IPictureDataService
    {
        Picture LoadBytesIntoPicture(HttpPostedFileBase pictureFile);

        int SaveMainPicture(Picture pic);

        Picture GetMainPictureFor(int surveyId);

        IEnumerable<Picture> GetPictures(int surveyId);

        Picture GetPicture(int surveyId, int pictureId);

        int SaveNewPicture(Picture pic);

        void SetMainPicture(int surveyId, int pictureId);

        void DeletePicture(int surveyId, int pictureId);
        
        int GetPictureId(int surveyId, int idx);
    }
}