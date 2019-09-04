using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkCenter;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.People
{
    public class WorkCenterByCompanyQueryResult : IWorkCenterByCompanyQueryResult
    {
        private IWorkCenterRepository _workCenterRepository;

        public WorkCenterByCompanyQueryResult(IWorkCenterRepository workCenterRepository)
        {
            _workCenterRepository = workCenterRepository ?? throw new ArgumentNullException($"{nameof(IWorkCenterRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetFiltered(QueryTypeParametersDto queryTypeParameters)
        {
            Int32.TryParse(queryTypeParameters.Value, out int companyId);
            var data = _workCenterRepository.GetActiveByCompanyKeyValue(companyId);
            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }
    }
}