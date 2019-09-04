using Group.Salto.Common;
using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Sites;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Calendar;
using Group.Salto.ServiceLibrary.Common.Contracts.EventCategories;
using Group.Salto.SOM.Web.Models.Scheduler;
using Group.Salto.ServiceLibrary.Common.Contracts.Country;
using Group.Salto.ServiceLibrary.Common.Contracts.FinalClients;
using Group.Salto.ServiceLibrary.Common.Contracts.PostalCode;
using Group.Salto.ServiceLibrary.Common.Contracts.Sites;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Sites;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Sites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.SOM.Web.Models;

namespace Group.Salto.SOM.Web.Controllers.Sites
{
    [Authorize]
    public class SitesController : BaseController
    {
        private readonly ISitesService _sitesService;
        private readonly IPostalCodeService _postalCodeService;
        private readonly IFinalClientsServices _finalClientsServices;
        private readonly ICountryService _countryService;
        private readonly IConfiguration _appconfiguration;
        private readonly IEventCategoriesService _eventCategoriesService;

        public SitesController(ILoggingService loggingService,
                                IConfiguration configuration,
                                IHttpContextAccessor accessor,
                                ICountryService countryService,
                                IFinalClientsServices finalClientsServices,
                                ISitesService sitesService,
                                IEventCategoriesService eventCategoriesService,
        IPostalCodeService postalCodeService) : base(loggingService, configuration, accessor)
        {
            _sitesService = sitesService ?? throw new ArgumentNullException($"{nameof(ISitesService)} is null");
            _postalCodeService = postalCodeService ?? throw new ArgumentNullException($"{nameof(IPostalCodeService)} is null");
            _finalClientsServices = finalClientsServices ?? throw new ArgumentNullException($"{nameof(ISitesService)} is null");
            _countryService = countryService ?? throw new ArgumentNullException($"{nameof(ICountryService)} is null");
            _appconfiguration = configuration ?? throw new ArgumentNullException($"{nameof(IConfiguration)} is null ");
            _eventCategoriesService = eventCategoriesService ?? throw new ArgumentNullException(nameof(IEventCategoriesService));
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            LoggingService.LogInfo($"Sites.Get get all ");
            var finalClient = _finalClientsServices.GetById(id);
            var result = DoFilterAndPaging(new SitesFilterViewModel()
            {
                FinalClientId = id,
                FilterHeader = new FilterHeaderViewModel()
                {
                    parentId = id
                },
                FinalClientName = finalClient.Data.Name
            });

            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            LoggingService.LogInfo("Sites.Create");

            var gmapsConfig = _appconfiguration.GetSection(AppsettingsKeys.GmapsConfiguration);

            var response = new ResultViewModel<SitesDetailViewModel>()
            {
                Data = new SitesDetailViewModel()
                {
                    GenericDetailViewModel = new GenericDetailViewModel(),
                    GeolocationDetailViewModel = new GeolocationDetailViewModel()
                    {
                        Apikey = gmapsConfig.GetValue<string>(AppsettingsKeys.GmapsAPIKey)
                    },
                }
            };
            response.Data.GenericDetailViewModel.FinalClientId = id;
            response.Data = FillData(response.Data);
            return View(response);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = _sitesService.GetById(id);
            var gmapsConfig = _appconfiguration.GetSection(AppsettingsKeys.GmapsConfiguration);
            var geolocation = new GeolocationDetailViewModel()
            {
                Apikey = gmapsConfig.GetValue<string>(AppsettingsKeys.GmapsAPIKey)
            };

            var response = new ResultViewModel<SitesDetailViewModel>()
            {
                Data = new SitesDetailViewModel()
                {
                    ModeActionType = ModeActionTypeEnum.Edit,
                    GenericDetailViewModel = result.ToGenericViewModel(),
                    GeolocationDetailViewModel = result.ToGeolocationViewModel(geolocation),
                    SchedulerViewModel = new SchedulerViewModel() { CalendarEventCategory = _eventCategoriesService.GetAllKeyValuesNotDeleted() }
                }
            };
            response.Data = FillData(response.Data);
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
        public IActionResult Create(SitesDetailViewModel model)
        {
            LoggingService.LogInfo("Sites.Post Create Sites");
            ResultViewModel<SitesDetailViewModel> result = null;

            if (ModelState.IsValid)
            {
                ResultDto<SitesDetailDto> resultCreate = null;
                SitesDetailDto modelDto = model.ToDto();

                if (ValidateCPandCity(modelDto))
                {
                    resultCreate = _sitesService.Create(model.ToDto());
                    if (resultCreate.Errors?.Errors == null || !resultCreate.Errors.Errors.Any())
                    {
                        SetFeedbackTempData(LocalizationsConstants.SuccessTitle, SitesConstants.SitesCreateSuccessMessage, FeedbackTypeEnum.Success);
                        return RedirectToAction("Index", new { id = model.GenericDetailViewModel.FinalClientId });
                    }
                    var resultData = resultCreate.ToViewModel();
                    LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                    return View(resultData);
                }
                else
                {
                    result = ProcessResult(FillData(model), LocalizationsConstants.Error, SitesConstants.SitesWrongCp, FeedbackTypeEnum.Error);
                    return View("Create", result);
                }
            }
            return ModelInvalid(ModeActionTypeEnum.Create, model);
        }

        [HttpPost]
        public IActionResult Edit(SitesDetailViewModel model)
        {
            LoggingService.LogInfo("Sites.Post Edit Sites");
            ResultViewModel<SitesDetailViewModel> result = null;
            if (ModelState.IsValid)
            {
                ResultDto<SitesDetailDto> resultUpdate = null;
                
                SitesDetailDto modelDto = model.ToDto();
                if (ValidateCPandCity(modelDto))
                {
                    resultUpdate = _sitesService.Update(modelDto);
                    if (resultUpdate.Errors?.Errors == null || !resultUpdate.Errors.Errors.Any())
                    {
                        //TODO Add constants
                        SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                            SitesConstants.SitesUpdateSuccessMessage,
                                            FeedbackTypeEnum.Success);
                        return RedirectToAction("Index", new { id = model.GenericDetailViewModel.FinalClientId });
                    }
                    var resultData = resultUpdate.ToViewModel();
                    LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                    return View(resultData);
                }
                else
                {
                    result = ProcessResult(FillData(model), LocalizationsConstants.Error, SitesConstants.SitesWrongCp, FeedbackTypeEnum.Error);
                    return View("Edit", result);
                }
            }
            result = ProcessResult(FillData(model), LocalizationsConstants.Error, LocalizationsConstants.FormErrorsMessage, FeedbackTypeEnum.Error);
            return View("Edit", result);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var deleteResult = _sitesService.Delete(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    SitesConstants.SiteDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                    SitesConstants.SitesDeleteErrorMessage,
                    FeedbackTypeEnum.Error);
            }
            return Ok(deleteResult.Data);
        }

