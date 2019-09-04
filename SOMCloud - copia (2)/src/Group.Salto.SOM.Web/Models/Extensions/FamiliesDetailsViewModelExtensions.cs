using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Families;
using Group.Salto.SOM.Web.Models.Families;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class FamiliesDetailsViewModelExtensions
    {
        public static FamiliesDetailsDto ToDto(this FamiliesDetailsViewModel source)
        {
            FamiliesDetailsDto result = null;
            if (source != null)
            {
                result = new FamiliesDetailsDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    SubFamiliesList = source.SubFamiliesList.ToFamiliesSubFamiliesDto()
                };
            }
            return result;
        }

        public static FamiliesDetailsDto ToEditDto(this FamiliesDetailsViewModel source)
        {
            FamiliesDetailsDto result = null;
            if (source != null)
            {
                result = new FamiliesDetailsDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    SubFamiliesList = source.SubFamiliesList.ToFamiliesEditSubFamiliesDto()
                };
            }
            return result;
        }

        public static ResultViewModel<FamiliesDetailsViewModel> ToViewModel(this ResultDto<FamiliesDetailsDto> source)
        {
            var response = source != null ? new ResultViewModel<FamiliesDetailsViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static FamiliesDetailsViewModel ToViewModel(this FamiliesDetailsDto source)
        {
            FamiliesDetailsViewModel result = null;
            if (source != null)
            {
                result = new FamiliesDetailsViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    SubFamiliesList = source.SubFamiliesList.ToSubFamiliesViewModelFamiliesSubFamilies()
                };
            }
            return result;
        }
    }
}