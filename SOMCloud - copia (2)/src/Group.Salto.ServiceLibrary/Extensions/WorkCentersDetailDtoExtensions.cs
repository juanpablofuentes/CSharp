using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkCenter;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WorkCentersDetailDtoExtensions
    {
        public static WorkCenterDetailDto ToDetailDto(this WorkCenters source)
        {
            WorkCenterDetailDto result = null;
            if (source != null)
            {
                result = new WorkCenterDetailDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    CountrySelected = source.CountryId,
                    RegionSelected = source.RegionId,
                    StateSelected = source.StateId,
                    MunicipalitySelected = source.MunicipalityId,
                    Address = source.Address,
                    CompanySelected = source.Company?.Id,
                    ResponsableSelected = source.People?.Id
                };
            }
            return result;
        }

        public static WorkCenters ToEntity(this WorkCenterDetailDto source)
        {
            WorkCenters result = null;
            if (source != null)
            {
                result = new WorkCenters()
                {
                    Name = source.Name,
                    CountryId = source.CountrySelected,
                    RegionId = source.RegionSelected,
                    StateId = source.StateSelected,
                    MunicipalityId = source.MunicipalitySelected,
                    Address = source.Address,                    
                };
            }
            return result;
        }
    }
}