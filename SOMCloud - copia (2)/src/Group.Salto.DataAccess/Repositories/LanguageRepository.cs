using System.Collections.Generic;
using DataAccess.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;

namespace Group.Salto.DataAccess.Repositories
{
    public class LanguageRepository : BaseRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public IEnumerable<Language> GetAll()
        {
            return All();
        }

        public Language GetByCulture(string culture)
        {
            return Find(x => x.CultureCode == culture);
        }
    }
}