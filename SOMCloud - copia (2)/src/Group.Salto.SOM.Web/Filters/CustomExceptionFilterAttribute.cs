using System.Net;
using Group.Salto.Log;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Group.Salto.SOM.Web.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private ILoggingService _loggingService;
        private readonly IModelMetadataProvider _modelMetadataProvider;
        
        public CustomExceptionFilterAttribute(
            IModelMetadataProvider modelMetadataProvider)
        {
            _modelMetadataProvider = modelMetadataProvider;
        }

        private ILoggingService GetLogginService(ExceptionContext context)
        {
            _loggingService = _loggingService ?? (ILoggingService)context.HttpContext.RequestServices.GetService(typeof(ILoggingService));
            return _loggingService;
        }

        public override void OnException(ExceptionContext context)
        {
            //TODO review error settings to return
            context.Result = new RedirectToActionResult("Index", "Error", new
            {
                errorMessage = context.Exception.Message,
                ErrorCode = HttpStatusCode.InternalServerError,
            });
            GetLogginService(context).LogError(context.Exception.Message);
        }
    }
}
