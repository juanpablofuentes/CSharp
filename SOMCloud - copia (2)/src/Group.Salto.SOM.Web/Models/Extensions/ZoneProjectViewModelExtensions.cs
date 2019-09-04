using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.ZoneProject;
using Group.Salto.ServiceLibrary.Common.Dtos.ZoneProjectPostalCode;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.ZoneProject;
using Group.Salto.SOM.Web.Models.ZoneProjectPostalCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ZoneProjectViewModelExtensions
    {
        public static ResultViewModel<ZoneProjectViewModel> ToViewModel(this ResultDto<ZoneProjectDto> source)
        {
            var response = source != null ? new ResultViewModel<ZoneProjectViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static ZoneProjectViewModel ToViewModel(this ZoneProjectDto source)
        {
            ZoneProjectViewModel result = null;
            if (source != null)
            {
                result = new ZoneProjectViewModel()
                {
                    ZoneProjectId = source.ZoneProjectId,
                    ProjectId = source.ProjectId,                   
                    ZoneId = source.ZoneId,
                    PostalCodes = source.PostalCodes.ToViewModel(),                                      
                };
                if (source.Project != null) {
                    result.ProjectName = source.Project.Name;
                }
                else
                {
                    result.ProjectName = "Default";
                }
            }
            return result;
        }
     
        public static IList<ZoneProjectViewModel> ToViewModel(this IList<ZoneProjectDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static ZoneProjectDto ToDto(this ZoneProjectViewModel source)
        {
            ZoneProjectDto result = null;
            if (source != null)
            {
                result = new ZoneProjectDto()
                {
                    ZoneProjectId = source.ZoneProjectId,
                    ZoneId = source.ZoneId,
                    ProjectId = source.ProjectId,  
                    State = source.State
                };
                
            }
            return result;
        }

        public static IList<ZoneProjectDto> ToListDto(this IList<ZoneProjectViewModel> source)
        {
            return source?.MapList(x => x.ToDto());
        }
    }
}