using Group.Salto.Common.Constants.Project;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Project;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderCategories;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProjectController : BaseAPIController
    {
        private readonly IProjectsService _projectsService;
        private readonly IProjectRelatedInfoAdapter _projectRelatedInfoAdapter;

        public ProjectController(
           ILoggingService loggingService,
           IConfiguration configuration,
           IHttpContextAccessor accessor,
           IProjectsService projectsService,
           IProjectRelatedInfoAdapter projectRelatedInfoAdapter) : base(loggingService, configuration, accessor)
        {
            _projectsService = projectsService ?? throw new ArgumentNullException($"{nameof(IProjectsService)} is null");
            _projectRelatedInfoAdapter = projectRelatedInfoAdapter ?? throw new ArgumentNullException($"{nameof(IProjectRelatedInfoAdapter)} is null");
        }

        [HttpGet("CanDelete")]
        public IActionResult CanDelete(int id)
        {
            var result = _projectsService.CanDelete(id);
            if (result == null)
            {
                return NotFound("Not data found");
            }

            if (!string.IsNullOrEmpty(result.Data.ErrorMessageKey))
            {
                var errorMessageKeyTranslation = LocalizedExtensions.GetUILocalizedText(result.Data.ErrorMessageKey);
                result.Data.ErrorMessageKey = errorMessageKeyTranslation;
            }            
            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult Post(QueryRequestViewModel queryRequest)
        {
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }

            queryRequest.QueryTypeParameters.Value = GetConfigurationUserId().ToString();
            var result  = _projectsService.Filter(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToKeyValuePair());
        }

        [HttpPost("GetProjectAll")]
        public IActionResult GetProjectAll(QueryCascadeViewModel queryRequest)
        {
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }

            var result = _projectsService.FilterByClient(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            result.Add(new BaseNameIdDto<int> {
                Id = 0,
                Name = "All"
            });
            return Ok(result.ToKeyValuePair());
        }

        [HttpGet("GetRelatedInfo")]
        public IActionResult GetRelatedInfo(int Id)
        {
            var result = _projectRelatedInfoAdapter.GetProjectRelatedInfo(Id);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.Data);
        }

        [HttpGet("AdvancedSearch")]
        public IActionResult AdvancedSearch(AdvancedSearchQueryTypeViewModel queryRequest)
        {
            LoggingService.LogInfo($"Project Post AdvancedSearch");
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }

            var result = _projectsService.GetAdvancedSearch(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.Data);
        }
    }
}