using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.File;

namespace Group.Salto.ServiceLibrary.Common.Contracts.AzureBlobStorage
{
    public interface IAzureBlobStorageService
    {
        FileContentDto GetFileFromSomFile(SomFileDto somDto);
        bool SaveFileToBlobStorage(SaveFileBlobDto fileBlobDto);
        bool DeleteFileToBlobStorage(SomFileDto fileBlobDto);
        List<RequestFileDto> GetFilesFromBlobFolder(string container, string filesBlobFolder);
    }
}
