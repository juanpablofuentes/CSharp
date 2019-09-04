using System;
using System.Collections.Generic;
using System.IO;
using Group.Salto.Common;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.AzureBlobStorage;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.File;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Group.Salto.ServiceLibrary.Implementations.AzureBlobStorage
{
    public class AzureBlobStorageService : BaseService, IAzureBlobStorageService
    {
        private readonly IConfiguration _configuration;
        private readonly CloudStorageAccount cloudStorageAccount;
        private readonly CloudBlobClient blobClient;
        private readonly string blobConn;

        public AzureBlobStorageService(ILoggingService logginingService,
                                        IConfiguration configuration) : base(logginingService)
        {
            _configuration = configuration;
            blobConn = _configuration.GetSection(AppsettingsKeys.AzureBlobStorage).GetValue<string>(AppsettingsKeys.BlobStorageConnectionString);
            cloudStorageAccount = CloudStorageAccount.Parse(blobConn);
            blobClient = cloudStorageAccount.CreateCloudBlobClient();
        }

        public FileContentDto GetFileFromSomFile(SomFileDto somDto)
        {
            var fileContentDto = new FileContentDto
            {
                FileName = somDto.Name
            };
            
            try
            {
                LogginingService.LogInfo($"Get file from Blob Storage: {somDto.Name}");
                var containerName = somDto.Container;
                CloudBlobContainer cloudBlobContainer = blobClient.GetContainerReference(containerName);

                var blobFileName = somDto.Name;
                if (!string.IsNullOrEmpty(somDto.Directory))
                {
                    blobFileName = $"{somDto.Directory}/{somDto.Name}";
                }

                CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(blobFileName);
                MemoryStream memStream = new MemoryStream();
                blockBlob.DownloadToStream(memStream);
                fileContentDto.FileBytes = memStream.ToArray();
            }
            catch (Exception e)
            {
                LogginingService.LogException(e);
            }

            return fileContentDto;
        }

        public bool SaveFileToBlobStorage(SaveFileBlobDto fileBlobDto)
        {
            var result = false;
            try
            {
                LogginingService.LogInfo($"Upload file to Blob Storage: {fileBlobDto.Name}");
                var containerName = fileBlobDto.Container;
                CloudBlobContainer cloudBlobContainer = blobClient.GetContainerReference(containerName);
                var fileNameBlob = $"{fileBlobDto.Directory}/{fileBlobDto.Name}";

                CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(fileNameBlob);
                blockBlob.UploadFromByteArray(fileBlobDto.FileBytes, 0, fileBlobDto.FileBytes.Length);

                result = true;
            }
            catch (Exception e)
            {
                LogginingService.LogException(e);
            }

            return result;
        }

        public bool DeleteFileToBlobStorage(SomFileDto fileBlobDto)
        {
            var result = false;
            try
            {
                LogginingService.LogInfo($"Delete file to Blob Storage: {fileBlobDto.Name}");
                var containerName = fileBlobDto.Container;
                CloudBlobContainer cloudBlobContainer = blobClient.GetContainerReference(containerName);
                var fileNameBlob = $"{fileBlobDto.Directory}/{fileBlobDto.Name}";
                CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(fileNameBlob);
                blockBlob.Delete();

                result = true;
            }
            catch (Exception e)
            {
                LogginingService.LogException(e);
            }

            return result;
        }

        public List<RequestFileDto> GetFilesFromBlobFolder(string container, string filesBlobFolder)
        {
            var result = new List<RequestFileDto>();
            try
            {
                LogginingService.LogInfo($"Get files from folder: {filesBlobFolder}");
                var blobConn = _configuration.GetSection(AppsettingsKeys.AzureBlobStorage).GetValue<string>(AppsettingsKeys.BlobStorageConnectionString);
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobConn);
                CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
                CloudBlobContainer cloudBlobContainer = blobClient.GetContainerReference(container);

                var itemsInFolder = cloudBlobContainer.ListBlobs(filesBlobFolder);

                foreach (var folderItem in itemsInFolder)
                {
                    if (folderItem is CloudBlobDirectory blobDir)
                    {
                        var filesBlobDir = blobDir.ListBlobs();
                        foreach (var fileBlob in filesBlobDir)
                        {
                            if (fileBlob is CloudBlockBlob blobBlock)
                            {
                                CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(blobBlock.Name);
                                MemoryStream memStream = new MemoryStream();
                                blockBlob.DownloadToStream(memStream);
                                var fileName = Path.GetFileName(blobBlock.Name);
                                result.Add(new RequestFileDto
                                {
                                    FileBytes = memStream.ToArray(),
                                    FileName = fileName
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogginingService.LogException(e);
            }

            return result;
        }
    }
}