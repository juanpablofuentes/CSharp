using Group.Salto.ServiceLibrary.Common.Contracts.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Contracts.Zones;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.Zones
{
    public class ZoneLiteralQueryResult : IZoneLiteralQueryResult
    {
        private IZonesService _zonesService;

        public ZoneLiteralQueryResult(IZonesService zonesService)
        {
            _zonesService = zonesService ?? throw new ArgumentNullException($"{nameof(IZonesService)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues(IFilterQueryParameters filterQueryParameters)
        {
            return _zonesService.GetAllkeyValues();
        }
    }
}