using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Brand;
using Group.Salto.SOM.Web.Models.Brands;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class BrandsViewModelExtensions
    {
        public static ResultViewModel<BrandViewModel> ToViewModel(this ResultDto<BrandsDto> source)
        {
            var response = source != null ? new ResultViewModel<BrandViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static BrandViewModel ToViewModel(this BrandsDto source)
        {
            BrandViewModel result = null;
            if (source != null)
            {
                result = new BrandViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Url = source.Url
                };
            }
            return result;
        }

        public static IList<BrandViewModel> ToViewModel(this IList<BrandsDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static BrandsDto ToDto(this BrandViewModel source)
        {
            BrandsDto result = null;
            if (source != null)
            {
                result = new BrandsDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Url = source.Url
                };
            }
            return result;
        }
    }
}