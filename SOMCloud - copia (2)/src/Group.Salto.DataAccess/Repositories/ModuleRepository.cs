using DataAccess.Common;
using Group.Salto.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Group.Salto.DataAccess.Repositories
{
    public class ModuleRepository : BaseRepository<Module>, IModuleRepository
    {
        public ModuleRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<Module> GetAllById(IList<Guid> modulesIds)
        {
            return this.All().Where(x => modulesIds.Any(y => y == x.Id));
        }

        public IQueryable<Module> GetAll()
        {
            return All();
        }

        public Module GetByIdIncludeActionGroups(Guid id)
        {
            return Find(x => x.Id == id, GetIncludeActionGroups());
        }

        public IQueryable<Module> GetListByIdIncludeActionGroups(IList<Guid> modulesIds)
        {
            return All().Where(x => modulesIds.Any(y => y == x.Id)).Include(ag => ag.ModuleActionGroups);
        }

        public SaveResult<Module> CreateModule(Module entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public SaveResult<Module> UpdateModule(Module entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        private List<Expression<Func<Module, object>>> GetIncludeActionGroups()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(ActionGroups) });
        }

        private List<Expression<Func<Module, object>>> GetIncludesPredicate(IList<Type> includeCollection)
        {
            var includesPredicate = new List<Expression<Func<Module, object>>>();
            foreach (var element in includeCollection)
            {
                if (element == typeof(ActionGroups))
                {
                    includesPredicate.Add(m => m.ModuleActionGroups);
                }
            }
            return includesPredicate;
        }
    }
}