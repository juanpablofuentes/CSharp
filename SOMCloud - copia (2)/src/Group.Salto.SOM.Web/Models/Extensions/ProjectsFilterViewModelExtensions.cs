using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Projects;
using Group.Salto.SOM.Web.Models.Projects;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ProjectsFilterViewModelExtensions
    {
        public static ProjectsFilterDto ToDto(this ProjectsFilterViewModel source)
        {
            ProjectsFilterDto result = null;
            if (source != null)
            {
                result = new ProjectsFilterDto()
                {
                    Name = source.Name,
                    Serie = source.Serie,
                    OrderBy = source.OrderBy,
                    Asc = source.Asc,
                };
            }
            return result;
        }

        public static ProjectsFilterViewModel ToViewModel(this ProjectsFilterDto source)
        {
            ProjectsFilterViewModel result = null;
            if (source != null)
            {
                result = new ProjectsFilterViewModel();
                source.ToViewModel(result);
            }

            return result;
        }

        public static void ToViewModel(this ProjectsFilterDto source, ProjectsFilterViewModel target)
        {
            if (source != null && target != null)
            {
                target.Name = source.Name;
                target.Serie = source.Serie;
            }
        }

        public static IList<ProjectsFilterViewModel> ToListViewModel(this IList<ProjectsFilterDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
    }
}