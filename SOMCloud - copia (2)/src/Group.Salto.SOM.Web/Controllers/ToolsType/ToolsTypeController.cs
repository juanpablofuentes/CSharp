using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.ToolsType;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Knowledge;
using Group.Salto.ServiceLibrary.Common.Contracts.ToolsType;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.ToolsType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.ToolsType
{
    public class ToolsTypeController : BaseController
    {
        private readonly IToolsTypeService _toolstypeService;
        private readonly IKnowledgeService _knowledgeService;

        public ToolsTypeController(ILoggingService loggingService,
                                    IConfiguration configuration,
                                    IHttpContextAccessor accessor,
                                    IToolsTypeService toolstypeService,
                                    IKnowledgeService knowledgeService) : base(loggingService, configuration, accessor)
        {
            _toolstypeService = toolstypeService ?? throw new ArgumentNullException($"{nameof(IToolsTypeService)} is null");
            _knowledgeService = knowledgeService ?? throw new ArgumentNullException($"{nameof(IKnowledgeService)} is null");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.ToolType, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            return ProccessFilter(new ToolsTypeFilterViewModel());
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.ToolType, ActionEnum.Create)]
        public IActionResult Create()
        {

            LoggingService.LogInfo("Return ToolsType Create");
            var response = new ResultViewModel<ToolsTypeDetailsViewModel>();

            return View(response);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.ToolType, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($" Get get by ToolsType id: {id}");
            var result = _toolstypeService.GetById(id);
            var resultData = result.ToViewModel();
            if (result != null && (result.Errors?.Errors == null || !result.Errors.Errors.Any()))
            {
                return View("Edit", resultData);
            }

            return View();
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.ToolType, ActionEnum.Create)]
        public IActionResult Create(ToolsTypeDetailsViewModel toolstypeData)
        {
            LoggingService.LogInfo($" Post create ToolsType");
            if (ModelState.IsValid)
            {
                var result = _toolstypeService.CreateToolsType(toolstypeData.ToDto());

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        ToolsTypeConstants.ToolsTypeCreateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            var results = ProcessResult(toolstypeData,
                         LocalizationsConstants.Error,
                         LocalizationsConstants.FormErrorsMessage,
                         FeedbackTypeEnum.Error);

            return View("Create", results);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.ToolType, ActionEnum.Update)]
        public IActionResult Edit(ToolsTypeDetailsViewModel toolstypeData)
        {
            LoggingService.LogInfo($"Post Update ToolsType by id {toolstypeData.Id}");

            if (ModelState.IsValid)
            {
                var resultUpdate = _toolstypeService.UpdateToolsType(toolstypeData.ToDto());
                if (resultUpdate.Errors?.Errors == null || !resultUpdate.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        ToolsTypeConstants.ToolsTypeUpdateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");

                }
                var resultData = resultUpdate.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);

                return View("Edit", resultData);
            }
            var result = ProcessResult(toolstypeData, LocalizationsConstants.Error, LocalizationsConstants.FormErrorsMessage, FeedbackTypeEnum.Error);

            return View("Edit", result);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.ToolType, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            LoggingService.LogInfo($"Actions Post Delete ToolsType by id {id}");
            var deleteResult = _toolstypeService.DeleteToolsType(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    ToolsTypeConstants.ToolsTypeDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
                return Ok(deleteResult.Data);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                                    ToolsTypeConstants.ToolsTypeDeleteErrorMessage,
                                    FeedbackTypeEnum.Error);
                return BadRequest(deleteResult.Data);
            }
        }

        [HttpPost]
        public IActionResult Filter(ToolsTypeFilterViewModel filter)
        {
            return ProccessFilter(filter);

        }

        private IActionResult ProccessFilter(ToolsTypeFilterViewModel filter)
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

        private ResultViewModel<ToolsTypeViewModel> DoFilterAndPaging(ToolsTypeFilterViewModel filters)
        {
            var data = new ResultViewModel<ToolsTypeViewModel>();

            var filteredData = _toolstypeService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();

            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<ToolTypeViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new ToolsTypeViewModel()
                {
                    ToolsType = new MultiItemViewModel<ToolTypeViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new ToolsTypeViewModel()
                {
                    ToolsType = new MultiItemViewModel<ToolTypeViewModel, int>(filteredData)
                };
            }

            data.Data.ToolsTypeFilters = filters;
            return data;
        }
    }
}