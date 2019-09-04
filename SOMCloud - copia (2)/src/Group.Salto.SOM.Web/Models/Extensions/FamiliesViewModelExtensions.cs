using Group.Salto.Common.Helpers;
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
    public static class FamiliesViewModelExtensions
    {
        public static ResultViewModel<FamilieViewModel> ToViewModel(this ResultDto<FamiliesDto> source)
        {
            var response = source != null ? new ResultViewModel<FamilieViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static FamilieViewModel ToViewModel(this FamiliesDto source)
        {
            FamilieViewModel result = null;
            if (source != null)
            {
                result = new FamilieViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description
                };
            }
            return result;
        }

        public static IList<FamilieViewModel> ToViewModel(this IList<FamiliesDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static FamiliesDto ToDto(this FamilieViewModel source)
        {
            FamiliesDto result = null;
            if (source != null)
            {
                result = new FamiliesDto()
                {
                    Id = source.Id ,
                    Name = source.Name,
                    Description = source.Description,
                };
            }
            return result;
        }
    }
}