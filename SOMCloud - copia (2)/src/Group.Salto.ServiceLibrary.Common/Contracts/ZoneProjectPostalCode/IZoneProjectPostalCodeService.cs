using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.ZoneProjectPostalCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Contracts.ZoneProjectPostalCode
{
    public interface IZoneProjectPostalCodeService
    {
        ResultDto<IList<ZoneProjectPostalCodeDto>> GetAllPostalCodesByProjectId(int Projectid);
    }
}
