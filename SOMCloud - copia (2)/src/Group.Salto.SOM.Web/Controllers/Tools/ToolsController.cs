using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Tools;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Tools;
using Group.Salto.ServiceLibrary.Common.Contracts.ToolsType;
using Group.Salto.ServiceLibrary.Common.Contracts.Vehicles;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.Tools
{
    public class ToolsController : BaseController
    {
        private readonly IToolsService _toolsService;
        private readonly IVehiclesService _vehiclesService;
        private readonly IToolsTypeService _toolstypeService;

        public ToolsController(ILoggingService loggingService,
                                    IConfiguration configuration,
                                    IHttpContextAccessor accessor,
                                    IToolsService toolsService,
                                    IToolsTypeService toolstypeService,
                                    IVehiclesService vehicleService) : base(loggingService, configuration, accessor)
        {
            _toolsService = toolsService ?? throw new ArgumentNullException($"{nameof(IToolsService)} is null");
            _vehiclesService = vehicleService ?? throw new ArgumentNullException($"{nameof(IVehiclesService)} is null");
            _toolstypeService = toolstypeService ?? throw new ArgumentNullException($"{nameof(IToolsTypeService)} is null");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Tools, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            return ProccessFilter(new ToolsFilterViewModel());
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Tools, ActionEnum.Create)]
        public IActionResult Create()
        {

            LoggingService.LogInfo("Return Tools Create");
            var response = new ResultViewModel<ToolsDetailsViewModel>()
            {
                Data = FillData(new ToolsDetailsViewModel())
            };
            return View(response);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Tools, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($"Actions Get get by Tools id: {id}");
            var result = _toolsService.GetById(id);

            var resultData = result.ToDetailsViewModel();
            FillData(resultData.Data);

            if (result != null && (result.Errors?.Errors == null || !result.Errors.Errors.Any()))
            {
                return View("Edit", resultData);
            }

            return View();
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Tools, ActionEnum.Create)]
        public IActionResult Create(ToolsDetailsViewModel toolsData)
        {
            LoggingService.LogInfo($"Actions Post create Tools");
            if (ModelState.IsValid)
            {
                var result = _toolsService.CreateTool(toolsData.ToDto());

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        ToolsConstants.ToolsCreateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToDetailsViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            var results = ProcessResult(toolsData,
                         LocalizationsConstants.Error,
                         LocalizationsConstants.FormErrorsMessage,
                         FeedbackTypeEnum.Error);
            FillData(toolsData);
            return View("Create", results);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Tools, ActionEnum.Update)]
        public IActionResult Edit(ToolsDetailsViewModel toolsData)
        {
            LoggingService.LogInfo($"Post Update Tools by id {toolsData.Id}");

            if (ModelState.IsValid)
            {
                var resultUpdate = _toolsService.UpdateTools(toolsData.ToDto());
                if (resultUpdate.Errors?.Errors == null || !resultUpdate.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        ToolsConstants.ToolsUpdateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");

                }
                var resultData = resultUpdate.ToDetailsViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);

                return View("Edit", resultData);
            }
            var result = ProcessResult(toolsData, LocalizationsConstants.Error, LocalizationsConstants.FormErrorsMessage, FeedbackTypeEnum.Error);

            return View("Edit", result);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Tools, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            LoggingService.LogInfo($"Actions Post Delete Tools by id {id}");
            var deleteResult = _toolsService.DeleteTools(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    ToolsConstants.ToolsDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
                return Ok(deleteResult.Data);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                                    ToolsConstants.ToolsDeleteErrorMessage,
                                    FeedbackTypeEnum.Error);
                return BadRequest(deleteResult.Data);
            }
        }

        [HttpPost]
        public IActionResult Filter(ToolsFilterViewModel filter)
        {
            return ProccessFilter(filter);

        }

        private IActionResult ProccessFilter(ToolsFilterViewModel filter)
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

        private ResultViewModel<ToolsViewModel> DoFilterAndPaging(ToolsFilterViewModel filters)
        {
            var data = new ResultViewModel<ToolsViewModel>();

            var filteredData = _toolsService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();

            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<ToolViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new ToolsViewModel()
                {
                    Tools = new MultiItemViewModel<ToolViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new ToolsViewModel()
                {
                    Tools = new MultiItemViewModel<ToolViewModel, int>(filteredData)
                };
            }

            data.Data.ToolsFilters = filters;
            return data;
        }

        private ToolsDetailsViewModel FillData(ToolsDetailsViewModel source)
        {
            source.Vehicles = _vehiclesService.GetAllKeyValues()?.ToSelectList();
            return source;
        }
    }
}