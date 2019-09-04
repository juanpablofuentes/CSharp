using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Items;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Items;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Items
{
    public class ItemsService: BaseFilterService, IItemsService
    {
        private readonly IItemsRepository _itemsRepository;
        private readonly IItemTypesRepository _itemTypesRepository;
        private readonly IPointsRateRepository _pointsRateRepository;
        private readonly IPurchaseRateRepository _purchaseRateRepository;
        private readonly ISalesRateRepository _salesRateRepository;

         public ItemsService(ILoggingService logginingService,
             IItemTypesRepository itemTypesRepository,
             IPointsRateRepository pointsRateRepository,
             IPurchaseRateRepository purchaseRateRepository,
             ISalesRateRepository salesRateRepository,
             IItemsRepository itemsRepository,
             IItemsQueryFactory queryFactory) : base(queryFactory, logginingService)
         {
            _itemsRepository = itemsRepository ?? throw new ArgumentNullException($"{nameof(IItemsRepository)} is null");
            _itemTypesRepository = itemTypesRepository ?? throw new ArgumentNullException($"{nameof(IItemTypesRepository)} is null");
            _pointsRateRepository = pointsRateRepository ?? throw new ArgumentNullException($"{nameof(IPointsRateRepository)} is null");
            _purchaseRateRepository = purchaseRateRepository ?? throw new ArgumentNullException($"{nameof(IPurchaseRateRepository)} is null");
            _salesRateRepository = salesRateRepository ?? throw new ArgumentNullException($"{nameof(ISalesRateRepository)} is null");
         }

        public ResultDto<ItemsDetailsDto> CreateItem(ItemsDetailsDto source)
        {
            var newItem = source.ToEntity();
            var result = _itemsRepository.CreateItem(newItem);            
            return ProcessResult(result.Entity?.ToDto(), result);
        }

        public ResultDto<ItemsDetailsDto> UpdateItem(ItemsDetailsDto source)
        {
            ResultDto<ItemsDetailsDto> result = null;     
            var entity = _itemsRepository.GetById(source.Id);
            if (entity != null)
            {
                entity.Update(source);
                entity = AssignItemsSerialNumbers(entity, source.ItemsSerialNumbers);

                IEnumerable<RateDto> updatedPointsRates = source.ItemPointsRates;
                entity = AssignPointsRates(entity, updatedPointsRates);
                IEnumerable<RateDto> availablePointsRates = source.AvailablePointsRates;
                entity = AssignPointsRates(entity, availablePointsRates);                   

                IEnumerable<RateDto> updatedPurchasesRates = source.ItemPurchaseRates;
                entity = AssignPurchaseRates(entity, updatedPurchasesRates);
                IEnumerable<RateDto> availablePurchasesRates = source.AvailablePurchaseRates;
                entity = AssignPurchaseRates(entity, availablePurchasesRates);

                IEnumerable<RateDto> updatedSalesRates = source.ItemPurchaseRates;
                entity = AssignSalesRates(entity, updatedSalesRates);
                IEnumerable<RateDto> availableSalesRates = source.AvailableSalesRates;
                entity = AssignSalesRates(entity, availableSalesRates);

                var resultRepository = _itemsRepository.UpdateItem(entity);
                result = ProcessResult(resultRepository.Entity?.ToDto(), resultRepository);
            }
            return result ?? new ResultDto<ItemsDetailsDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = source,
            };
        }

        public ResultDto<ItemsDetailsDto> GetById(int id)
        {
            var data = _itemsRepository.GetById(id);
            var result = ProcessResult(data.ToDto());
            var allPointsRates = _pointsRateRepository.GetAll();
            result.Data.AvailablePointsRates = GetAvailablePointsRates(data, allPointsRates);

            var allPurchaseRates = _purchaseRateRepository.GetAll();
            result.Data.AvailablePurchaseRates = GetAvailablePurchaseRates(data, allPurchaseRates);
            
            var allSalesRates = _salesRateRepository.GetAllNotDeleted();
            result.Data.AvailableSalesRates = GetAvailableSalesRates(data, allSalesRates);
            return result;
        }

        public ResultDto<IList<ItemsListDto>> GetAllFiltered(ItemsFilterDto filter)
        {
            var query = _itemsRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.ERPReference, au => au.ErpReference.Contains(filter.ERPReference));
            query = query.WhereIfNotDefault(filter.InternalReference, au => au.InternalReference.Contains(filter.InternalReference));
            query = query.WhereIfNotDefault(filter.Blocked, au => au.IsBlocked == filter.Blocked);            
            query = OrderBy(query, filter);
            var types = _itemTypesRepository.GetAll()?.ToList();
            return ProcessResult(query.ToListDto(types));
        }

        public ResultDto<bool> DeleteItem(int id)
        {
            ResultDto<bool> result = null;
            var item = _itemsRepository.GetByIdIncludeReferencesToDelete(id);
            if (item != null)
            {
                item.ItemsPointsRate?.Clear();
                item.ItemsSalesRate?.Clear();
                item.ItemsPurchaseRate?.Clear();
                var resultSave = _itemsRepository.DeleteItem(item);
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
            var item = _itemsRepository.GetByIdCanDelete(id);
            
            ErrorDto result = new ErrorDto() { ErrorMessageKey = string.Empty };

            if (item.ItemsSerialNumber?.Any() == true)
            {
                result.ErrorMessageKey = "ItemsErrorHaveSerialNumbers";
            }
            else if (item.BillingRuleItem?.Any() == true)
            {
                result.ErrorMessageKey = "ItemsErrorHaveBillingRuleItem";
            }
            else if (item.BillLine?.Any() == true)
            {
                result.ErrorMessageKey = "ItemsErrorHaveBill";
            }
            else if (item.WarehouseMovements?.Any() == true)
            {
                result.ErrorMessageKey = "ItemsErrorWarehouseMovements";
            }
            else if (item.DnAndMaterialsAnalysis?.Any() == true)
            {
                result.ErrorMessageKey = "ItemsErrorMaterialsAnalysis";
            }

            return ProcessResult(result);
        }

        private Entities.Tenant.Items AssignPointsRates(Entities.Tenant.Items item,IEnumerable<RateDto> pointsRates)
        {
            if (pointsRates != null)
            {
                item.ItemsPointsRate = item.ItemsPointsRate ?? new List<ItemsPointsRate>();
                foreach (var rate in pointsRates)
                {
                    if (rate != null && rate.Value.HasValue)
                    {
                        var currentPointsRate = item.ItemsPointsRate.Where(x => x.PointsRateId == rate.Id).SingleOrDefault();
                        if (currentPointsRate != null)
                        {
                            currentPointsRate.Points = rate.Value.Value;
                        }
                        else
                        {
                            item.ItemsPointsRate.Add(new ItemsPointsRate
                            {
                                Item = item,
                                PointsRateId = rate.Id,
                                Points = rate.Value.Value
                            });
                        }
                    }
                }
            }
            return item;
        }

        private Entities.Tenant.Items AssignSalesRates(Entities.Tenant.Items item,IEnumerable<RateDto> salesRates)
        {
            if (salesRates != null)
            {
                item.ItemsSalesRate = item.ItemsSalesRate ?? new List<ItemsSalesRate>();
                foreach (var rate in salesRates)
                {
                    if (rate != null && rate.Value.HasValue)
                    {
                        var currentSalesRate = item.ItemsSalesRate.Where(x => x.SalesRateId == rate.Id).SingleOrDefault();
                        if (currentSalesRate != null)
                        {
                            currentSalesRate.Price = rate.Value.Value;
                        }
                        else
                        {
                            item.ItemsSalesRate.Add(new ItemsSalesRate
                            {
                                ItemId = item.Id,
                                SalesRateId = rate.Id,
                                Price = rate.Value.Value
                            });
                        }
                    }
                }
            }
            return item;
        }

        private Entities.Tenant.Items AssignPurchaseRates(Entities.Tenant.Items item,IEnumerable<RateDto> purchaseRates)
        {
            if (purchaseRates != null)
            {
                item.ItemsPurchaseRate = item.ItemsPurchaseRate ?? new List<ItemsPurchaseRate>();
                foreach (var rate in purchaseRates)
                {
                    if (rate != null && rate.Value.HasValue)
                    {
                        var currentPurchaseRate = item.ItemsPurchaseRate.Where(x => x.PurchaseRateId == rate.Id).SingleOrDefault();
                        if (currentPurchaseRate != null)
                        {
                            currentPurchaseRate.Price = rate.Value.Value;
                        }
                        else
                        {
                            item.ItemsPurchaseRate.Add(new ItemsPurchaseRate
                            {
                                ItemId = item.Id,
                                PurchaseRateId = rate.Id,
                                Price = rate.Value.Value
                            });
                        }
                    }
                }
            }
            return item;
        }

        private Entities.Tenant.Items AssignItemsSerialNumbers(Entities.Tenant.Items item,IList<ItemsSerialNumberDto> itemSerialNumbers)
        {
            if (itemSerialNumbers != null)
            {
                item.ItemsSerialNumber?.Clear();
                item.ItemsSerialNumber = item.ItemsSerialNumber ?? new List<ItemsSerialNumber>();
                foreach (var serialNumber in itemSerialNumbers)
                {
                    if (serialNumber != null)
                    {
                        item.ItemsSerialNumber.Add(new ItemsSerialNumber
                        {
                            ItemId = serialNumber.Id,
                            SerialNumber = serialNumber.SerialNumber,
                            Observations = serialNumber.Observations,
                            ItemsSerialNumberStatusesId = serialNumber.SerialNumberStatusId
                        });
                    }
                }
            }
            return item;
        }
        
        private IList<RateDto> GetAvailablePointsRates(Entities.Tenant.Items item,IQueryable<PointsRate> rates) 
        {
            IList<RateDto> result = null;
            var availableRates = rates?.Where(x => !item.ItemsPointsRate.Any(y => y.PointsRateId == x.Id));
            if (availableRates?.Count() > 0)
            {
                result = new List<RateDto>();
                foreach (var rate in availableRates)
                {
                    result.Add(new RateDto
                    {
                        Id = rate.Id,
                        Name = rate.Name
                    });
                }
            }
            return result;
        }

        private IList<RateDto> GetAvailablePurchaseRates(Entities.Tenant.Items item,IQueryable<Entities.Tenant.PurchaseRate> rates) 
        {
            IList<RateDto> result = null;
            var availableRates = rates?.Where(x => !item.ItemsPurchaseRate.Any(y => y.PurchaseRateId == x.Id));
            if (availableRates?.Count() > 0)
            {
                result = new List<RateDto>();
                foreach (var rate in availableRates)
                {
                    result.Add(new RateDto
                    {
                        Id = rate.Id,
                        Name = rate.Name
                    });
                }
            }
            return result;
        }

        private IList<RateDto> GetAvailableSalesRates(Entities.Tenant.Items item,IQueryable<Entities.Tenant.SalesRate> rates) 
        {
            IList<RateDto> result = null;
            var availableRates = rates?.Where(x => !item.ItemsSalesRate.Any(y => y.SalesRateId == x.Id));
            if (availableRates?.Count() > 0)
            {
                result = new List<RateDto>();
                foreach (var rate in availableRates)
                {
                    result.Add(new RateDto
                    {
                        Id = rate.Id,
                        Name = rate.Name
                    });
                }
            }
            return result;
        }

        private IQueryable<Entities.Tenant.Items> OrderBy(IQueryable<Entities.Tenant.Items> data, ItemsFilterDto filter)
        {
            var query = data.AsQueryable();
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            return query;
        }
    }
}