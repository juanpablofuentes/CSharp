using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Contracts.DamagedEquipment
{
    public interface IDamagedEquipmentService
    {
        IList<BaseNameIdDto<Guid>> GetAllKeyValues();
    }
}