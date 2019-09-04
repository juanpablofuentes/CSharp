using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.SubFamilies;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.SubFamilies
{
    public class SubFamiliesService : BaseFilterService, ISubFamiliesService
    {
        private readonly ISubFamiliesRepository _subFamiliesRepository;
        public SubFamiliesService(
            ILoggingService logginingService,
            ISubFamiliesRepository subFamiliesRepository,
            ISubFamiliesQueryFactory queryFactory) : base(queryFactory, logginingService)
        {
            _subFamiliesRepository = subFamiliesRepository ?? throw new ArgumentNullException($"{nameof(ISubFamiliesRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> FilterByClientSite(QueryCascadeDto queryCascadeDto)
        {
            var query = _subFamiliesRepository.FilterByClientSite(queryCascadeDto.Text, queryCascadeDto.Selected);
            var result = query.Select(x => new BaseNameIdDto<int>()
                                {
                                    Id = x.Id,
                                    Name = x.Nom
                                }).ToList();
            return result;
        }
    }
}