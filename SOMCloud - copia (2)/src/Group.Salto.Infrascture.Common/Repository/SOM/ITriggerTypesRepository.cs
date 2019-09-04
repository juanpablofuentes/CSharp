using Group.Salto.Entities;
using System;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface ITriggerTypesRepository
    {
        IQueryable<TriggerTypes> GetAll();
        TriggerTypes GetById(Guid id);
        TriggerTypes GetByName(string name);
    }
}