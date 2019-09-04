using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.RepetitionParameter;
using Group.Salto.SOM.Web.Models.RepetitionParameter;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class RepetitionParametersDetailViewModelExtension
    {
        public static RepetitionParameterDetailsViewModel ToViewModel(this RepetitionParametersDetailDto source)
        {
            RepetitionParameterDetailsViewModel result = null;
            if (source != null)
            {
                result = new RepetitionParameterDetailsViewModel()
                {
                    Id = source.Id,
                    Days = source.Days,
                    IdCalculationType = source.IdCalculationType,
                    IdDamagedEquipment = source.IdDamagedEquipment,
                    IdDaystype = source.IdDaysType
                };
            }
            return result;
        }
        public static ResultViewModel<RepetitionParameterDetailsViewModel> ToViewModel(this ResultDto<RepetitionParametersDetailDto> source)
        {
            var response = source != null ? new ResultViewModel<RepetitionParameterDetailsViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }
        
        public static RepetitionParametersDetailDto ToDto(this RepetitionParameterDetailsViewModel genericVM)
        {
            RepetitionParametersDetailDto result = null;
            if (genericVM != null)
            {
                result = new RepetitionParametersDetailDto()
                {
                    Id = genericVM.Id,
                    Days = genericVM.Days,
                    IdCalculationType = genericVM.IdCalculationType,
                    IdDamagedEquipment = genericVM.IdDamagedEquipment,
                    IdDaysType = genericVM.IdDaystype
                };
            }
            return result;
        }
        public static ResultViewModel<RepetitionParameterViewModel> ToResultViewModel(this ResultDto<RepetitionParametersDetailDto> source)
        {
            ResultViewModel<RepetitionParameterViewModel> result = null;
            if (source != null)
            {
                result = new ResultViewModel<RepetitionParameterViewModel>()
                {
                    Data = source.Data.ToViewModel(),
                    Feedbacks = source.Errors.ToViewModel(),
                };
            }
            return result;
        }
    }
}
