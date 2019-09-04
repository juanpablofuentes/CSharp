using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.WorkOrderCategory;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.EventCategories;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.ServiceLibrary.Common.Contracts.Sla;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderCategories;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderCategoriesCollection;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.MultiSelect;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Scheduler;
using Group.Salto.SOM.Web.Models.Technicians;
using Group.Salto.SOM.Web.Models.WorkOrderCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.WorkOrderCategory
{
    public class WorkOrderCategoryController : BaseController
    {
        private readonly IWorkOrderCategoriesService _workOrderCategoriesService;
        private readonly ISlaService _slaService;
        private readonly IEventCategoriesService _eventCategoriesService;
        private readonly IWorkOrderCategoriesCollectionService _workOrderCategoriesCollectionService;

        public WorkOrderCategoryController(ILoggingService loggingService,
                                           IConfiguration configuration,
                                           IHttpContextAccessor accessor,
                                           IWorkOrderCategoriesService workOrderCategoriesService,
                                           IWorkOrderCategoriesCollectionService workOrderCategoriesCollectionService,
                                           ISlaService slaService,
                                           IEventCategoriesService eventCategoriesService) : base(loggingService, configuration, accessor)
        {
            _workOrderCategoriesService = workOrderCategoriesService ?? throw new ArgumentNullException($"{nameof(IWorkOrderCategoriesService)} is null");
            _workOrderCategoriesCollectionService = workOrderCategoriesCollectionService ?? throw new ArgumentNullException($"{nameof(IWorkOrderCategoriesCollectionService)} is null");
            _slaService = slaService ?? throw new ArgumentNullException($"{nameof(ISlaService)} is null");
            _eventCategoriesService = eventCategoriesService ?? throw new ArgumentNullException($"{nameof(IEventCategoriesService)} is null ");
        }

        [CustomAuthorization(ActionGroupEnum.WOCategory, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"WorkOrderCategory.Get get all ");
            var result = DoFilterAndPaging(new WorkOrderCategoryFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.WOCategory, ActionEnum.Create)]
        public async Task<IActionResult> Create()
        {
            LoggingService.LogInfo("WorkOrderCategory.Create");
            var response = new ResultViewModel<WorkOrderCategoryDetailViewModel>()
            {
                Data = await FillData(new WorkOrderCategoryDetailViewModel(), ModeActionTypeEnum.Create)
            };

            return View(response);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.WOCategory, ActionEnum.GetById)]
        public async Task<IActionResult> Edit(int id)
        {
            LoggingService.LogInfo($"WorkOrderCategory.Get get workOrderCategory for id:{id}");
            var result = _workOrderCategoriesService.GetById(id);
            var response = result.ToViewModel();
            await FillData(response.Data, ModeActionTypeEnum.Edit);
            LogFeedbacks(response.Feedbacks?.Feedbacks);
            if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
            {
                return View("Edit", response);
            }
            SetFeedbackTempData(LocalizationsConstants.Error, LocalizationsConstants.ErrorLoadingDataMessage, FeedbackTypeEnum.Error);
            return Redirect(nameof(Index));
        }

        [HttpPost]
        public IActionResult Filter(WorkOrderCategoryFilterViewModel filter)
        {
            var resultData = DoFilterAndPaging(filter);
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                resultData.Feedbacks = resultData.Feedbacks ?? new FeedbacksViewModel();
                resultData.Feedbacks.AddFeedback(feedback);
            }
            return View(nameof(Index), resultData);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.WOCategory, ActionEnum.Create)]
        public async Task<IActionResult> Create(WorkOrderCategoryDetailViewModel workOrderCategory)
        {
            LoggingService.LogInfo("WorkOrderCategory.Post workOrderCategory");
            if (ModelState.IsValid)
            {
                var result = _workOrderCategoriesService.Create(workOrderCategory.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle, WorkOrderCategoryConstants.WOCategoryCreateSuccessMessage, FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View(resultData);
            }
            return await ModelInvalid(ModeActionTypeEnum.Create, workOrderCategory);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.WOCategory, ActionEnum.Update)]
        public async Task<IActionResult> Edit(WorkOrderCategoryDetailViewModel workOrderCategory)
        {
            LoggingService.LogInfo($"workOrderCategory update for id = {workOrderCategory.GenericEditViewModel.Id}");
            if (ModelState.IsValid)
            {
                var result = _workOrderCategoriesService.Update(workOrderCategory.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle, WorkOrderCategoryConstants.WOCategoryUpdateSuccessMessage, FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View(resultData);
            }
            return await ModelInvalid(ModeActionTypeEnum.Edit, workOrderCategory);
        }

        [HttpDelete]
        [CustomAuthorization(ActionGroupEnum.WOCategory, ActionEnum.Delete)]
        public bool Delete(int id)
        {
            var deleteResult = _workOrderCategoriesService.Delete(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    WorkOrderCategoryConstants.WorkOrderCategoryDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                    WorkOrderCategoryConstants.WorkOrderCategoryDeleteMessage,
                    FeedbackTypeEnum.Error);
            }
            return deleteResult.Data;
        }

        private async Task<IActionResult> ModelInvalid(ModeActionTypeEnum modeAction, WorkOrderCategoryDetailViewModel workOrderCategory)
        {
            return View(modeAction.ToString(), ProcessResult(await FillData(workOrderCategory, modeAction), new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = LocalizationsConstants.FormErrorsMessage,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            }));
        }

        private async Task<WorkOrderCategoryDetailViewModel> FillData(WorkOrderCategoryDetailViewModel source, ModeActionTypeEnum modeAction)
        {
            source.ModeActionType = modeAction;
            source.GenericEditViewModel.SLAListItems = _slaService.GetAllKeyValues().ToSelectList();
            int? workOrderCategoryId = null;
            if (modeAction == ModeActionTypeEnum.Edit)
            {
                workOrderCategoryId = source.GenericEditViewModel.Id;
                source.SchedulerViewModel = new SchedulerViewModel() { CalendarEventCategory = _eventCategoriesService.GetAllKeyValuesNotDeleted() };
            }

            MultiSelectViewModel permissions = new MultiSelectViewModel(WorkOrderCategoryConstants.WOCategoryPermissionsCategoryPermissions);
            permissions.Items = _workOrderCategoriesService.GetPermissionList(workOrderCategoryId).Data.ToViewModel();
            source.Permissions = permissions;

            MultiSelectViewModel roles = new MultiSelectViewModel(WorkOrderCategoryConstants.WOCategoryPermissionsCategoryPermissionsRoles);
            var data = await _workOrderCategoriesService.GetPermissionRoleList(workOrderCategoryId);
            roles.Items = data.Data.ToViewModel();
            source.Roles = roles;

            source.GenericEditViewModel.WOCollectionListItems = _workOrderCategoriesCollectionService.GetAllKeyValues().ToSelectList();

            return source;
        }

        private ResultViewModel<WorkOrderCategoryListViewModel> DoFilterAndPaging(WorkOrderCategoryFilterViewModel filter)
        {
            var data = new ResultViewModel<WorkOrderCategoryListViewModel>();
            var filteredData = _workOrderCategoriesService.GetAllFiltered(filter.ToFilterDto()).Data.ToListViewModel();

            if (filter.Page != 0)
            {
                filter.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<WorkOrderCategoryBaseViewModel>()
                {
                    Page = filter.Page,
                    PageSize = filter.Size
                };
                data.Data = new WorkOrderCategoryListViewModel()
                {
                    WorkOrderCategoriesList = new MultiItemViewModel<WorkOrderCategoryBaseViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filter.PagesCount = pager.PagesCount;
                filter.Page = pager.Page;
                filter.TotalValues = filteredData.Count;
            }
            else
            {
                data.Data = new WorkOrderCategoryListViewModel()
                {
                    WorkOrderCategoriesList = new MultiItemViewModel<WorkOrderCategoryBaseViewModel, int>(filteredData)
                };
            }
            data.Data.Filters = filter;
            return data;
        }
    }
}