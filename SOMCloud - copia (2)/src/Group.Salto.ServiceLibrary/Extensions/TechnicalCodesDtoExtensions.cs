using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.TechnicalCodes;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class TechnicalCodesDtoExtensions
    {
        public static TechnicalCodesDto ToTechnicalCodesDto(this TechnicalCodes source)
        {
            TechnicalCodesDto result = null;
            if (source != null)
            {
                result = new TechnicalCodesDto()
                {
                    Id = source.Id,
                    Name = $"{source.PeopleTechnic?.Name} {source.PeopleTechnic?.FisrtSurname ?? string.Empty} {source.PeopleTechnic?.SecondSurname ?? string.Empty}",
                    PeopleId = source.PeopleTechnicId,
                    Code = source.Code
                };
            }

            return result;
        }

        public static IList<TechnicalCodesDto> ToTechnicalCodesDto(this IList<TechnicalCodes> source)
        {
            return source?.MapList(cc => cc.ToTechnicalCodesDto());
        }

        public static TechnicalCodes ToEntity(this TechnicalCodesDto source)
        {
            TechnicalCodes result = null;
            if (source != null)
            {
                result = new TechnicalCodes()
                {
                    PeopleTechnicId = source.PeopleId,
                    Code = source.Code
                };
            }

            return result;
        }

        public static IList<TechnicalCodes> ToEntity(this IList<TechnicalCodesDto> source)
        {
            return source?.MapList(e => e.ToEntity());
        }
    }
}