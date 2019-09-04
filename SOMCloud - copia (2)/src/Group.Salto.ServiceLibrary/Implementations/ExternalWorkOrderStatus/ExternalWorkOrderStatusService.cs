using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ExternalWorkOrderStatus;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.ExternalWorkOrderStatus;
using Group.Salto.ServiceLibrary.Common.Dtos.Translation;
using Group.Salto.ServiceLibrary.Extensions;

namespace Group.Salto.ServiceLibrary.Implementations.ExternalWorkOrderStatus
{
    public class ExternalWorkOrderStatusService : BaseService, IExternalWorkOrderStatusService
    {
        private readonly IExternalWorkOrderStatusRepository _externalWorkOrderStatusRepository;

        public ExternalWorkOrderStatusService(ILoggingService loggingService,
            IExternalWorkOrderStatusRepository externalWorkOrderStatusRepository) : base(loggingService)
        {
            _externalWorkOrderStatusRepository = externalWorkOrderStatusRepository ?? throw new ArgumentNullException($"{nameof(IExternalWorkOrderStatusRepository)} is null");
        }

        public ResultDto<IList<ExternalWorkOrderStatusListDto>> GetAllFilteredByLanguage(ExternalWorkOrderStatusFilterDto filter)
        {
            LogginingService.LogInfo($"Get all ExternalWorkOrderStatus with include translations");
            var query = _externalWorkOrderStatusRepository.GetAllWithIncludeTranslations();
            query = query.WhereIfNotDefault(filter.Name,
                au => !au.ExternalWorkOrderStatusesTranslations.Any(t => t.LanguageId == filter.LanguageId)
                    ? au.Name.Contains(filter.Name)
                    : au.ExternalWorkOrderStatusesTranslations.FirstOrDefault(t => t.LanguageId == filter.LanguageId).NameText.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Description,
                au => !au.ExternalWorkOrderStatusesTranslations.Any(t => t.LanguageId == filter.LanguageId)
                    ? au.Description.Contains(filter.Description)
                    : au.ExternalWorkOrderStatusesTranslations.FirstOrDefault(t => t.LanguageId == filter.LanguageId).DescriptionText.Contains(filter.Description));
            var result = OrderBy(query.MapList(wos => wos.FilterByLanguage(filter.LanguageId).ToWorkOrderStatusListDto(wos)).AsQueryable(), filter);
            return ProcessResult<IList<ExternalWorkOrderStatusListDto>>(result.ToList());
        }

        public ResultDto<ExternalWorkOrderStatusListDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get ExternalWorkOrderStatus by id {id}");
            var result = _externalWorkOrderStatusRepository.GetById(id)?.ToDto();
            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<ExternalWorkOrderStatusListDto> Create(ExternalWorkOrderStatusListDto model)
        {
            LogginingService.LogInfo($"Create ExternalWorkOrderStatus");
            var entity = model.ToEntity();
            var resultSave = _externalWorkOrderStatusRepository.CreateExternalWorkOrderStatus(entity);
            return ProcessResult(resultSave.Entity.ToDto(), resultSave);
        }

        public ResultDto<ExternalWorkOrderStatusListDto> Update(ExternalWorkOrderStatusListDto model)
        {
            LogginingService.LogInfo($"Update ExternalWorkOrderStatus with id {model.Id}");

            ResultDto<ExternalWorkOrderStatusListDto> result = null;
            var localModel = _externalWorkOrderStatusRepository.GetById(model.Id);
            if (localModel != null)
            {
                localModel.Name = model.Name;
                localModel.Color = model.Color;
                localModel.Description = model.Description;
                localModel = UpdateTranslations(localModel, model.Translations);
                var resultSave = _externalWorkOrderStatusRepository.UpdateExternalWorkOrderStatus(localModel);
                result = ProcessResult(resultSave.Entity.ToDto(), resultSave);
            }
            return result ?? new ResultDto<ExternalWorkOrderStatusListDto>()
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
            LogginingService.LogInfo($"Delete ExternalWorkOrderStatus by id {id}");
            ResultDto<bool> result = null;
            var localExternalWorkOrderStatus = _externalWorkOrderStatusRepository.GetById(id);
            if (localExternalWorkOrderStatus != null)
            {
                var resultSave = _externalWorkOrderStatusRepository.DeleteExternalWorkOrderStatus(localExternalWorkOrderStatus);
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
            return ProcessResult(!_externalWorkOrderStatusRepository.GetByIdWithWorkOrders(id)?.WorkOrders?.Any() ?? false);
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues(int languageId)
        {
            LogginingService.LogInfo($"Get ExternalWorkOrderStatus Key Value");
            var data = _externalWorkOrderStatusRepository.GetAllWithIncludeTranslations();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Id,
                Name = !x.ExternalWorkOrderStatusesTranslations.Any(t => t.LanguageId == languageId) ? x.Name : x.ExternalWorkOrderStatusesTranslations.FirstOrDefault(t => t.LanguageId == languageId).NameText
            }).ToList();
        }

        public ResultDto<List<MultiSelectItemDto>> GetExternalWorkOrderStatusMultiSelect(int languageId, List<int> selectItems)
        {
            LogginingService.LogInfo($"GetExternalWorkOrderStatusMultiSelect");
            IEnumerable<BaseNameIdDto<int>> workOrdersStatuses = GetAllKeyValues(languageId);
            return GetMultiSelect(workOrdersStatuses, selectItems);
        }

        public string GetNamesComaSeparated(int languageId, List<int> ids)
        {
            List<string> names = _externalWorkOrderStatusRepository.GetByIds(ids).Select(x => x.Name).ToList();
            return names.Aggregate((i, j) => $"{i}, {j}");
        }

        private ExternalWorOrderStatuses UpdateTranslations(ExternalWorOrderStatuses localModel, IList<ContentTranslationDto> translations)
        {
            localModel.ExternalWorkOrderStatusesTranslations?.Clear();
            localModel.ExternalWorkOrderStatusesTranslations = translations.ToExternalWorkOrderStatusTranslationEntity();
            return localModel;
        }

        private IQueryable<ExternalWorkOrderStatusListDto> OrderBy(IQueryable<ExternalWorkOrderStatusListDto> query, ExternalWorkOrderStatusFilterDto filter)
        {
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Description);
            return query;
        }
    }
}