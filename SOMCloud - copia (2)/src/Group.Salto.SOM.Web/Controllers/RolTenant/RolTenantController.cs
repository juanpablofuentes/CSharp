using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.RolTenant;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.RolesTenant;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.RolesTenant;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.RolTenant;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Group.Salto.SOM.Web.Controllers.RolTenant
{
    public class RolTenantController : BaseController
    {
        private readonly IRolTenantService _rolTenantService;

        public RolTenantController(ILoggingService loggingService, 
            IConfiguration configuration, 
            IHttpContextAccessor accessor,
            IRolTenantService rolTenantService) 
            : base(loggingService, configuration, accessor)
        {
            _rolTenantService = rolTenantService ?? throw new ArgumentNullException($"{nameof(rolTenantService)} is null");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.RoleTenant, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Rol Tenant - Get Index action");
            var result = DoFilterAndPaging(new RolTenantFilterViewModel());
            var feedback = GetFeedbackTempData();

            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.RoleTenant, ActionEnum.Create)]
        public IActionResult Create()
        {
            LoggingService.LogInfo("Rol Tenant - Create Get");
            ResultViewModel<RolTenantViewModel> response = ToCreateResultViewModel();
            return View(response);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.RoleTenant, ActionEnum.GetById)]
        public IActionResult Edit(int Id)
        {
            LoggingService.LogInfo($"Rol Tenant - Edit Get {Id}");
            ResultViewModel<RolTenantViewModel> response = ToEditResultViewModel(Id);
            return View(response);
        }

        [HttpPost]
        public IActionResult Filter(RolTenantFilterViewModel filter)
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

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.RoleTenant, ActionEnum.Create)]
        public IActionResult Create(RolTenantViewModel rolTenant)
        {
            LoggingService.LogInfo($"Rol Tenant - Create Post");
            if (ModelState.IsValid)
            {
                RolTenantDto rolTenantDto = rolTenant.ToRolTenantDto();
                var result = _rolTenantService.Create(rolTenantDto);

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle, RolTenantConstants.RolTenantCreatedSuccess, FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }

                var resultData = ToCreateResultViewModel();
                resultData.Feedbacks = result.Errors.ToViewModel();
                LogFeedbacks(resultData.Feedbacks.Feedbacks);

                return View("Create", resultData);
            }

            var data = ToCreateResultViewModel();
            return View("Create", ProcessResult(data.Data, new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = RolTenantConstants.RolTenantCreateError,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            }));
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.RoleTenant, ActionEnum.Update)]
        public IActionResult Edit(RolTenantViewModel rolTenant)
        {
            LoggingService.LogInfo($"Rol Tenant - Edit Post");
            if (ModelState.IsValid)
            {
                RolTenantDto rolDto = rolTenant.ToRolTenantDto();
                var result = _rolTenantService.Update(rolDto);

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle, RolTenantConstants.RolTenantUpdateSuccess, FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }

                var resultData = ToEditResultViewModel(rolTenant.Id);
                resultData.Feedbacks = result.Errors.ToViewModel();
                LogFeedbacks(resultData.Feedbacks.Feedbacks);

                return View("Edit", resultData);
            }

            var editData = ToEditResultViewModel(rolTenant.Id);
            return View("Edit", ProcessResult(editData.Data, new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = RolTenantConstants.RolTenantUpdateError,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            }));
        }

        private ResultViewModel<RolTenantListViewModel> DoFilterAndPaging(RolTenantFilterViewModel filters)
        {
            var data = new ResultViewModel<RolTenantListViewModel>();
            var filteredData = _rolTenantService.GetListFiltered(filters.ToDto()).Data.ToRolTenantListViewModel();
            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<RolTenantViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new RolTenantListViewModel()
                {
                    RolesTenant = new MultiItemViewModel<RolTenantViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new RolTenantListViewModel()
                {
                    RolesTenant = new MultiItemViewModel<RolTenantViewModel, int>(filteredData)
                };
            }
            data.Data.RolTenantFilters = filters;
            return data;
        }

        private ResultViewModel<RolTenantViewModel> ToCreateResultViewModel()
        {
            ResultViewModel<RolTenantViewModel> response = new ResultViewModel<RolTenantViewModel>();
            response.Data = new RolTenantViewModel();
            //TODO Carmen. Falta ternaria
            return response;
        }

        private ResultViewModel<RolTenantViewModel> ToEditResultViewModel(int Id)
        {
            ResultViewModel<RolTenantViewModel> response = new ResultViewModel<RolTenantViewModel>();
            ResultDto<RolTenantDto> rolTenant = _rolTenantService.GetById(Id);
            response.Data = rolTenant.Data.ToRolTenantViewModel();
            //TODO Carmen. Falta ternaria
            return response;
        }
    }
}