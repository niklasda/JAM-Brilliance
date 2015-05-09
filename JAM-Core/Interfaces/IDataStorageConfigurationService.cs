using JAM.Core.Models;

namespace JAM.Core.Interfaces
{
    public interface IDataStorageConfigurationService
    {
        void UploadPicture(Picture pic);

        Picture DownloadPicture(Picture pic);

        void DeletePicture(Picture pic);
    }
}