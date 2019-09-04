using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Brand;
using System.Collections.Generic;
using System.Linq;


namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class BrandsDetailsDtoExtensions
    {
        public static BrandsDetailsDto ToDetailDto(this Brands source)
        {
            BrandsDetailsDto result = null;
            if (source != null)
            {
                result = new BrandsDetailsDto()
                {
                    Name = source.Name,
                    Description = source.Description,
                    Url = source.Url,
                    Models = source.Models?.ToList().ToDto()
                };
            }
            return result;
        }

        public static Brands ToEntity(this BrandsDetailsDto source)
        {
            Brands result = null;
            if (source != null)
            {
                result = new Brands()
                {
                    Name = source.Name,
                    Description = source.Description,
                    Url = source.Url,
                    Models = source.Models?.ToList().ToEntity()
                };
            }
            return result;
        }
        public static IList<BrandsDetailsDto> ToDetailDto(this IList<Brands> source)
        {
            return source.MapList(x => x.ToDetailDto());
        }

        public static Brands Update(this Brands target, BrandsDetailsDto source)
        {
            if (target != null && source != null)
            {
                target.Id = source.Id;
                target.Name = source.Name;
                target.Description = source.Description;
                target.Url = source.Url;
            }
            return target;
        }
    }
}