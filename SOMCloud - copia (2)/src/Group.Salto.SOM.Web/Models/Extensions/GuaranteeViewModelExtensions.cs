using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Team;
using Group.Salto.SOM.Web.Models.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class GuaranteeViewModelExtensions
    {
        public static GuaranteeViewModel ToViewModel(this GuaranteeDto source)
        {
            GuaranteeViewModel result = null;
            if (source != null)
            {
                result = new GuaranteeViewModel()
                {
                    Armored = source.Armored,
                    BlnEndDate = source.BlnEndDate,
                    BlnStartDate = source.BlnStartDate,
                    Id = source.Id,
                    IdExternal = source.IdExternal,
                    ProEndDate = source.ProEndDate,
                    ProStartDate = source.ProStartDate,
                    Provider = source.Provider,
                    Standard = source.Standard,
                    StdEndDate = source.StdEndDate,
                    StdStartDate = source.StdStartDate
                };
            }
            return result;
        }

        public static GuaranteeDto ToDto(this GuaranteeViewModel source)
        {
            GuaranteeDto result = null;
            if (source != null)
            {
                result = new GuaranteeDto()
                {
                    Armored = source.Armored,
                    BlnEndDate = source.BlnEndDate,
                    BlnStartDate = source.BlnStartDate,
                    Id = source.Id,
                    IdExternal = source.IdExternal,
                    ProEndDate = source.ProEndDate,
                    ProStartDate = source.ProStartDate,
                    Provider = source.Provider,
                    Standard = source.Standard,
                    StdEndDate = source.StdEndDate,
                    StdStartDate = source.StdStartDate
                };
            }
            return result;
        }
    }
}