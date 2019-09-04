using Group.Salto.Entities;
using System.Collections.Generic;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface IAvailabilityCategoriesRepository : IRepository<AvailabilityCategories>
    {
        Dictionary<int, string> GetAllKeyValues();
    }
}
