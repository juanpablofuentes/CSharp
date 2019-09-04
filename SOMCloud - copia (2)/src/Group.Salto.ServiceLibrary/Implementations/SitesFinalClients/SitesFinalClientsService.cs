using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.SitesFinalClients;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.SitesFinalClients
{
    public class SitesFinalClientsService : BaseService, ISitesFinalClientsService
    {
        private readonly ISitesFinalClientsRepository _sitesFinalClientsRepository;

        public SitesFinalClientsService(ILoggingService logginingService,
            ISitesFinalClientsRepository sitesFinalClientsRepository) : base(logginingService)
        {
            _sitesFinalClientsRepository = sitesFinalClientsRepository ?? throw new ArgumentNullException($"{nameof(ISitesFinalClientsRepository)} is null");
        }
        
        public ResultDto<SitesFinalClientsDto> GetBySiteId(int id)
        {
            var entity = _sitesFinalClientsRepository.GetBySiteId(id);
            return ProcessResult(entity.ToDetailDto(), entity != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<SitesFinalClientsDto> Create(SitesFinalClientsDto model)
        {
            SaveResult<LocationsFinalClients> result = null;
            var entity = model.ToEntity();
            result = _sitesFinalClientsRepository.CreateSitesFinalClient(entity);
            return ProcessResult(result.Entity?.ToDetailDto(), result);
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValuesByFinalClientsIds(List<int> IdsToMatch)
        {
            LogginingService.LogInfo($"Get Locations Key Value");
            var data = _sitesFinalClientsRepository.GetAllWhereFinalClientId(IdsToMatch);

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Location.Id,
                Name = x.Location.Name,
            }).ToList(); ;
        }
    }
}