using Group.Salto.Log;
using Group.Salto.SOM.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Group.Salto.SOM.Web.Controllers
{
    public class ErrorController : BaseController
    {
        public ErrorController(ILoggingService loggingService, IConfiguration configuration, IHttpContextAccessor accessor)
                                : base(loggingService, configuration, accessor)
        {
        }

        public IActionResult Index(string errorMessage, int? errorCode)
        {
            var vm = new ErrorViewModel
            {
                ErrorCode = errorCode ?? 0,
                Message = errorMessage
            };
            return View(ProcessResult(vm));
        }

    }
}
