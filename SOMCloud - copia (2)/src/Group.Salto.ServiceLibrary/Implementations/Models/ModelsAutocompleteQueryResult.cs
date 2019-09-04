﻿using Group.Salto.Common.Enums;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.Models;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Models
{
    public class ModelsAutocompleteQueryResult : IModelsAutocompleteQueryResult
    {
        private IModelsRepository _modelsRepository;

        public ModelsAutocompleteQueryResult(IModelsRepository modelsRepository)
        {
            _modelsRepository = modelsRepository ?? throw new ArgumentNullException($"{nameof(IModelsRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetFiltered(QueryTypeParametersDto queryTypeParameters)
        {
            FilterQueryDto filterQuery = new FilterQueryDto() { Name = queryTypeParameters.Text, Active = ActiveEnum.Active };
            return _modelsRepository.GetAllByFilters(filterQuery)
                .Select(x => new BaseNameIdDto<int>()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
        }
    }
}