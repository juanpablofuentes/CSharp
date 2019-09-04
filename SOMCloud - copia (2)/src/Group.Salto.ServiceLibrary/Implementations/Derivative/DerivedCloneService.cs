using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.AzureBlobStorage;
using Group.Salto.ServiceLibrary.Common.Contracts.Derivative;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.File;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.Derivative
{
    public class DerivedCloneService : BaseService, IDerivedCloneService
    {
        private readonly IAzureBlobStorageService _azureBlobStorageService;
        private readonly IAssetsRepository _assetsRepository;
        private readonly ISitesRepository _sitesRepository;
        private readonly IFinalClientsRepository _finalClientsRepository;
        private readonly IPeopleRepository _peopleRepository;
        private readonly IQueueRepository _queueRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IWorkOrderTypesRepository _workOrderTypesRepository;

        public DerivedCloneService(ILoggingService logginingService,
                                   IAzureBlobStorageService azureBlobStorageService,
                                   IAssetsRepository assetsRepository,
                                   ISitesRepository sitesRepository,
                                   IFinalClientsRepository finalClientsRepository,
                                   IPeopleRepository peopleRepository,
                                   IQueueRepository queueRepository,
                                   IProjectRepository projectRepository,
                                   IWorkOrderTypesRepository workOrderTypesRepository) : base(logginingService)
        {
            _azureBlobStorageService = azureBlobStorageService;
            _assetsRepository = assetsRepository;
            _sitesRepository = sitesRepository;
            _finalClientsRepository = finalClientsRepository;
            _peopleRepository = peopleRepository;
            _queueRepository = queueRepository;
            _projectRepository = projectRepository;
            _workOrderTypesRepository = workOrderTypesRepository;
        }

        public ResultDto<Services> CreateService(Entities.Tenant.People peopleResponsible, string serviceFolder, string container, DerivedServices derivedService, int responsibleId)
        {
            var result = new ResultDto<Services>();
            var formState = FormState.NoBillable;
            if (derivedService.PredefinedServices.Billable == true && derivedService.PredefinedServices.MustValidate == true)
            {
                formState = FormState.ValidationPending;
            }
            else if (derivedService.PredefinedServices.Billable == true)
            {
                formState = FormState.DeliveringPending;
            }
            var form = new Services
            {
                PredefinedServiceId = derivedService.PredefinedServicesId,
                IdentifyInternal = derivedService.InternalIdentifier,
                IdentifyExternal = derivedService.ExternalIdentifier,
                Description = derivedService.Description,
                Observations = derivedService.Observations,
                PeopleResponsibleId = peopleResponsible.Id,
                ServiceStateId = derivedService.ServiceStatesId,
                ClosingCodeFirstId = derivedService.ClosingCodesIdN1,
                ClosingCodeSecondId = derivedService.ClosingCodesIdN2,
                ClosingCodeThirdId = derivedService.ClosingCodesIdN3,
                CreationDate = DateTime.UtcNow,
                Latitude = derivedService.Latitude,
                Longitude = derivedService.Longitude,
                FormState = formState.ToString(),
                ExtraFieldsValues = new List<ExtraFieldsValues>()
            };

            if (responsibleId > 0)
            {
                form.PeopleResponsibleId = responsibleId;
            }

            var success = CloneExtraFieldValues(serviceFolder, container, derivedService.ExtraFieldsValues, form);

            if (success)
            {
                result.Data = form;
            }
            else
            {
                if (result.Errors == null)
                {
                    result.Errors = new ErrorsDto();
                }
                result.Errors.AddError(new ErrorDto
                {
                    ErrorType = ErrorType.ValidationError,
                    ErrorMessageKey = "File not saved to Blob storage."
                });
            }

            return result;
        }

        public ResultDto<Services> CloneService(Entities.Tenant.People peopleResponsible, string serviceFolder, string container, Services service, WorkOrders workOrder)
        {
            var result = new ResultDto<Services>();
            var form = new Services
            {
                Cancelled = service.Cancelled,
                ServicesCancelFormId = service.ServicesCancelFormId,
                WorkOrder = workOrder,
                CreationDate = service.CreationDate,
                DeliveryNote = service.DeliveryNote,
                DeliveryProcessInit = service.DeliveryProcessInit,
                Description = service.Description,
                FormState = service.FormState,
                ClosingCodeFirstId = service.ClosingCodeFirstId,
                ClosingCodeSecondId = service.ClosingCodeSecondId,
                ClosingCodeThirdId = service.ClosingCodeThirdId,
                ServiceStateId = service.ServiceStateId,
                IcgId = service.IcgId,
                PredefinedServiceId = service.PredefinedServiceId,
                PredefinedService = service.PredefinedService,
                IdentifyExternal = service.IdentifyExternal,
                IdentifyInternal = service.IdentifyInternal,
                Latitude = service.Latitude,
                Longitude = service.Longitude,
                Observations = service.Observations,
                PeopleResponsibleId = service.PeopleResponsibleId,
                PeopleResponsible = service.PeopleResponsible,
                SubcontractResponsibleId = service.SubcontractResponsibleId,
                ExtraFieldsValues = new List<ExtraFieldsValues>()
            };
            var success = CloneExtraFieldValues(serviceFolder, container, service.ExtraFieldsValues, form);

            if (success)
            {
                result.Data = form;
            }
            else
            {
                if (result.Errors == null)
                {
                    result.Errors = new ErrorsDto();
                }
                result.Errors.AddError(new ErrorDto
                {
                    ErrorType = ErrorType.ValidationError,
                    ErrorMessageKey = "File not saved to Blob storage."
                });
            }

            return result;
        }

        private bool CloneExtraFieldValues(string serviceFolder, string container, IEnumerable<ExtraFieldsValues> extraFieldsValues, Services form)
        {
            var success = true;
            foreach (var extraFieldValue in extraFieldsValues)
            {
                var efValue = new ExtraFieldsValues
                {
                    ServiceId = extraFieldValue.ServiceId,
                    WorkOrderDeritativeId = extraFieldValue.WorkOrderDeritativeId,
                    DerivedServiceId = extraFieldValue.DerivedServiceId,
                    WorkOrderId = extraFieldValue.WorkOrderId,
                    ExtraFieldId = extraFieldValue.ExtraFieldId,
                    EnterValue = extraFieldValue.EnterValue,
                    DataValue = extraFieldValue.DataValue,
                    DecimalValue = extraFieldValue.DecimalValue,
                    BooleaValue = extraFieldValue.BooleaValue,
                    StringValue = extraFieldValue.StringValue,
                    Signature = extraFieldValue.Signature,
                    MaterialForm = new List<MaterialForm>()
                };
                foreach (var material in extraFieldValue.MaterialForm)
                {
                    efValue.MaterialForm.Add(new MaterialForm
                    {
                        ExtraFieldValueId = material.ExtraFieldValueId,
                        SerialNumber = material.SerialNumber,
                        Reference = material.Reference,
                        Description = material.Description,
                        Units = material.Units,
                        AssetId = material.AssetId
                    });
                }
                if (extraFieldValue.ExtraField?.Type == (int)ExtraFieldValueTypeEnum.Fitxer)
                {
                    var filesBlobFolder = $"{serviceFolder}/{extraFieldValue.StringValue}";
                    var files = _azureBlobStorageService.GetFilesFromBlobFolder(container, filesBlobFolder);
                    var folderDirectory = Guid.NewGuid().ToString();
                    foreach (var file in files)
                    {
                        var fileBlobDto = new SaveFileBlobDto
                        {
                            Name = file.FileName,
                            Container = container,
                            Directory = $"{serviceFolder}/{folderDirectory}",
                            FileBytes = file.FileBytes
                        };
                        success = _azureBlobStorageService.SaveFileToBlobStorage(fileBlobDto);
                    }
                }
                form.ExtraFieldsValues.Add(efValue);
            }

            return success;
        }

        public WorkOrders CreateWorkOrder(WorkOrdersDeritative derivedWo, WorkOrders wo)
        {
            var newWo = new WorkOrders();

            newWo.WorkOrdersFatherId = wo.Id;
            newWo.WorkOrdersFather = wo;
            newWo.InternalIdentifier = wo.InternalIdentifier;
            newWo.ExternalIdentifier = wo.ExternalIdentifier;
            newWo.TextRepair = wo.TextRepair;
            newWo.Observations = wo.Observations;
            newWo.AssetId = wo.AssetId;
            if (newWo.AssetId != null)
            {
                newWo.Asset = _assetsRepository.GetByIdIncludingLocation(newWo.AssetId.Value);
            }
            newWo.LocationId = wo.LocationId;
            newWo.Location = _sitesRepository.GetByIdWithFinalClient(newWo.LocationId);
            newWo.SiteUserId = wo.SiteUserId;
            newWo.FinalClientId = wo.FinalClientId;
            newWo.FinalClient = _finalClientsRepository.GetById(newWo.FinalClientId);
            newWo.CreationDate = DateTime.UtcNow;
            newWo.AssignmentTime = DateTime.UtcNow;
            newWo.PickUpTime = DateTime.UtcNow;
            newWo.PeopleManipulatorId = null;
            newWo.PeopleResponsibleId = derivedWo.InheritTechnician == true ? wo.PeopleResponsibleId : derivedWo.PeopleResponsibleId;
            if (newWo.PeopleResponsibleId != null)
            {
                newWo.PeopleResponsible = _peopleRepository.GetByIdWithSubContractCompanyAndCost(newWo.PeopleResponsibleId.Value);
            }
            newWo.QueueId = derivedWo.QueueId ?? int.MinValue;
            newWo.Queue = _queueRepository.GetById(newWo.QueueId);
            newWo.ActionDate = derivedWo.ActionDate;
            newWo.ProjectId = derivedWo.InheritProject == true ? wo.ProjectId : (derivedWo.ProjectId ?? int.MinValue);
            newWo.Project = _projectRepository.GetByIdWithZoneProjectAndContract(newWo.ProjectId);
            newWo.WorkOrderTypesId = derivedWo.InheritProject == true ? wo.WorkOrderTypesId : derivedWo.WorkOrderTypeId;
            if (newWo.WorkOrderTypesId != null)
            {
                newWo.WorkOrderTypes = _workOrderTypesRepository.GetByIdIncludingFather(newWo.WorkOrderTypesId.Value);
            }
            newWo.WorkOrderCategoryId = derivedWo.InheritProject == true ? wo.WorkOrderCategoryId : (derivedWo.WorkOrderCategoryId ?? int.MinValue);
            newWo.WorkOrderStatusId = derivedWo.WorkOrderStatusId ?? int.MinValue;
            newWo.ExternalWorOrderStatusId = derivedWo.ExternalWorOrderStatusId;
            newWo.OriginId = 7;

            return newWo;
        }
    }
}
