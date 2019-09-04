using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Projects;
using Group.Salto.ServiceLibrary.Common.Dtos.ZoneProject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Contracts.ZoneProject
{
    public interface IZoneProjectService
    {
        ResultDto<IList<ZoneProjectDto>> GetAllById(int zoneid);
        ResultDto<IList<ZoneProjectDto>> GetAllPostalCodesByZoneProjectId(int ZoneProjectid);
        ProjectsDto GetProjectByProjectName(string ProjectName);
    }
}
