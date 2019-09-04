using System.Linq;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Company;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class CompanyDetailDtoExtensions
    {
        public static CompanyDetailDto ToDetailDto(this Companies source)
        {
            CompanyDetailDto result = null;
            if (source != null)
            {
                result = new CompanyDetailDto();
                source.ToDto(result);
                result.Departments = source.Departments?.ToList().ToDto();
                result.WorkCentersSelected = source.WorkCenters?.Select(x => new BaseNameIdDto<int>
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsLocked = x.WorkCenterPeople != null && x.WorkCenterPeople.Any(),
                });
            }

            return result;
        }

        public static Companies ToEntity(this CompanyDetailDto source)
        {
            Companies result = null;
            if (source != null)
            {
                result = new Companies();
                source.ToEntity(result);
                result.Departments = source.Departments?.ToEntity();
            }

            return result;
        }
    }
}