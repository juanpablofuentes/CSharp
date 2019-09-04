using Group.Salto.Common;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.AzureBlobStorage;
using Group.Salto.ServiceLibrary.Common.Contracts.Service;
using Group.Salto.ServiceLibrary.Common.Contracts.Tasks;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Service;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.File;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;
using Group.Salto.ServiceLibrary.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Service
{
    public class FormService : BaseService, IFormService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IConfiguration _configuration;
        private readonly IAzureBlobStorageService _azureBlobStorageService;
        private readonly IExtraFieldsValuesRepository _extraFieldsValuesRepository;
        private readonly ITasksService _tasksService;

        public FormService(ILoggingService logginingService,
                            IServiceRepository serviceRepository,
                            IConfiguration configuration,
                            IAzureBlobStorageService azureBlobStorageService,
                            IExtraFieldsValuesRepository extraFieldsValuesRepository,
                            ITasksService tasksService) : base(logginingService)
        {
            _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(IServiceRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(IConfiguration));
            _azureBlobStorageService = azureBlobStorageService ?? throw new ArgumentNullException(nameof(IAzureBlobStorageService));
            _extraFieldsValuesRepository = extraFieldsValuesRepository ?? throw new ArgumentNullException(nameof(IExtraFieldsValuesRepository));
            _tasksService = tasksService ?? throw new ArgumentNullException(nameof(ITasksService));
        }

        public IEnumerable<ServiceFilesDto> GetFilesFromService(int serviceId)
        {
            var service = _serviceRepository.GetServiceByIdIncludeExtraFields(serviceId);
            var fileFields = service.ExtraFieldsValues.Where(efv => efv.ExtraField.Type == (int)ExtraFieldValueTypeEnum.Fitxer && !string.IsNullOrWhiteSpace(efv.StringValue)).ToDto();

            var serviceFolder = _configuration.GetSection(AppsettingsKeys.BlobStorageFolders).GetValue<string>(AppsettingsKeys.BlobStorageFolderServices);
            var container = _configuration.GetSection(AppsettingsKeys.AzureBlobStorage).GetValue<string>(AppsettingsKeys.StorageName);

            var filesDto = new List<ServiceFilesDto>();
            foreach (var extraField in fileFields)
            {
                var filesBlobFolder = $"{serviceFolder}/{extraField.StringValue}";
                var files = _azureBlobStorageService.GetFilesFromBlobFolder(container, filesBlobFolder);
                if (files.Any())
                {
                    filesDto.Add(new ServiceFilesDto
                    {
                        ExtraFieldId = extraField.Id,
                        Files = files
                    });
                }
            }

            return filesDto;
        }

        public WorkOrderServiceDto GetFormsWO(int workOrderId, int FatherId, int GeneratedServiceId)
        {
            IList<WorkOrderFormsDto> services = _serviceRepository.GetServiceWOForms(workOrderId).ToList().ToListDto(false);
            IList<WorkOrderFormsDto> servicesFather = _serviceRepository.GetServiceWOForms(FatherId).ToList().ToListDto(true);
            IList<WorkOrderFormsDto> servicesGenerated = _serviceRepository.GetGeneratedServiceWOForms(GeneratedServiceId).ToList().ToListDto(false);
            WorkOrderServiceDto allWOService = new WorkOrderServiceDto
            {
                WOService = services.ToList(),
            };
            allWOService.WOService.AddRange(servicesFather);
            allWOService.WOService.AddRange(servicesGenerated);
            allWOService.WOService = allWOService.WOService.OrderBy(x => x.DateForOrder).ToList();

            //TODO:Extraer esto a un metodo
            string serviceFolder = _configuration.GetSection(AppsettingsKeys.BlobStorageFolders).GetValue<string>(AppsettingsKeys.BlobStorageFolderServices);
            string container = _configuration.GetSection(AppsettingsKeys.AzureBlobStorage).GetValue<string>(AppsettingsKeys.StorageName);
            foreach (WorkOrderFormsDto service in allWOService.WOService)
            {
                foreach (WorkOrderExtraFieldsValuesDto extraField in service.ExtraFieldsValues)
                {
                    if (extraField.IsFile && !string.IsNullOrEmpty(extraField.ExtraFieldValue))
                    {
                        var filesDto = new List<ServiceFilesDto>();
                        var files = GetFilesFromBlobFolder(serviceFolder, container, extraField.ExtraFieldValue);
                        if (files.Any())
                        {
                            extraField.Files = files;
                        }
                    }
                }
            }

            return allWOService;
        }

        public List<RequestFileDto> GetFilesFromExtraFieldsValues(int Id)
        {
            var file = _extraFieldsValuesRepository.GetById(Id);
            var serviceFolder = _configuration.GetSection(AppsettingsKeys.BlobStorageFolders).GetValue<string>(AppsettingsKeys.BlobStorageFolderServices);
            var container = _configuration.GetSection(AppsettingsKeys.AzureBlobStorage).GetValue<string>(AppsettingsKeys.StorageName);
            var files = GetFilesFromBlobFolder(serviceFolder, container, file.StringValue);
            return files;
        }

        public List<WorkOrderExtraFieldsValuesDto> GetExtraFieldsValues(int Id)
        {
            var serviceFolder = _configuration.GetSection(AppsettingsKeys.BlobStorageFolders).GetValue<string>(AppsettingsKeys.BlobStorageFolderServices);
            var container = _configuration.GetSection(AppsettingsKeys.AzureBlobStorage).GetValue<string>(AppsettingsKeys.StorageName);
            var files = _extraFieldsValuesRepository.GetAllExtrafields(Id).ToList().ToExtraFieldListDto();
            foreach(var file in files)
            {
                if(file.IsFile && !string.IsNullOrEmpty(file.ExtraFieldValue))
                {
                    var filesDto = new List<ServiceFilesDto>();
                    var extraFieldFile = GetFilesFromBlobFolder(serviceFolder, container, file.ExtraFieldValue);
                    if (files.Any())
                    {
                        file.Files = extraFieldFile;
                    }
                }
            }
            return files.ToList();
        }

        public ResultDto<IList<WorkOrderExtraFieldsValuesDto>> UpdateExtraFieldsValues(IList<WorkOrderExtraFieldsValuesDto> model, FormServiceDto formServiceDto)
        {
            ResultDto <IList<WorkOrderExtraFieldsValuesDto>> result = null;
            var entity = _extraFieldsValuesRepository.GetAllExtrafields(formServiceDto.ServiceId);
            if (entity != null)
            {
                foreach (var extraFieldValue in entity)
                {
                    var modelDto = model.Where(x => x.Id == extraFieldValue.Id).FirstOrDefault();
                    extraFieldValue.UpdateExtraFieldValue(modelDto);
                }
                var resultRepository = _extraFieldsValuesRepository.UpdateExtraFieldValues(entity.ToList());
                result = ProcessResult(entity.ToList().ToExtraFieldListDto(), resultRepository);
                UpdateFilesFromExtraFieldValues(model);
                DeleteFilesFromExtraFieldValues(model);
            }

            WorkOrderFormsDto service = _serviceRepository.GetServiceWOForms(formServiceDto.WorkOrderId).Where(x => x.Id == formServiceDto.ServiceId).FirstOrDefault().ToDto(false);

            TaskExecuteFormDto taskExecuteParametersDto = new TaskExecuteFormDto()
            {
                WorkOrderId = formServiceDto.WorkOrderId,
                Service = service,
                ServiceId = formServiceDto.ServiceId,
                Type = TaskTypeEnum.IdServeiPredefinit,
                CustomerId = formServiceDto.CustomerId,
                UserId = formServiceDto.UserId
            };

            TaskExecutionFormValues taskExecutionFormValues = CanExecuteFormTask(service);

            var test = _tasksService.TaskExecuteForm(taskExecuteParametersDto, taskExecutionFormValues);

            return result;
        }

        private TaskExecutionFormValues CanExecuteFormTask(WorkOrderFormsDto service)
        {
            TaskExecutionFormValues result = new TaskExecutionFormValues();

            if (service.IsWorkOrderOpen() || (service.IsClosedOrder()))
            {
                if (!service.HasBill && service.IsSystemForm)
                {
                    result.ExecuteAnalysis = true;
                }
                else if (service.BillState == Common.Mobility.Dto.Enums.BillStatus.Pending)
                {
                    result.ExecuteAnalysis = true;
                    result.ExecuteBillRules = true;
                }
            }

            return result;
        }

        private bool CanExecuteBillRules(WorkOrderFormsDto service)
        {
            bool result = false;

            return result;
        }

        private void UpdateFilesFromExtraFieldValues(IList<WorkOrderExtraFieldsValuesDto> model)
        {
            for (int i = 0; i < model.Count(); i++)
            {
                if (model[i].UploadFiles != null)
                {
                    foreach (var file in model[i].UploadFiles)
                    {
                        var fileData = new RequestFileDto();
                        fileData.FileName = file.FileName;
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            fileData.FileBytes = fileBytes;
                        }
                        var resultFile = UpdateFilesBlobStorage(fileData, model[i].ExtraFieldValue);
                    }
                }
            }
        }

        private void DeleteFilesFromExtraFieldValues(IList<WorkOrderExtraFieldsValuesDto> model)
        {
            for (int i = 0; i < model.Count(); i++)
            {
                if (model[i].Files != null)
                {
                    foreach (var file in model[i].Files)
                    {
                        if(file.CheckedForDelete != false)
                        {
                            var resultFile = DeleteFilesFromBlobStorage(file, model[i].ExtraFieldValue);
                        }
                    }
                }
            }
        }

        private bool UpdateFilesBlobStorage(RequestFileDto file, string folder)
        {
            var result = false;
            var extraFieldFolder = _configuration.GetSection(AppsettingsKeys.BlobStorageFolders).GetValue<string>(AppsettingsKeys.BlobStorageFolderServices);
            var container = _configuration.GetSection(AppsettingsKeys.AzureBlobStorage).GetValue<string>(AppsettingsKeys.StorageName);
            var fileBlobDto = new SaveFileBlobDto
            {
                Name = file.FileName,
                Container = container,
                Directory = $"{extraFieldFolder}/{folder}",
                FileBytes = file.FileBytes,
            };
            result = _azureBlobStorageService.SaveFileToBlobStorage(fileBlobDto);
            return result;
        }

        private bool DeleteFilesFromBlobStorage(RequestFileDto file, string folder)
        {
            var result = false;
            var extraFieldFolder = _configuration.GetSection(AppsettingsKeys.BlobStorageFolders).GetValue<string>(AppsettingsKeys.BlobStorageFolderServices);
            var container = _configuration.GetSection(AppsettingsKeys.AzureBlobStorage).GetValue<string>(AppsettingsKeys.StorageName);
            var fileBlobDto = new SomFileDto
            {
                Name = file.FileName,
                Container = container,
                Directory = $"{extraFieldFolder}/{folder}",
            };
            result = _azureBlobStorageService.DeleteFileToBlobStorage(fileBlobDto);
            return result;
        }

        private List<RequestFileDto> GetFilesFromBlobFolder(string folder, string container, string extraFieldValue)
        {
            var filesBlobFolder = $"{folder}/{extraFieldValue}";
            var files = _azureBlobStorageService.GetFilesFromBlobFolder(container, filesBlobFolder);
            return files;
        }
    }
}