using Group.Salto.Entities;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Municipality
{
    public interface IMunicipalityService
    {
        Municipalities GetByIdWithStatesRegionsCountriesIncludes(int id);
    }
}