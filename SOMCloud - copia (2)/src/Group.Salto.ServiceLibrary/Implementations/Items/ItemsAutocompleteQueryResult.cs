using Group.Salto.Common.Enums;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.Items;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Items
{
    public class ItemsAutocompleteQueryResult : IItemsAutocompleteQueryResult
    {
        private IItemsRepository _itemsRepository;

        public ItemsAutocompleteQueryResult(IItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository ?? throw new ArgumentNullException($"{nameof(IItemsRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetFiltered(QueryTypeParametersDto queryTypeParameters)
        {
            FilterQueryDto filterQuery = new FilterQueryDto() { Name = queryTypeParameters.Text, Active = ActiveEnum.Active };
            return _itemsRepository.GetAllByFilters(filterQuery)
                .Select(x => new BaseNameIdDto<int>()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
        }
    }
}