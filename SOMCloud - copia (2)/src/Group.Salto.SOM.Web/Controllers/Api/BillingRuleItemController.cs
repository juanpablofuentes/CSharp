using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.BillingRuleItem;
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
    public class BillingRuleItemController: BaseAPIController
    {
        private IBillingRuleItemService _billingRuleItemService;

        public BillingRuleItemController(ILoggingService loggingService,
                                IConfiguration configuration,
                                IHttpContextAccessor accessor,
                                IBillingRuleItemService billingRuleItemService) : base(loggingService, configuration, accessor)
        {           
            _billingRuleItemService = billingRuleItemService ?? throw new ArgumentNullException(nameof(IBillingRuleItemService));
        }

        [HttpPost("PostCreateBillingRuleItem")]
        public IActionResult PostCreateBillingRuleItem(BillingRuleItemDto billingRuleItem)
        {
            var result = _billingRuleItemService.Create(billingRuleItem);
            
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }
                
        [HttpPost("PostUpdateBillingRuleItem")]
        public IActionResult PostUpdateBillingRuleItem(BillingRuleItemDto billingRuleItem)
        {
            var result = _billingRuleItemService.Update(billingRuleItem);
            
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }

        [HttpDelete("DeleteBillingRuleItem")]
        public IActionResult DeleteBillingRuleItem(int id)
        {
            var result = _billingRuleItemService.Delete(id);
            
            if (result == null || result?.Data == false)
            {
                return NotFound("Not data found");
            }
            return Ok(result);
        }
    }
}