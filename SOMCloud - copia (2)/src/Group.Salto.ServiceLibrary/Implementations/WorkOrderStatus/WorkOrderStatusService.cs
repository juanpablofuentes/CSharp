using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderStatus;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Translation;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderStatus;
using Group.Salto.ServiceLibrary.Extensions;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrderStatus
{
    public class WorkOrderStatusService : BaseService, IWorkOrderStatusService
    {
        private readonly IWorkOrderStatusRepository _workOrderStatusRepository;

        public WorkOrderStatusService(ILoggingService logginingService, IWorkOrderStatusRepository workOrderStatusRepository) : base(logginingService)
        {
            _workOrderStatusRepository = workOrderStatusRepository ?? throw new ArgumentNullException($"{nameof(IWorkOrderStatusRepository)} is null");
        }

        public ResultDto<IList<WorkOrderStatusListDto>> GetAllFilteredByLanguage(WorkOrderStatusFilterDto filter)
        {
            LogginingService.LogInfo($"Get all work order status with include translations");
            var query = _workOrderStatusRepository.GetAllWithIncludeTranslations();

            query = query.WhereIfNotDefault(filter.Name,
                au => !au.WorkOrderStatusesTranslations.Any(t => t.LanguageId == filter.LanguageId)
                ? au.Name.Contains(filter.Name)
                : au.WorkOrderStatusesTranslations.FirstOrDefault(t => t.LanguageId == filter.LanguageId).NameText.Contains(filter.Name));

            query = query.WhereIfNotDefault(filter.Description,
                au => !au.WorkOrderStatusesTranslations.Any(t => t.LanguageId == filter.LanguageId)
                ? au.Description.Contains(filter.Description)
                : au.WorkOrderStatusesTranslations.FirstOrDefault(t => t.LanguageId == filter.LanguageId).DescriptionText.Contains(filter.Description));

            var data = query.MapList(wos => wos.FilterByLanguage(filter.LanguageId).ToWorkOrderStatusListDto(wos)).AsQueryable();
            var result = OrderBy(data, filter).ToList();

            return ProcessResult<IList<WorkOrderStatusListDto>>(result.ToList());
        }

        public ResultDto<WorkOrderStatusListDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get WorkOrderStatus by id {id}");
            var result = _workOrderStatusRepository.GetById(id)?.ToDto();
            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<WorkOrderStatusListDto> Create(WorkOrderStatusListDto model)
        {
            LogginingService.LogInfo($"Create WorkOrderStatus");
            var entity = model.ToEntity();
            var resultSave = _workOrderStatusRepository.CreateWorkOrderStatus(entity);
            return ProcessResult(resultSave.Entity.ToDto(), resultSave);
        }

        public ResultDto<WorkOrderStatusListDto> Update(WorkOrderStatusListDto model)
        {
            LogginingService.LogInfo($"Update WorkOrderStatus with id {model.Id}");

            ResultDto<WorkOrderStatusListDto> result = null;
            var localModel = _workOrderStatusRepository.GetById(model.Id);
            if (localModel != null)
            {
                localModel.Name = model.Name;
                localModel.Color = model.Color;
                localModel.Description = model.Description;
                localModel = UpdateTranslations(localModel, model.Translations);
                localModel.IsPlannable = model.IsPlannable;
                localModel.IsWoclosed = model.IsWorkOrderClosed;
                var resultSave = _workOrderStatusRepository.UpdateWorkOrderStatus(localModel);
                result =  ProcessResult(resultSave.Entity.ToDto(), resultSave);
            }
            return result ?? new ResultDto<WorkOrderStatusListDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = model,
            };
        }

        public ResultDto<bool> Delete(int id)
        {
            LogginingService.LogInfo($"Delete WorkOrderStatus by id {id}");
            ResultDto<bool> result = null;
            var localWorkOrderStatus = _workOrderStatusRepository.GetById(id);
            if (localWorkOrderStatus != null)
            {
                var resultSave = _workOrderStatusRepository.DeleteWorkOrderStatus(localWorkOrderStatus);
                result = ProcessResult(resultSave.IsOk, resultSave);
            }
            return result ?? new ResultDto<bool>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = false,
            };
        }

        public ResultDto<bool> CanDelete(int id)
        {
            return ProcessResult(!_workOrderStatusRepository.GetByIdWithWorkOrders(id)?.WorkOrders?.Any() ?? false);
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues(int languageId)
        {
            LogginingService.LogInfo($"Get WorkOrderStatus Key Value");
            var data = _workOrderStatusRepository.GetAllWithIncludeTranslations();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Id,
                Name = !x.WorkOrderStatusesTranslations.Any(t => t.LanguageId == languageId) ? x.Name : x.WorkOrderStatusesTranslations.FirstOrDefault(t => t.LanguageId == languageId).NameText
            }).ToList();
        }

        public ResultDto<List<MultiSelectItemDto>> GetWorkOrderStatusMultiSelect(int languageId, List<int> selectItems)
        {
            LogginingService.LogInfo($"GetWorkOrderStatusMultiSelect");
            IEnumerable<BaseNameIdDto<int>> workOrdersStatuses = GetAllKeyValues(languageId);
            return GetMultiSelect(workOrdersStatuses, selectItems);
        }

        public string GetNamesComaSeparated(int languageId, List<int> ids)
        {
            List<string> names = _workOrderStatusRepository.GetByIds(ids).Select(x => x.Name).ToList();
            return names.Aggregate((i, j) => $"{i}, {j}");
        }

        private WorkOrderStatuses UpdateTranslations(WorkOrderStatuses localModel, IList<ContentTranslationDto> translations)
        {
            localModel.WorkOrderStatusesTranslations?.Clear();
            localModel.WorkOrderStatusesTranslations = translations.ToWorkOrderStatusTranslationEntity();
            return localModel;
        }

        private IQueryable<WorkOrderStatusListDto> OrderBy(IQueryable<WorkOrderStatusListDto> query, WorkOrderStatusFilterDto filter)
        {
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Description);
            return query;
        }
    }
}