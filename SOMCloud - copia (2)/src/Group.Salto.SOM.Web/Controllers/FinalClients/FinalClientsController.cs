using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.FinalClients;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Calendar;
using Group.Salto.ServiceLibrary.Common.Contracts.EventCategories;
using Group.Salto.ServiceLibrary.Common.Contracts.FinalClients;
using Group.Salto.ServiceLibrary.Common.Contracts.Origins;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.FinalClients;
using Group.Salto.SOM.Web.Models.MultiSelect;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Scheduler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.FinalClients
{
    public class FinalClientsController : BaseController
    {
        private readonly IFinalClientsServices _finalClients;
        private readonly IPeopleService _peopleservice;
        private readonly IOriginsService _originService;
        private readonly IEventCategoriesService _eventCategoriesService;

        public FinalClientsController(ILoggingService loggingService,
                                    IConfiguration configuration,
                                    IHttpContextAccessor accessor,
                                    IFinalClientsServices finalClients,
                                    IPeopleService people,
                                    IOriginsService origin,
                                    IEventCategoriesService eventCategoriesService) : base(loggingService, configuration, accessor)
        {
            _finalClients = finalClients ?? throw new ArgumentNullException(nameof(IFinalClientsServices));
            _peopleservice = people ?? throw new ArgumentNullException(nameof(IPeopleService));
            _originService = origin ?? throw new ArgumentNullException(nameof(IOriginsService));
            _eventCategoriesService = eventCategoriesService ?? throw new ArgumentNullException(nameof(IEventCategoriesService));
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.ClientSites, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Actions.Get get all Final Clients");
            var result = DoFilterAndPaging(new FinalClientsFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.ClientSites, ActionEnum.Create)]
        public IActionResult Create()
        {
            LoggingService.LogInfo("FinalClients.Create Get");
            var model = new FinalClientsDetailViewModel();
            ResultViewModel<FinalClientsEditViewModel> response = ToCreateResultViewModel(ModeActionTypeEnum.Create, model);
            return View(response);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.ClientSites, ActionEnum.Create)]
        public IActionResult Create(FinalClientsEditViewModel response)
        {
            LoggingService.LogInfo("FinalClients.Post Create");
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                var result = _finalClients.Create(response.FinalClientsDetailViewModel.ToDetailDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle, FinalClientsConstants.FinalClientsCreateSuccessMessage, FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToDetailViewModel();
                FillData(resultData.Data);
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                ResultViewModel<FinalClientsEditViewModel> responseVM = ToCreateResultViewModel(ModeActionTypeEnum.Create, resultData.Data);
                return View(responseVM);
            }
            return ModelInvalid(ModeActionTypeEnum.Create, response);
        }

        [HttpPost]
        public IActionResult Filter(FinalClientsFilterViewModel filter)
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
        [CustomAuthorization(ActionGroupEnum.ClientSites, ActionEnum.GetById)]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo("FinalClients.Create Get");
            var model = _finalClients.GetById(id).ToDetailViewModel();
            ResultViewModel<FinalClientsEditViewModel> response = ToCreateResultViewModel(ModeActionTypeEnum.Edit, model.Data);
            var editResult = ProcessResult(response, new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey =  LocalizationsConstants.FormErrorsMessage,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            });
            return View(response);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.ClientSites, ActionEnum.Update)]
        public IActionResult Edit(FinalClientsEditViewModel response)
        {
            LoggingService.LogInfo("FinalClients.Post Update");
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                var result = _finalClients.Update(response.FinalClientsDetailViewModel.ToDetailDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle, FinalClientsConstants.FinalclientsEditSuccessMessage, FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToDetailViewModel();
                resultData.Feedbacks = result.Errors.ToViewModel();
                FillData(resultData.Data);
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);

                ResultViewModel<FinalClientsEditViewModel> responseVM = ToCreateResultViewModel(ModeActionTypeEnum.Edit, resultData.Data);

                return View(responseVM);
            }
            return ModelInvalid(ModeActionTypeEnum.Edit, response);
        }

        [HttpDelete]
        [CustomAuthorization(ActionGroupEnum.ClientSites, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            var deleteResult = _finalClients.Delete(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    FinalClientsConstants.FinalClientsDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                    FinalClientsConstants.FinalClientsDeleteErrorMessage,
                    FeedbackTypeEnum.Error);
            }
            return Ok(deleteResult.Data);
        }

        private ResultViewModel<FinalClientsEditViewModel> ToCreateResultViewModel(ModeActionTypeEnum modeAction, FinalClientsDetailViewModel finalClientsEditViewModel)
        {
            var finalClientsViewModel = FillData(finalClientsEditViewModel);
            finalClientsViewModel.Contacts = finalClientsEditViewModel.Contacts;
            var response = new ResultViewModel<FinalClientsEditViewModel>()
            {
                Data = new FinalClientsEditViewModel()
                {
                    ModeActionType = modeAction,
                    FinalClientsDetailViewModel = finalClientsViewModel,
                }
            };
            if (modeAction == ModeActionTypeEnum.Edit)
            {
                response.Data.SchedulerViewModel = new SchedulerViewModel() { CalendarEventCategory = _eventCategoriesService.GetAllKeyValuesNotDeleted() };
            }
            return response;
        }

        private IActionResult ModelInvalid(ModeActionTypeEnum modeAction, FinalClientsEditViewModel finalClientsEdit)
        {
            FinalClientsEditViewModel returnModel = finalClientsEdit;
            FillData(returnModel.FinalClientsDetailViewModel);
            return View(modeAction.ToString(), ProcessResult(returnModel, new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = LocalizationsConstants.FormErrorsMessage,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            }));
        }

        private FinalClientsDetailViewModel FillData(FinalClientsDetailViewModel viewModel)
        {
            viewModel.ComercialPeople = _peopleservice.GetAllCommercialKeyValues().ToSelectList();
            viewModel.Origins = _originService.GetAllOriginKeyValues().ToSelectList();

            return viewModel;
        }

        private ResultViewModel<FinalClientsListViewModel> DoFilterAndPaging(FinalClientsFilterViewModel filters)
        {
            var data = new ResultViewModel<FinalClientsListViewModel>();
            var filteredData = _finalClients.GetAllFiltered(filters.ToDto()).Data.ToViewModel();
            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<FinalClientsViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new FinalClientsListViewModel()
                {
                    FinalClientsList = new MultiItemViewModel<FinalClientsViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new FinalClientsListViewModel()
                {
                    FinalClientsList = new MultiItemViewModel<FinalClientsViewModel, int>(filteredData)
                };
            }
            data.Data.Filters = filters;
            return data;
        }
    }
}