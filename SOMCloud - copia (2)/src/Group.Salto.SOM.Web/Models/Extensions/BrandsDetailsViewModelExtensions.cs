using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Brand;
using Group.Salto.ServiceLibrary.Common.Dtos.Team;
using Group.Salto.SOM.Web.Models.Brands;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class BrandsDetailsViewModelExtensions
    {
        public static BrandsDetailsDto ToDto(this BrandsDetailsViewModel source)
        {
            BrandsDetailsDto result = null;
            if (source != null)
            {
                result = new BrandsDetailsDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Url = source.Url,
                    Models = source.ModelsSelected.ToBrandsModelDto()
                };
            }
            return result;
        }

        public static ResultViewModel<BrandsDetailsViewModel> ToViewModel(this ResultDto<BrandsDetailsDto> source)
        {
            var response = source != null ? new ResultViewModel<BrandsDetailsViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static BrandsDetailsViewModel ToViewModel(this BrandsDetailsDto source)
        {
            BrandsDetailsViewModel result = null;
            if (source != null)
            {
                result = new BrandsDetailsViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Url = source.Url,
                    ModelsSelected = source.Models.ToModelViewModelBrandsModel()
                };
            }
            return result;
        }
    }
}