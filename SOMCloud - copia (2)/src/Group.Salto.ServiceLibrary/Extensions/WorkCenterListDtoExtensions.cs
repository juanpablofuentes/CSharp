using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkCenter;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WorkCenterListDtoExtensions
    {
        public static WorkCenterListDto ToDto(this WorkCenters source)
        {
            WorkCenterListDto result = null;
            if(source != null)
            {
                result = new WorkCenterListDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    MunicipalitySelected = source.MunicipalityId,
                    Company = source.Company?.Name,
                    ResponsableSelected = source.People?.Id,
                    HasPeopleAssigned = source.WorkCenterPeople.Any()
                };
            }
            return result;
        }
    }
}