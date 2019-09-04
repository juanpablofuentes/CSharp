using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.ZoneProjectPostalCode;
using Group.Salto.ServiceLibrary.Common.Dtos.Zones;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Zones
{
    public interface IZonesService
    {
        ResultDto<IList<ZonesDto>> GetAllFiltered(ZonesFilterDto filter);
        ResultDto<ZonesDto> CreateZones(ZonesDto source, IList<ZoneProjectPostalCodeDto> SelectedPostalCodes);
        ResultDto<ZonesDto> UpdateZones(ZonesDto source, IList<ZoneProjectPostalCodeDto> SelectedPostalCodes);
        ResultDto<bool> DeleteZone(int id);
        ResultDto<ZonesDto> GetByIdWithZoneProjects(int id);
        IList<BaseNameIdDto<int>> GetAllkeyValues();
        ResultDto<List<MultiSelectItemDto>> GetZoneMultiSelect(List<int> selectItems);
        string GetNamesComaSeparated(List<int> ids);
    }
}