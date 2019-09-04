using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ItemsSerialNumberStatuses;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Implementations.ItemsSerialNumberStatusesService
{
    public class ItemsSerialNumberStatusesService : BaseService, IItemsSerialNumberStatusesService
    {
        private readonly IItemsSerialNumberStatusesRepository _itemsSerialNumberStatusesRepository;

        public ItemsSerialNumberStatusesService(ILoggingService logginingService, IItemsSerialNumberStatusesRepository itemsSerialNumberStatusesRepository) : base(logginingService)
        {
            _itemsSerialNumberStatusesRepository = itemsSerialNumberStatusesRepository ?? throw new ArgumentNullException($"{nameof(IItemsSerialNumberStatusesService)} is null");
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            var query = _itemsSerialNumberStatusesRepository.GetAllKeyValues();
            var data = query.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
            return data;
        }
    }
}