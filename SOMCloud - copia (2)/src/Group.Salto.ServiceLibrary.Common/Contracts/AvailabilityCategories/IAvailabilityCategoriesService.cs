using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.AvailabilityCategories
{
    public interface IAvailabilityCategoriesService
    {
        IList<BaseNameIdDto<int>> GetAllKeyValues();
    }
}