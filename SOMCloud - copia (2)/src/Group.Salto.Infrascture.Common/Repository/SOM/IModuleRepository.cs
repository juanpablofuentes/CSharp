using Group.Salto.Common;
using Group.Salto.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface IModuleRepository : IRepository<Module>
    {
        IQueryable<Module> GetAllById(IList<Guid> modulesIds);
        IQueryable<Module> GetAll();
        SaveResult<Module> CreateModule(Module entity);
        Module GetByIdIncludeActionGroups(Guid id);
        SaveResult<Module> UpdateModule(Module entity);
        IQueryable<Module> GetListByIdIncludeActionGroups(IList<Guid> modulesIds);
    }
}