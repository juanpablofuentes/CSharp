using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderCategoriesCollection;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategoriesCollection;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrderCategoriesCollection
{
    public class WorkOrderCategoriesCollectionService : BaseFilterService, IWorkOrderCategoriesCollectionService
    {
        private readonly IWorkOrderCategoriesCollectionRepository _workOrderCategoriesCollectionRepository;

        public WorkOrderCategoriesCollectionService(ILoggingService logginingService,
                                        IWorkOrderCategoriesCollectionRepository workOrderCategoriesCollectionRepository,
                                        IWorkOrderCategoriesCollectionQueryFactory queryFactory)
                                        : base(queryFactory, logginingService)
        {
            _workOrderCategoriesCollectionRepository = workOrderCategoriesCollectionRepository ?? throw new ArgumentNullException($"{nameof(IWorkOrderCategoriesCollectionRepository)} is null");
        }

        public ResultDto<IList<WorkOrderCategoriesCollectionDetailDto>> GetAllFiltered(WorkOrderCategoriesCollectionFilterDto filter)
        {
            var query = _workOrderCategoriesCollectionRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Info, au => au.Info.Contains(filter.Info));
            query = query.Where(au => !au.IsDeleted);
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().ToListDetailDto());
        }

        private IQueryable<WorkOrderCategoriesCollections> OrderBy(IQueryable<WorkOrderCategoriesCollections> data, WorkOrderCategoriesCollectionFilterDto filter)
        {
            var query = data.AsQueryable();
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Info);
            return query;
        }

        public ResultDto<WorkOrderCategoriesCollectionDetailDto> GetById(int id)
        {
            var workOrderCategoriesCollection = _workOrderCategoriesCollectionRepository.GetWithCategoriesById(id);
            return ProcessResult(workOrderCategoriesCollection.ToDetailDto(), workOrderCategoriesCollection != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<WorkOrderCategoriesCollectionDetailDto> Create(WorkOrderCategoriesCollectionDetailDto model)
        {
            ResultDto<WorkOrderCategoriesCollectionDetailDto> result = null;
            var entity = model.ToEntity();
            var resultRepository = _workOrderCategoriesCollectionRepository.CreateWorkOrderCategoriesCollections(entity);
            result = ProcessResult(resultRepository?.Entity?.ToDetailDto(), resultRepository);
            return result;
        }

        public ResultDto<WorkOrderCategoriesCollectionDetailDto> Update(WorkOrderCategoriesCollectionDetailDto model)
        {
            ResultDto<WorkOrderCategoriesCollectionDetailDto> result = null;

            var entity = _workOrderCategoriesCollectionRepository.GetById(model.Id);
            if (entity != null)
            {
                entity.Name = model.Name;
                entity.Info = model.Info;
                var resultRepository = _workOrderCategoriesCollectionRepository.UpdateWorkOrderCategoriesCollections(entity);
                result = ProcessResult(resultRepository?.Entity?.ToDetailDto(), resultRepository);
            }
            return result ?? new ResultDto<WorkOrderCategoriesCollectionDetailDto>()
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
            LogginingService.LogInfo($"Delete WO Categories Collection by id {id}");
            ResultDto<bool> result = null;
            var entity = _workOrderCategoriesCollectionRepository.GetById(id);
            if (entity != null)
            {
                var resultSave = _workOrderCategoriesCollectionRepository.DeletesWorkOrdersCategoryCollection(entity);
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
            var collection = _workOrderCategoriesCollectionRepository.GetWithProjectById(id);
            var result = collection != null && !collection.Projects.Any();
            return ProcessResult(result);
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get all WO Categories Key Value");
            var query = _workOrderCategoriesCollectionRepository.GetAllKeyValues();
            var data = query.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
            return data;
        }
    }
}