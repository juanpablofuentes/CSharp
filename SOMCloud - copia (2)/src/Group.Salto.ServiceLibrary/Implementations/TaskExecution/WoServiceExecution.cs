using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.Analysis;
using Group.Salto.ServiceLibrary.Common.Contracts.AzureBlobStorage;
using Group.Salto.ServiceLibrary.Common.Contracts.TaskExecution;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.File;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;
using Microsoft.Extensions.Configuration;

namespace Group.Salto.ServiceLibrary.Implementations.TaskExecution
{
    public class WoServiceExecution : IWoServiceExecution
    {
        private readonly IConfiguration _configuration;
        private readonly IAzureBlobStorageService _azureBlobStorageService;
        private readonly IAnalysisService _analysisService;
        private readonly IExtraFieldsRepository _extraFieldsRepository;
        private readonly IPredefinedServiceRepository _predefinedServiceRepository;
        private readonly IPeopleRepository _peopleRepository;

        public WoServiceExecution(IConfiguration configuration,
                                IAzureBlobStorageService azureBlobStorageService,
                                IAnalysisService analysisService,
                                IExtraFieldsRepository extraFieldsRepository,
                                IPredefinedServiceRepository predefinedServiceRepository,
                                IPeopleRepository peopleRepository)
        {
            _configuration = configuration;
            _azureBlobStorageService = azureBlobStorageService;
            _analysisService = analysisService;
            _extraFieldsRepository = extraFieldsRepository;
            _predefinedServiceRepository = predefinedServiceRepository;
            _peopleRepository = peopleRepository;
        }

        public ResultDto<bool> PerformTask(TaskExecutionValues taskExecutionValues)
        {
            if (taskExecutionValues.TaskParameters.Service != null)
            {
                var currentService = GetServiceInit(taskExecutionValues);
                
                if (taskExecutionValues.TaskParameters.Service.ClosingCodeId != 0)
                {
                    currentService.ClosingCodeId = taskExecutionValues.TaskParameters.Service.ClosingCodeId;
                }

                var result = AddExtraFieldsToService(taskExecutionValues.CurrentWorkOrder, currentService, taskExecutionValues.TaskParameters.Service.ExtraFieldsValues);

                if (result)
                {
                    var errors = _analysisService.ValidateServiceFields(taskExecutionValues.CurrentWorkOrder, currentService);
                    if (errors.Any())
                    {
                        taskExecutionValues.Result = FillFormErrors(errors);
                    }
                    else
                    {
                        taskExecutionValues.CurrentWorkOrder.Services.Add(currentService);
                        taskExecutionValues.CreatedService = currentService;
                    }
                }
            }
            else
            {
                taskExecutionValues.Result.Data = false;
                taskExecutionValues.Result.Errors = new ErrorsDto
                {
                    Errors = new List<ErrorDto> { new ErrorDto {ErrorType = ErrorType.ValidationError, ErrorMessageKey = " Task - Service not found"} }
                };
            }

            return taskExecutionValues.Result;
        }

        private ResultDto<bool> FillFormErrors(IEnumerable<ServiceFieldErrorEnum> errors)
        {
            var resultDto = new ResultDto<bool>
            {
                Data = false,
                Errors = new ErrorsDto()
            };
            foreach (var errorItem in errors)
            {
                resultDto.Errors.AddError(new ErrorDto
                {
                    ErrorType = ErrorType.FormFieldsValidationError,
                    ErrorMessageKey = errorItem.ToString()
                });
            }

            return resultDto;
        }

        private Services GetServiceInit(TaskExecutionValues taskExecutionValues)
        {
            var service = new Services
            {
                Description = taskExecutionValues.TaskParameters.Observations,
                Observations = taskExecutionValues.TaskParameters.Service.Observations,
                UpdateDate = DateTime.UtcNow,
                CreationDate = DateTime.UtcNow,
                Latitude = taskExecutionValues.TaskParameters.Latitude,
                Longitude = taskExecutionValues.TaskParameters.Longitude,
                PredefinedServiceId = taskExecutionValues.TaskParameters.Service.PredefinedServiceId,
                WorkOrderId = taskExecutionValues.TaskParameters.WorkOrderId,
                PeopleResponsibleId = taskExecutionValues.CurrentPeople.Id,
                PeopleResponsible = taskExecutionValues.CurrentPeople,
                SubcontractResponsibleId = taskExecutionValues.CurrentPeople.SubcontractId,
                IdentifyInternal = taskExecutionValues.CurrentWorkOrder.InternalIdentifier,
                IdentifyExternal = taskExecutionValues.CurrentWorkOrder.ExternalIdentifier,
                ExtraFieldsValues = new List<ExtraFieldsValues>(),
            };
            service.PredefinedService = _predefinedServiceRepository.GetByIdIncludeExtraFields(service.PredefinedServiceId);

            if (service.PredefinedService?.Billable != null && service.PredefinedService.Billable.Value)
            {
                service.FormState = service.PredefinedService.MustValidate == true ? FormState.ValidationPending.ToString() : FormState.DeliveringPending.ToString();
            }
            else
            {
                service.FormState = FormState.NoBillable.ToString();
            }

            if (taskExecutionValues.TaskParameters.ResponsibleId > 0)
            {
                service.PeopleResponsibleId = taskExecutionValues.TaskParameters.ResponsibleId;
                service.PeopleResponsible = _peopleRepository.GetByIdIncludeIncludeContractInfo(taskExecutionValues.TaskParameters.ResponsibleId);
            }

            return service;
        }

