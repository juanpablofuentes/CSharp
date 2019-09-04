using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Entities;
namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface ICalculationTypeRepository : IRepository<CalculationType>
    {
        IQueryable<CalculationType> GetAll();
        CalculationType GetById(Guid id);
        List<string> GetAllNamesById(IList<Guid> queryType);
        Dictionary<Guid, string> GetAllKeyValues();
    }
}