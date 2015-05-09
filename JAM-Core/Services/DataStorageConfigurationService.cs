using System.Threading.Tasks;
using JAM.Core.Interfaces;
using JAM.Core.Models;
using JetBrains.Annotations;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace JAM.Core.Services
{
    [UsedImplicitly]
    public class DataStorageConfigurationService : IDataStorageConfigurationService
    {
        private const string ContainerName = "profileimages";

        private readonly string _storageConnString;

        public DataStorageConfigurationService(string storageConnString)
        {
            _storageConnString = storageConnString;
        }

        public void UploadPicture(Picture picture)
        {
            CloudBlobContainer container = CreateStorageContainer();

            string blobName = GetBlobName(picture);
            ICloudBlob blob = container.GetBlockBlobReference(blobName);
            blob.Properties.ContentType = picture.ContentType;
            blob.UploadFromByteArray(picture.ThePicture, 0, picture.ThePicture.Length);
        }

        public Picture DownloadPicture(Picture picture)
        {
            CloudBlobContainer container = CreateStorageContainer();

            string blobName = GetBlobName(picture);
            ICloudBlob blob = container.GetBlockBlobReference(blobName);

            if (blob.Exists())
            {
                blob.FetchAttributes(); // crashes if not exists
                long fileByteLength = blob.Properties.Length;

                picture.ThePicture = new byte[fileByteLength];
                picture.ContentType = blob.Properties.ContentType;
                blob.DownloadToByteArray(picture.ThePicture, 0);
            }

            return picture;
        }

        public void DeletePicture(Picture picture)
        {
            CloudBlobContainer container = CreateStorageContainer();

            string blobName = GetBlobName(picture);
            ICloudBlob blob = container.GetBlockBlobReference(blobName);

            Task.Run(() => blob.DeleteIfExistsAsync());
        }

        private string GetBlobName(Picture picture)
        {
            string blobName = string.Format("{0}/image_{1}_{2}", ContainerName, picture.SurveyId, picture.PictureGuid);
            return blobName;
        }

        private CloudBlobContainer CreateStorageContainer()
        {
            var storageAccount = CloudStorageAccount.Parse(_storageConnString);

            CloudBlobClient blobStorage = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobStorage.GetContainerReference(ContainerName);
            if (container.CreateIfNotExists())
            {
                // configure container for public access
                var permissions = container.GetPermissions();
                permissions.PublicAccess = BlobContainerPublicAccessType.Container;
                container.SetPermissions(permissions);
            }

            return container;
        }
    }
}