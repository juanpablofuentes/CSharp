using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class PeopleCollectionRepository : BaseRepository<PeopleCollections>, IPeopleCollectionRepository
    {
        public PeopleCollectionRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<PeopleCollections> GetAll()
        {
            return Filter(x => !x.IsDeleted);
        }

        public PeopleCollections GetById(int id)
        {
            return All()
                    .Include(x => x.PeopleCollectionsPeople)
                        .ThenInclude(x => x.People)
                    .Include(x => x.PeopleCollectionsAdmins)
                        .ThenInclude(x => x.People)
                .SingleOrDefault(x => x.Id == id);
        }

        public SaveResult<PeopleCollections> UpdatePeopleCollection(PeopleCollections entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<PeopleCollections> CreatePeopleCollection(PeopleCollections entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<PeopleCollections> DeletePeopleCollection(PeopleCollections entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            var result = SaveChange(entity);
            return result;
        }

        public Dictionary<int, string> GetAllKeyValuesByPeopleId(int peopleId)
        {
            return Filter(x => !x.IsDeleted && x.PeopleCollectionsAdmins.Where(pc => pc.PeopleId == peopleId).Any())
                    .Include(x => x.PeopleCollectionsAdmins)
                        .ThenInclude(x => x.People)
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }
    }
}