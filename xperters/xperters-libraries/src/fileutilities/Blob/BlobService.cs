using System;
using System.IO;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Logging;
using xperters.configurations;
using xperters.constants;
using xperters.domain;
using xperters.enums;
using xperters.fileutilities.Interfaces;

namespace xperters.fileutilities.Blob
{
    public class BlobService : IBlobService
    {
        private readonly string _cloudStorageBaseUrl;
        private readonly CloudBlobClient _blobClient;
        private AppConfig _config;
        private readonly ILogger _logger;

        public BlobService(AppConfig appConfig, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger<BlobService>();
            _config = appConfig;
            _logger = loggerFactory.CreateLogger<BlobService>();
            try
            {
                CloudStorageAccount storageAccount;

                if (string.IsNullOrEmpty(_config.Storage.ConnectionString) ||
                    string.IsNullOrEmpty(_config.Storage.BaseUrl))
                {
                    storageAccount = CloudStorageAccount.Parse(AppSettings.StorageAccountDeveloper);
                    _blobClient = storageAccount.CreateCloudBlobClient();
                    logger.LogWarning("Using development storage account as config for storage account is empty");
                    return;
                }

                var storageConnectionString = _config.Storage.ConnectionString.Trim();
                _cloudStorageBaseUrl = _config.Storage.BaseUrl.Trim();

                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
                _blobClient = storageAccount.CreateCloudBlobClient();
                logger.LogDebug("Storage account has been setup");
            }
            catch (Exception ex)
            {
                // log the exception to aid traceability
                logger.LogCritical(ex.Message);

                if (appConfig == null)
                {
                    logger.LogCritical($"Storage account connection string: is blank");
                }
                else
                {
                    logger.LogCritical($"Storage account connection string length: {_config.Storage.ConnectionString.Length}");
                    logger.LogCritical($"Storage account base url length: {_config.Storage.BaseUrl.Length}");
                }

                throw;
            }
        }

        private string FileUrl(string container, string localPath)
        {
            return $"/{container}/{localPath}";
        }

        public BlobFileResult File(Enums.FileFor eFileFor, string fileName)
        {

            var blobContainer = GetContainer(eFileFor);
            var blockBlob = blobContainer.GetBlockBlobReference(fileName);
            blockBlob.FetchAttributesAsync();

            var memStream = new MemoryStream();
            blockBlob.DownloadToStreamAsync(memStream);

            return new BlobFileResult
            {
                ContentType = blockBlob.Properties.ContentType,
                FileName = fileName,
                ContentLength = blockBlob.Properties.Length,
                File = memStream
            };

        }



        private CloudBlobContainer GetContainer(string containerName)
        {
            // Retrieve a reference to a container.    
            var blobContainer = _blobClient.GetContainerReference(containerName);
            blobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container });
            blobContainer.CreateIfNotExistsAsync();
            return blobContainer;
        }

        public string GetContainerName(Enums.FileFor eFileFor)
        {
            string containerName = string.Empty;
            switch (eFileFor)
            {
                case Enums.FileFor.Users:
                    containerName = _config.Storage.UserContainer;
                    break;
                case Enums.FileFor.FreelancerFiles:
                    containerName = _config.Storage.ContractContainer;
                    break;
                case Enums.FileFor.JobAttachments:
                    containerName = _config.Storage.JobContainer;
                    break;
            }

            return containerName;

        }
        private CloudBlobContainer GetContainer(Enums.FileFor eFileFor)
        {
            string containerName = GetContainerName(eFileFor);

            return GetContainer(containerName);
        }

        public string AddToBlobForJobDto(Enums.FileFor eUploadFileFor, JobAttachmentDto attachment)
        {
            var blobContainer = GetContainer(eUploadFileFor);
            AddToBlobStorage(blobContainer, attachment.LocalPath, attachment.FileData);
            return FileUrl(blobContainer.Name, attachment.LocalPath);
        }
        public string AddToBlobForJobBidDto(Enums.FileFor eUploadFileFor, JobBidAttachmentDto attachment)
        {
            var blobContainer = GetContainer(eUploadFileFor);
            AddToBlobStorage(blobContainer, attachment.LocalPath, attachment.FileData);
            return FileUrl(blobContainer.Name, attachment.LocalPath);
        }
        public string AddToBlobForMilestoneDto(Enums.FileFor eUploadFileFor, MilestoneAttachmentDto attachment)
        {
            var blobContainer = GetContainer(eUploadFileFor);
            AddToBlobStorage(blobContainer, attachment.LocalPath, attachment.FileData);
            return FileUrl(blobContainer.Name, attachment.LocalPath);
        }

        

        private void AddToBlobStorage(CloudBlobContainer blobContainer, string blobName, byte[] fileStream)
        {
            var blockBlob = blobContainer.GetBlockBlobReference(blobName);
            blockBlob.UploadFromByteArrayAsync(fileStream, 0, fileStream.Length);
        }

        public byte[] GetBytesFromBlobStorage(Enums.FileFor eFileFor, string fullPath)
        {
            _logger.LogDebug($"GetBytesFromBlobStorage {fullPath}");
            var byteArray = (dynamic)null;
            try
            {
                string Path =_blobClient.BaseUri + fullPath;
                if (Path.Contains("//jobattachments")) {
                  Path= Path.Replace("//jobattachments", "/jobattachments");
                }
                var blob = new CloudBlockBlob(new Uri(Path));
                _logger.LogDebug($"Get Blob");
                
                blob.FetchAttributes();
                byteArray = new byte[blob.Properties.Length];
                blob.DownloadToByteArray(byteArray, 0);
                return byteArray;
            } catch(StorageException se)
            {
                _logger.LogDebug($"Exception during file download is {se.Message}");
                if (se.Message.Contains("404") || se.Message.Contains("Not Found"))
                {
                    return byteArray;
                }
            }
            return byteArray;
        }

    

    }
}
