using Group.Salto.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface IPostalCodeRepository
    {
        Dictionary<int, string> GetAllKeyValuesByMunicipality(int municipalityId);
        PostalCodes GetById(int id);
        PostalCodes GetByCode(string code);
        PostalCodes GetByCity(int city);
        bool ExistCodeAndCity(string code, int city);
    }
}