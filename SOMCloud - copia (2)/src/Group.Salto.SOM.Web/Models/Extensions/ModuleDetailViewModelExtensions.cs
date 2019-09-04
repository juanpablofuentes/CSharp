using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.ActionGroups;
using Group.Salto.ServiceLibrary.Common.Dtos.Modules;
using Group.Salto.SOM.Web.Models.Modules;
using Group.Salto.SOM.Web.Models.MultiSelect;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ModuleDetailViewModelExtensions
    {
        public static ModuleDetailDto ToDto(this ModuleDetailViewModel source)
        {
            ModuleDetailDto result = null;
            if (source != null)
            {
                result = new ModuleDetailDto()

                {
                    Id = source.Id ?? default(Guid),
                    Key = source.Key,
                    Description = source.Description
                };
                result.ActionGroupsSelected = source.ActionGroups?.Items.Where(x => x.IsChecked
                    && Guid.TryParse(x.Value, out var temp))?.Select(x => Guid.Parse(x.Value))?.ToList();
            }
            return result;
        }

        public static ResultViewModel<ModuleDetailViewModel> ToResultViewModel(this ResultDto<ModuleDetailDto> source)
        {
            var response = source != null ? new ResultViewModel<ModuleDetailViewModel>()
            {
                Data = source.Data.ToDetailViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static ModuleDetailViewModel ToDetailViewModel(this ModuleDetailDto source)
        {
            ModuleDetailViewModel module = null;
            if (source != null)
            {
                module = new ModuleDetailViewModel()
                {
                    Id = source.Id,
                    Key = source.Key,
                    Description = source.Description
                };
            }
            return module;
        }
    }
}