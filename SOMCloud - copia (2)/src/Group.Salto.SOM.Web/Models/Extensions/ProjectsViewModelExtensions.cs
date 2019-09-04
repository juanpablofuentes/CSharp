using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Projects;
using Group.Salto.SOM.Web.Models.Projects;
using Group.Salto.SOM.Web.Models.Result;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ProjectsViewModelExtensions
    {
        public static ProjectsDto ToDto(this ProjectsViewModel source)
        {
            ProjectsDto result = null;
            if (source != null)
            {
                result = new ProjectsDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Serie = source.Serie,
                    Counter = source.Counter,
                    StartDate = source.StartDate,
                    EndDate = source.EndDate,
                    IsActive = source.IsActive,
                    Observations = source.Observations,
                    ProjectManager = source.ProjectManager,
                    ContractName = source.ContractName,
                };
            }
            return result;
        }

        public static ProjectsDto ToDetailDto(this ProjectsViewModel source)
        {
            ProjectsDto result = null;
            if (source != null)
            {
                result = new ProjectsDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Serie = source.Serie,
                    Counter = source.Counter,
                    StartDate = source.StartDate,
                    EndDate = source.EndDate,
                    IsActive = source.IsActive,
                    Observations = source.Observations,
                    ProjectManager = source.ProjectManager,
                    ContractName = source.ContractName,
                };
            }
            return result;
        }

        public static ResultViewModel<ProjectsViewModel> ToResultViewModel(this ResultDto<ProjectsDto> source)
        {
            ResultViewModel<ProjectsViewModel> result = null;
            if (source != null)
            {
                result = new ResultViewModel<ProjectsViewModel>()
                {
                    Data = source.Data.ToViewModel(),
                    Feedbacks = source.Errors.ToViewModel(),
                };
            }

            return result;
        }

        public static ProjectsViewModel ToViewModel(this ProjectsDto source)
        {
            ProjectsViewModel result = null;
            if (source != null)
            {
                result = new ProjectsViewModel();
                source.ToViewModel(result);
            }

            return result;
        }

        public static void ToViewModel(this ProjectsDto source, ProjectsViewModel target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.Name = source.Name;
                target.Serie = source.Serie;
                target.Counter = source.Counter;
                target.StartDate = source.StartDate;
                target.EndDate = source.EndDate;
                target.IsActive = source.IsActive;
                target.Observations = source.Observations;
                target.ProjectManager = source.ProjectManager;
                target.ContractName = source.ContractName;
            }
        }

        public static IList<ProjectsViewModel> ToListViewModel(this IList<ProjectsDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
    }
}