using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.SiteUser;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Sites;
using Group.Salto.ServiceLibrary.Common.Contracts.SitesFinalClients;
using Group.Salto.ServiceLibrary.Common.Contracts.SiteUser;
using Group.Salto.ServiceLibrary.Implementations.SiteUser;
using Group.Salto.SOM.Web.Models;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.SiteUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Group.Salto.SOM.Web.Controllers.SiteUser
{
    [Authorize]
    public class SiteUserController : BaseController
    {
        private readonly ISiteUserService _siteUserService;
        private readonly ISitesService _sitesService;
        private readonly ISitesFinalClientsService _sitesFinalClientsService;
        public SiteUserController(ILoggingService loggingService,
                                                    IConfiguration configuration,
                                                    IHttpContextAccessor accessor,
                                                    ISitesService sitesService,
                                                    ISitesFinalClientsService sitesFinalClientsService,
                                                    ISiteUserService siteUserService) : base(loggingService, configuration, accessor)
        {
            _sitesService = sitesService ?? throw new ArgumentNullException($"{nameof(ISitesService)} is null");
            _siteUserService = siteUserService ?? throw new ArgumentNullException($"{nameof(SiteUserService)} is null");
            _sitesFinalClientsService = sitesFinalClientsService ?? throw new ArgumentNullException($"{nameof(ISitesFinalClientsService)} is null");
        }

        [HttpGet]
        public IActionResult Index(int Id)
        {
            LoggingService.LogInfo($"SiteUser.Get get all Site Users");
            var site = _sitesService.GetById(Id);
            var result = DoFilterAndPaging(new SiteUserFilterViewModel()
            {
                SitesId = Id,
                FilterHeader = new FilterHeaderViewModel() {
                    parentId = Id
                },
                SitesName = site.Data.Name
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
        public IActionResult Edit(int id)
        {
            var result = _siteUserService.GetById(id);
            var resultData = result.ToViewModel();
            if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
            {
                return View("Edit", resultData);
            }
            SetFeedbackTempData(LocalizationsConstants.Error,
                LocalizationsConstants.ErrorLoadingDataMessage,
                FeedbackTypeEnum.Error);
            return Redirect(nameof(Index));
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            LoggingService.LogInfo("CollectionsExtraField.Create Get");
            var response = new ResultViewModel<SiteUserDetailViewModel>()
            {
                Data = FillData(new SiteUserDetailViewModel() {
                    LocationId = id
                })
            };

            return View(response);
        }

        [HttpPost]
        public IActionResult Edit(SiteUserDetailViewModel model)
        {
            LoggingService.LogInfo($"SiteUser.Post update for id = {model.IdUser}");
            if (ModelState.IsValid)
            {
                var result = _siteUserService.Update(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                       SiteUserConstants.SiteUserUpdateSuccessMessage,
                       FeedbackTypeEnum.Success);
                    return RedirectToAction(nameof(Index), new { id = model.LocationId });
                }
                var resultData = result.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View(nameof(Edit), resultData);
            }
            return View(nameof(Edit), ProcessResult(model));
        }

        [HttpPost]
        public IActionResult Create(SiteUserDetailViewModel model)
        {
            LoggingService.LogInfo("SiteUser create");
            if (ModelState.IsValid)
            {
                var result = _siteUserService.Create(model.ToDto());

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        SiteUserConstants.SiteUserCreateSuccess,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index", new { id = model.LocationId });
                }
                var resultData = result.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            var results = ProcessResult(model,
                         LocalizationsConstants.Error,
                         LocalizationsConstants.FormErrorsMessage,
                         FeedbackTypeEnum.Error);
            return View("Create", results);
        }

        [HttpPost]
        public IActionResult FilterWeb(SiteUserFilterViewModel filter)
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

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var deleteResult = _siteUserService.Delete(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    SiteUserConstants.SiteUserDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                    SiteUserConstants.SiteUserDeleteErrorMessage,
                    FeedbackTypeEnum.Error);
            }
            return Ok(deleteResult.Data);
        }

        private ResultViewModel<SiteUserListViewModel> DoFilterAndPaging(SiteUserFilterViewModel filters)
        {
            var data = new ResultViewModel<SiteUserListViewModel>();
            var filteredData = _siteUserService.GetAllFiltered(filters.ToFilterDto()).Data.ToListViewModel();

            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<SiteUserViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new SiteUserListViewModel()
                {
                    SiteUserList = new MultiItemViewModel<SiteUserViewModel, int>(pager.GetPageIEnumerable(filteredData)),
                    FinalClientsId = _sitesFinalClientsService.GetBySiteId(filters.SitesId).Data.FinalClientId,
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new SiteUserListViewModel()
                {
                    SiteUserList = new MultiItemViewModel<SiteUserViewModel, int>(filteredData)
                };
            }

            data.Data.Filters = filters;
            return data;
        }

        private SiteUserDetailViewModel FillData(SiteUserDetailViewModel source)
        {
            return source;
        }
    }
}