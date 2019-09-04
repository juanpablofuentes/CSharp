using Group.Salto.Common;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.CollectionsExtraField;
using Group.Salto.ServiceLibrary.Common.Contracts.ExtraFieldTypes;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.CollectionsExtraField;
using Group.Salto.ServiceLibrary.Common.Dtos.CollectionsExtraFieldExtraField;
using Group.Salto.ServiceLibrary.Common.Dtos.ExtraFieldTypes;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.CollectionsExtraField
{
    public class CollectionsExtraFieldService : BaseService, ICollectionsExtraFieldService
    {
        private readonly ICollectionsExtraFieldRepository _collectionsExtraFieldRepository;
        private readonly IExtraFieldTypesService _extraFieldTypesService;
        private readonly IExtraFieldsRepository _extraFieldsRepository;
        private readonly ICollectionsExtraFieldExtraFieldRepository _collectionsExtraFieldExtraFieldRepository;
        private readonly IPredefinedServiceRepository _predefinedServiceRepository;

        public CollectionsExtraFieldService(ILoggingService logginingService,
                                            ICollectionsExtraFieldRepository collectionsExtraFieldRepository,
                                            IExtraFieldTypesService extraFieldTypesService,
                                            IExtraFieldsRepository extraFieldsRepository,
                                            ICollectionsExtraFieldExtraFieldRepository collectionsExtraFieldExtraFieldRepository,
                                            IPredefinedServiceRepository predefinedServiceRepository) : base(logginingService)
        {
            _collectionsExtraFieldRepository = collectionsExtraFieldRepository ?? throw new ArgumentNullException($"{nameof(ICollectionsExtraFieldRepository)} is null");
            _extraFieldTypesService = extraFieldTypesService ?? throw new ArgumentNullException($"{nameof(IExtraFieldTypesService)} is null");
            _extraFieldsRepository = extraFieldsRepository ?? throw new ArgumentNullException($"{nameof(IExtraFieldsRepository)} is null");
            _collectionsExtraFieldExtraFieldRepository = collectionsExtraFieldExtraFieldRepository ?? throw new ArgumentNullException($"{nameof(ICollectionsExtraFieldExtraFieldRepository)} is null");
            _predefinedServiceRepository = predefinedServiceRepository ?? throw new ArgumentNullException($"{nameof(IPredefinedServiceRepository)} is null");
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get CollectionsExtraField Key Value");
            var data = _collectionsExtraFieldRepository.GetAllKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }

        public ResultDto<IList<CollectionsExtraFieldDto>> GetAllFiltered(CollectionsExtraFieldFilterDto filter)
        {
            var query = _collectionsExtraFieldRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().ToListDto());
        }

        public ResultDto<CollectionsExtraFieldDetailDto> GetByIdWithExtraFields(int id)
        {
            var collectionsExtraField = _collectionsExtraFieldRepository.GetByIdWithExtraFields(id);
            IList<ExtraFieldsTypesDto> extraFieldsType = _extraFieldTypesService.GetAll().Data;
            return ProcessResult(collectionsExtraField.ToDetailDto(extraFieldsType), collectionsExtraField != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<CollectionsExtraFieldDetailDto> Create(CollectionsExtraFieldDetailDto model)
        {
            LogginingService.LogInfo($"Create CollectionsExtraField");
            IEnumerable<int> ids = model.CollectionsExtraFieldExtraField.Select(x => x.ExtraFieldId);
            IList<Entities.Tenant.ExtraFields> extraFieldsIds = _extraFieldsRepository.GetByIds(ids);

            var localCollectionsExtraField = model.ToEntity();
            localCollectionsExtraField = localCollectionsExtraField.AssignToAddCollectionsExtraFieldExtraField(model, extraFieldsIds);
            var resultSave = _collectionsExtraFieldRepository.CreateCollectionsExtraField(localCollectionsExtraField);
            if (resultSave.IsOk)
            {
                IList<ExtraFieldsTypesDto> extraFieldsType = _extraFieldTypesService.GetAll().Data;
                return ProcessResult(resultSave.Entity.ToDetailDto(extraFieldsType), resultSave);
            }
            else
            {
                return ProcessResult(model, resultSave);
            }
        }

        public ResultDto<CollectionsExtraFieldDetailDto> Update(CollectionsExtraFieldDetailDto model)
        {
            LogginingService.LogInfo($"Update CollectionsExtraField for Id {model.Id}");
            ResultDto<CollectionsExtraFieldDetailDto> result = null;
            IEnumerable<int> ids = model.CollectionsExtraFieldExtraField.Select(x => x.ExtraFieldId);
            IList<Entities.Tenant.ExtraFields> extraFieldsIds = _extraFieldsRepository.GetByIds(ids);

            Entities.Tenant.CollectionsExtraField entity = _collectionsExtraFieldRepository.GetByIdWithExtraFields(model.Id);
            if (entity != null)
            {
                entity.UpdateCollectionsExtraField(model.ToEntity());
                entity = entity.AssignToAddCollectionsExtraFieldExtraField(model, extraFieldsIds);
                entity = entity.AssignToUpdateCollectionsExtraFieldExtraField(model, extraFieldsIds);
                entity = DeleteCollectionsExtraFieldExtraField(model, entity);

                SaveResult<Entities.Tenant.CollectionsExtraField> resultRepository = _collectionsExtraFieldRepository.UpdateCollectionsExtraField(entity);
                if (resultRepository.IsOk)
                {
                    IList<ExtraFieldsTypesDto> extraFieldsType = _extraFieldTypesService.GetAll().Data;
                    result = ProcessResult(resultRepository.Entity.ToDetailDto(extraFieldsType), resultRepository);
                }
                else
                {
                    result = ProcessResult(model, resultRepository);
                }
            }
            return result ?? new ResultDto<CollectionsExtraFieldDetailDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = model,
            };
        }

        public ResultDto<ErrorDto> CanDelete(int id)
        {
            LogginingService.LogInfo($"Get PredefinedServices CanDelete");
            var data = _collectionsExtraFieldRepository.GetByIdWithPredefinedServices(id);
            ErrorDto result = new ErrorDto() { ErrorMessageKey = string.Empty };
            if (data?.PredefinedServices?.Any() == true)
            {
                result.ErrorMessageKey = "CollectionExtraFieldHavePredefinedServices";
            }
            else if (data?.Projects?.Any() == true && data?.Projects?.Where(x => x.IsDeleted == false).Count() > 0)
            {
                result.ErrorMessageKey = "CollectionExtraFieldHaveProjects";
            }
            return ProcessResult(result);
        }

        public ResultDto<bool> Delete(int id)
        {
            LogginingService.LogInfo($"Deleting CollectionsExtraField for Id {id}");
            var collection = _collectionsExtraFieldRepository.GetByIdWithPredefinedServices(id);
            ResultDto<bool> result  = new ResultDto<bool>();

            if (collection != null)
            {
                var resultRepository = _collectionsExtraFieldRepository.DeleteCollectionsExtraField(collection);
                result = ProcessResult(resultRepository.IsOk, resultRepository);
            }
            return result ?? new ResultDto<bool>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = false
            };
        }        

        private Entities.Tenant.CollectionsExtraField DeleteCollectionsExtraFieldExtraField(CollectionsExtraFieldDetailDto model, Entities.Tenant.CollectionsExtraField entity)
        {
            IList<CollectionsExtraFieldExtraFieldDto> forDelete = model.CollectionsExtraFieldExtraField.Where(x => x.ExtraField.State == "D").ToList();
            if (forDelete != null && forDelete.Count > 0)
            {
                foreach (CollectionsExtraFieldExtraFieldDto collectionsExtraField in forDelete)
                {
                    var entityToDelete = entity.CollectionsExtraFieldExtraField.Where(x => x.ExtraFieldId == collectionsExtraField.ExtraFieldId).FirstOrDefault();
                    if (entityToDelete != null)
                    {
                        _collectionsExtraFieldExtraFieldRepository.DeleteOnContext(entityToDelete);
                        entity.CollectionsExtraFieldExtraField.Remove(entityToDelete);
                    }
                }
            }
            return entity;
        }

        private IQueryable<Entities.Tenant.CollectionsExtraField> OrderBy(IQueryable<Entities.Tenant.CollectionsExtraField> data, CollectionsExtraFieldFilterDto filter)
        {
            var query = data.AsQueryable();
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            return query;
        }

    }
}