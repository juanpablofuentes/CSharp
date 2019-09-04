using Group.Salto.Common.Constants.ExpenseTicket;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Expense;
using Group.Salto.ServiceLibrary.Common.Contracts.ExpenseTicket;
using Group.Salto.SOM.Web.Models.Expense;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class ExpenseTicketController : ControllerBase
    {
        private readonly ILoggingService _loggingService;
        private IExpenseService _expenseService;

        public ExpenseTicketController(ILoggingService loggingService,
                                IExpenseService expenseService)
        {
            _loggingService = loggingService ?? throw new ArgumentNullException(nameof(ILoggingService));
            _expenseService = expenseService ?? throw new ArgumentNullException(nameof(IExpenseService));
        }

        [HttpPost]
        public IActionResult Post(QueryRequestViewModel queryRequest)
        {
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }
            var result = _expenseService.Filter(queryRequest.ToDto());
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToKeyValuePair());
        }

        [HttpPost("States")]
        public IActionResult States(QueryRequestViewModel queryRequest)
        {
            if (queryRequest == null)
            {
                return BadRequest("Not data request");
            }
            var result = _expenseService.GetExpenseTicketStatus();
            if (result == null)
            {
                return NotFound("Not data found");
            }
            return Ok(result.ToKeyValuePair());
        }
    }
}