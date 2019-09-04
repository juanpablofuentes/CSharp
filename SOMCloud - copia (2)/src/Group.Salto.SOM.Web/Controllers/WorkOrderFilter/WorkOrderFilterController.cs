using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.WorkOrder;
using Group.Salto.Common.Constants.WorkOrderFilter;
using Group.Salto.Common.Constants.WorkOrderStatus;
using Group.Salto.Common.Enums;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Language;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.ServiceLibrary.Common.Contracts.PeopleCollection;
using Group.Salto.ServiceLibrary.Common.Contracts.Queue;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderStatus;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderViewConfigurations;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderViewConfigurations;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.MultiSelect;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.WorkOrderFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.WorkOrderFilters
{
    public class WorkOrderFilterController : BaseLanguageController
    {
        private readonly IWorkOrderViewConfigurationsServices _workOrderViewConfigurationsServices;
        private readonly IWorkOrderViewMultiselect _workOrderViewMultiselect;

        public WorkOrderFilterController(ILoggingService loggingService,
                                         IHttpContextAccessor accessor,
                                         IConfiguration configuration,
                                         ILanguageService languageService,
                                         IWorkOrderViewConfigurationsServices workOrderViewConfigurationsServices,
                                         IWorkOrderViewMultiselect workOrderViewMultiselect) : base(loggingService, configuration, accessor, languageService)
        {
            _workOrderViewConfigurationsServices = workOrderViewConfigurationsServices ?? throw new ArgumentNullException($"{nameof(IWorkOrderViewConfigurationsServices)} is null");
            _workOrderViewMultiselect = workOrderViewMultiselect ?? throw new ArgumentNullException($"{nameof(IWorkOrderViewMultiselect)} is null");
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            LoggingService.LogInfo($"workOrderFilters Index");
            int userId = base.GetConfigurationUserId();
            IList<WorkOrderFilterListViewModel> data = _workOrderViewConfigurationsServices.GetAllViewsByUserId(userId).Data?.ToListViewModel(id);
            var result = ProcessResult(data);
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        public IActionResult Filters(int id)
        {
            LoggingService.LogInfo($"workOrderFilters Filters for id = {id}");
            ConfigurationViewDto dataDto = _workOrderViewConfigurationsServices.GetConfiguredViewById(id, GetLanguageId()).Data;
            WorkOrderFilterViewModel data = dataDto.ToViewConfigurationViewModel();

            if (data != null)
            {
                MultiSelectViewModel technicians = new MultiSelectViewModel(WorkOrderFilterConstants.WorkOrderFilterTechnicians);
                data.Technicians = technicians;

                MultiSelectViewModel groups = new MultiSelectViewModel(WorkOrderFilterConstants.WorkOrderFilterGroups);
                data.Groups = groups;
            }
            return PartialView("_ViewFilters", data);
        }

        [HttpPost]
        public IActionResult Create(WorkOrderFilterViewModel workOrderFilters)
        {
            LoggingService.LogInfo($"workOrderFilters create");
            if (ModelState.IsValid)
            {
                workOrderFilters.UserId = base.GetConfigurationUserId();
                var result = _workOrderViewConfigurationsServices.Create(workOrderFilters.ToEditDto(GetLanguageId()));
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle, WorkOrderFilterConstants.WorkOrderFilterCreateSuccessMessage, FeedbackTypeEnum.Success);
                    return RedirectToAction("Index", new { id = result.Data.Id });
                }
                WorkOrderFilterViewModel resultData = result.Data.ToViewConfigurationViewModel();
                return View(resultData);
                
            }
            return ModelInvalid(ModeActionTypeEnum.Create, workOrderFilters);
        }

        [HttpPost]
        public IActionResult Edit(WorkOrderFilterViewModel workOrderFilters)
        {
            LoggingService.LogInfo($"workOrderFilters update for id = {workOrderFilters.Id}");
            if (ModelState.IsValid)
            {
                workOrderFilters.UserId = base.GetConfigurationUserId();
                ResultDto<ConfigurationViewDto> result = _workOrderViewConfigurationsServices.Update(workOrderFilters.ToEditDto(GetLanguageId()));
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle, WorkOrderFilterConstants.WorkOrderFilterUpdateSuccessMessage, FeedbackTypeEnum.Success);
                    return RedirectToAction($"Index", new { id = workOrderFilters.Id });
                }
                WorkOrderFilterViewModel resultData = result.Data.ToViewConfigurationViewModel();
                return View(resultData);
            }
            return ModelInvalid(ModeActionTypeEnum.Index, workOrderFilters);
        }

        [HttpGet]
        public IActionResult MultiSelect(int id, string filter)
        {
            LoggingService.LogInfo($"workOrderFilters MultiSelect for columnId = {id}, filter = '{filter}' " );
            MultiSelectConfigurationViewDto multiSelectConfigurationViewDto = new MultiSelectConfigurationViewDto()
            {
                ColumnId = id,
                LanguageId = GetLanguageId(),
                SelectedFilter = filter,
                UserId = GetConfigurationUserId()
            };

            MultiSelectViewModel multiselect = new MultiSelectViewModel(WorkOrderFilterConstants.WorkOrderFilterModalMultiSelect);
            multiselect.Items = _workOrderViewMultiselect.GetMultiSelect(multiSelectConfigurationViewDto).Data.ToViewModel();
            return PartialView("_MultiSelectControl", multiselect);
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            var deleteResult = _workOrderViewConfigurationsServices.Delete(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle, WorkOrderFilterConstants.WorkOrderFilterDeleteSuccessMessage, FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error, WorkOrderFilterConstants.WorkOrderFilterDeleteErrorMessage, FeedbackTypeEnum.Error);
            }
            return deleteResult.Data;
        }

        private IActionResult ModelInvalid(ModeActionTypeEnum modeAction, WorkOrderFilterViewModel projectDetail)
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

        private WorkOrderFilterViewModel FillData(WorkOrderFilterViewModel source, ModeActionTypeEnum modeAction)
        {
            source.ModeActionType = modeAction;
            return source;
        }
    }
}