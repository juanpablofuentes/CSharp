using Group.Salto.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface ILanguageRepository : IRepository<Language>
    {
        IEnumerable<Language> GetAll();
        Language GetByCulture(string culture);
    }
}
