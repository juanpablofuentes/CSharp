using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class ToolsRepository : BaseRepository<Tools>, IToolsRepository
    {
        public ToolsRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }
        public SaveResult<Tools> CreateTools(Tools tools)
        {
            tools.UpdateDate = DateTime.UtcNow;
            Create(tools);
            var result = SaveChange(tools);
            return result;
        }
        public SaveResult<Tools> UpdateTools(Tools tools)
        {
            tools.UpdateDate = DateTime.UtcNow;
            Update(tools);
            var result = SaveChange(tools);
            return result;
        }

        public SaveResult<Tools> DeleteTools(Tools entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            SaveResult<Tools> result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }
        public IQueryable<Tools> GetAll()
        {
            return Filter(x => !x.IsDeleted, GetIncludeVehicle());
        }

        public Tools GetById(int id)
        {
            return DbSet.Include(x => x.ToolsToolTypes)
                            .ThenInclude(x => x.ToolType)
                        .Include(x => x.Vehicle)
                        .SingleOrDefault(x => x.Id == id);
        }

        private List<Expression<Func<Tools, object>>> GetIncludeVehicle()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(Vehicles) });
        }

        private List<Expression<Func<Tools, object>>> GetIncludesPredicate(IList<Type> includeCollection)
        {
            var includesPredicate = new List<Expression<Func<Tools, object>>>();
            foreach (var element in includeCollection)
            {
                if (element == typeof(Vehicles))
                {
                    includesPredicate.Add(p => p.Vehicle);
                }
            }
            return includesPredicate;
        }

    }
}
