using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Brand;
using Group.Salto.ServiceLibrary.Common.Dtos.Team;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class BrandsDtoExtensions
    {
        public static BrandsDto ToDto(this Brands source)
        {
            BrandsDto result = null;
            if (source != null)
            {
                result = new BrandsDto()
                {
                    Name = source.Name,
                    Id = source.Id,
                    Description = source.Description,
                    Url = source.Url
                };
            }
            return result;
        }

        public static BrandDto ToApiDto(this Brands dbModel)
        {
            var dto = new BrandDto
            {
                Id = dbModel.Id,
                Name = dbModel.Name,
                Description = dbModel.Description,
                Url = dbModel.Url
            };
            return dto;
        }

        public static IList<BrandsDto> ToDto(this IList<Brands> source)
        {
            return source.MapList(x => x.ToDto());
        }
    }
}