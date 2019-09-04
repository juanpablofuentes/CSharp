using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Maturity
{
    public interface IMaturityService
    {
        IList<BaseNameIdDto<int>> GetBaseMaturities();
    }
}