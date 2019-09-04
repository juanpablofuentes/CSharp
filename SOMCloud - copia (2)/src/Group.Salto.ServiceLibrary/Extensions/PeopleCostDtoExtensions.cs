using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleCost;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PeopleCostDtoExtensions
    {
        public static PeopleCostDetailDto ToPeopleCostDto(this PeopleCost source)
        {
            PeopleCostDetailDto result = null;
            if (source != null)
            {
                result = new PeopleCostDetailDto()
                {
                    Id = source.Id,
                    PeopleId = source.PeopleId,
                    HourCost = source.HourCost,
                    StartDate = source.StartDate,
                    EndDate = source.EndDate,
                };
            }

            return result;
        }

        public static IList<PeopleCostDetailDto> ToPeopleCostDto(this IList<PeopleCost> source)
        {
            return source?.MapList(pc => pc.ToPeopleCostDto());
        }

        public static PeopleCost ToEntity(this PeopleCostDetailDto source)
        {
            PeopleCost result = null;
            if (source != null)
            {
                result = new PeopleCost()
                {
                    Id = source.Id,
                    PeopleId = source.PeopleId,
                    HourCost = source.HourCost,
                    StartDate = source.StartDate,
                    EndDate = source.EndDate
                };
            }

            return result;
        }

        public static void UpdatePeopleCost(this PeopleCost target, PeopleCost source)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.PeopleId = source.PeopleId;
                target.HourCost = source.HourCost;
                target.StartDate = source.StartDate;
                target.EndDate = source.EndDate;
            }
        }

        public static bool IsValid(this PeopleCostDetailDto source)
        {
            bool result = false;
            result = source != null;

            return result;
        }
    }
}