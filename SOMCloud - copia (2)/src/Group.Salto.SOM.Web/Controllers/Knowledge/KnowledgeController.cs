using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Knowledge;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Knowledge;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Knowledge;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Group.Salto.SOM.Web.Controllers.Knowledge
{
    public class KnowledgeController : BaseController
    {
        private readonly IKnowledgeService _knowledgeService;
        public KnowledgeController(ILoggingService loggingService,
                                    IConfiguration configuration,
                                    IHttpContextAccessor accessor,
                                    IKnowledgeService knowledgeService) : base(loggingService, configuration, accessor)
        {
            _knowledgeService = knowledgeService ?? throw new ArgumentNullException($"{nameof(knowledgeService)} is null");
        }

        [CustomAuthorization(ActionGroupEnum.Skills, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Actions Get get all Knowledge");
            var result = DoFilterAndPaging(new KnowledgesFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [CustomAuthorization(ActionGroupEnum.Skills, ActionEnum.GetById)]
        public IActionResult Details(int id)
        {
            LoggingService.LogInfo($"Actions Get get by Knowledge id: {id}");
            var result = _knowledgeService.GetById(id);
            var resultData = result.ToViewModel();

            if (result != null && (result.Errors?.Errors == null || !result.Errors.Errors.Any()))
            {
                return View("Details", resultData);
            }

            return View();
        }

        [CustomAuthorization(ActionGroupEnum.Skills, ActionEnum.Create)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Skills, ActionEnum.Create)]
        public IActionResult Create(ResultViewModel<KnowledgeViewModel> knowledgeData)
        {
            LoggingService.LogInfo($"Actions Post create Knowledge");
            if (ModelState.IsValid)
            {
                var result = _knowledgeService.CreateKnowledge(knowledgeData.Data.ToDto());

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        KnowledgeConstants.KnowledgeCreateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            var results = ProcessResult(knowledgeData.Data,
                         LocalizationsConstants.Error,
                         LocalizationsConstants.FormErrorsMessage,
                         FeedbackTypeEnum.Error);

            return View("Create", results);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Skills, ActionEnum.Update)]
        public IActionResult Update(ResultViewModel<KnowledgeViewModel> knowledgeData)
        {
            LoggingService.LogInfo($"Actions Post Update Knowledge by id {knowledgeData.Data.Id}");

            if (ModelState.IsValid && knowledgeData.Data.Name != null)
            {
                var resultUpdate = _knowledgeService.UpdateKnowledge(knowledgeData.Data.ToDto());
                if (resultUpdate.Errors?.Errors == null || !resultUpdate.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        KnowledgeConstants.KnowledgeUpdateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");

                }
                var resultData = resultUpdate.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);

                return View("Details", resultData);
            }
            var result = ProcessResult(knowledgeData.Data, LocalizationsConstants.Error, LocalizationsConstants.FormErrorsMessage, FeedbackTypeEnum.Error);

            return View("Details", result);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Skills, ActionEnum.Delete)]
        public bool Delete(int id)
        {
            LoggingService.LogInfo($"Actions Post Delete Knowledge by id {id}");
            var deleteResult = _knowledgeService.DeleteKnowledge(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    KnowledgeConstants.KnowledgeDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                                    KnowledgeConstants.KnowledgeDeleteErrorMessage,
                                    FeedbackTypeEnum.Error);
            }

            return deleteResult.Data;
        }

        [HttpPost]
        public IActionResult Filter(KnowledgesFilterViewModel filter)
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
        public IActionResult GetKnowledges()
        {
            var knowledges = _knowledgeService.GetAllKeyValues().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            });
            return Ok(knowledges);
        }

        private ResultViewModel<KnowledgesViewModel> DoFilterAndPaging(KnowledgesFilterViewModel filters)
        {
            var data = new ResultViewModel<KnowledgesViewModel>();
            var filteredData = _knowledgeService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();

            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<KnowledgeViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new KnowledgesViewModel()
                {
                    Knowledge = new MultiItemViewModel<KnowledgeViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new KnowledgesViewModel()
                {
                    Knowledge = new MultiItemViewModel<KnowledgeViewModel, int>(filteredData)
                };
            }

            data.Data.KnowledgeFilters = filters;
            return data;
        }
    }
}