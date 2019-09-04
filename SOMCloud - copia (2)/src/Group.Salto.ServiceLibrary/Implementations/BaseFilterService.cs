using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Query;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations
{
    public class BaseFilterService : BaseService
    {
        private readonly IQueryFactory _queryFactory;

        public BaseFilterService(IQueryFactory queryFactory, ILoggingService logginingService) : base(logginingService)
        {
            _queryFactory = queryFactory ?? throw new ArgumentNullException($"{nameof(IQueryFactory)} is null");
        }

        public IList<BaseNameIdDto<int>> Filter(QueryRequestDto queryRequest)
        {
            var query = _queryFactory.GetQuery(queryRequest.QueryType);
            var result = query.GetFiltered(queryRequest.QueryTypeParameters);
            return result;
        }
    }
}