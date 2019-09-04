using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderViewConfigurations;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderViewConfigurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.ServiceLibrary.Common.Contracts.Language;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.ServiceLibrary.Common.Contracts.PeopleCollection;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class WorkOrderFilterController : BaseLanguageController
    {
        private readonly IWorkOrderViewConfigurationsServices _workOrderViewConfigurationsServices;
        private readonly IPeopleService _peopleService;
        private readonly IPeopleCollectionService _peopleCollectionService;

        public WorkOrderFilterController(
           ILoggingService loggingService,
           IConfiguration configuration,
           IHttpContextAccessor accessor,
           ILanguageService languageService,
           IPeopleService peopleService,
           IPeopleCollectionService peopleCollectionService,
           IWorkOrderViewConfigurationsServices workOrderViewConfigurationsServices) : base(loggingService, configuration, accessor, languageService)
        {
            _workOrderViewConfigurationsServices = workOrderViewConfigurationsServices ?? throw new ArgumentNullException(nameof(IWorkOrderViewConfigurationsServices));
            _peopleService = peopleService ?? throw new ArgumentNullException($"{nameof(IPeopleService)} is null");
            _peopleCollectionService = peopleCollectionService ?? throw new ArgumentNullException($"{nameof(IPeopleCollectionService)} is null");
        }

        [HttpGet("GetConfiguredViews")]
        public IActionResult GetConfiguredViews(int id)
        {
            IList<WorkOrderViewConfigurationsDto> result = _workOrderViewConfigurationsServices.GetAllViewsByUserId(base.GetConfigurationUserId()).Data;

            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToViewModel());
        }

        [HttpGet("GetTechnicians")]
        public IActionResult GetTechnicians(int id)
        {
            ConfigurationViewDto dataDto = _workOrderViewConfigurationsServices.GetConfiguredViewById(id, GetLanguageId()).Data;
            var result = _peopleService.GetPeopleTechniciansMultiSelect(GetConfigurationUserId(), dataDto.TechnicianValues).Data.ToViewModel();

            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpGet("GetGroups")]
        public IActionResult GetGroups(int id)
        {
            ConfigurationViewDto dataDto = _workOrderViewConfigurationsServices.GetConfiguredViewById(id, GetLanguageId()).Data;
            var result = _peopleCollectionService.GetPeopleCollectionMultiSelect(GetConfigurationUserId(), dataDto.GroupsValues).Data.ToViewModel();

            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }
    }
}