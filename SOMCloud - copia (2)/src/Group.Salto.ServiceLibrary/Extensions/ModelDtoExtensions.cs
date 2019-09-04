using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Team;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ModelDtoExtensions
    {
        public static ModelDto ToDto(this Models dbModel)
        {
            var dto = new ModelDto
            {
                Id = dbModel.Id,
                Name = dbModel.Name,
                Description = dbModel.Description,
                Url = dbModel.Url,
                BrandId = dbModel.BrandId,
                Brand = dbModel.Brand?.ToApiDto() //TODO: Victor. Revisa que esté todo correcto
            };

            return dto;
        }
        public static IList<ModelDto> ToDto(this IList<Models> source)
        {
            return source?.MapList(sC => sC.ToDto());
        }

        public static Models ToEntity(this ModelDto source)
        {
            Models result = null;
            if (source != null)
            {
                result = new Models
                {
                    Name = source.Name,
                    Description = source.Description,
                    Url = source.Url,
                    BrandId = source.BrandId
                };
            }

            return result;
        }

        public static IList<Models> ToEntity(this IList<ModelDto> source)
        {
            return source?.MapList(sC => sC.ToEntity());
        }
    }
}