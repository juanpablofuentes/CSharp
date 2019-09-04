using Group.Salto.Entities;
using System;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface IPreconditionTypesRepository
    {
        IQueryable<PreconditionTypes> GetAll();
        PreconditionTypes GetById(Guid id);
        PreconditionTypes GetByName(string name);
    }
}