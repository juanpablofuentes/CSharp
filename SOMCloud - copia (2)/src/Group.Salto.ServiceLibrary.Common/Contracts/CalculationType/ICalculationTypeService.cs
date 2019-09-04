using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Contracts.CalculationType
{
    public interface ICalculationTypeService
    {
        IList<BaseNameIdDto<Guid>> GetAllKeyValues();
    }
}