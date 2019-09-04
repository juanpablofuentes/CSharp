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

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class SitesRepository : BaseRepository<Locations>, ISitesRepository
    {
        public SitesRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<Locations> GetAll()
        {
            return All();
        }

        public IQueryable<Locations> GetAllByClientSite(int clientId)
        {
            return All()
                .Include(lfc => lfc.LocationsFinalClients)
                .Where(l => l.LocationsFinalClients.Any(lfc => lfc.FinalClientId == clientId));
        }

        public SaveResult<Locations> CreateSite(Locations entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<Locations> UpdateSite(Locations entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            return result;
        }

        public Locations GetById(int id)
        {
            return Find(x => x.Id == id);
        }

        public Locations GetByIdWithContacts(int id)
        {
            var query = Filter(x => x.Id == id)
                    .Include(x => x.ContactsLocationsFinalClients)
                        .ThenInclude(x => x.Contact);
            return query.FirstOrDefault();
        }

        public Locations GetByIdWithFinalClient(int id)
        {
            var query = Filter(x => x.Id == id)
                    .Include(x => x.LocationsFinalClients)
                        .ThenInclude(x => x.FinalClient);
            return query.FirstOrDefault();
        }

        public Locations GetByIdCanDelete(int id)
        {
            return Filter(x => x.Id == id)
                    .Include(x => x.AssetsLocationClient)
                    .Include(x => x.SiteUser)
                    .Include(x => x.LocationsFinalClients)
                    .Include(x => x.LocationCalendar)
                        .ThenInclude(p => p.Calendar)
                    .Include(x => x.ContactsLocationsFinalClients)
                        .ThenInclude(x => x.Contact)
                    .SingleOrDefault();
        }

        public IQueryable<Locations> GetAllByFilters(FilterAdditionalQueryDto filterQuery)
        {
            IQueryable<Locations> query = All().Include(x => x.LocationsFinalClients);
            query = FilterQuery(filterQuery, query);
            return query;
        }

        public bool ValidateCodeSite(Locations entity, int FinalClientId)
        {
            IQueryable<Locations> query = GetAllByClientSite(FinalClientId);
            return query.Any(x => x.Id != entity.Id && x.Code.Equals(entity.Code));
        }

        public IQueryable<Locations> FilterByClientSite(string text, int?[] selected)
        {
            IQueryable<Locations> query = Filter(x => x.Name.Contains(text));
            if (selected?.Length > 0)
            {
                query = query.Where(z => z
                             .LocationsFinalClients.Any(y =>
                              selected.Contains(y.FinalClientId)));
            }
            return query;
        }

        public List<Locations> GetSitesForAdvancedSearch(AdvancedSearchDto filterQuery)
        {
            IQueryable<Locations> query = All()
                                        .Include(x => x.LocationsFinalClients);

            if (!string.IsNullOrEmpty(filterQuery.Text))
            {
                int finalClientId = Convert.ToInt32(filterQuery.Text);
                query = query.Where(x => x.LocationsFinalClients.Any(l => l.FinalClientId == finalClientId));
            }

            if (!string.IsNullOrEmpty(filterQuery.Name))
            {
                switch ((SiteAdvancedSearchEnum)filterQuery.SelectType)
                {
                    case SiteAdvancedSearchEnum.Name:
                        query = query.Where(x => x.Name.Contains(filterQuery.Name));
                        break;
                    case SiteAdvancedSearchEnum.Code:
                        query = query.Where(x => x.Code.Contains(filterQuery.Name));
                        break;
                    case SiteAdvancedSearchEnum.Phone:
                        query = query.Where(x => x.Phone1.Contains(filterQuery.Name) || x.Phone2.Contains(filterQuery.Name) || x.Phone3.Contains(filterQuery.Name));
                        break;
                }
            }

            var data = query.ToList();
            return data;
        }

        public Locations DeleteOnContext(Locations entity)
        {
            Delete(entity);
            return entity;
        }

        public bool DeleteLocation(Locations entity)
        {
            Delete(entity);
            SaveResult<Locations> result = SaveChange(entity);
            result.Entity = entity;
            return result.IsOk;
        }

        private IQueryable<Locations> FilterQuery(FilterAdditionalQueryDto filterQuery, IQueryable<Locations> query)
        {
            if (filterQuery.Id != 0)
            {
                query = query.Where(x => x.LocationsFinalClients.Any(l => l.FinalClientId == filterQuery.Id));
            }

            if (!string.IsNullOrEmpty(filterQuery.Name))
            {
                query = query.Where(x => x.Name.Contains(filterQuery.Name));
            }

            return query;
        }
    }
}