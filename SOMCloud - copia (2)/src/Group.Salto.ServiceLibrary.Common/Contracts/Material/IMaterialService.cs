using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Material
{
    public interface IMaterialService 
    {
        IEnumerable<FieldMaterialFormGetDto> GetMaterialsByPeopleConfigId(int peopleConfigId);
    }
}
