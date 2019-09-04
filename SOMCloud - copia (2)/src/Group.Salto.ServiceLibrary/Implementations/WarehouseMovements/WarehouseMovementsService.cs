using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.WarehouseMovements;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.WarehouseMovements;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.WarehouseMovements
{
    public class WarehouseMovementsService : BaseService, IWarehouseMovementsService
    {
        private readonly IWarehouseMovementsRepository _warehouseMovementsRepository;

        public WarehouseMovementsService(ILoggingService logginingService,            
             IWarehouseMovementsRepository warehouseMovementsRepository) : base(logginingService)
        {
            _warehouseMovementsRepository = warehouseMovementsRepository ?? throw new ArgumentNullException($"{nameof(IWarehouseMovementsRepository)} is null");
        }

        public ResultDto<WarehouseMovementsDto> CreateWarehouseMovement(WarehouseMovementsDto movement)
        {
            var entity = movement.ToEntity();
            var result = _warehouseMovementsRepository.CreateWarehouseMovements(entity);            
            return ProcessResult(result.Entity?.ToDto(), result);
        }

        public ResultDto<IList<WarehouseMovementsDto>> GetAllFiltered(WarehouseMovementsFilterDto filter)
        {
            var query = _warehouseMovementsRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Items, au => filter.Items.Contains(au.ItemsId));
            query = query.WhereIfNotDefault(filter.StartDate, au => au.UpdateDate >= filter.StartDate);       
            query = query.WhereIfNotDefault(filter.EndDate, au => au.UpdateDate < filter.EndDate);       
            query = OrderBy(query, filter);
            return ProcessResult(query.ToListDto());          
        }

        private IQueryable<Entities.Tenant.WarehouseMovements> OrderBy(IQueryable<Entities.Tenant.WarehouseMovements> data, WarehouseMovementsFilterDto filter)
        {
            var query = data.AsQueryable();
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.UpdateDate);
            return query;
        }
    }
}