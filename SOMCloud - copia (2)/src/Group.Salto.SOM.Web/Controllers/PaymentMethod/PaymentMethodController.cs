using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.PaymentMethod;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.PaymentMethod;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.PaymentMethod;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.PaymentMethod
{
    public class PaymentMethodController : BaseController
    {
        private readonly IPaymentMethodService _paymentmethodService;

        public PaymentMethodController(ILoggingService loggingService,
            IConfiguration configuration,
            IHttpContextAccessor accessor,
            IPaymentMethodService paymentmethodService)
            : base(loggingService, configuration, accessor)
        {
            _paymentmethodService = paymentmethodService ?? throw new ArgumentNullException(nameof(IPaymentMethodService));
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.PaymentMethods, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Actions Get get all Payment method");
            var result = DoFilterAndPaging(new PaymentMethodFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.PaymentMethods, ActionEnum.Create)]
        public IActionResult Create()
        {
            LoggingService.LogInfo("Return PaymentMethod Create");
            var response = new ResultViewModel<PaymentMethodViewModel>();
            return View(response);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.PaymentMethods, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo($"Payment method .get for id:{id}");
            var result = _paymentmethodService.GetById(id);
            var response = result.ToViewModel();
            LogFeedbacks(response.Feedbacks?.Feedbacks);
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
        public IActionResult Filter(PaymentMethodFilterViewModel filter)
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
        [CustomAuthorization(ActionGroupEnum.PaymentMethods, ActionEnum.Create)]
        public IActionResult Create(PaymentMethodViewModel paymentmethodData)
        {
            LoggingService.LogInfo($"Actions Post create payment method");
            if (ModelState.IsValid)
            {
                var result = _paymentmethodService.CreatePaymentMethod(paymentmethodData.ToDto());

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        PaymentMethodConstants.PaymentMethodCreateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            var results = ProcessResult(paymentmethodData,
                         LocalizationsConstants.Error,
                         LocalizationsConstants.FormErrorsMessage,
                         FeedbackTypeEnum.Error);
            return View("Create", results);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.PaymentMethods, ActionEnum.Update)]
        public IActionResult Edit(PaymentMethodViewModel paymentmethodData)
        {
            LoggingService.LogInfo($"Actions Post Update payment method by id {paymentmethodData.Id}");

            if (ModelState.IsValid)
            {
                var resultUpdate = _paymentmethodService.UpdatePaymentMethod(paymentmethodData.ToDto());
                if (resultUpdate.Errors?.Errors == null || !resultUpdate.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        PaymentMethodConstants.PaymentMethodUpdateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = resultUpdate.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Edit", resultData);
            }
            var result = ProcessResult(paymentmethodData, LocalizationsConstants.Error, LocalizationsConstants.FormErrorsMessage, FeedbackTypeEnum.Error);
            return View("Edit", result);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.PaymentMethods, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            LoggingService.LogInfo($"Actions Post Delete Payment method by id {id}");
            var deleteResult = _paymentmethodService.DeletePaymentMethod(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    PaymentMethodConstants.PaymentMethodDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                    PaymentMethodConstants.PaymentMethodDeleteErrorMessage,
                    FeedbackTypeEnum.Error);
            }
            return Ok(deleteResult.Data);
        }

        private ResultViewModel<PaymentsMethodViewModel> DoFilterAndPaging(PaymentMethodFilterViewModel filters)
        {
            var data = new ResultViewModel<PaymentsMethodViewModel>();
            var filteredData = _paymentmethodService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();

            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<PaymentMethodViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new PaymentsMethodViewModel()
                {
                    PaymentMethod = new MultiItemViewModel<PaymentMethodViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new PaymentsMethodViewModel()
                {
                    PaymentMethod = new MultiItemViewModel<PaymentMethodViewModel, int>(filteredData)
                };
            }

            data.Data.PaymentMethodFilters = filters;
            return data;
        }
    }
}