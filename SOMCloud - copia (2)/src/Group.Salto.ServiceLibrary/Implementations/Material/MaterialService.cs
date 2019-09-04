using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.ErpSystemInstanceQuery;
using Group.Salto.ServiceLibrary.Common.Contracts.Material;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Material
{
    public class MaterialService : IMaterialService
    {
        private readonly IErpSystemInstanceQueryService _erpSystemInstanceQueryService;
        private readonly IPeopleRepository _peopleRepository;

        public MaterialService(IErpSystemInstanceQueryService erpSystemInstanceQueryService,
                               IPeopleRepository peopleRepository)
        {
            _erpSystemInstanceQueryService = erpSystemInstanceQueryService;
            _peopleRepository = peopleRepository;
        }

        public IEnumerable<FieldMaterialFormGetDto> GetMaterialsByPeopleConfigId(int peopleConfigId)
        {
            IEnumerable<FieldMaterialFormGetDto> result = new List<FieldMaterialFormGetDto>();
            var currentPeople = _peopleRepository.GetByConfigId(peopleConfigId);
            if (currentPeople != null)
            {
                result = _erpSystemInstanceQueryService.GetMaterialFormItemsFromPeople(currentPeople.Id);
            }

            return result;
        }
    }
}
