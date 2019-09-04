using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class ItemsRepository : BaseRepository<Items>, IItemsRepository
    {
        public ItemsRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<Items> GetAll()
        {
            return All();
        }

        public SaveResult<Items> CreateItem(Items Item)
        {
            Item.UpdateDate = DateTime.UtcNow;
            Create(Item);
            var result = SaveChange(Item);
            return result;            
        }

        public SaveResult<Items> UpdateItem(Items Item)
        {
            Item.UpdateDate = DateTime.UtcNow;
            Update(Item);
            var result = SaveChange(Item);
            return result;            
        }

        public Items GetById(int Id)
        {
            return Filter(x=>x.Id == Id)
                .Include(x => x.ItemsPointsRate)
                    .ThenInclude(x => x.PointsRate)
                .Include(x => x.ItemsPurchaseRate)
                    .ThenInclude(x => x.PurchaseRate)
                .Include(x => x.ItemsSalesRate)
                    .ThenInclude(x => x.SalesRate)
                .Include(x => x.ItemsSerialNumber)
                    .ThenInclude(x => x.ItemsSerialNumberAttributeValues)
                .Include(x => x.SubFamilies)
                    .ThenInclude(x => x.Family)
                .SingleOrDefault();         
        }

        public Items GetByIdCanDelete(int Id) 
        {
            return Filter(p => p.Id == Id)
                    .Include(x => x.ItemsSerialNumber)
                    .Include(x => x.BillingRuleItem)
                    .Include(x => x.BillLine)
                    .Include(x => x.DnAndMaterialsAnalysis)
                    .Include(x => x.WarehouseMovements)
                    .SingleOrDefault();     
        }

        public Items GetByIdIncludeReferencesToDelete(int Id) 
        {
             return Filter(p => p.Id == Id)
                    .Include(x => x.ItemsPointsRate)
                    .Include(x => x.ItemsPurchaseRate)
                    .Include(x => x.ItemsSalesRate)
                    .SingleOrDefault();              
        }

        public SaveResult<Items> DeleteItem(Items Item) 
        {
            Item.UpdateDate = DateTime.UtcNow;
            Delete(Item);
            SaveResult<Items> result = SaveChange(Item);
            result.Entity = Item;
            return result;        
        }

        public Items GetByErpReference(string reference)
        {
            return Filter(i => i.ErpReference == reference)
                        .Include(i => i.ItemsSerialNumber)
                        .SingleOrDefault();
        }

        public IQueryable<Items> GetAllByFilters(FilterQueryDto filterQuery)
        {
            IQueryable<Items> query = All();
            query = FilterQuery(filterQuery, query);
            return query;
        }

        private IQueryable<Items> FilterQuery(FilterQueryDto filterQuery, IQueryable<Items> query)
        {
            if (!string.IsNullOrEmpty(filterQuery.Name))
            {
                query = query.Where(x => x.Name.Contains(filterQuery.Name));
            }
            return query;
        }
    }
}