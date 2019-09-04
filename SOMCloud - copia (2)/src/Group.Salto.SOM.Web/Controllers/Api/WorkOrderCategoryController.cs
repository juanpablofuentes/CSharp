using Group.Salto.Log;
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
    public class WorkOrderCategoryController : BaseAPIController
    {
        private readonly IWorkOrderCategoriesService _workOrderCategoriesService;

        public WorkOrderCategoryController(ILoggingService loggingService,
                                           IConfiguration configuration,
                                           IHttpContextAccessor accessor,
                                           IWorkOrderCategoriesService workOrderCategoriesService) : base(loggingService, configuration, accessor)
        {
            _workOrderCategoriesService = workOrderCategoriesService ?? throw new ArgumentNullException($"{nameof(IWorkOrderCategoriesService)} is null");
        }

        [HttpPost]
        public IActionResult Post(QueryRequestViewModel queryRequest)
        {
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }

            queryRequest.QueryTypeParameters.Value = GetConfigurationUserId().ToString();
            var result = _workOrderCategoriesService.Filter(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToKeyValuePair());
        }

        [HttpGet("CanDelete")]
        public IActionResult CanDelete(int id)
        {
            var result = _workOrderCategoriesService.CanDelete(id);
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

        [HttpGet("HasSLADates")]
        public IActionResult HasSLADates(int id)
        {
            var result = _workOrderCategoriesService.HasSLADates(id);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.Data);
        }

        [HttpPost("GetWoCategoryAll")]
        public IActionResult GetProjectAll(QueryCascadeViewModel queryRequest)
        {
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }

            var result = _workOrderCategoriesService.FilterByProject(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            result.Add(new BaseNameIdDto<int>
            {
                Id = 0,
                Name = "All"
            });
            return Ok(result.ToKeyValuePair());
        }
    }
}