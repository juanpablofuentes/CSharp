using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Project;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ClosureCode;
using Group.Salto.ServiceLibrary.Common.Contracts.CollectionsExtraField;
using Group.Salto.ServiceLibrary.Common.Contracts.CollectionTypeWorkOrders;
using Group.Salto.ServiceLibrary.Common.Contracts.Contracts;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.ServiceLibrary.Common.Contracts.EventCategories;
using Group.Salto.ServiceLibrary.Common.Contracts.Project;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderCategoriesCollection;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderStatus;
using Group.Salto.ServiceLibrary.Common.Dtos.Projects;
using Group.Salto.ServiceLibrary.Implementations.ClosureCode;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.MultiSelect;
using Group.Salto.SOM.Web.Models.Projects;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Scheduler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.ServiceLibrary.Common.Contracts.Language;
using Group.Salto.ServiceLibrary.Common.Contracts.Queue;
using Group.Salto.SOM.Web.Filters;

namespace Group.Salto.SOM.Web.Controllers.Projects
{
    public class ProjectsController : BaseLanguageController
    {
        private readonly IProjectsService _projectService;
        private readonly IEventCategoriesService _eventCategoriesService;
        private readonly IContractsService _contractsService;
        private readonly IPeopleService _peopleService;
        private readonly IWorkOrderStatusService _workOrderStatusService;
        private readonly ICollectionsExtraFieldService _collectionsExtraFieldService;
        private readonly IClosureCodeService _collectionsClosureCodesService;
        private readonly ICollectionTypeWorkOrdersService _collectionTypeWorkOrdersService;
        private readonly IWorkOrderCategoriesCollectionService _workOrderCategoriesCollectionService;
        private readonly IQueueService _queueService;

        public ProjectsController(
           ILoggingService loggingService,
           IHttpContextAccessor accessor,
           IConfiguration configuration,
           ILanguageService languageService,
           IProjectsService projectService,
           IEventCategoriesService eventCategoriesService,
           IContractsService contractsService,
           IPeopleService peopleService,
           IWorkOrderStatusService workOrderStatusService,
           ICollectionsExtraFieldService collectionsExtraFieldService,
           IClosureCodeService collectionsClosureCodesService,
           ICollectionTypeWorkOrdersService collectionTypeWorkOrdersService,
           IWorkOrderCategoriesCollectionService workOrderCategoriesCollectionService,
           IQueueService queueService) : base(loggingService, configuration, accessor, languageService)
        {
            _projectService = projectService ?? throw new ArgumentNullException($"{nameof(IProjectsService)} is null");
            _eventCategoriesService = eventCategoriesService ?? throw new ArgumentNullException($"{nameof(IEventCategoriesService)} is null ");
            _contractsService = contractsService ?? throw new ArgumentNullException($"{nameof(IContractsService)} is null");
            _peopleService = peopleService ?? throw new ArgumentNullException($"{nameof(IPeopleService)} is null");
            _workOrderStatusService = workOrderStatusService ?? throw new ArgumentNullException($"{nameof(IWorkOrderStatusService)} is null");
            _collectionsExtraFieldService = collectionsExtraFieldService ?? throw new ArgumentNullException($"{nameof(ICollectionsExtraFieldService)} is null");
            _collectionsClosureCodesService = collectionsClosureCodesService ?? throw new ArgumentNullException($"{nameof(ClosureCodeService)} is null");
            _collectionTypeWorkOrdersService = collectionTypeWorkOrdersService ?? throw new ArgumentNullException($"{nameof(ICollectionTypeWorkOrdersService)} is null");
            _workOrderCategoriesCollectionService = workOrderCategoriesCollectionService ?? throw new ArgumentNullException($"{nameof(IWorkOrderCategoriesCollectionService)} is null");
            _queueService = queueService ?? throw new ArgumentNullException($"{nameof(IQueueService)} is null");
        }

