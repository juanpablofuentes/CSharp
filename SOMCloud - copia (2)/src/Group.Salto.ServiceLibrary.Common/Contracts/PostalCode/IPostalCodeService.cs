using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Common.Contracts.PostalCode
{
    public interface IPostalCodeService
    {
        IList<BaseNameIdDto<int>> GetAllKeyValuesByMunicipality(int municipalityId);
        PostalCodes GetById(int id);
        PostalCodes GetByCode(string code);
        PostalCodes GetByCity(int id);
        bool ValidateCodeAndCity(string code, int city);
    }
}