﻿using Group.Salto.ServiceLibrary.Common.Contracts.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.People
{
    public class PeopleTechnicianTriggerQueryResult : IPeopleTechnicianTriggerQueryResult
    {
        private IPeopleService _peopleService;

        public PeopleTechnicianTriggerQueryResult(IPeopleService peopleService)
        {
            _peopleService = peopleService ?? throw new ArgumentNullException($"{nameof(IPeopleService)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues(IFilterQueryParameters filterQueryParameters)
        {
            PeopleFilterDto filter = new PeopleFilterDto()
            {
                IsVisible = true
            };
            return _peopleService.GetPeopleTechniciansKeyValues(filter);
        }
    }
}