using Group.Salto.Common;
using Group.Salto.Entities;
using Group.Salto.Entities.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IRepetitionParametersRepository : IRepository<RepetitionParameters>
    {
        IQueryable<RepetitionParameters> GetAll();
        RepetitionParameters GetFirst();
        RepetitionParameters GetById(Guid id);
        SaveResult<RepetitionParameters> UpdateRepetitionParameters(RepetitionParameters entity);
    }
}