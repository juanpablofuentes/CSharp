using System.Collections.Generic;
using Group.Salto.Entities;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface ITranslationRepository : IRepository<Translation>
    {
        Translation GetTranslation(string key, string culture);
        IEnumerable<Translation> GetAll(string culture);
    }
}
