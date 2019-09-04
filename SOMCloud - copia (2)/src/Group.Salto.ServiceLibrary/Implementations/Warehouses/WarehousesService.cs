using Group.Salto.Common;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Warehouses;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Warehouses;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Warehouses
{
    public class WarehousesService : BaseService, IWarehousesService
    {
        private readonly IWarehousesRepository _warehousesRepository;

        public WarehousesService(ILoggingService logginingService,            
             IWarehousesRepository warehousesRepository) : base(logginingService)
        {
            _warehousesRepository = warehousesRepository ?? throw new ArgumentNullException($"{nameof(IWarehousesRepository)} is null");
        }

        public ResultDto<WarehousesBaseDto> CreateWarehouse(WarehousesBaseDto source)
        {
            var warehouse = source.ToEntity();
            var result = _warehousesRepository.CreateWarehouse(warehouse);            
            return ProcessResult(result.Entity?.ToBaseDto(), result);
        }

        public ResultDto<IList<WarehousesBaseDto>> GetAllFiltered(WarehousesFilterDto filter)
        {
            var query = _warehousesRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.ErpReference, au => au.ErpReference.Contains(filter.ErpReference));       
            query = OrderBy(query, filter);
            return ProcessResult(query.ToListDto());            
        }

        public ResultDto<WarehousesBaseDto> GetById(int id)
        {
            var data = _warehousesRepository.GetById(id);
            return ProcessResult(data.ToBaseDto());
        }

        public ResultDto<WarehousesBaseDto> UpdateWarehouse(WarehousesBaseDto source) 
        {
            ResultDto<WarehousesBaseDto> result = null;     
            var entity = _warehousesRepository.GetById(source.Id);
            if (entity != null)
            {
                entity.Update(source);
                var resultRepository = _warehousesRepository.UpdateWarehouse(entity);
                result = ProcessResult(resultRepository.Entity?.ToBaseDto(), resultRepository);
            }
            return result ?? new ResultDto<WarehousesBaseDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = source,
            };        
        }

        public ResultDto<bool> DeleteWarehouse(int id) 
        {
            ResultDto<bool> result = null;
            var warehouse = _warehousesRepository.GetByIdIncludeReferencesToDelete(id);
            if (warehouse != null)
            {
                var resultSave = _warehousesRepository.DeleteWarehouse(warehouse);
                result = ProcessResult(resultSave.IsOk, resultSave);
            }
            return result ?? new ResultDto<bool>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = false,
            };        
        }

        public ResultDto<ErrorDto> CanDelete(int id) 
        {
            var warehouse = _warehousesRepository.GetByIdCanDelete(id);
            
            ErrorDto result = new ErrorDto() { ErrorMessageKey = string.Empty };

            if (warehouse.People?.Any() == true)
            {
                result.ErrorMessageKey = "WarehousesErrorHavePeople";
            }
            else if (warehouse.WarehouseMovementEndpoints?.Any(x => x.WarehouseMovementsFrom.Any() ||
                    x.WarehouseMovementsTo.Any()) == true)
            {
                result.ErrorMessageKey = "WarehousesErrorHaveWarehouseMovement";
            }

            return ProcessResult(result);        
        }

        private IQueryable<Entities.Tenant.Warehouses> OrderBy(IQueryable<Entities.Tenant.Warehouses> data, WarehousesFilterDto filter)
        {
            var query = data.AsQueryable();
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            return query;
        }
    }
}