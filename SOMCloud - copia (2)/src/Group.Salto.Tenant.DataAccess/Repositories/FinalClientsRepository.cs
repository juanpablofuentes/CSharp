using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Common.Enums;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class FinalClientsRepository : BaseRepository<FinalClients>, IFinalClientsRepository
    {
        public FinalClientsRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<FinalClients> GetAll()
        {
            return All();
        }        

        public FinalClients GetById(int id)
        {
            var query = Filter(c => c.Id == id)
                .Include(c => c.ContactsFinalClients)
                .ThenInclude(d => d.Contact);
            return query.SingleOrDefault();
        }

        public SaveResult<FinalClients> CreateFinalClients(FinalClients finalClients)
        {
            finalClients.UpdateDate = DateTime.UtcNow;
            Create(finalClients);
            var result = SaveChange(finalClients);
            return result;
        }

        public SaveResult<FinalClients> UpdateFinalClients(FinalClients finalClients)
        {
            finalClients.UpdateDate = DateTime.UtcNow;
            Update(finalClients);
            var result = SaveChange(finalClients);
            return result;
        }

        public bool DeleteFinalClients(FinalClients finalClients)
        {
            Delete(finalClients);
            SaveResult<FinalClients> result = SaveChange(finalClients);
            result.Entity = finalClients;
            return result.IsOk;
        }

        public IQueryable<FinalClients> GetAllByFilters(FilterAdditionalQueryDto filterQuery)
        {
            IQueryable<FinalClients> query = All();
            query = FilterQuery(filterQuery, query);
            return query;
        }       

        public bool CheckUniqueCode(string code)
        {
            return Filter(p => p.Code == code).Count() > 0;
        }

        public FinalClients GetByIdIncludeReferencesToDelete(int id)
        {
            return Filter(p => p.Id == id)
                .Include(p => p.LocationsFinalClients)
                    .ThenInclude(p => p.Location)
                        .ThenInclude(p=>p.SiteUser)
                .Include(p=>p.LocationsFinalClients)
                    .ThenInclude(p=>p.Location)
                        .ThenInclude(p=>p.ContactsLocationsFinalClients)                
                .Include(p => p.ContactsFinalClients)
                .Include(p => p.FinalClientSiteCalendar)
                    .ThenInclude(p => p.Calendar)
                .SingleOrDefault();
        }

        public FinalClients GetByIdCanDelete(int id)
        {
            return Filter(p => p.Id == id)
                .Include(p => p.WorkOrders)
                .Include(p => p.WorkOrdersDeritative)
                .Include(p => p.PreconditionsLiteralValues)
                .Include(p => p.LocationsFinalClients)
                    .ThenInclude( p => p.Location)
                        .ThenInclude(p=>p.AssetsLocationClient)
                 .Include(p => p.LocationsFinalClients)
                    .ThenInclude(p => p.Location)
                        .ThenInclude(p=>p.SiteUser)
                .Include(p => p.LocationsFinalClients)
                .SingleOrDefault();
        }

        public List<FinalClients> GetFinalClientsForAdvancedSearch(AdvancedSearchDto filterQuery)
        {
            IQueryable<FinalClients> query = All()
                .Include(c => c.PeopleCommercial)
                .Include(c => c.LocationsFinalClients)
                .Include(c => c.ContactsFinalClients)
                    .ThenInclude(d => d.Contact);

            if (!string.IsNullOrEmpty(filterQuery.Text))
            {
                int locationId = Convert.ToInt32(filterQuery.Text);
                query = query.Where(x => x.LocationsFinalClients.Any(l => l.LocationId == locationId));
            }

            if (!string.IsNullOrEmpty(filterQuery.Name))
            {
                switch((ClientSiteAdvancedSearchEnum) filterQuery.SelectType)
                {
                    case ClientSiteAdvancedSearchEnum.Code:
                        query = query.Where(x => x.Code.Contains(filterQuery.Name));
                        break;
                    case ClientSiteAdvancedSearchEnum.Name:
                        query = query.Where(x => x.Name.Contains(filterQuery.Name));
                        break;
                    case ClientSiteAdvancedSearchEnum.Phone:
                        query = query.Where(x => x.Phone1.Contains(filterQuery.Name) || x.Phone2.Contains(filterQuery.Name) || x.Phone3.Contains(filterQuery.Name));
                        break;
                    case ClientSiteAdvancedSearchEnum.Fax:
                        query = query.Where(x => x.Fax.Contains(filterQuery.Name));
                        break;
                    case ClientSiteAdvancedSearchEnum.Nif:
                        query = query.Where(x => x.Nif.Contains(filterQuery.Name));
                        break;
                    case ClientSiteAdvancedSearchEnum.ComercialName:
                        query = query.Where(x => x.PeopleCommercial.FullName.Contains(filterQuery.Name));
                        break;
                    case ClientSiteAdvancedSearchEnum.ComercialDNI:
                        query = query.Where(x => x.PeopleCommercial.Dni.Contains(filterQuery.Name));
                        break;
                    case ClientSiteAdvancedSearchEnum.ContactName:
                        query = query.Where(x => x.PeopleCommercial.Dni.Contains(filterQuery.Name));
                        break;
                    case ClientSiteAdvancedSearchEnum.ContactPhone:
                        query = query.Where(x => x.PeopleCommercial.Dni.Contains(filterQuery.Name));
                        break;
                    case ClientSiteAdvancedSearchEnum.ContactEmail:
                        query = query.Where(x => x.PeopleCommercial.Dni.Contains(filterQuery.Name));
                        break;
                }
            }

            var data = query.ToList();
            return data;
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return All()
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }

        public IQueryable<FinalClients> GetByIds(IList<int> ids)
        {
            return Filter(x => ids.Contains(x.Id));
        }

        private List<Expression<Func<FinalClients, object>>> GetIncludesPredicate(IList<Type> includeCollection)
        {
            var includesPredicate = new List<Expression<Func<FinalClients, object>>>();
            foreach (var element in includeCollection)
            {
                if (element == typeof(LocationsFinalClients))
                {
                    includesPredicate.Add(p => p.LocationsFinalClients);
                }
            }
            return includesPredicate;
        }

        private List<Expression<Func<FinalClients, object>>> GetIncludeDeleteRefenreces()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(LocationsFinalClients) });
        }
        
        private IQueryable<FinalClients> FilterQuery(FilterAdditionalQueryDto filterQuery, IQueryable<FinalClients> query)
        {
            if (!string.IsNullOrEmpty(filterQuery.Name))
            {
                query = query.Where(x => x.Name.Contains(filterQuery.Name));
            }

            if (filterQuery.Id != 0)
            {
                query = query.Where(x => x.LocationsFinalClients.Any(l => l.LocationId == filterQuery.Id));
            }

            return query;
        }
    }
}