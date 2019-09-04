using Group.Salto.Common.Helpers;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ZoneProject;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Projects;
using Group.Salto.ServiceLibrary.Common.Dtos.ZoneProject;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.ServiceLibrary.Implementations.ZoneProject
{
    public class ZoneProjectService : BaseService, IZoneProjectService
    {
        private readonly IZoneProjectRepository _zoneProjectRepository;
        private readonly IProjectRepository _projectRepository;
        public ZoneProjectService(ILoggingService logginingService, IZoneProjectRepository zoneProjectRepository, IProjectRepository projectRepository) : base(logginingService)
        {
            _zoneProjectRepository = zoneProjectRepository ?? throw new ArgumentNullException($"{nameof(IZoneProjectRepository)} is null ");
            _projectRepository = projectRepository ?? throw new ArgumentNullException($"{nameof(IProjectRepository)} is null ");
        }
        public ResultDto<IList<ZoneProjectDto>> GetAllById(int id)
        {
            LogginingService.LogInfo($"Get All  ZoneProject by id zone");
            var query = _zoneProjectRepository.GetAllById(id);
            var data = query.ToList().MapList(c => c.ToDto()).AsQueryable();
            return ProcessResult<IList<ZoneProjectDto>>(data.ToList());
        }

        public ResultDto<IList<ZoneProjectDto>> GetAllPostalCodesByZoneProjectId(int ZoneProjectid)
        {
            LogginingService.LogInfo($"Get All  ZoneProject postals codes by id project");
            var query = _zoneProjectRepository.GetPostalcodesByZoneProjectId(ZoneProjectid);
            var data = query.ToList().MapList(c => c.ToDto()).AsQueryable();
            return ProcessResult<IList<ZoneProjectDto>>(data.ToList());
        }

        public ProjectsDto GetProjectByProjectName(string ProjectName)
        {
            LogginingService.LogInfo($"Get All  ZoneProject postals codes by id project");
            var query = _projectRepository.GetProjectByProjectName(ProjectName);
            var data = query.ToDto();
            return data;
        }
    }
}
