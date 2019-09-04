using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class PostconditionsRepository : BaseRepository<Postconditions>, IPostconditionsRepository
    {
        public PostconditionsRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public SaveResult<Postconditions> UpdatePostconditions(Postconditions entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<Postconditions> CreatePostconditions(Postconditions entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            return result;
        }

        public Postconditions GetById(int id)
        {
            return Find(x => x.Id == id);
        }

        public IQueryable<Postconditions> GetByPostconditionCollectionId(int id)
        {
            return Filter(x => x.PostconditionCollectionsId == id);
        }

        public SaveResult<Postconditions> DeletePostconditions(Postconditions entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public Postconditions DeletePostconditionsOnContext(Postconditions entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            return entity;
        }

        public SaveResult<IEnumerable<Postconditions>> DeleteRangePostconditions(IEnumerable<Postconditions> entities)
        {
            SaveResult<IEnumerable<Postconditions>> result = new SaveResult<IEnumerable<Postconditions>>(entities)
            {
                IsOk = DeleteRange(entities)
            };
            if (!result.IsOk)
            {
                result.Error = new SaveError
                {
                    ErrorMessage = "Not change has persisted.",
                    ErrorType = ErrorType.SaveChangesException,
                };
            }
            return result;
        }
    }
}