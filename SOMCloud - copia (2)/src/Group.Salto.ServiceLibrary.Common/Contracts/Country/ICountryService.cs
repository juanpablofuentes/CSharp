using System.Collections.Generic;
using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Country
{
    public interface ICountryService
    {
        IList<BaseNameIdDto<int>> GetAllKeyValues();
        IList<BaseNameIdDto<int>> GetKeyValuesMunicipalities(int stateId);
        IList<BaseNameIdDto<int>> GetKeyValuesRegions(int countryId);
        IList<BaseNameIdDto<int>> GetKeyValuesStates(int regionId);
        IList<BaseNameIdDto<int>> GetKeyValuesCities(int muniId);
        Cities GetCity(int cityId);
        Municipalities GetMunicipality(int municipalityId);
    }
}
