using Group.Salto.ServiceLibrary.Common.Contracts.Country;
using Group.Salto.ServiceLibrary.Common.Contracts.Expense;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Expense;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Group.Salto.SOM.Mobility.Controllers.Expenses
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExpensesController : BaseController
    {
        private readonly IExpenseService _expenseService;

        public ExpensesController(ICountryService countryService, 
            IPeopleService peopleService, 
            IConfiguration configuration,
            IExpenseService expenseService) :  base(configuration)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        [ActionName("GetExpensesFromAppUser")]
        public IActionResult GetAllExpensesFromAppUser()
        {
            var peopleConfigId = GetUserConfigId();
            var result = _expenseService.GetExpensesFromAppUser(peopleConfigId);
            return Ok(result);
        }

        [HttpGet]
        [ActionName("GetBasicInfo")]
        public IActionResult GetBasicInfo()
        {
            ExpensesBasicFiltersInfoDto res = _expenseService.GetBasicFiltersInfo();
            return Ok(res);
        }

        [HttpPost]
        [ActionName("AddExpense")]
        public IActionResult AddExpense(ExpenseTicketExtDto expenseTicketExtDto)
        {
            var peopleConfigId = GetUserConfigId();
            var result = _expenseService.AddExpense(expenseTicketExtDto, peopleConfigId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ActionName("GetFileFromExpense")]
        public IActionResult GetFileFromExpense(int id)
        {
            var res = _expenseService.GetFileFromExpense(id);
            return Ok(res);
        }

        [HttpPost]
        [ActionName("AddExpenseFile")]
        public IActionResult AddExpenseFile(RequestFileDto requestFileDto)
        {
            var result = _expenseService.AddFileToExpense(requestFileDto);
            return Ok(result);
        }
    }
}