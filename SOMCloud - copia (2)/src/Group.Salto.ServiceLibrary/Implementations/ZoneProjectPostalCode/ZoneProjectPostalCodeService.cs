using Group.Salto.Common.Helpers;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ZoneProjectPostalCode;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.ZoneProjectPostalCode;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.ServiceLibrary.Implementations.ZoneProjectPostalCode
{
    public class ZoneProjectPostalCodeService : BaseService, IZoneProjectPostalCodeService
    {
        private readonly IZoneProjectPostalCodeRepository _zoneProjectPostalCodeRepository;
        public ZoneProjectPostalCodeService(ILoggingService logginingService, IZoneProjectPostalCodeRepository zoneProjectPostalCodeRepository) : base(logginingService)
        {
            _zoneProjectPostalCodeRepository = zoneProjectPostalCodeRepository ?? throw new ArgumentNullException($"{nameof(IZoneProjectPostalCodeRepository)} is null ");
        }

        public ResultDto<IList<ZoneProjectPostalCodeDto>> GetAllPostalCodesByProjectId(int Projectid)
        {
            LogginingService.LogInfo($"Get All  ZoneProject postals codes by id project");
            var query = _zoneProjectPostalCodeRepository.GetPostalcodesByProjectId(Projectid);
            var data = query.ToList().MapList(c => c.ToDto()).AsQueryable();
            return ProcessResult<IList<ZoneProjectPostalCodeDto>>(data.ToList());
        }
    }
}
