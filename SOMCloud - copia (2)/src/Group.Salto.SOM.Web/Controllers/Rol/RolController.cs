using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Rol;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ActionsRoles;
using Group.Salto.ServiceLibrary.Common.Contracts.Rol;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Rols;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Actions;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.MultiSelect;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Rol;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.Rol
{
    public class RolController : BaseController
    {
        private readonly IRolService _rolService;
        private readonly IActionsRolesService _actionRolService;

        public RolController(ILoggingService loggingService,
                                IConfiguration configuration,
                                IRolService rolService,
                                IHttpContextAccessor accessor,
                                IActionsRolesService actionRolService) : base(loggingService, configuration, accessor)
        {
            _rolService = rolService ?? throw new ArgumentNullException($"{nameof(rolService)} is null");
            _actionRolService = actionRolService ?? throw new ArgumentNullException($"{nameof(actionRolService)} is null");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Roles, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Rol.Get Index action");
            var result = DoFilterAndPaging(new RolFilterViewModel());
            var feedback = GetFeedbackTempData();

            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }

            return View(result);
        }

        [HttpPost]
        public IActionResult Filter(RolFilterViewModel filter)
        {
            var result = DoFilterAndPaging(filter);
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(nameof(Index), result);
        }        

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Roles, ActionEnum.Create)]
        public async Task<IActionResult> Create()
        {
            LoggingService.LogInfo("Rol.Create Get");
            ResultViewModel<RolEditViewModel> response = await ToCreateResultViewModel();
            return View(response);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Roles, ActionEnum.GetById)]
        public async Task<IActionResult> Edit(int Id)
        {
            LoggingService.LogInfo($"Rol.Edit Get {Id}");
            ResultViewModel<RolEditViewModel> response = await ToEditResultViewModel(Id);
            return View(response);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Roles, ActionEnum.Create)]
        public async Task<IActionResult> Create(RolEditViewModel rolData)
        {
            LoggingService.LogInfo($"Rol.Create Post");
            if (ModelState.IsValid)
            {
                RolDto rolDto = rolData.ToRolDto();
                var result = await _rolService.CreateRol(rolDto);

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle, RolConstants.RolCreatedSuccess, FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }

                var resultData = await ToCreateResultViewModel();
                resultData.Feedbacks = result.Errors.ToViewModel();
                LogFeedbacks(resultData.Feedbacks.Feedbacks);

                return View("Create", resultData);
            }

            var data = await ToCreateResultViewModel();
            return View("Create", ProcessResult(data.Data, new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = RolConstants.RolCreateError,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            }));
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Roles, ActionEnum.Update)]
        public async Task<IActionResult> Edit(RolEditViewModel rolData)
        {
            LoggingService.LogInfo($"Rol.Edit Post");
            if (ModelState.IsValid)
            {
                RolDto rolDto = rolData.ToRolDto();
                var result = await _rolService.UpdateRol(rolDto);

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle, RolConstants.RolUpdateSuccess, FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }

                var resultData = await ToEditResultViewModel(rolData.Id);
                resultData.Feedbacks = result.Errors.ToViewModel();
                LogFeedbacks(resultData.Feedbacks.Feedbacks);

                return View("Edit", resultData);
            }

            var editData = await ToEditResultViewModel(rolData.Id);
            return View("Edit", ProcessResult(editData.Data, new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = RolConstants.RolUpdateError,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            }));
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Roles, ActionEnum.Delete)]
        public async Task<bool> Delete(int Id)
        {
            LoggingService.LogInfo($"Rol.Delete Post {Id}");
            var deleteResult = await _rolService.DeleteRol(Id);

            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle, RolConstants.RolDeleteSuccessMessage, FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(deleteResult.Errors.Errors.FirstOrDefault().ErrorType.ToString(), deleteResult.Errors.Errors.FirstOrDefault().ErrorMessageKey, FeedbackTypeEnum.Error);
            }

            return deleteResult.Data;
        }

        private ResultViewModel<RolsViewModel> DoFilterAndPaging(RolFilterViewModel filters)
        {
            var data = new ResultViewModel<RolsViewModel>();

            var filteredData = _rolService.GetListFiltered(filters.ToDto()).Data.ToViewModel();

            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<RolViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new RolsViewModel()
                {
                    Roles = new MultiItemViewModel<RolViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new RolsViewModel()
                {
                    Roles = new MultiItemViewModel<RolViewModel, int>(filteredData)
                };
            }

            data.Data.RolFilters = filters;

            return data;
        }

        private async Task<ResultViewModel<RolEditViewModel>> ToCreateResultViewModel()
        {
            ResultViewModel<RolEditViewModel> response = new ResultViewModel<RolEditViewModel>();
            response.Data = new RolEditViewModel();
            response.Data.ActionRoles = new MultiSelectViewModel(RolConstants.ListActionRoles);
            var items = await _actionRolService.GetActionsRoles(null);
            response.Data.ActionRoles.Items = items.Data.ToViewModel();
            return response;
        }

        private async Task<ResultViewModel<RolEditViewModel>> ToEditResultViewModel(int Id)
        {
            ResultViewModel<RolEditViewModel> response = new ResultViewModel<RolEditViewModel>();
            ResultDto<RolDto> rol = await _rolService.GetById(Id);
            response.Data = rol.Data.ToEditViewModel();
            response.Data.ActionRoles = new MultiSelectViewModel(RolConstants.ListActionRoles);
            var item = await _actionRolService.GetActionsRoles(Id);
            response.Data.ActionRoles.Items = item.Data.ToViewModel();
            return response;
        }
    }
}