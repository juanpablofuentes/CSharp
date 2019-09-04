using Group.Salto.Common;
using Group.Salto.Common.Constants;
using Group.Salto.Common.Enums;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Helpers;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading;

namespace Group.Salto.SOM.Web.Controllers
{
    public class BaseController : BaseAPIController
    {
        public BaseController(ILoggingService loggingService, IConfiguration configuration, IHttpContextAccessor accessor) : base(loggingService, configuration, accessor) { }

        public string GetCookies(string key)
        {
            return this.GetCookie(key);
        }

        public ActionResult RedirectToErrorPage(string errorMessageTranslationKey = "DefaultError")
        {
            return RedirectToAction("Index", "Error",
                new
                {
                    errorMessage = errorMessageTranslationKey
                });
        }

        public void SetFeedbackTempData(string titleKey, string messageKey, FeedbackTypeEnum type)
        {
            var feedback = new FeedbackViewModel()
            {
                MessageKey = messageKey,
                FeedbackType = type,
                TitleKey = titleKey,
            };
            TempData.Put(AppTempDataKeys.FeedbackTempDataKey, feedback);
            LogFeedback(feedback);
        }

        public FeedbackViewModel GetFeedbackTempData()
        {
            return TempData.Get<FeedbackViewModel>(AppTempDataKeys.FeedbackTempDataKey);
        }

        public ResultViewModel<TViewModel> ProcessResult<TViewModel>(TViewModel data, IList<FeedbackViewModel> feedbacks = null)
        {
            var result = new ResultViewModel<TViewModel>()
            {
                Data = data,
                Feedbacks = feedbacks != null && feedbacks.Any()
                    ? new FeedbacksViewModel() { Feedbacks = feedbacks }
                    : null,
            };
            LogFeedbacks(feedbacks);
            return result;
        }

        public ResultViewModel<TViewModel> ProcessResult<TViewModel>(TViewModel data, string title, string message, FeedbackTypeEnum feedbackType)
        {
            var result = new ResultViewModel<TViewModel>()
            {
                Data = data,
                Feedbacks = !string.IsNullOrEmpty(title)
                    ? new FeedbacksViewModel()
                    {
                        Feedbacks = new List<FeedbackViewModel>()
                        {
                            new FeedbackViewModel()
                            {
                                FeedbackType = feedbackType,
                                MessageKey = message ?? string.Empty,
                                TitleKey = title,
                            }
                        }
                    }
                    : null,
            };
            LogFeedbacks(result.Feedbacks.Feedbacks);
            return result;
        }

        public void LogFeedbacks(IList<FeedbackViewModel> feedbacks)
        {
            if (feedbacks != null && feedbacks.Any())
            {
                foreach (var feedback in feedbacks)
                {
                    LogFeedback(feedback);
                }
            }
        }

        private void LogFeedback(FeedbackViewModel feedback)
        {
            var message = $"{TranslationHelper.GetText(feedback.TitleKey, AppConstants.DefaultCultureLog)}" +
                          $": {TranslationHelper.GetText(feedback.MessageKey, AppConstants.DefaultCultureLog)}";
            switch (feedback.FeedbackType)
            {
                case FeedbackTypeEnum.Error:
                    LoggingService.LogError(message);
                    break;
                case FeedbackTypeEnum.Info:
                    LoggingService.LogInfo(message);
                    break;
                case FeedbackTypeEnum.Success:
                    LoggingService.LogInfo(message);
                    break;
                case FeedbackTypeEnum.Warning:
                    LoggingService.LogWarning(message);
                    break;
            }
        }

        protected int GetNumberEntriesPerPage()
        {
            var result = 0;
            int.TryParse((HttpContext?.User?.Identity as ClaimsIdentity)
                ?.FindFirst(AppIdentityClaims.NumberEntriesPerPage)?.Value, out result);
            return result;
        }

        protected string GetTenantId()
        {
            string result = string.Empty;
            result = (HttpContext?.User?.Identity as ClaimsIdentity)?.FindFirst(AppIdentityClaims.TenantId)?.Value;

            return result;
        }

        protected string GetUserIdClaim()
        {
            string userIdValue = string.Empty;
            if (User.Identity is ClaimsIdentity claimsIdentity)
            {
                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim != null) userIdValue = userIdClaim.Value;
            }
            return userIdValue;
        }

        protected List<BreadcrumbViewModel> AddBreadcrumb(List<string> label)
        {
            var result = new List<BreadcrumbViewModel>();

            foreach (string item in label)
            {
                result.Add(new BreadcrumbViewModel()
                {
                    Label = item.GetTranslationsText()
                });
            }
            return result;
        }
    }
}