using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Permissions;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Language;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.ServiceLibrary.Common.Contracts.Permisions;
using Group.Salto.ServiceLibrary.Common.Contracts.PredefinedServices;
using Group.Salto.ServiceLibrary.Common.Contracts.Project;
using Group.Salto.ServiceLibrary.Common.Contracts.Queue;
using Group.Salto.ServiceLibrary.Common.Contracts.Tasks;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderCategories;
using Group.Salto.ServiceLibrary.Common.Dtos.Permisions;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Permissions;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.SOM.Web.Controllers
{
    public class PermissionsController : BaseLanguageController
    {
        private readonly IPeopleService _peopleService;
        private readonly IQueueService _queueService;
        private readonly ITasksService _tasksService;
        private readonly IProjectsService _projectService;
        private readonly IPredefinedServiceService _predefinedServiceService;
        private readonly IWorkOrderCategoriesService _workOrderCategoriesService;
        private readonly IPermissionsService _permissionsService;

        public PermissionsController(ILoggingService loggingService,
                                    IConfiguration configuration,
                                    IHttpContextAccessor accessor,
                                    IPeopleService peopleService,
                                    IQueueService queueService,
                                    ITasksService tasksService,
                                    IProjectsService projectService,
                                    IPredefinedServiceService predefinedServiceService,
                                    IWorkOrderCategoriesService workOrderCategoriesService,
                                    ILanguageService languageService,
                                    IPermissionsService permissionsService) : base(loggingService, configuration, accessor, languageService)
        {
            _peopleService = peopleService ?? throw new ArgumentNullException($"{nameof(IPeopleService)} is null");
            _queueService = queueService ?? throw new ArgumentNullException($"{nameof(IQueueService)} is null");
            _tasksService = tasksService ?? throw new ArgumentNullException($"{nameof(ITasksService)} is null");
            _projectService = projectService ?? throw new ArgumentNullException($"{nameof(IProjectsService)} is null");
            _predefinedServiceService = predefinedServiceService ?? throw new ArgumentNullException($"{nameof(IPredefinedServiceService)} is null");
            _workOrderCategoriesService = workOrderCategoriesService ?? throw new ArgumentNullException($"{nameof(IWorkOrderCategoriesService)} is null");
            _permissionsService = permissionsService ?? throw new ArgumentNullException($"{nameof(IPermissionsService)} is null");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Permisions, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Permissions -- Get get all");
            var result = DoFilterAndPaging(new PermissionsFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Permisions, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($"Permissions -- detail id: {id}");
            var result = _permissionsService.GetById(id);
            var response = result.ToResultViewModel();
            FillCombosData(response?.Data, result?.Data);
            LogFeedbacks(response?.Feedbacks?.Feedbacks);
            if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
            {
                return View("Edit", response);
            }
            SetFeedbackTempData(LocalizationsConstants.Error,
                LocalizationsConstants.ErrorLoadingDataMessage,
                FeedbackTypeEnum.Error);
            return Redirect(nameof(Index));
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Permisions, ActionEnum.Update)]
        public IActionResult Edit(PermissionDetailViewModel model)
        {
            LoggingService.LogInfo($"Permissions update for id ={model.Id}");
            if (ModelState.IsValid)
            {
                var result = _permissionsService.Update(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        PermissionsConstants.PermissionsSuccessUpdateMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction(nameof(Index));
                }

                var resultData = ProcessResult(model, result.Errors.ToViewModel()?.Feedbacks);
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View(nameof(Edit), resultData);
            }
            return ModelInvalid(nameof(Edit), model);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Permisions, ActionEnum.Create)]
        public IActionResult Create()
        {
            var result = new PermissionDetailViewModel();
            FillCombosData(result);
            return View(ProcessResult(result));
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Permisions, ActionEnum.Create)]
        public IActionResult Create(PermissionDetailViewModel model)
        {
            LoggingService.LogInfo("Permission create");
            if (ModelState.IsValid)
            {
                var result = _permissionsService.Create(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        PermissionsConstants.PermissionsSuccessCreateMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction(nameof(Index));
                }
                var resultData = ProcessResult(model, result.Errors.ToViewModel()?.Feedbacks);
                return View(nameof(Create), resultData);
            }
            return ModelInvalid(nameof(Create), model);
        }

        [HttpPost]
        public IActionResult Filter(PermissionsFilterViewModel filter)
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

        [HttpDelete]
        [CustomAuthorization(ActionGroupEnum.Permisions, ActionEnum.Delete)]
        public bool Delete(int id)
        {
            var deleteResult = _permissionsService.Delete(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    PermissionsConstants.PermissionsDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                    PermissionsConstants.PermissionsDeleteErrorMessage,
                    FeedbackTypeEnum.Error);
            }
            return deleteResult.Data;
        }

        private IActionResult ModelInvalid(string view, PermissionDetailViewModel workOrderStatus)
        {
            return View(view, ProcessResult(workOrderStatus, new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = LocalizationsConstants.FormErrorsMessage,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            }));
        }

        private ResultViewModel<PermissionsListViewModel> DoFilterAndPaging(PermissionsFilterViewModel filters)
        {
            var data = new ResultViewModel<PermissionsListViewModel>();
            var filteredData = _permissionsService.GetAllFiltered(filters.ToFilterDto()).Data.ToViewModel();

            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<PermissionListViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new PermissionsListViewModel()
                {
                    Permissions = new MultiItemViewModel<PermissionListViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new PermissionsListViewModel()
                {
                    Permissions = new MultiItemViewModel<PermissionListViewModel, int>(filteredData)
                };
            }

            data.Data.PermissionsFilter = filters;
            return data;
        }

        private void FillCombosData(PermissionDetailViewModel model, PermissionDetailDto data = null)
        {
            if (model != null)
            {
                var people = _peopleService.GetAllActiveKeyValue();
                var tasks = _tasksService.GetAllKeyValues();
                var projects = _projectService.GetAllKeyValues();
                var workOrderCategories = _workOrderCategoriesService.GetAllKeyValues();
                var predefinedServices = _predefinedServiceService.GetAllKeyValue();
                var queues = _queueService.GetAllKeyValues(GetLanguageId());
                model.People = people.SetSelectedValues(data?.People, PermissionsConstants.PermissionsPeopleMultiSelectorTitle);
                model.Tasks = tasks.SetSelectedValues(data?.Tasks, PermissionsConstants.PermissionsTasksMultiSelectorTitle);
                model.Projects = projects.SetSelectedValues(data?.Projects, PermissionsConstants.PermissionsProjectsMultiSelectorTitle);
                model.WorkOrderCategories = workOrderCategories.SetSelectedValues(data?.WorkOrdersCategories, PermissionsConstants.PermissionsWorkOrderCategoriesMultiSelectorTitle);
                model.PredefinedServices = predefinedServices.SetSelectedValues(data?.PredefinedServices, PermissionsConstants.PermissionsPredefinedServicesMultiSelectorTitle);
                model.Queues = queues.SetSelectedValues(data?.Queues, PermissionsConstants.PermissionsQueuesMultiSelectorTitle);
            }
        }
    }
}