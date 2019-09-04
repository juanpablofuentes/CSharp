using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.TenantConfiguration;
using Group.Salto.Common.Constants.UserOptions;
using Group.Salto.Common.Enums;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Idantity;
using Group.Salto.ServiceLibrary.Common.Contracts.Language;
using Group.Salto.ServiceLibrary.Common.Contracts.TenantConfiguration;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.UserOptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Group.Salto.SOM.Web.Controllers.UserOptions
{
    [Authorize]
    public class UserOptionsController : IdentityController
    {
        private readonly ILanguageService _languageService;
        private readonly ITenantConfigurationService _tenantConfigurationService;

        public UserOptionsController(IIdentityService identityService,
                                        ILoggingService loggingService,
                                        IConfiguration configuration,
                                        ILanguageService languageService,
                                        IHttpContextAccessor accessor,
                                        ITenantConfigurationService tenantConfigurationService) : base(identityService, loggingService, accessor, configuration)
        {
            _languageService = languageService ?? throw new ArgumentNullException(nameof(ILanguageService));
            _tenantConfigurationService = tenantConfigurationService ?? throw new ArgumentNullException(nameof(ITenantConfigurationService));
        }

        public async Task<IActionResult> Details()
        {
            var userIdClaim = this.GetUserIdClaim();
            LoggingService.LogInfo($"UserOptionsController.Get detail action for id = {userIdClaim}");
            var userOptionsViewModel = new ResultViewModel<UserOptionsViewModel>();
            var user = await _identityService.FindUserOptionById(userIdClaim);
            var feedBack = GetFeedbackTempData();
            if (user != null && (user.Errors?.Errors == null || !user.Errors.Errors.Any()))
            {
                var userDataViewmodel = user.Data.ToViewModel();
                FillData(userDataViewmodel);
                userOptionsViewModel.Data = userDataViewmodel;
            }
            else userOptionsViewModel.Data = FillData(new UserOptionsViewModel());
            if (feedBack != null)
            {
                userOptionsViewModel.Feedbacks = userOptionsViewModel.Feedbacks ?? new FeedbacksViewModel();
                userOptionsViewModel.Feedbacks.AddFeedback(feedBack);
            }
            return View(userOptionsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ResultViewModel<UserOptionsViewModel> model)
        {
            LoggingService.LogInfo($"UserOptionsController.Post Update action");
            string title = LocalizationsConstants.Error;
            string message = LocalizationsConstants.FormErrorsMessage;
            FeedbackTypeEnum feedback = FeedbackTypeEnum.Error;
            FillData(model.Data);
            if (ModelState.IsValid)
            {
                var resultUpdate = await _identityService.UpdateUser(model.ToUserOptionsDto());
                if (string.IsNullOrEmpty(resultUpdate.ErrorMessageKey))
                {
                    await RefreshUserLanguage();
                    await RefreshSignIn();
                    LoggingService.LogInfo($"UserOptionsController.Post Updated action with id = {model.Data.Id}");
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        UserOptionsConstants.UserOptionsUpdateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Details");
                }
                else
                {
                    LoggingService.LogInfo($"UserOptionsController. Error post updated action with id = {model.Data.Id}");
                    message = resultUpdate.ErrorMessageKey;
                }
            }
            var result = ProcessResult(FillData(model.Data), title, message, feedback);
            return View("Details", result);
        }

        private UserOptionsViewModel FillData(UserOptionsViewModel model)
        {
            model.Languages = _languageService.GetAll().Data.ToSelectListItemEnumerable();
            var numberEntriesPerPage = _tenantConfigurationService.GetValueByGroup(TenantConfigurationConstants.NumberEntriesPerPage).ToList();
            List<SelectListItem> numberEntriesPerPageList = new List<SelectListItem>();
            numberEntriesPerPage.ForEach(value => numberEntriesPerPageList.Add(new SelectListItem() { Text = value, Value = value }));
            model.NumberEntriesPerPage = numberEntriesPerPageList;
            return model;
        }
    }
}