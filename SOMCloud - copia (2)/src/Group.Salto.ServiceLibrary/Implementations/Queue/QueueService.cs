using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Queue;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using Group.Salto.ServiceLibrary.Common.Dtos.Queue;
using Group.Salto.ServiceLibrary.Common.Dtos.Translation;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderViewConfigurations;
using Group.Salto.ServiceLibrary.Extensions;

namespace Group.Salto.ServiceLibrary.Implementations.Queue
{
    public class QueueService : BaseFilterService, IQueueService
    {
        private readonly IPermissionsRepository _permissionsRepository;
        private readonly IQueueRepository _queueRepository;

        public QueueService(ILoggingService logginingService,
                            IPermissionsRepository permissionsRepository,
                            IQueueRepository queueRepository,
                            IQueueQueryFactory queueQueryFactory) : base(queueQueryFactory, logginingService)
        {
            _permissionsRepository = permissionsRepository ?? throw new ArgumentNullException($"{nameof(IPermissionsRepository)} is null");
            _queueRepository = queueRepository ?? throw new ArgumentNullException($"{nameof(IQueueRepository)} is null");
        }

        public ResultDto<IList<QueueListDto>> GetAllFilteredByLanguage(QueueFilterDto filter)
        {
            LogginingService.LogInfo($"Get all work order status with include translations");
            var query = _queueRepository.GetAllWithIncludeTranslations();
            query = query.WhereIfNotDefault(filter.Name,
                au => !au.QueuesTranslations.Any(t => t.LanguageId == filter.LanguageId)
                ? au.Name.Contains(filter.Name)
                : au.QueuesTranslations.FirstOrDefault(t => t.LanguageId == filter.LanguageId).NameText.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Description,
                au => !au.QueuesTranslations.Any(t => t.LanguageId == filter.LanguageId)
                ? au.Description.Contains(filter.Description)
                : au.QueuesTranslations.FirstOrDefault(t => t.LanguageId == filter.LanguageId).DescriptionText.Contains(filter.Description));
            var result = OrderBy(query.MapList(wos => wos.FilterByLanguage(filter.LanguageId).ToQueueListDto(wos)).AsQueryable(), filter).ToList();
            return ProcessResult<IList<QueueListDto>>(result);
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues(int languageId)
        {
            LogginingService.LogInfo($"Get all KeyValue Queues");
            IQueryable<Queues> query = _queueRepository.GetAllWithIncludeTranslations();
            return query.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Id,
                Name = !x.QueuesTranslations.Any(t => t.LanguageId == languageId) ? x.Name : x.QueuesTranslations.FirstOrDefault(t => t.LanguageId == languageId).NameText,
            })
            .OrderBy(o => o.Name)
            .ToList();
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValuesWithPermissions(int languageId, int userId)
        {
            LogginingService.LogInfo($"Get all KeyValue Queues");

            return Filter(new QueryRequestDto()
            {
                QueryType = QueryTypeEnum.Autocomplete,
                QueryTypeParameters = new QueryTypeParametersDto()
                {
                    Value = userId.ToString(),
                    LanguageId = languageId
                }
            })
            .OrderBy(o => o.Name)
            .ToList();
        }

        public ResultDto<QueueListDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get Queue by id {id}");
            var result = _queueRepository.GetById(id)?.ToDto();
            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<QueueListDto> Create(QueueListDto model)
        {
            LogginingService.LogInfo($"Create Queue");
            var entity = model.ToEntity();
            entity = AssingPermissions(entity, model.PermissionsSelected);
            var resultSave = _queueRepository.CreateQueue(entity);
            return ProcessResult(resultSave.Entity.ToDto(), resultSave);
        }

        public ResultDto<QueueListDto> Update(QueueListDto model)
        {
            LogginingService.LogInfo($"Update Queue with id {model.Id}");

            ResultDto<QueueListDto> result = null;
            var localModel = _queueRepository.GetById(model.Id);
            if (localModel != null)
            {
                localModel.Name = model.Name;
                localModel.Description = model.Description;
                localModel = UpdateTranslations(localModel, model.Translations);
                localModel = AssingPermissions(localModel, model.PermissionsSelected);
                var resultSave = _queueRepository.UpdateQueue(localModel);
                result = ProcessResult(resultSave.Entity.ToDto(), resultSave);
            }
            return result ?? new ResultDto<QueueListDto>()
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
            LogginingService.LogInfo($"Delete Queue by id {id}");
            ResultDto<bool> result = null;
            var localQueue = _queueRepository.GetById(id);
            if (localQueue != null)
            {
                var resultSave = _queueRepository.DeleteQueue(localQueue);
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
            return ProcessResult(!_queueRepository.GetByIdWithWorkOrders(id)?.WorkOrders?.Any() ?? false);
        }

        public ResultDto<List<MultiSelectItemDto>> GetQueueMultiSelect(MultiSelectConfigurationViewDto multiSelectConfigurationViewDto, List<int> selectItems)
        {
            LogginingService.LogInfo($"GetQueueMultiSelectMultiSelect");
            IList<BaseNameIdDto<int>> queues = Filter(new QueryRequestDto() { QueryType = QueryTypeEnum.Autocomplete, QueryTypeParameters = new QueryTypeParametersDto() { Value = multiSelectConfigurationViewDto.UserId.ToString(), LanguageId = multiSelectConfigurationViewDto.LanguageId } });
            return GetMultiSelect(queues, selectItems);
        }

        public string GetNamesComaSeparated(int languageId, List<int> ids)
        {
            List<string> names = _queueRepository.GetByIds(ids).Select(x => x.Name).ToList();
            return names.Aggregate((i, j) => $"{i}, {j}");
        }

        private Queues AssingPermissions(Queues entity, IList<int> permissionsSelected)
        {
            entity.PermissionQueue?.Clear();
            if (permissionsSelected != null && permissionsSelected.Any())
            {
                entity.PermissionQueue = entity.PermissionQueue ?? new List<PermissionsQueues>();
                var permissions = _permissionsRepository.GetAllById(permissionsSelected);
                foreach (var permission in permissions)
                {
                    entity.PermissionQueue.Add(new PermissionsQueues()
                    {
                        Permission = permission,
                    });
                }
            }
            return entity;
        }

        private Queues UpdateTranslations(Queues localModel, IList<ContentTranslationDto> translations)
        {
            localModel.QueuesTranslations?.Clear();
            localModel.QueuesTranslations = translations.ToQueueTranslationEntity();
            return localModel;
        }

        private IQueryable<QueueListDto> OrderBy(IQueryable<QueueListDto> query, QueueFilterDto filter)
        {
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Description);
            return query;
        }
    }
}