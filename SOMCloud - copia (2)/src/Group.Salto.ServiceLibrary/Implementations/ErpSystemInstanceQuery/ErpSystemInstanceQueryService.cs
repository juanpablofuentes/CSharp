using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ErpSystemInstanceQuery;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common.Constants.ErpSystemInstanceQuery;
using Group.Salto.Common.Constants.TenantConfiguration;
using Group.Salto.Entities.Tenant.QueryEntities;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;
using Group.Salto.ServiceLibrary.Extensions;

namespace Group.Salto.ServiceLibrary.Implementations.ErpSystemInstanceQuery
{
    public class ErpSystemInstanceQueryService : BaseService, IErpSystemInstanceQueryService
    {
        private readonly IErpSystemInstanceQueryRepository _erpSystemInstanceQueryRepository;
        private readonly ITenantConfigurationRepository _tenantConfigurationRepository;

        public ErpSystemInstanceQueryService(ILoggingService logginingService,
                                             IErpSystemInstanceQueryRepository erpSystemInstanceQueryRepository,
                                             ITenantConfigurationRepository tenantConfigurationRepository) : base(logginingService)
        {
            _erpSystemInstanceQueryRepository = erpSystemInstanceQueryRepository ?? throw new ArgumentNullException(nameof(IErpSystemInstanceQueryRepository));
            _tenantConfigurationRepository = tenantConfigurationRepository;
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get ErpSystemInstanceQuery Key Value");
            var data = _erpSystemInstanceQueryRepository.GetAllKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }

        public decimal? GetErpPriceFromItem(int billLineItemId, string erpNameKey)
        {
            decimal? result = null;

            try
            {
                var currentErpConfig = _tenantConfigurationRepository.GetByKey(TenantConfigurationConstants.CurrentErpSystemInstanceId);
                if (int.TryParse(currentErpConfig.Value, out int erpId))
                {
                    Entities.Tenant.ErpSystemInstanceQuery currentErpSystem = _erpSystemInstanceQueryRepository.GetByIdAndName(erpId, erpNameKey);
                    result = _erpSystemInstanceQueryRepository.GetItemPriceFromQuery(currentErpSystem.SqlQuery, billLineItemId);
                }
            }
            catch (Exception e)
            {
                //Don't throw error
                LogginingService.LogException(e);
            }

            return result;
        }

        public IEnumerable<FieldMaterialFormGetDto> GetMaterialFormItemsFromPeople(int peopleId)
        {
            var result = new List<FieldMaterialFormGetDto>();

            try
            {
                var currentErpConfig = _tenantConfigurationRepository.GetByKey(TenantConfigurationConstants.CurrentErpSystemInstanceId);
                if (int.TryParse(currentErpConfig.Value, out int erpId))
                {
                    var currentErpSystem = _erpSystemInstanceQueryRepository.GetByIdAndName(erpId, ErpSystemInstanceQueryConstants.StockTechnician);
                    var formResult = _erpSystemInstanceQueryRepository.GetMaterialFormItemsFromPeople(currentErpSystem.SqlQuery, peopleId);
                    result = formResult.ToDto().ToList();
                }
            }
            catch (Exception e)
            {
                //Don't throw error
                LogginingService.LogException(e);
            }

            return result;
        }
    }
}