        [HttpPost]
        public IActionResult FilterWeb(SitesFilterViewModel filter)
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

        private IActionResult ModelInvalid(ModeActionTypeEnum modeAction, SitesDetailViewModel site)
        {
            return View(modeAction.ToString(), ProcessResult(FillData(site), new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = LocalizationsConstants.FormErrorsMessage,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            }));
        }

        private SitesDetailViewModel FillData(SitesDetailViewModel source)
        {
            source.GeolocationDetailViewModel.Countries = _countryService.GetAllKeyValues().ToKeyValuePair();
            return source;
        }

        private ResultViewModel<SitesListViewModel> DoFilterAndPaging(SitesFilterViewModel filter)
        {
            var data = new ResultViewModel<SitesListViewModel>();
            var filteredData = _sitesService.GetAllFiltered(filter.ToFilterDto()).Data.ToListViewModel();

            if (filter.Page != 0)
            {
                filter.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<SitesViewModel>()
                {
                    Page = filter.Page,
                    PageSize = filter.Size
                };
                data.Data = new SitesListViewModel()
                {
                    SitesList = new MultiItemViewModel<SitesViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filter.PagesCount = pager.PagesCount;
                filter.Page = pager.Page;
                filter.TotalValues = filteredData.Count;
            }
            else
            {
                data.Data = new SitesListViewModel()
                {
                    SitesList = new MultiItemViewModel<SitesViewModel, int>(filteredData)
                };
            }
            data.Data.Filters = filter;
            return data;
        }

        private bool ValidateCPandCity(SitesDetailDto model)
        {
            var city = model.MunicipalityId.HasValue ? model.MunicipalityId.Value : 0;
            var cp = model.PostalCode;
            return _postalCodeService.ValidateCodeAndCity(cp, city);  
        }
    }
}