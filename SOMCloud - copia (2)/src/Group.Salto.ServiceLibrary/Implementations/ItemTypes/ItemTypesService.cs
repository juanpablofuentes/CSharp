using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ItemTypes;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.ItemTypes
{
    public class ItemTypesService: BaseService, IItemTypesService
    {
        private readonly IItemTypesRepository _itemTypesRepository;

        public ItemTypesService(ILoggingService logginingService,
            IItemTypesRepository itemTypesRepository) : base(logginingService)
        {
            _itemTypesRepository = itemTypesRepository ?? throw new ArgumentNullException($"{nameof(IItemTypesRepository)} is null");
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            var query = _itemTypesRepository.GetAll();
            var data = query.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
            return data;
        }
    }
}