        private bool AddExtraFieldsToService(WorkOrders wo, Services service, IEnumerable<ExtraFieldValueAddDto> serviceExtraFieldsValues)
        {
            var result = true;
            foreach (var extraFieldValueAddDto in serviceExtraFieldsValues)
            {
                var modelEfv = GetExtraFieldsValuesEntity(wo, extraFieldValueAddDto);

                result = extraFieldValueAddDto.ExtraField.Type != ExtraFieldValueTypeEnum.Fitxer || SaveFilesInBlobStorage(extraFieldValueAddDto, modelEfv);

                if (result)
                {
                    service.ExtraFieldsValues.Add(modelEfv);
                }
                else
                {
                    break;
                }
            }

            return result;
        }

        private ExtraFieldsValues GetExtraFieldsValuesEntity(WorkOrders wo, ExtraFieldValueAddDto extraFieldValueAddDto)
        {
            var modelEfv = new ExtraFieldsValues
            {
                BooleaValue = extraFieldValueAddDto.BooleaValue,
                DataValue = extraFieldValueAddDto.DateValue,
                DecimalValue = extraFieldValueAddDto.DecimalValue,
                EnterValue = extraFieldValueAddDto.EnterValue,
                Signature = extraFieldValueAddDto.Signature,
                WorkOrderId = wo.Id,
                StringValue = extraFieldValueAddDto.StringValue,
                UpdateDate = DateTime.UtcNow,
                ExtraFieldId = extraFieldValueAddDto.ExtraField.Id,
            };
            modelEfv.ExtraField = _extraFieldsRepository.GetByIdWithIncludeTranslations(modelEfv.ExtraFieldId);
            if (extraFieldValueAddDto.ExtraField.Type == ExtraFieldValueTypeEnum.Instalation)
            {
                modelEfv.MaterialForm = new List<MaterialForm>();
                foreach (var matForm in extraFieldValueAddDto.MaterialForms)
                {
                    modelEfv.MaterialForm.Add(new MaterialForm
                    {
                        Description = matForm.Description,
                        UpdateDate = DateTime.UtcNow,
                        Units = matForm.Units,
                        Reference = matForm.Reference,
                        SerialNumber = matForm.SerialNumber,
                        AssetId = wo.AssetId
                    });
                }
            }

            return modelEfv;
        }

        private bool SaveFilesInBlobStorage(ExtraFieldValueAddDto extraFieldValueAddDto, ExtraFieldsValues modelEfv)
        {
            bool result = false;
            if (extraFieldValueAddDto.Files != null && extraFieldValueAddDto.Files.Any())
            {
                var folderDirectory = Guid.NewGuid().ToString();
                modelEfv.StringValue = folderDirectory;
                var serviceFolder = _configuration.GetSection(AppsettingsKeys.BlobStorageFolders).GetValue<string>(AppsettingsKeys.BlobStorageFolderServices);
                var container = _configuration.GetSection(AppsettingsKeys.AzureBlobStorage).GetValue<string>(AppsettingsKeys.StorageName);

                foreach (var fileToSave in extraFieldValueAddDto.Files)
                {
                    var fileBlobDto = new SaveFileBlobDto
                    {
                        Name = fileToSave.FileName,
                        Container = container,
                        Directory = $"{serviceFolder}/{folderDirectory}",
                        FileBytes = fileToSave.FileBytes
                    };
                    result = _azureBlobStorageService.SaveFileToBlobStorage(fileBlobDto);
                    if (!result)
                    {
                        break;
                    }
                }
            }
            else
            {
                result = true;
            }

            return result;
        }
    }
}
