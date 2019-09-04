using Group.Salto.ServiceLibrary.Common.Dtos.RepetitionParameter;
using Group.Salto.SOM.Web.Models.RepetitionParameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class RepetitionParameterFilterViewModelExtensions
    {
        public static RepetitionParameterFilterDto ToDto(this RepetitionParameterFilterViewModel source)
        {
            RepetitionParameterFilterDto result = null;
            if (source != null)
            {
                result = new RepetitionParameterFilterDto()
                {
                    Days = source.Days
                };
            }
            return result;
        }

        public static RepetitionParameterFilterViewModel ToViewModel(this RepetitionParameterFilterDto source)
        {
            RepetitionParameterFilterViewModel result = null;
            if (source != null)
            {
                result = new RepetitionParameterFilterViewModel()
                {
                    Days = source.Days 
                };
            }
            return result;
        }
    }
}