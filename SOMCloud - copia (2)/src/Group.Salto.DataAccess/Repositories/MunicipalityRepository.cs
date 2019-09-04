using DataAccess.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Group.Salto.DataAccess.Repositories
{
    public class MunicipalityRepository : BaseRepository<Municipalities>, IMunicipalityRepository
    {
        public MunicipalityRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public Municipalities GetById(int id)
        {
            return Find(x => x.Id == id);
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return All()
                .Select(s => new { s.Id, s.Name })
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }

        public Municipalities GetByIdWithStatesRegionsCountriesIncludes(int id)
        {
            var municipalities = this.Find(m => m.Id == id, GetIncludeStatesRegionsAndCountries());
            return municipalities;
        }

        private List<Expression<Func<Municipalities, object>>> GetIncludeStatesRegionsAndCountries()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(States), typeof(Regions), typeof(Countries) });
        }

        private List<Expression<Func<Municipalities, object>>> GetIncludesPredicate(IList<Type> includeCollection)
        {
            var includesPredicate = new List<Expression<Func<Municipalities, object>>>();
            foreach (var element in includeCollection)
            {
                if (element == typeof(States)) includesPredicate.Add(p => p.State);
                if (element == typeof(Regions)) includesPredicate.Add(p => p.State.Region);
                if (element == typeof(Countries)) includesPredicate.Add(p => p.State.Region.Country);
            }
            return includesPredicate;
        }
    }
}