using System.Collections.Generic;
using System.Linq;
using DataAccess.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Group.Salto.DataAccess.Repositories
{
    public class TranslationRepository : BaseRepository<Translation>, ITranslationRepository
    {
        public TranslationRepository(IUnitOfWork uow) : base(uow)
        {

        }

        public Translation GetTranslation(string key, string culture)
        {
            return GetFull().SingleOrDefault(l => l.Key == key
                                                  && l.Language.CultureCode.ToUpper() == culture.ToUpper());
        }

        public IEnumerable<Translation> GetAll(string culture)
        {
            return GetFull().Where(l => l.Language.CultureCode.ToUpper() == culture.ToUpper());
        }

        private IIncludableQueryable<Translation, Language> GetFull()
        {
            return DbSet.Include(table => table.Language);
        }
    }
}