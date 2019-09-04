using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Models;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Models
{
    public class ModelsService : BaseFilterService, IModelsService
    {
        private readonly IModelsRepository _modelsRepository;
        public ModelsService(
            ILoggingService logginingService,
            IModelsRepository modelsRepository,
            IModelsQueryFactory queryFactory) : base(queryFactory, logginingService)
        {
            _modelsRepository = modelsRepository ?? throw new ArgumentNullException($"{nameof(IModelsRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> FilterByBrand(QueryCascadeDto queryCascadeDto) 
        { 
            var query = _modelsRepository.FilterByBrand(queryCascadeDto.Text, queryCascadeDto.Selected);
            var result = query.Select(x => new BaseNameIdDto<int>()
                                {
                                    Id = x.Id,
                                    Name = x.Name
                                }).ToList();
            return result;        
        }
    }
}