        [CustomAuthorization(ActionGroupEnum.Project, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Projects.Get get all Projects");
            var result = DoFilterAndPaging(new ProjectsFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpPost]
        public IActionResult Filter(ProjectsFilterViewModel filter)
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

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Project, ActionEnum.Create)]
        public IActionResult Create()
        {
            LoggingService.LogInfo("Projects.Create Get");
            var response = new ResultViewModel<ProjectsDetailViewModel>()
            {
                Data = FillData(new ProjectsDetailViewModel(), ModeActionTypeEnum.Create)
            };

            return View(response);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Project, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo("Projects.Edit Get for id:{id}");
            var result = _projectService.GetById(id);
            var response = result.ToViewModel();
            FillData(response.Data, ModeActionTypeEnum.Edit);
            LogFeedbacks(response.Feedbacks?.Feedbacks);
            if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
            {
                return View("Edit", response);
            }
            SetFeedbackTempData(LocalizationsConstants.Error, LocalizationsConstants.ErrorLoadingDataMessage, FeedbackTypeEnum.Error);
            return Redirect(nameof(Index));
        }
        
        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Project, ActionEnum.Create)]
        public IActionResult Create(ProjectsDetailViewModel projectDetail)
        {
            LoggingService.LogInfo("Projects.Post Create");
            if (ModelState.IsValid)
            {
                var result = _projectService.Create(projectDetail.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle, ProjectConstants.ProjectCreateSuccessMessage, FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                FillData(resultData.Data, ModeActionTypeEnum.Create);
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View(resultData);
            }
            return ModelInvalid(ModeActionTypeEnum.Create, projectDetail);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Project, ActionEnum.Update)]
        public IActionResult Edit(ProjectsDetailViewModel projectDetail)
        {
            LoggingService.LogInfo($"projects update for id = {projectDetail.GenericDetailViewModel.Id}");
            if (ModelState.IsValid)
            {
                var result = _projectService.Update(projectDetail.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle, ProjectConstants.ProjectUpdateSuccessMessage, FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                FillData(resultData.Data, ModeActionTypeEnum.Edit);
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View(resultData);
            }
            return ModelInvalid(ModeActionTypeEnum.Edit, projectDetail);
        }

        [HttpDelete]
        [CustomAuthorization(ActionGroupEnum.Project, ActionEnum.Delete)]
        public bool Delete(int id)
        {
            var deleteResult = _projectService.Delete(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    ProjectConstants.ProjectDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                    ProjectConstants.ProjectDeleteErrorMessage,
                    FeedbackTypeEnum.Error);
            }
            return deleteResult.Data;
        }

        private IActionResult ModelInvalid(ModeActionTypeEnum modeAction, ProjectsDetailViewModel projectDetail)
        {
            return View(modeAction.ToString(), ProcessResult(FillData(projectDetail, modeAction), new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = LocalizationsConstants.FormErrorsMessage,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            }));
        }

        private ProjectsDetailViewModel FillData(ProjectsDetailViewModel source, ModeActionTypeEnum modeAction)
        {
            source.ModeActionType = modeAction;
            source.GenericDetailViewModel.ContractListItems = _contractsService.GetAllKeyValues().ToSelectList();
            source.GenericDetailViewModel.ProjectManagerListItems = _peopleService.GetPeopleManagerKeyValues(new ServiceLibrary.Common.Dtos.People.PeopleFilterDto()).ToSelectList();
            source.GenericDetailViewModel.WOStatusestListItems = _workOrderStatusService.GetAllKeyValues(GetLanguageId()).ToSelectList();
            source.GenericDetailViewModel.ExtraFieldsCollectionListItems = _collectionsExtraFieldService.GetAllKeyValues().ToSelectList();
            source.GenericDetailViewModel.ClosingCodesCollectionListItems = _collectionsClosureCodesService.GetAllKeyValues().ToSelectList();
            source.GenericDetailViewModel.WOTypeCollectionListItems = _collectionTypeWorkOrdersService.GetAllKeyValues().ToSelectList();
            source.GenericDetailViewModel.WOCategoriesCollectionListItems = _workOrderCategoriesCollectionService.GetAllKeyValues().ToSelectList();
            source.GenericDetailViewModel.QueueListItems = _queueService.GetAllKeyValues(GetLanguageId()).ToSelectList();

            MultiSelectViewModel permissions = new MultiSelectViewModel(ProjectConstants.ProjectsPermissionsPermissions);
            permissions.Items = _projectService.GetPermissionList(source.GenericDetailViewModel.Id).Data.ToViewModel();
            source.GenericDetailViewModel.Permissions = permissions;

            if (modeAction == ModeActionTypeEnum.Edit)
            {
                source.SchedulerViewModel = new SchedulerViewModel() { CalendarEventCategory = _eventCategoriesService.GetAllKeyValuesNotDeleted() };
            }

            return source;
        }
       
        private ResultViewModel<ProjectsListViewModel> DoFilterAndPaging(ProjectsFilterViewModel filter)
        {
            var data = new ResultViewModel<ProjectsListViewModel>();
            var filteredData = _projectService.GetAllFiltered(filter.ToDto()).Data.ToListViewModel();

            if (filter.Page != 0)
            {
                filter.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<ProjectsViewModel>()
                {
                    Page = filter.Page,
                    PageSize = filter.Size
                };
                data.Data = new ProjectsListViewModel()
                {
                    ProjectsList = new MultiItemViewModel<ProjectsViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filter.PagesCount = pager.PagesCount;
                filter.Page = pager.Page;
                filter.TotalValues = filteredData.Count;
            }
            else
            {
                data.Data = new ProjectsListViewModel()
                {
                    ProjectsList = new MultiItemViewModel<ProjectsViewModel, int>(filteredData)
                };
            }
            data.Data.Filters = filter;
            return data;
        }
    }
}