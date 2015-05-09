using System.Collections.Generic;

using JAM.Core.Models;

namespace JAM.Core.Interfaces.Admin
{
    public interface IPictureAdminDataService : IPictureDataService
    {
        IEnumerable<Picture> GetUnapprovedPictures();

        bool ApprovePicture(int pictureId);

        int GetUnapprovedPictureCount();

        bool RemoveUnapprovedPicture(int pictureId);

        Picture GetPicture(int pictureId);
    }
}