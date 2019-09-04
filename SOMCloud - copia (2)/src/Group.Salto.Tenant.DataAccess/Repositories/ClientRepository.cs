using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class ClientRepository : BaseRepository<Clients>, IClientRepository
    {
        public ClientRepository(ITenantUnitOfWork uow) : base(uow){ }

        public Clients GetById(int id)
        {
            return Find(c => c.Id == id);
        }

        public IQueryable<Clients> GetAll()
        {
            return Filter(x => !x.IsDeleted);
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return Filter(x => !x.IsDeleted)
                   .OrderBy(o => o.CorporateName)
                   .ToDictionary(t => t.Id, t => t.CorporateName);
        }

        public SaveResult<Clients> CreateClients(Clients client)
        {
            client.UpdateDate = DateTime.UtcNow;
            Create(client);
            var result = SaveChange(client);
            return result;
        }

        public SaveResult<Clients> UpdateClients(Clients client)
        {
            client.UpdateDate = DateTime.UtcNow;
            Update(client);
            var result = SaveChange(client);
            return result;
        }

        public IQueryable<Clients> GetByIds(IList<int> ids)
        {
            return Filter(x => !x.IsDeleted && ids.Contains(x.Id));
        }

        public SaveResult<Clients> DeleteClients(Clients client)
        {
            client.UpdateDate = DateTime.UtcNow;
            Delete(client);
            SaveResult<Clients> result = SaveChange(client);
            result.Entity = client;
            return result;
        }

        public IQueryable<Clients> GetAllByFiltersWithPermisions(PermisionsFilterQueryDto filterQuery)
        {
            IQueryable<Clients> query = Filter(x => !x.IsDeleted);
            query = FilterQuery(query, filterQuery);
            return query;
        }

        private IQueryable<Clients> FilterQuery(IQueryable<Clients> query, PermisionsFilterQueryDto filterQuery)
        {
            if (!string.IsNullOrEmpty(filterQuery.Name))
            {
                query = query.Where(x => x.CorporateName.Contains(filterQuery.Name));
            }
            return query;
        }
    }
}