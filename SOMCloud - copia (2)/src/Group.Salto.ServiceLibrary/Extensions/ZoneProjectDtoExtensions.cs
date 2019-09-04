using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.ZoneProject;
using Group.Salto.ServiceLibrary.Common.Dtos.ZoneProjectPostalCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ZoneProjectDtoExtensions
    {
        public static ZoneProjectDto ToDto(this ZoneProject source)
        {
            ZoneProjectDto result = null;
            if (source != null)
            {
                result = new ZoneProjectDto()
                {
                    ZoneProjectId = source.Id,
                    ZoneId = source.ZoneId,
                    ProjectId = source.ProjectId,
                    Project = source.Project.ToDto(),
                    PostalCodes = source.ZoneProjectPostalCode?.ToList().ToDto()
                };
            }
            return result;
        }
        public static IList<ZoneProjectDto> ToListDto(this IList<ZoneProject> source)
        {
                return source.MapList(x => x.ToDto());
        }

        public static ZoneProject ToEntity(this ZoneProjectDto source)
        {
        ZoneProject result = null;
         if (source != null)
                {
                    result = new ZoneProject()
                    {
                        ZoneId = source.ZoneId,
                        Zone = source.Zone.ToEntity(),
                        ProjectId = source.ProjectId,
                        Project = source.Project.ToEntity(),
                        ZoneProjectPostalCode = source.PostalCodes.ToEntity()
                    };
                }
                return result;
            }
        public static IList<ZoneProject> ToEntity(this IList<ZoneProjectDto> source)
        {
            return source?.MapList(sC => sC.ToEntity());
        }
    }
}