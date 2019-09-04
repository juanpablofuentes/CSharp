using Group.Salto.Common;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ToolsType;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.ToolsType;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Group.Salto.ServiceLibrary.Implementations.Toolstype
{
    public class ToolsTypeService : BaseFilterService, IToolsTypeService
    {
        private readonly IToolsTypeRepository _toolstypeRepository;
        private readonly IKnowledgeRepository _knowledgeRepository;
        public ToolsTypeService(ILoggingService logginingService, IToolsTypeRepository toolstypeRepository,IKnowledgeRepository knowledgeRepository, IToolsTypeQueryFactory queryFactory) : base(queryFactory, logginingService)
        {
            _toolstypeRepository = toolstypeRepository ?? throw new ArgumentNullException($"{nameof(IToolsRepository)} is null ");
            _knowledgeRepository = knowledgeRepository ?? throw new ArgumentNullException($"{nameof(IKnowledgeRepository)} is null ");
        }

        public ResultDto<IList<ToolsTypeDto>> GetAllFiltered(ToolsTypeFilterDto filter)
        {
            LogginingService.LogInfo($"Get All Tools Filtered");
            var query = _toolstypeRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Description, au => au.Description.Contains(filter.Description));
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().MapList(c => c.ToListDto()));
        }
        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get Tools Key Value");
            var data = _toolstypeRepository.GetAllKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }
        private ToolsType AssignKnowledges(ToolsType entity, IList<ToolsTypeKnowledgeDto> knowledges)
        {
            entity.KnowledgeToolsType?.Clear();
            if (knowledges != null && knowledges.Any())
            {
                entity.KnowledgeToolsType = entity.KnowledgeToolsType ?? new List<KnowledgeToolsType>();
                var localKnowledges = _knowledgeRepository.FilterById(knowledges.Select(x => x.Id));
                foreach (var localKnowledge in localKnowledges)
                {
                    entity.KnowledgeToolsType.Add(new KnowledgeToolsType()
                    {
                        Knowledge = localKnowledge
                    });
                }
            }
            return entity;
        }

        private IQueryable<Entities.Tenant.ToolsType> OrderBy(IQueryable<Entities.Tenant.ToolsType> query, ToolsTypeFilterDto filter)
        {
            LogginingService.LogInfo($"Order By Tools");
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Description);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Observations);
            return query;
        }

        public ResultDto<ToolsTypeDetailsDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get By Id Tools");
            var data = _toolstypeRepository.GetById(id);
            return ProcessResult(data.ToDetailDto());
        }

        public ResultDto<ToolsTypeDetailsDto> CreateToolsType(ToolsTypeDetailsDto source)
        {
            LogginingService.LogInfo($"Create ToolsType");
            var entityToolsType = source.ToEntity();
            entityToolsType = AssignKnowledges(entityToolsType, source.KnowledgeSelected);
            var result = _toolstypeRepository.CreateToolsType(entityToolsType);
            return ProcessResult(result.Entity?.ToDetailDto(), result);
        }
        public ResultDto<ToolsTypeDetailsDto> UpdateToolsType(ToolsTypeDetailsDto source)
        {
            LogginingService.LogInfo($"Update ToolsType");
            ResultDto<ToolsTypeDetailsDto> result = null;
            var findToolType = _toolstypeRepository.GetById(source.Id);
            if (findToolType != null)
            {
                var updatedToolsType = findToolType.Update(source);
                updatedToolsType = AssignKnowledges(updatedToolsType, source.KnowledgeSelected);
                var resultRepository = _toolstypeRepository.UpdateToolsType(updatedToolsType);
                result = ProcessResult(resultRepository.Entity?.ToDetailDto(), resultRepository);
            }

            return result ?? new ResultDto<ToolsTypeDetailsDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = source,
            };
        }

        public ResultDto<bool> DeleteToolsType(int id)
        {
            LogginingService.LogInfo($"Delete ToolsType by id {id}");
            ResultDto<bool> result = null;
            var localToolsType = _toolstypeRepository.GetById(id);
            if (localToolsType != null)
            {
                localToolsType = RemoveAssingationForKnowledge(localToolsType);
                localToolsType = RemoveAssignationTool(localToolsType);
                var resultSave = _toolstypeRepository.DeleteToolsType(localToolsType);
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

        private ToolsType RemoveAssignationTool(ToolsType localToolsType)
        {
            localToolsType?.ToolsToolTypes?.Clear();
            return localToolsType;
        }

        private ToolsType RemoveAssingationForKnowledge(ToolsType localToolsType)
        {
            if (localToolsType.KnowledgeToolsType != null && localToolsType.KnowledgeToolsType.Any())
            {
                localToolsType.KnowledgeToolsType.Clear();
            }
            localToolsType.KnowledgeToolsType = null;
            return localToolsType;
        }
    }
}