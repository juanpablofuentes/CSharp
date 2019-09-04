using DataAccess.Common;
using Group.Salto.Common;
using Group.Salto.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Repositories
{
    public class OriginsRepository : BaseRepository<Origins>, IOriginsRepository
    {
        public OriginsRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public SaveResult<Origins> CreateOrigin(Origins origins)
        {
            origins.UpdateDate = DateTime.UtcNow;
            Create(origins);
            var result = SaveChange(origins);
            return result;
        }

        public bool DeleteOrigin(Origins origins)
        {
            origins.UpdateDate = DateTime.UtcNow;
            Delete(origins);
            SaveResult<Origins> result = SaveChange(origins);
            result.Entity = origins;

            return result.IsOk;
        }

        public SaveResult<Origins> UpdateOrigin(Origins origins)
        {
            origins.UpdateDate = DateTime.UtcNow;
            Update(origins);
            var result = SaveChange(origins);
            return result;
        }

        public IQueryable<Origins> GetAllNotDeleted()
        {
            return Filter(x => !x.IsDeleted);
        }

        public Origins GetByIdNotDeleted(int id)
        {
            return Find(x => x.Id == id && !x.IsDeleted);
        }

        public Dictionary<int, string> GetAllOriginKeyValues()
        {
            return All()
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }
    }
}
