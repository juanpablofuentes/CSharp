using Group.Salto.Common.Entities.Contracts;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Rols
{
    public class RolDto : BaseComboDto, IKeyValue
    {
        public string Description { get; set; }

        public IList<MultiSelectItemDto> ActionsRoles { get; set; }
    }
}