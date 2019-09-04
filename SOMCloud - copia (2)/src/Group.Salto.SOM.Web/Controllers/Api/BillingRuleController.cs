using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.BillingRule;
using Group.Salto.ServiceLibrary.Common.Contracts.ErpSystemInstance;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.BillingRule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class BillingRuleController : BaseAPIController
    {
        private IBillingRuleService _billingRuleService;
        private IErpSystemInstanceService _erpSystemInstanceService;

        public BillingRuleController(ILoggingService loggingService,
                                IConfiguration configuration,
                                IHttpContextAccessor accessor,
                                IErpSystemInstanceService erpSystemInstanceService,
                                IBillingRuleService billingRuleService) : base(loggingService, configuration, accessor)
        {           
            _billingRuleService = billingRuleService ?? throw new ArgumentNullException(nameof(IBillingRuleService));
            _erpSystemInstanceService = erpSystemInstanceService ?? throw new ArgumentNullException(nameof(IErpSystemInstanceService));
        }

        [HttpGet("GetAllBillingRulesByTaskId")]
        public IActionResult GetAllBillingRulesByTaskId(int id)
        {
            var result = _billingRuleService.GetAllByTaskId(id);
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpGet("GetErpSystems")]
        public IActionResult GetErpSystems()
        {
            var result = _erpSystemInstanceService.GetAllKeyValues();
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }
                
        [HttpPost("PostCreateBillingRule")]
        public IActionResult PostCreateBillingRule(BillingRuleBaseDto billingRule)
        {
            var result = _billingRuleService.Create(billingRule);
            
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }
                
        [HttpPost("PostUpdateBillingRule")]
        public IActionResult PostUpdateBillingRule(BillingRuleBaseDto billingRule)
        {
            var result = _billingRuleService.Update(billingRule);
            
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpDelete("DeleteBillingRule")]
        public IActionResult DeleteBillingRule(int id)
        {
            var result = _billingRuleService.Delete(id);
            
            if (result == null || result?.Data == false)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }
    }
}