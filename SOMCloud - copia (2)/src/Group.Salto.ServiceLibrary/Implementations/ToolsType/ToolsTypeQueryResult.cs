using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.ToolsType;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Toolstype
{
    public class ToolsTypeQueryResult : IToolsTypeQueryResult
    {
        private IToolsTypeRepository _toolstypeRepository;

        public ToolsTypeQueryResult(IToolsTypeRepository toolstypeRepository)
        {
            _toolstypeRepository = toolstypeRepository ?? throw new ArgumentNullException($"{nameof(IToolsTypeRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetFiltered(QueryTypeParametersDto queryTypeParameters)
        {
            Int32.TryParse(queryTypeParameters.Value, out int companyId);
            var data = _toolstypeRepository.GetAllKeyValues();
            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }
    }
}