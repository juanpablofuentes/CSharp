using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.AvailabilityCategories;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.AvailabilityCategories
{
    public class AvailabilityCategoriesService : BaseService, IAvailabilityCategoriesService
    {
        private readonly IAvailabilityCategoriesRepository _availabilityCategories;

        public AvailabilityCategoriesService(ILoggingService logginingService,
                                             IAvailabilityCategoriesRepository availabilityCategories) : base(logginingService)
        {
            _availabilityCategories = availabilityCategories ?? throw new ArgumentNullException($"{nameof(IAvailabilityCategoriesRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"AvailabilityCategories --> GetAllKeyValues");
            var data = _availabilityCategories.GetAllKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }
    }
}
