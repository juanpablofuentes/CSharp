using Group.Salto.Common;
using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.People;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Company;
using Group.Salto.ServiceLibrary.Common.Contracts.EventCategories;
using Group.Salto.ServiceLibrary.Common.Contracts.Idantity;
using Group.Salto.ServiceLibrary.Common.Contracts.Knowledge;
using Group.Salto.ServiceLibrary.Common.Contracts.Language;
using Group.Salto.ServiceLibrary.Common.Contracts.Maturity;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.ServiceLibrary.Common.Contracts.PeopleCost;
using Group.Salto.ServiceLibrary.Common.Contracts.PointRate;
using Group.Salto.ServiceLibrary.Common.Contracts.Project;
using Group.Salto.ServiceLibrary.Common.Contracts.Rol;
using Group.Salto.ServiceLibrary.Common.Contracts.RolesTenant;
using Group.Salto.ServiceLibrary.Common.Contracts.Subcontracts;
using Group.Salto.ServiceLibrary.Common.Contracts.User;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkCenter;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleCost;
using Group.Salto.ServiceLibrary.Common.Dtos.User;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.MultiSelect;
using Group.Salto.SOM.Web.Models.People;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Scheduler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.People
{
    public class PeopleController : IdentityController
    {
        private readonly IPeopleService _peopleService;
        private readonly IAccessService _accessService;
        private readonly IRolTenantService _rolTenantService;
        private readonly IPeopleAdapter _peopleAdapter;
        private readonly ILanguageService _languageService;
        private readonly ICompanyService _companyService;
        private readonly IPointRateService _pointRateService;
        private readonly ISubContractService _subContractService;
        private readonly IMaturityService _maturityService;
        private readonly IProjectsService _projectRateService;
        private readonly IPeopleCostService _peopleCostService;
        private readonly IConfiguration _Appconfiguration;
        private readonly IEventCategoriesService _eventCategoriesService;

        public PeopleController(ILoggingService loggingService,
                                IConfiguration configuration,
                                ILanguageService languageService,
                                IPeopleService peopleService,
                                IAccessService accessService,
                                IRolTenantService rolTenantService,
                                IPeopleAdapter peopleAdapter,
                                IHttpContextAccessor accessor,
                                IIdentityService identityService,
                                ICompanyService companyService,
                                IPointRateService pointRateService,
                                ISubContractService subContractService,
                                IMaturityService maturityService,
                                IProjectsService projectRateService,
                                IPeopleCostService peopleCostService,
                                IEventCategoriesService eventCategoriesService)
            : base(identityService, loggingService, accessor, configuration)
        {
            _peopleService = peopleService ?? throw new ArgumentNullException($"{nameof(IPeopleService)} is null ");
            _rolTenantService = rolTenantService ?? throw new ArgumentNullException($"{nameof(IRolTenantService)} is null ");
            _peopleAdapter = peopleAdapter ?? throw new ArgumentNullException($"{nameof(IPeopleAdapter)} is null ");
            _languageService = languageService ?? throw new ArgumentNullException($"{nameof(ILanguageService)} is null ");
            _accessService = accessService ?? throw new ArgumentNullException($"{nameof(IAccessService)} is null ");
            _companyService = companyService ?? throw new ArgumentNullException($"{nameof(ICompanyService)} is null ");
            _pointRateService = pointRateService ?? throw new ArgumentNullException($"{nameof(IPointRateService)} is null ");
            _subContractService = subContractService ?? throw new ArgumentNullException($"{nameof(ISubContractService)} is null ");
            _maturityService = maturityService ?? throw new ArgumentNullException($"{nameof(IMaturityService)} is null ");
            _projectRateService = projectRateService ?? throw new ArgumentNullException($"{nameof(IProjectsService)} is null ");
            _peopleCostService = peopleCostService ?? throw new ArgumentNullException($"{nameof(IPeopleCostService)} is null ");
            _Appconfiguration = configuration ?? throw new ArgumentNullException($"{nameof(IConfiguration)} is null ");
            _eventCategoriesService = eventCategoriesService ?? throw new ArgumentNullException($"{nameof(IEventCategoriesService)} is null ");
        }

        [CustomAuthorization(ActionGroupEnum.People, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"People.Get Index action");
            var result = DoFilterAndPaging(new PeopleFilterViewModel() { Active = ActiveEnum.Active });
            var feedback = GetFeedbackTempData();

            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpPost]
        public IActionResult Filter(PeopleFilterViewModel filter)
        {
            var result = DoFilterAndPaging(filter);
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(nameof(Index), result);
        }

        [HttpPost]
        public ActionResult Excel(PeopleFilterViewModel filter)
        {
            FileContentDto file = _peopleAdapter.ExportToExcel(filter.ToDto(), new Guid(base.GetTenantId()));
            if (file != null && file.FileBytes != null)
            {
                return File(file.FileBytes, LocalizationsConstants.ExcelMimeType, file.FileName);
            }
            else
            {
                return PartialView("_NoData", null);
            }
        }

        private ResultViewModel<PeoplesViewModel> DoFilterAndPaging(PeopleFilterViewModel filters)
        {
            var data = new ResultViewModel<PeoplesViewModel>();
            var filteredData = _peopleAdapter.GetList(filters.ToDto(), new Guid(base.GetTenantId())).ToViewModel();

            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<PeopleViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new PeoplesViewModel()
                {
                    Peoples = new MultiItemViewModel<PeopleViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new PeoplesViewModel()
                {
                    Peoples = new MultiItemViewModel<PeopleViewModel, int>(filteredData)
                };
            }

            data.Data.PeopleFilters = filters;

            return data;
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.People, ActionEnum.Create)]
        public async Task<IActionResult> Create()
        {
            LoggingService.LogInfo("People.Create Get");
            ResultViewModel<PeopleEditViewModel> response = ToCreateResultViewModel();

            return View(response);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.People, ActionEnum.GetById)]
        public async Task<IActionResult> Edit(int Id)
        {
            LoggingService.LogInfo("People.Create Edit");

            ResultViewModel<PeopleEditViewModel> response = await ToEditResultViewModel(Id, default(Guid));

            if (response is null)
                return RedirectToAction(nameof(Index));
            else
                return View(response);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.People, ActionEnum.Create)]
        public async Task<IActionResult> Create(PeopleEditViewModel peopleData)
        {
            if (ModelState.IsValid)
            {
                GlobalPeopleDto globalPeopleDto = new GlobalPeopleDto()
                {
                    PeopleDto = peopleData.PersonalEditViewModel.ToPeopleDto(peopleData.WorkEditViewModel, peopleData.GeoLocalitzationEditViewModel),
                    AccessUserDto = peopleData.AccessEditViewModel.ToAccessUserDto(peopleData.PersonalEditViewModel, GetTenantId())
                };

                var result = await _peopleAdapter.CreatePeople(globalPeopleDto);

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle, PeopleConstants.PeopleCreatedSuccess, FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }

                var resultData = ProcessResult(peopleData);
                resultData.Feedbacks = result.Errors.ToViewModel();
                LogFeedbacks(resultData.Feedbacks.Feedbacks);
                FillComboData(resultData);
                return View("Create", resultData);
            }

            var createResult = ProcessResult(peopleData, new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = PeopleConstants.PeopleCreateError,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            });
            FillComboData(createResult);
            return View("Create", createResult);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.People, ActionEnum.Update)]
        public async Task<IActionResult> Edit(PeopleEditViewModel peopleData)
        {
            if (ModelState.IsValid)
            {
                GlobalPeopleDto globalPeopleDto = new GlobalPeopleDto()
                {
                    PeopleDto = peopleData.PersonalEditViewModel.ToPeopleDto(peopleData.WorkEditViewModel, peopleData.GeoLocalitzationEditViewModel),
                    AccessUserDto = peopleData.AccessEditViewModel.ToAccessUserDto(peopleData.PersonalEditViewModel, GetTenantId())
                };

                var result = await _peopleAdapter.UpdatePeople(globalPeopleDto);

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle, PeopleConstants.PeopleUpdateSuccess, FeedbackTypeEnum.Success);
                    await RefreshUserLanguage();
                    return RedirectToAction("Index");
                }

                var resultData = ProcessResult(peopleData);
                resultData.Feedbacks = result.Errors.ToViewModel();
                LogFeedbacks(resultData.Feedbacks.Feedbacks);
                FillComboData(resultData);
                return View("Edit", resultData);
            }

            peopleData.ModeActionType = ModeActionTypeEnum.Edit;
            peopleData.SchedulerViewModel = new SchedulerViewModel() { CalendarEventCategory = _eventCategoriesService.GetAllKeyValuesNotDeleted() };
            var editResult = ProcessResult(peopleData, new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = PeopleConstants.PeopleUpdateError,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            });
            FillComboData(editResult);
            return View("Edit", editResult);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.People, ActionEnum.Delete)]
        public bool Delete(int Id)
        {
            LoggingService.LogInfo($"People.Delete Post {Id}");
            ResultDto<bool> deleteResult = _peopleAdapter.DeletePeople(Id);

            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle, PeopleConstants.PeopleDeleteSuccessMessage, FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(deleteResult.Errors.Errors.FirstOrDefault().ErrorType.ToString(), deleteResult.Errors.Errors.FirstOrDefault().ErrorMessageKey, FeedbackTypeEnum.Error);
            }

            return deleteResult.Data;
        }

        [HttpGet]
        public IActionResult GetDepartmentsByCompany(int id)
        {
            var departments = _companyService.GetDepartmentsByCompanyIdKeyValues(id).ToKeyValuePair();
            return Ok(departments);
        }

        [HttpGet]
        public IActionResult GetPeopleByCompanyId(int companyId, int id)
        {
            var people = _peopleService.GetByCompanyIdKeyValues(companyId, id).ToKeyValuePair();
            return Ok(people);
        }

        [HttpGet]
        public IActionResult Cost()
        {
            var model = new PeopleCostEditViewModel { CostId = 0 };

            return PartialView("_CostModalForm", model);
        }

        [HttpPost]
        public IActionResult CostCreate(PeopleCostEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _peopleCostService.Create(model.ToPeopleCostDto());
                if (result.Errors?.Errors != null)
                {
                    foreach (var error in result.Errors?.Errors)
                    {
                        ModelState.AddModelError(error.ErrorMessageKey, error.ErrorType.ToString());
                    }
                }
            }
            return PartialView("_CostModalForm", model);
        }

        [HttpPost]
        public IActionResult CostEdit(PeopleCostEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _peopleCostService.Update(model.ToPeopleCostDto());
                if (result.Errors?.Errors != null)
                {
                    foreach (var error in result.Errors?.Errors)
                    {
                        ModelState.AddModelError(error.ErrorMessageKey, error.ErrorType.ToString());
                    }
                }
            }
            return PartialView("_CostModalForm", model);
        }

        [HttpPost]
        public int CostDelete(int id)
        {
            ResultDto<bool> result = new ResultDto<bool>();
            if (ModelState.IsValid)
            {
                result = _peopleCostService.Delete(id);
            }
            return id;
        }

        private ResultViewModel<PeopleEditViewModel> ToCreateResultViewModel()
        {
            var gmapsConfig = _Appconfiguration.GetSection(AppsettingsKeys.GmapsConfiguration);
            var response = new ResultViewModel<PeopleEditViewModel>()
            {
                Data = new PeopleEditViewModel()
                {
                    ModeActionType = ModeActionTypeEnum.Create,
                    PersonalEditViewModel = new PersonalEditViewModel(),
                    AccessEditViewModel = new AccessEditViewModel(),
                    WorkEditViewModel = new WorkEditViewModel(),
                    GeoLocalitzationEditViewModel = new GeoLocalitzationEditViewModel()
                    {
                        Apikey = gmapsConfig.GetValue<string>(AppsettingsKeys.GmapsAPIKey),
                        Latitude = gmapsConfig.GetValue<string>(AppsettingsKeys.GmapsLatitude),
                        Longitude = gmapsConfig.GetValue<string>(AppsettingsKeys.GmapsLongitude)
                    },
                    SchedulerViewModel = new SchedulerViewModel()
                }
            };

            FillComboData(response);

            return response;
        }

        private void FillComboData(ResultViewModel<PeopleEditViewModel> response)
        {
            MultiSelectViewModel permissions = new MultiSelectViewModel(PeopleConstants.ListPermissions);
            response.Data.PersonalEditViewModel.Languages = _languageService.GetAll().Data.ToSelectListItemEnumerable();
            response.Data.AccessEditViewModel.Roles = _rolTenantService.GetAll().Data.ToSelectRolListItemEnumerable();
            permissions.Items = _peopleAdapter.GetPermissionList(response.Data.PersonalEditViewModel.Id).Data.ToViewModel();
            response.Data.AccessEditViewModel.Permissions = permissions;
            response.Data.WorkEditViewModel.Companies = _companyService.GetAllNotDeleteKeyValues().ToSelectList();
            response.Data.WorkEditViewModel.PointRates = _pointRateService.GetAllKeyValues().ToSelectList();
            response.Data.WorkEditViewModel.SubContracts = _subContractService.GetAllKeyValues().ToSelectList();
            response.Data.WorkEditViewModel.Projects = _projectRateService.GetAllKeyValues().ToSelectList();
            response.Data.WorkEditViewModel.Priorities = _maturityService.GetBaseMaturities().ToSelectList();
        }

        private async Task<ResultViewModel<PeopleEditViewModel>> ToEditResultViewModel(int id, Guid userIdGuid)
        {
            ResultDto<PeopleDto> data = _peopleService.GetById(id);
            if (data.Errors?.Errors == null || !data.Errors.Errors.Any())
            {
                PeopleDto peopleData = data.Data;
                string userId = GetUserId(userIdGuid, peopleData.UserConfigurationId.Value);

                ResultDto<AccessUserDto> accessData = new ResultDto<AccessUserDto>();
                accessData.Data = new AccessUserDto();

                if (!string.IsNullOrEmpty(userId))
                {
                    accessData = await _accessService.GetUserById(userId);
                }

                ResultDto<IList<PeopleCostDetailDto>> peopleCost = _peopleCostService.GetByPeopleId(id);

                var response = new ResultViewModel<PeopleEditViewModel>()
                {
                    Data = new PeopleEditViewModel()
                    {
                        ModeActionType = ModeActionTypeEnum.Edit,
                        PersonalEditViewModel = peopleData.ToPeopleViewModel(accessData.Data.LanguageId),
                        AccessEditViewModel = accessData.Data.ToViewModel(),
                        WorkEditViewModel = peopleData.ToWorkViewModel(),
                        GeoLocalitzationEditViewModel = peopleData.ToGeoLocalitzationViewModel(_Appconfiguration.GetSection(AppsettingsKeys.GmapsConfiguration).GetValue<string>(AppsettingsKeys.GmapsAPIKey)),
                        CostSelected = peopleCost.Data.ToPeopleCostEditViewModel(),
                        SchedulerViewModel = new SchedulerViewModel() { CalendarEventCategory = _eventCategoriesService.GetAllKeyValuesNotDeleted() }
                    }
                };

                response.Data.AccessEditViewModel.EditMode = true;
                FillComboData(response);
                return response;
            }
            else
            {
                SetFeedbackTempData(data.Errors.Errors.First().ErrorType.ToString(), data.Errors.Errors.First().ErrorMessageKey, FeedbackTypeEnum.Error);
                return null;
            }
        }

        private string GetUserId(Guid userId, int userConfigurationId)
        {
            string result = string.Empty;

            if (userId == default(Guid))
            {
                Entities.Users user = base._identityService.GetByUserConfigurationId(userConfigurationId);
                if (user != null)
                {
                    result = user.Id;
                }
            }
            else
            {
                result = userId.ToString();
            }

            return result;
        }        
    }
}