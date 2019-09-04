using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Team;
using Group.Salto.SOM.Web.Models.Brands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ModelViewModelExtensions
    {
        public static ModelViewModel ToModelViewModelBrandsModel(this ModelDto source)
        {
            ModelViewModel result = null;
            if (source != null)
            {
                result = new ModelViewModel()
                {
                    ModelId = source.Id,
                    ModelName = source.Name,
                    ModelDescription = source.Description,
                    ModelUrl = source.Url
                };
            }
            return result;
        }

        public static IList<ModelViewModel> ToModelViewModelBrandsModel(this IList<ModelDto> source)
        {
            return source?.MapList(sCK => sCK.ToModelViewModelBrandsModel());
        }

        public static ModelDto ToBrandsModelDto(this ModelViewModel source)
        {
            ModelDto result = null;
            if (source != null)
            {
                result = new ModelDto()
                {
                    Id = source.ModelId,
                    Name = source.ModelName,
                    Description = source.ModelDescription,
                    Url = source.ModelUrl
                };
            }
            return result;
        }

        public static IList<ModelDto> ToBrandsModelDto(this IList<ModelViewModel> source)
        {
            return source?.MapList(sCK => sCK.ToBrandsModelDto());
        }
    }
}