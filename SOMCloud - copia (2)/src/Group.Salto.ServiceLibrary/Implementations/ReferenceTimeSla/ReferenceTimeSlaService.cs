using Group.Salto.Common.Cache;
using Group.Salto.Common.Constants;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ReferenceTimeSla;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.ReferenceTimeSla;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.ServiceLibrary.Implementations.ReferenceTimeSla
{
    public class ReferenceTimeSlaService : BaseService, IReferenceTimeSlaService
    {
        private readonly IReferenceTimeSlaRepository _referenceTimeSlaRepository;
        
        public ReferenceTimeSlaService(ILoggingService logginingService, IReferenceTimeSlaRepository referenceTimeSlaRepository) : base(logginingService)
        {
            _referenceTimeSlaRepository = referenceTimeSlaRepository ?? throw new ArgumentNullException(nameof(IReferenceTimeSlaRepository));
            
        }

        public IList<BaseNameIdDto<Guid?>> GetAll()
        {
            return GetCacheData().Select(x => new BaseNameIdDto<Guid?>()
            {
                Name = x.Name,
                Id = x.Id,
            }).ToList();
        }

        private IList<ReferenceTimeSlaDto> GetCacheData()
        {
            LogginingService.LogInfo("Get Reference Times");
            var sla = _referenceTimeSlaRepository.GetAll().ToList().ToDto();   
            return sla;
        }
    }
}