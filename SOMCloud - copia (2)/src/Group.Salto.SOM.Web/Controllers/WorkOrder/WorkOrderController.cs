using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.WorkOrder;
using Group.Salto.Common.Dictionaries;
using Group.Salto.Common.Enums;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Assets;
using Group.Salto.ServiceLibrary.Common.Contracts.ExternalWorkOrderStatus;
using Group.Salto.ServiceLibrary.Common.Contracts.Language;
using Group.Salto.ServiceLibrary.Common.Contracts.Origins;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.ServiceLibrary.Common.Contracts.Queue;
using Group.Salto.ServiceLibrary.Common.Contracts.Service;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrder;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderDerivated;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderStatus;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Assets;
using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.ServiceLibrary.Common.Dtos.Service;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderDerivated;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderViewConfigurations;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.File;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Grid;
using Group.Salto.SOM.Web.Models.MultiSelect;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.WorkOrder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.WorkOrder
{
    public class WorkOrderController : BaseLanguageController
    {
        private readonly IWorkOrderService _workOrderService;
        private readonly IWorkOrderStatusService _workOrderStatusService;
        private readonly IQueueService _queueService;
        private readonly IOriginsService _originsService;
        private readonly IExternalWorkOrderStatusService _externalWorkOrderStatusService;
        private readonly IFormService _formService;
        private readonly IAssetsService _assetsService;
        private readonly IWorkOrderDerivatedService _workOrderDerivatedService;

        public WorkOrderController(
           ILoggingService loggingService,
           IHttpContextAccessor accessor,
           IConfiguration configuration,
           ILanguageService languageService,
           IQueueService queueService,
           IWorkOrderService workOrderService,
           IWorkOrderStatusService workOrderStatusService,
           IOriginsService originsService,
           IExternalWorkOrderStatusService externalWorkOrderStatusService,
           IFormService formService,
           IAssetsService assetsService,
           IWorkOrderDerivatedService workOrderDerivatedService) : base(loggingService, configuration, accessor, languageService)
        {
            _workOrderService = workOrderService ?? throw new ArgumentNullException($"{nameof(IWorkOrderService)} is null");
            _workOrderStatusService = workOrderStatusService ?? throw new ArgumentNullException($"{nameof(IWorkOrderStatusService)} is null");
            _queueService = queueService ?? throw new ArgumentNullException($"{nameof(IQueueService)} is null");
            _originsService = originsService ?? throw new ArgumentNullException($"{nameof(IOriginsService)} is null");
            _externalWorkOrderStatusService = externalWorkOrderStatusService ?? throw new ArgumentNullException($"{nameof(IExternalWorkOrderStatusService)} is null");
            _formService = formService ?? throw new ArgumentNullException($"{nameof(IFormService)} is null");
            _assetsService = assetsService ?? throw new ArgumentNullException($"{nameof(IAssetsService)} is null");
            _workOrderDerivatedService = workOrderDerivatedService ?? throw new ArgumentNullException($"{nameof(IWorkOrderDerivatedService)} is null");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.WorkOrders, ActionEnum.Create)]
        public IActionResult Index(Models.WorkOrder.WorkOrderSearch workOrderSearch)
        {
            ResultViewModel<WorkOrderGridResponseViewModel> data = new ResultViewModel<WorkOrderGridResponseViewModel>();
            data.Breadcrumbs = AddBreadcrumb(new List<string>() { "GeneralMenuWorkOrders", WorkOrderConstants.WorkOrderPageTitle });
            data.Data = new WorkOrderGridResponseViewModel();
            data.Data.WorkOrderFilterGridViewModel = new WorkOrderFilterGridViewModel();
            MultiSelectViewModel workOrderStatus = new MultiSelectViewModel(WorkOrderConstants.WorkOrderFilterStatus);
            workOrderStatus.Items = _workOrderStatusService.GetWorkOrderStatusMultiSelect(GetLanguageId(), new List<int>()).Data.ToViewModel();
            data.Data.WorkOrderFilterGridViewModel.WorkOrderStatus = workOrderStatus;

            MultiSelectViewModel workOrderQueue = new MultiSelectViewModel(WorkOrderConstants.WorkOrderFilterQueue);
            MultiSelectConfigurationViewDto multiSelectConfigurationViewDto = new MultiSelectConfigurationViewDto() { LanguageId = GetLanguageId(), UserId = GetConfigurationUserId() };
            workOrderQueue.Items = _queueService.GetQueueMultiSelect(multiSelectConfigurationViewDto, new List<int>()).Data.ToViewModel();
            data.Data.WorkOrderFilterGridViewModel.WorkOrderQueue = workOrderQueue;
            data.Data.PaginationNumber = GetNumberEntriesPerPage();
            data.Data.WorkOrderFilterGridViewModel.WorkOrderSearch = workOrderSearch;
            return View(data);
        }

        [HttpGet]
        public ActionResult Data(WorkOrderGridRequestViewModel model)
        {
            if (model.Count == 0) model.Count = GetNumberEntriesPerPage();
            model.UserId = GetConfigurationUserId();
            model.LanguageId = GetLanguageId();

            WorkOrderResultDto alldata = _workOrderService.GetConfiguredWorkOrdersList(model.ToDto());
            RootGrid rootObject = model.ToRootGrid(alldata);

            return Ok(rootObject);
        }

        [HttpPost]
        public ActionResult Excel(string filterParameterJSON)
        {
            WorkOrderGridRequestViewModel model = new WorkOrderGridRequestViewModel();
            model = JsonConvert.DeserializeObject<WorkOrderGridRequestViewModel>(filterParameterJSON);
            if (model.Count == 0) model.Count = GetNumberEntriesPerPage();
            model.UserId = GetConfigurationUserId();
            model.LanguageId = GetLanguageId();

            WorkOrderResultDto alldata = _workOrderService.GetConfiguredWorkOrdersList(model.ToDto());
            FileContentDto file = _workOrderService.ExportToExcel(alldata.Data, alldata.Columns);
            if (file != null && file.FileBytes != null)
            {
                return File(file.FileBytes, LocalizationsConstants.ExcelMimeType, file.FileName);
            }
            else
            {
                return PartialView("_NoData", null);
            }
        }

        [HttpGet]
        public IActionResult Detail(int Id)
        {
            LoggingService.LogInfo("Work Orders.Detail Get");
            ResultDto<bool> access = _workOrderService.GetPermissionToWorkOrder(Id, GetConfigurationUserId());
            if (access.Data)
            {
                var result = _workOrderService.GetDetailWO(Id);
                var resultData = result.ToDetailViewModel();
                resultData.Breadcrumbs = AddBreadcrumb(new List<string>() { MenuLocalizationConstants.GeneralMenuWorkOrders, WorkOrderConstants.WorkOrderPageTitle, WorkOrderConstants.WorkOrderDetailTitle });

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    return View("Detail", resultData);
                }
                SetFeedbackTempData(LocalizationsConstants.Error,
                   LocalizationsConstants.ErrorLoadingDataMessage,
                   FeedbackTypeEnum.Error);
                return Redirect(nameof(Index));
            }
            else
            {
                return PartialView("_ForbiddenAccess");
            }
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.WorkOrders, ActionEnum.Create)]
        public IActionResult Create()
        {
            LoggingService.LogInfo("WorkOrder.Create Get");
            var response = new ResultViewModel<WorkOrderEditViewModel>()
            {
                Data = FillData(new WorkOrderEditViewModel(), ModeActionTypeEnum.Create)
            };
            response.Breadcrumbs = AddBreadcrumb(new List<string>() { MenuLocalizationConstants.GeneralMenuWorkOrders, WorkOrderConstants.WorkOrderPageTitle, WorkOrderConstants.WorkOrderEditPageTitle });
            return View(response);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.WorkOrders, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo("WorkOrder.Edit Get for id:{id}");
            ResultDto<bool> access = _workOrderService.GetPermissionToWorkOrder(id, GetConfigurationUserId());
            if (access.Data)
            {
                var result = _workOrderService.GetById(id);
                var response = result.ToEditViewModel();
                response.Breadcrumbs = AddBreadcrumb(new List<string>() { MenuLocalizationConstants.GeneralMenuWorkOrders, WorkOrderConstants.WorkOrderPageTitle, WorkOrderConstants.WorkOrderEditPageTitle });
                FillData(response.Data, ModeActionTypeEnum.Edit);
                LogFeedbacks(response.Feedbacks?.Feedbacks);
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    return View("Edit", response);
                }
                SetFeedbackTempData(LocalizationsConstants.Error, LocalizationsConstants.ErrorLoadingDataMessage, FeedbackTypeEnum.Error);
                return Redirect(nameof(Index));
            }
            else
            {
                return PartialView("_ForbiddenAccess");
            }
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.WorkOrders, ActionEnum.Create)]
        public IActionResult Create(WorkOrderEditViewModel workOrderEdit)
        {
            LoggingService.LogInfo("workOrder.Post Create");
            if (ModelState.IsValid)
            {
                var result = _workOrderService.Create(workOrderEdit.ToEditDto(GetConfigurationUserId(), GetTenantId()));
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle, WorkOrderConstants.WorkOrderCreateSuccessMessage, FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToEditViewModel();
                FillData(resultData.Data, ModeActionTypeEnum.Create);
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View(resultData);
            }
            return ModelInvalid(ModeActionTypeEnum.Create, workOrderEdit);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.WorkOrders, ActionEnum.Update)]
        public IActionResult Edit(WorkOrderEditViewModel workOrderEdit)
        {
            LoggingService.LogInfo($"workOrder update for id = {workOrderEdit.WorkOrderGenericEditViewModel.Id}");
            if (ModelState.IsValid)
            {
                var result = _workOrderService.Update(workOrderEdit.ToEditDto(GetConfigurationUserId(), GetTenantId()));
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle, WorkOrderConstants.WorkOrderUpdateSuccessMessage, FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToEditViewModel();
                FillData(resultData.Data, ModeActionTypeEnum.Edit);
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View(resultData);
            }
            return ModelInvalid(ModeActionTypeEnum.Edit, workOrderEdit);
        }

        [HttpGet]
        public IActionResult Forms(int Id, int FatherId, bool ReferenceOtherService, int GeneratedServiceId, bool GeneratedService)
        {
            LoggingService.LogInfo($"Work Orders.Forms Get for workorder: {Id}");
            WorkOrderServiceDto services;
            if (ReferenceOtherService)
            {
                services = _formService.GetFormsWO(Id, FatherId, 0);
            }
            else if (GeneratedService)
            {
                services = _formService.GetFormsWO(Id, 0, GeneratedServiceId);
            }
            else
            {
                services = _formService.GetFormsWO(Id, 0, 0);
            }
            if (services?.WOService.Count != 0)
            {
                var model = new WorkOrderServiceViewModel();
                model.WOService = services.WOService.ToListViewModel();
                return PartialView("_FormsForm", model);
            }
            else
            {
                return NoData(services.WOService.Count);
            }
        }

        [HttpGet]
        public IActionResult Assets(int Id)
        {
            LoggingService.LogInfo("Work Orders.Assets Get");
            if (Id != 0)
            {
                ResultDto<AssetForWorkOrderDetailsDto> service = _assetsService.GetForWorkOrderEditById(Id);
                AssetsDetailViewModel model = service.Data.ToAssetViewModel();
                return PartialView("_AssetMainForm", model);
            }
            else
            {
                return NoData(Id);
            }
        }

        [HttpGet]
        public IActionResult AssetsWorkOrderServices(int Id)
        {
            LoggingService.LogInfo("Work Orders.Assets Services Get");
            ResultDto<AssetsDetailWorkOrderServicesDto> service = _workOrderService.GetAllServiceAndExtraFieldsById(Id);
            AssetsDetailWorkOrderServicesViewModel model = service.Data.ToAssetsDetailWorkOrderServicesViewModel();
            return PartialView("_AssetsWorkOrderServices", model);
        }

        [HttpGet]
        public IActionResult Operations(int Id)
        {
            LoggingService.LogInfo("Work Orders.Assets Services Get");
            WorkOrderOperationsDto result = _workOrderService.GetWOAnalysisAndServiceAnalysis(Id);
            if (result != null)
            {
                WorkOrderOperationsViewModel model = result.ToOperationsViewModel();
                return PartialView("_OperationsForm", model);
            }
            else
            {
                return NoData(0);
            }
        }

        [HttpGet]
        public IActionResult Download(int Id, string FileName)
        {
            List<RequestFileDto> files = _formService.GetFilesFromExtraFieldsValues(Id);
            List<FileContentResult> allFiles = new List<FileContentResult>();
            if (files.Count > 0)
            {
                for (var i = 0; i < files.Count; i++)
                {
                    if (files[i].FileBytes != null && files[i].FileName == FileName)
                    {
                        return File(files[i].FileBytes, MimeTypesDictionary.GetMimeType(Path.GetExtension(files[i].FileName)), files[i].FileName);
                    }
                }
            }
            SetFeedbackTempData(LocalizationsConstants.Error,
             LocalizationsConstants.ErrorLoadingDataMessage,
             FeedbackTypeEnum.Error);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditForm(int Id)
        {
            LoggingService.LogInfo("WorkOrder EditForm Get");
            var result = _formService.GetExtraFieldsValues(Id);
            if (result != null)
            {
                IList<WorkOrderFormEditViewModel> model = result.ToFormViewModel();
                return PartialView("_editFormExtraFields", model);
            }
            return NoData(0);
        }

        [HttpPost]
        public IActionResult EditForm(IList<WorkOrderFormEditViewModel> model, int serviceId, int workOrderId)
        {
            LoggingService.LogInfo("WorkOrder EditForm POST");
            ResultDto<IList<WorkOrderExtraFieldsValuesDto>> result = null;
            if (ModelState.IsValid)
            {
                FormServiceDto formServiceDto = new FormServiceDto()
                {
                    WorkOrderId = workOrderId,
                    ServiceId = serviceId,
                    UserId = GetConfigurationUserId(),
                    CustomerId = new Guid(GetTenantId())
                };
                result = _formService.UpdateExtraFieldsValues(model.ToDto().ToList(), formServiceDto);
            }
            return Ok(result.Data);
        }

        public IActionResult NoData(int? services = null)
        {
            return PartialView("_NoData", services);
        }
        
        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.WorkOrders, ActionEnum.Create)]
        public IActionResult CreateDerived(int taskId, int flowId)
        {
            LoggingService.LogInfo("WorkOrderDerivated.Create Get");
            var response = new ResultViewModel<WorkOrderEditViewModel>()
            {
                Data = FillData(new WorkOrderEditViewModel(), ModeActionTypeEnum.CreateDerived)
            };
            FillDataDerived(response.Data, taskId, flowId);
            response.Breadcrumbs = AddBreadcrumb(new List<string>() { MenuLocalizationConstants.GeneralMenuWorkOrders, WorkOrderConstants.WorkOrderPageTitle, WorkOrderConstants.WorkOrderEditPageTitle });
            return View(response);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.WorkOrders, ActionEnum.Create)]
        public IActionResult CreateDerived(WorkOrderEditViewModel workOrderEdit)
        {
            LoggingService.LogInfo("workOrderDerivated.Post Create");
            if (ModelState.IsValid)
            {
                ResultDto<WorkOrderDerivatedDto> result = _workOrderDerivatedService.Create(workOrderEdit.ToEditDerivatedDto(GetConfigurationUserId(), GetTenantId()));
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle, WorkOrderConstants.WorkOrderCreateSuccessMessage, FeedbackTypeEnum.Success);
                    return RedirectToAction("Edit", "Flows", new { Id = workOrderEdit.WorkOrderGenericEditViewModel.FlowId });
                }
                var resultData = result.ToEditDerivatedViewModel();
                FillData(resultData.Data, ModeActionTypeEnum.CreateDerived);
                FillDataDerived(resultData.Data, workOrderEdit.WorkOrderGenericEditViewModel.TaskId, workOrderEdit.WorkOrderGenericEditViewModel.FlowId);
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View(resultData);
            }
            FillDataDerived(workOrderEdit, workOrderEdit.WorkOrderGenericEditViewModel.TaskId, workOrderEdit.WorkOrderGenericEditViewModel.FlowId);
            return ModelInvalid(ModeActionTypeEnum.CreateDerived, workOrderEdit);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.WorkOrders, ActionEnum.Update)]
        public IActionResult EditDerived(int id, int flowId)
        {
            LoggingService.LogInfo("WorkOrderDerived.Edit Get for id:{id}");
            ResultDto<WorkOrderDerivatedDto> result = _workOrderDerivatedService.GetById(id);
            var response = result.ToEditViewModel();
            response.Breadcrumbs = AddBreadcrumb(new List<string>() { MenuLocalizationConstants.GeneralMenuWorkOrders, WorkOrderConstants.WorkOrderPageTitle, WorkOrderConstants.WorkOrderEditPageTitle });
            FillData(response.Data, ModeActionTypeEnum.EditDerived);
            FillDataDerived(response.Data, response.Data.WorkOrderGenericEditViewModel.TaskId, flowId);
            LogFeedbacks(response.Feedbacks?.Feedbacks);
            if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
            {
                return View(response);
            }
            SetFeedbackTempData(LocalizationsConstants.Error, LocalizationsConstants.ErrorLoadingDataMessage, FeedbackTypeEnum.Error);
            return RedirectToAction("Edit", "Flows", new { Id = flowId });
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.WorkOrders, ActionEnum.Update)]
        public IActionResult EditDerived(WorkOrderEditViewModel workOrderEdit)
        {
            LoggingService.LogInfo($"WorkOrderDerived update for id = {workOrderEdit.WorkOrderGenericEditViewModel.Id}");
            if (ModelState.IsValid)
            {
                var result = _workOrderDerivatedService.Update(workOrderEdit.ToEditDerivatedDto(GetConfigurationUserId(), GetTenantId()));
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle, WorkOrderConstants.WorkOrderUpdateSuccessMessage, FeedbackTypeEnum.Success);
                    return RedirectToAction("Edit", "Flows", new { Id = workOrderEdit.WorkOrderGenericEditViewModel.FlowId });
                }
                var resultData = result.ToEditViewModel();
                FillData(resultData.Data, ModeActionTypeEnum.EditDerived);
                FillDataDerived(resultData.Data, resultData.Data.WorkOrderGenericEditViewModel.TaskId, workOrderEdit.WorkOrderGenericEditViewModel.FlowId);
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View(resultData);
            }
            FillDataDerived(workOrderEdit, workOrderEdit.WorkOrderGenericEditViewModel.TaskId, workOrderEdit.WorkOrderGenericEditViewModel.FlowId);
            return ModelInvalid(ModeActionTypeEnum.EditDerived, workOrderEdit);
        }

        private IActionResult ModelInvalid(ModeActionTypeEnum modeAction, WorkOrderEditViewModel workOrderEdit)
        {
            return View(modeAction.ToString(), ProcessResult(FillData(workOrderEdit, modeAction), new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = LocalizationsConstants.FormErrorsMessage,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            }));
        }

        private WorkOrderEditViewModel FillData(WorkOrderEditViewModel source, ModeActionTypeEnum modeAction)
        {
            var languageId = GetLanguageId();
            source.ModeActionType = modeAction;
            source.WorkOrderGenericEditViewModel.ModeActionType = modeAction;
            source.WorkOrderGenericEditViewModel.OriginListItems = _originsService.GetAllOriginKeyValues().ToSelectList();
            source.WorkOrderGenericEditViewModel.QueueListItems = _queueService.GetAllKeyValues(languageId).ToSelectList();
            source.WorkOrderGenericEditViewModel.WorkOrderStatusListItems = _workOrderStatusService.GetAllKeyValues(languageId).ToSelectList();
            source.WorkOrderGenericEditViewModel.ExternalWorkOrderStatusListItems = _externalWorkOrderStatusService.GetAllKeyValues(languageId).ToSelectList();
            
            return source;
        }

        private void FillDataDerived(WorkOrderEditViewModel source, int taskId, int flowId)//TODO: pasarlo a 2 parametros
        {
            var languageId = GetLanguageId();
            source.WorkOrderGenericEditViewModel.IsDerived = true;
            source.WorkOrderGenericEditViewModel.TaskId = taskId;
            source.WorkOrderGenericEditViewModel.FlowId = flowId;
            source.WorkOrderGenericEditViewModel.OtherServicesDuplicationPolicyItems = _workOrderDerivatedService.GetDuplicationPolicyItemsKeyValues(languageId).ToSelectList();
            source.WorkOrderGenericEditViewModel.GeneratorServiceDuplicationPolicyItems = _workOrderDerivatedService.GetDuplicationPolicyItemsKeyValues(languageId).ToSelectList();
        }
    }
}