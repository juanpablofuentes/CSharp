using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderDerivated;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderDerivated;
using System;
using Group.Salto.ServiceLibrary.Extensions;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Contracts;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrderDerivated
{
    public class WorkOrderDerivatedService : BaseService, IWorkOrderDerivatedService
    {
        private readonly IWorkOrdersDerivateRepository _workOrdersDerivateRepository;
        private readonly IPeopleRepository _peopleRepository;
        private readonly ITranslationService _translationService;

        public WorkOrderDerivatedService(ILoggingService logginingService,
                                         IPeopleRepository peopleRepository,
                                         IWorkOrdersDerivateRepository workOrdersDerivateRepository,
                                         ITranslationService translationService) : base(logginingService)
        {
            _workOrdersDerivateRepository = workOrdersDerivateRepository ?? throw new ArgumentNullException($"{nameof(IWorkOrdersDerivateRepository)} is null");
            _peopleRepository = peopleRepository ?? throw new ArgumentNullException($"{nameof(IWorkOrdersDerivateRepository)} is null");
            _translationService = translationService ?? throw new ArgumentNullException($"{nameof(ITranslationService)} is null");
        }

        public ResultDto<WorkOrderDerivatedDto> GetById(int Id)
        {
            LogginingService.LogInfo($"Get WorkOrder by id {Id}");
            WorkOrderDerivatedDto result = _workOrdersDerivateRepository.GetEditById(Id).ToEditDto();

            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<List<List<BaseNameIdDto<string>>>> GetByTaskId(int Id, int languageId)
        {
            LogginingService.LogInfo($"Get WorkOrder derivated by taskid {Id}");
            IList<WorkOrderDerivatedDto> data = _workOrdersDerivateRepository.GetByTaskId(Id).ToEditDto();
            List<List<BaseNameIdDto<string>>> rows = new List<List<BaseNameIdDto<string>>>();
            IList<BaseNameIdDto<int>> politicy = GetDuplicationPolicyItemsKeyValues(languageId);

            foreach (WorkOrderDerivatedDto row in data)
            {
                List<BaseNameIdDto<string>> derivateWorkOrder = new List<BaseNameIdDto<string>>();
                derivateWorkOrder.Add(new BaseNameIdDto<string>() { Name = _translationService.GetTranslationText("WorkOrderDerivatedIdText"), Id = row.Id.ToString()});
                derivateWorkOrder.Add(new BaseNameIdDto<string>() { Name = _translationService.GetTranslationText("WorkOrderDerivatedTextRepairText"), Id = row.TextRepair ?? "-" });
                derivateWorkOrder.Add(new BaseNameIdDto<string>() { Name = _translationService.GetTranslationText("WorkOrderDerivatedObservationsText"), Id = row.Observations ?? "-" });
                derivateWorkOrder.Add(new BaseNameIdDto<string>() { Name = _translationService.GetTranslationText("WorkOrderDerivatedCreationDateText"), Id = row.CreationDate.ToString() ?? "-" });
                derivateWorkOrder.Add(new BaseNameIdDto<string>() { Name = _translationService.GetTranslationText("WorkOrderDerivatedAssignmentDateText"), Id = row.AssignmentTime.ToString() ?? "-" });
                derivateWorkOrder.Add(new BaseNameIdDto<string>() { Name = _translationService.GetTranslationText("WorkOrderDerivatedPickUpTimeText"), Id = row.PickUpTime.ToString() ?? "-" });
                derivateWorkOrder.Add(new BaseNameIdDto<string>() { Name = _translationService.GetTranslationText("WorkOrderDerivatedActuationDateText"), Id = row.ActuationDate.ToString() ?? "-" });
                derivateWorkOrder.Add(new BaseNameIdDto<string>() { Name = _translationService.GetTranslationText("WorkOrderDerivatedFinalClientClosingTimeText"), Id = row.FinalClientClosingTime.ToString() ?? "-" });
                derivateWorkOrder.Add(new BaseNameIdDto<string>() { Name = _translationService.GetTranslationText("WorkOrderDerivatedInternalClosingTimeText"), Id = row.InternalClosingTime.ToString() ?? "-" });
                derivateWorkOrder.Add(new BaseNameIdDto<string>() { Name = _translationService.GetTranslationText("WorkOrderDerivatedInheritTechnicianText"), Id = row.InheritTechnician ? _translationService.GetTranslationText("Yes") : _translationService.GetTranslationText("No") });
                derivateWorkOrder.Add(new BaseNameIdDto<string>() { Name = _translationService.GetTranslationText("WorkOrderDerivatedQueueText"), Id = row.QueueName ?? "-" });
                derivateWorkOrder.Add(new BaseNameIdDto<string>() { Name = _translationService.GetTranslationText("WorkOrderDerivatedInheritProjectText"), Id = row.InheritProject ? _translationService.GetTranslationText("Yes") : _translationService.GetTranslationText("No") });
                derivateWorkOrder.Add(new BaseNameIdDto<string>() { Name = _translationService.GetTranslationText("WorkOrderDerivatedProjectText"), Id = row.ProjectName ?? "-" });
                derivateWorkOrder.Add(new BaseNameIdDto<string>() { Name = _translationService.GetTranslationText("WorkOrderDerivatedClientSiteText"), Id = row.ClientSiteName ?? "-" });
                derivateWorkOrder.Add(new BaseNameIdDto<string>() { Name = _translationService.GetTranslationText("WorkOrderDerivatedSiteText"), Id = row.SiteName ?? "-" });
                derivateWorkOrder.Add(new BaseNameIdDto<string>() { Name = _translationService.GetTranslationText("WorkOrderDerivatedStateText"), Id = row.WorkOrderStatusName ?? "-" });
                derivateWorkOrder.Add(new BaseNameIdDto<string>() { Name = _translationService.GetTranslationText("WorkOrderDerivatedExternalStateText"), Id = row.ExternalWorOrderStatusName ?? "-" });
                derivateWorkOrder.Add(new BaseNameIdDto<string>() { Name = _translationService.GetTranslationText("WorkOrderDerivatedTypeText"), Id = row.WorkOrderTypeName ?? "-" });
                derivateWorkOrder.Add(new BaseNameIdDto<string>() { Name = _translationService.GetTranslationText("WorkOrderDerivatedCategoryText"), Id =  row.WorkOrderCategoryName ?? "-" });
                derivateWorkOrder.Add(new BaseNameIdDto<string>() { Name = _translationService.GetTranslationText("WorkOrderDerivatedGeneratorServiceDuplicationPolicyText"), Id = politicy.FirstOrDefault(x => x.Id == row.GeneratorServiceDuplicationPolicy)?.Name });
                derivateWorkOrder.Add(new BaseNameIdDto<string>() { Name = _translationService.GetTranslationText("WorkOrderDerivatedOtherServicesDuplicationPolicyText"), Id = politicy.FirstOrDefault(x => x.Id == row.OtherServicesDuplicationPolicy)?.Name });
                rows.Add(derivateWorkOrder);
            }

            return ProcessResult(rows, rows != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<WorkOrderDerivatedDto> Create(WorkOrderDerivatedDto workOrderEditDto)
        {
            LogginingService.LogInfo($"Create WorkOrder Derivated");
            SaveResult<WorkOrdersDeritative> resultSave = null;
            Entities.Tenant.People people = _peopleRepository.GetByConfigId(workOrderEditDto.UserConfigurationId);
            workOrderEditDto.PeopleIntroducedById = people.Id;

            WorkOrdersDeritative local = workOrderEditDto.ToEntity();

            resultSave = _workOrdersDerivateRepository.CreateWorkOrderDerivated(local);
            if (resultSave.IsOk)
            {
                return ProcessResult(resultSave.Entity.ToEditDto(), resultSave);
            }
            else
            {
                return ProcessResult(workOrderEditDto, resultSave);
            }
        }

        public ResultDto<WorkOrderDerivatedDto> Update(WorkOrderDerivatedDto workOrderEditDto)
        {
            LogginingService.LogInfo($"Update WorkOrder Derivated with id = {workOrderEditDto.Id}");
            ResultDto<WorkOrderDerivatedDto> result = null;
            var local = _workOrdersDerivateRepository.GetEditById(workOrderEditDto.Id);
            if (local != null)
            {
                local.UpdateWorkOrdersDeritative(workOrderEditDto.ToEntity());

                var resultSave = _workOrdersDerivateRepository.UpdateWorkOrderDerivated(local);
                if (resultSave.IsOk)
                {
                    return ProcessResult(resultSave.Entity.ToEditDto(), resultSave);
                }
                else
                {
                    result = ProcessResult(workOrderEditDto, resultSave);
                }
            }
            return result ?? new ResultDto<WorkOrderDerivatedDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = workOrderEditDto,
            };
        }

        public IList<BaseNameIdDto<int>> GetDuplicationPolicyItemsKeyValues(int languageId)
        {
            List<BaseNameIdDto<int>> data = new List<BaseNameIdDto<int>>();
            data.Add(new BaseNameIdDto<int>() { Id = 0, Name = _translationService.GetTranslationText("WorkOrderDerivatedNothing") });
            data.Add(new BaseNameIdDto<int>() { Id = 1, Name = _translationService.GetTranslationText("WorkOrderDerivatedRefence") });
            data.Add(new BaseNameIdDto<int>() { Id = 2, Name = _translationService.GetTranslationText("WorkOrderDerivatedClone") });
            return data;
        }
    }
}