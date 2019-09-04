using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Origins;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Language;
using Group.Salto.ServiceLibrary.Common.Contracts.Origins;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Origins;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.Origins
{
    public class OriginsController : BaseController
    {
        private readonly IOriginsService _originsService;

        public OriginsController(ILoggingService loggingService,
            IConfiguration configuration,
            IHttpContextAccessor accessor, IOriginsService originsService) : base(loggingService, configuration, accessor)
        {
            _originsService = originsService ?? throw new ArgumentNullException($"{nameof(IOriginsService)} is null");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Origin, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Origins -- Get get all");
            var result = DoFilterAndPaging(new OriginsFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Origin, ActionEnum.Create)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Origin, ActionEnum.GetById)]
        public ActionResult Edit(int id)
        {
            LoggingService.LogInfo($"Origins -- detail id: {id}");
            var result = _originsService.GetById(id);
            var resultData = result.ToDetailViewModel();

            if (result != null && (result.Errors?.Errors == null || !result.Errors.Errors.Any()))
            {
                return View(resultData);
            }
            return View();
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Origin, ActionEnum.Update)]
        public IActionResult Edit(OriginDetailViewModel updateOrigin)
        {
            LoggingService.LogInfo($"update origins by id {updateOrigin.Id}");

            if (ModelState.IsValid)
            {
                var resultUpdate = _originsService.UpdateOrigin(updateOrigin.ToDto());
                if (resultUpdate.Errors?.Errors == null || !resultUpdate.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        OriginsConstants.OriginsUpdateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = resultUpdate.ToDetailViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View(resultData);
            }
            return ModelInvalid("Edit", updateOrigin);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Origin, ActionEnum.Create)]
        public IActionResult Create(OriginDetailViewModel createOrigin)
        {
            LoggingService.LogInfo("Event Origins -- create");
            if (ModelState.IsValid)
            {
                var result = _originsService.CreateOrigin(createOrigin.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        OriginsConstants.OriginsCreateSuccessMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToDetailViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View(resultData);
            }
            return ModelInvalid("Create", createOrigin);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Origin, ActionEnum.Delete)]
        public ActionResult Delete(int id)
        {
            var deleteResult = _originsService.DeleteOrigin(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    OriginsConstants.OriginsDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                    OriginsConstants.OriginsDeleteErrorMessage,
                    FeedbackTypeEnum.Error);
            }
            return Ok(deleteResult.Data);
        }

        [HttpPost]
        public IActionResult Filter(OriginsFilterViewModel filter)
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

        private ResultViewModel<OriginsListViewModel> DoFilterAndPaging(OriginsFilterViewModel filters)
        {
            var data = new ResultViewModel<OriginsListViewModel>();
            var filteredData = _originsService.GetAllFiltered(filters.ToFilterDto()).Data.ToViewModel();

            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<OriginListViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new OriginsListViewModel()
                {
                    Origins = new MultiItemViewModel<OriginListViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new OriginsListViewModel()
                {
                    Origins = new MultiItemViewModel<OriginListViewModel, int>(filteredData)
                };
            }

            data.Data.OriginFilter = filters;
            return data;
        }

        private IActionResult ModelInvalid(string view, OriginDetailViewModel results, string KeyMessage = null)
        {
            return View(view, ProcessResult(results, new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = KeyMessage ?? LocalizationsConstants.FormErrorsMessage,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            }));
        }
    }
}