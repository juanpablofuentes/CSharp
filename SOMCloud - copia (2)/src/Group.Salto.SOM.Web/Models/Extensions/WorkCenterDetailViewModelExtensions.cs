using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkCenter;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.WorkCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class WorkCenterDetailViewModelExtensions
    {
        public static WorkCenterDetailDto ToDto(this WorkCenterDetailViewModel source)
        {
            WorkCenterDetailDto result = null;
            if (source != null)
            {
                result = new WorkCenterDetailDto()
                {
                    Id = source.WorkCenter.Id,
                    Name = source.WorkCenter.Name,
                    CountrySelected = source.CountrySelected,
                    RegionSelected = source.RegionSelected,
                    StateSelected = source.StateSelected,
                    MunicipalitySelected = source.MunicipalitySelected,
                    Address = source.WorkCenter.Address,
                    CompanySelected = source.CompanySelected,
                    ResponsableSelected = source.ResponsableSelected
                };
            }
            return result;
        }

        public static ResultViewModel<WorkCenterDetailViewModel> ToViewModel(this ResultDto<WorkCenterDetailDto> source)
        {
            var response = source != null ? new ResultViewModel<WorkCenterDetailViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static WorkCenterDetailViewModel ToViewModel(this WorkCenterDetailDto source)
        {
            WorkCenterDetailViewModel result = null;
            if (source != null)
            {
                result = new WorkCenterDetailViewModel();
                result.WorkCenter = new WorkCenterViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Address = source.Address
                };
                result.CompanySelected = source.CompanySelected;
                result.ResponsableSelected = source.ResponsableSelected;
                result.MunicipalitySelected = source.MunicipalitySelected;
                result.StateSelected = source.StateSelected;
                result.RegionSelected = source.RegionSelected;
                result.CountrySelected = source.CountrySelected;                
            }
            return result;
        }
    }
}