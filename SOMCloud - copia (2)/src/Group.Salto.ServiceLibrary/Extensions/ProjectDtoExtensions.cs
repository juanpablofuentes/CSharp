using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Projects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ProjectDtoExtensions
    {
        public static ProjectsDto ToDto(this Projects source)
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
                    StartDate = source.StartDate.ToShortDateString(),
                    EndDate = source.EndDate.ToShortDateString(),
                    IsActive = source.IsActive,
                    Observations = source.Observations,
                    ContractName = source.Contract?.Object,
                };
                
                var people = source.PeopleProjects?.Where(x => x.IsManager).FirstOrDefault()?.People;
                result.ProjectManager = $"{people?.FullName}";
            }
            return result;
        }

        public static IList<ProjectsDto> ToListDto(this IList<Projects> source)
        {
            return source?.MapList(x => x.ToDto());
        }
        public static Projects ToEntity(this ProjectsDto source)
        {
            Projects result = null;
            if (source != null)
            {
                DateTime dtStartDate;
                DateTime dtEndDate;
                DateTime.TryParse(source.StartDate, out dtStartDate);
                DateTime.TryParse(source.EndDate, out dtEndDate);
                result = new Projects()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Serie = source.Serie,
                    Counter = source.Counter,
                    StartDate = dtStartDate,
                    EndDate = dtEndDate,
                    IsActive = source.IsActive,
                    Observations = source.Observations,
                };
            }
            return result;
        }
    }
}