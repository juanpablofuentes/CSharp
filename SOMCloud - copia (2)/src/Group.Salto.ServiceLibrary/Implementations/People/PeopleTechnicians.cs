using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.People
{
    public class PeopleTechnicians: IPeopleTechnicians
    {
        private readonly IPeopleRepository _peopleRepository;

        public PeopleTechnicians(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository ?? throw new ArgumentNullException($"{nameof(IPeopleRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetFiltered(QueryTypeParametersDto queryTypeParameters)
        {
            var people = _peopleRepository.GetPeopleByPermissionKeyValues(Salto.Common.Enums.PermissionTypeEnum.Technical, new Infrastructure.Common.Dto.PeopleFilterDto() { Name = queryTypeParameters.Text })
                .Select(x => new BaseNameIdDto<int>()
                {
                    Id = x.Key,
                    Name = x.Value
                }).ToList();

            return people;
        }
    }
}