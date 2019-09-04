using Group.Salto.Common;
using Group.Salto.Common.Helpers;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Knowledge;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Knowledge;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Knowledge
{
    public class KnowledgeService : BaseFilterService, IKnowledgeService 
    {
        private readonly IKnowledgeRepository _knowledgeRepository;
        public KnowledgeService(ILoggingService logginingService, IKnowledgeRepository knowledgeRepository, IKnowledgeQueryFactory queryFactory) : base(queryFactory,logginingService)
        {
            _knowledgeRepository = knowledgeRepository ?? throw new ArgumentNullException($"{nameof(knowledgeRepository)} is null ");
        }

        public ResultDto<IList<KnowledgeDto>> GetAll()
        {
            LogginingService.LogInfo($"Get All Knowledge");
            var data = _knowledgeRepository.GetAll().ToList();
            return ProcessResult(data.ToDto());
        }

        public ResultDto<IList<KnowledgeDto>> GetAllFiltered(KnowledgeFilterDto filter)
        {
            LogginingService.LogInfo($"Get All  Knowledge Filtered");
            var query = _knowledgeRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Description, au => au.Name.Contains(filter.Description));
            query = query.WhereIfNotDefault(filter.Observations, au => au.Observations.Contains(filter.Observations));
            query = query.Where(au => au.IsDeleted.Equals(false));
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().MapList(c => c.ToDto()));
        }

        private IQueryable<Entities.Tenant.Knowledge> OrderBy(IQueryable<Entities.Tenant.Knowledge> query, KnowledgeFilterDto filter)
        {
            LogginingService.LogInfo($"Order By Knowledge");
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Description);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Observations);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.UpdateDate);
            return query;
        }

        public ResultDto<KnowledgeDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get By Id Knowledge");
            var data = _knowledgeRepository.GetById(id);
            return ProcessResult(data.ToDto());
        }

        public ResultDto<KnowledgeDto> UpdateKnowledge(KnowledgeDto source)
        {
            LogginingService.LogInfo($"Update Knowledge");
            ResultDto<KnowledgeDto> result = null;
            var findKnowledge = _knowledgeRepository.GetById(source.Id);
            if (findKnowledge != null)
            {
                var updatedKnowledge = findKnowledge.Update(source);
                var resultRepository = _knowledgeRepository.UpdateKnowledge(updatedKnowledge);
                result = ProcessResult(resultRepository.Entity?.ToDto(), resultRepository);
            }

            return result ?? new ResultDto<KnowledgeDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = source,
            };
        }

        public ResultDto<bool> DeleteKnowledge(int id)
        {
            LogginingService.LogInfo($"Delete Knowledge by id {id}");
            ResultDto<bool> result = null;
            var knowledge = _knowledgeRepository.GetByIdWithAllReferences(id);
            if (knowledge != null)
            {
                knowledge.KnowledgeToolsType?.Clear();
                knowledge.KnowledgePeople?.Clear();
                knowledge.KnowledgeSubContracts?.Clear();
                knowledge.KnowledgeWorkOrderTypes?.Clear();
                knowledge.KnowledgeWorkOrderTypes?.Clear();
                var resultSave = _knowledgeRepository.DeleteKnowledge(knowledge);
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

        public ResultDto<KnowledgeDto> CreateKnowledge(KnowledgeDto source)
        {
            LogginingService.LogInfo($"Create Knowledge");
            var newKnowledge = source.ToEntity();
            var result = _knowledgeRepository.CreateKnowledge(newKnowledge);
            
            return ProcessResult(result.Entity?.ToDto(), result);
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get Knowledge Key Value");
            var data = _knowledgeRepository.GetAllKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
                
            }).ToList();
        }
    }
}