using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Assets;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Assets;
using Group.Salto.ServiceLibrary.Common.Contracts.AssetStatuses;
using Group.Salto.ServiceLibrary.Common.Contracts.Contracts;
using Group.Salto.ServiceLibrary.Common.Contracts.Idantity;
using Group.Salto.ServiceLibrary.Common.Contracts.Sites;
using Group.Salto.ServiceLibrary.Common.Contracts.SitesFinalClients;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models;
using Group.Salto.SOM.Web.Models.Assets;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.SOM.Web.Controllers.Assets
{
    public class AssetsController : BaseController
    {
        private readonly IAssetsService _assetsService;
        private readonly ISitesService _sitesService;
        private readonly ISitesFinalClientsService _sitesFinalClientsService;
        private readonly IAssetStatusesService _assetStatusesService;
        private readonly IContractsService _contractsService;

        public AssetsController(ILoggingService loggingService,
                                IConfiguration configuration,
                                IHttpContextAccessor accessor,
                                IContractsService contractsService,
                                ISitesFinalClientsService sitesFinalClientsService,
                                IAssetStatusesService assetStatusesService,
                                ISitesService sitesService,
                                IAssetsService assetsService) : base(loggingService, configuration, accessor)
        {
            _assetsService = assetsService ?? throw new ArgumentNullException($"{nameof(IAssetsService)} is null");
            _sitesService = sitesService ?? throw new ArgumentNullException($"{nameof(ISitesService)} is null");
            _assetStatusesService = assetStatusesService ?? throw new ArgumentNullException($"{nameof(IAssetStatusesService)} is null");
            _contractsService = contractsService ?? throw new ArgumentNullException($"{nameof(IAssetsService)} is null");
            _sitesFinalClientsService = sitesFinalClientsService ?? throw new ArgumentNullException($"{nameof(ISitesFinalClientsService)} is null");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Assets, ActionEnum.GetAll)]
        public IActionResult Index(int? id)
        {
            LoggingService.LogInfo($"Assets.Get get all ");

            var idNotNull = id.HasValue ? id.Value : 0;

            var result = DoFilterAndPaging(new AssetsFilterViewModel()
            {
                SitesId = idNotNull,
                FilterHeader = new FilterHeaderViewModel()
                {
                    parentId = idNotNull
                }
            });

            if (idNotNull != 0)
            {
                var site = _sitesService.GetById(idNotNull);
                result.Data.Filters.SitesId = idNotNull;
                result.Data.Filters.SitesName = site.Data.Name;
                result.Data.FromSiteId = idNotNull;
            }

            //TODO refactor status maybe
            ViewBag.Statuses = _assetStatusesService.GetAllKeyValues()?.ToSelectList().ToList();

            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Assets, ActionEnum.Create)]
        public IActionResult Create(int? id)
        {
            var response = new ResultViewModel<AssetsDetailViewModel>();
            response.Data = new AssetsDetailViewModel
            {
                FromSiteId = id,
                GenericDetailViewModel = FillData(new GenericDetailViewModel(), id),
                SecondaryDetailViewModel = new SecondaryDetailViewModel()
            };
            if (id != null && id != 0)
            {
                var site = _assetsService.GetAssetPartialDetailBySiteIdWithFinalClient((int)id);
                if (site != null)
                {
                    response.Data.GenericDetailViewModel.SiteClient = site.Data.SiteClient.ToComboViewModel();
                    response.Data.GenericDetailViewModel.SiteLocation = new MultiComboViewModel<int, string>
                    {
                        Value = site.Data.SelectedSiteLocation.Key,
                        Text = site.Data.SelectedSiteLocation.Value,
                    };
                    response.Data.GenericDetailViewModel.FromSiteId = id;
                }
            }

            return View(response);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Assets, ActionEnum.GetById)]
        public IActionResult Edit(int id, int? siteId)
        {
            var result = _assetsService.GetById(id);
            var resultData = result.ToViewModel();
            resultData.Data.FromSiteId = siteId;
            resultData.Data.GenericDetailViewModel = FillData(resultData.Data.GenericDetailViewModel, resultData.Data.FromSiteId);

            if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
            {
                return View("Edit", resultData);
            }
            SetFeedbackTempData(LocalizationsConstants.Error,
                LocalizationsConstants.ErrorLoadingDataMessage,
                FeedbackTypeEnum.Error);
            return RedirectToAction(nameof(Index), new { id = siteId });
        }

        [HttpPost]
        public IActionResult Transfer(AssetsTransferViewModel model)
        {
            var siteId = model.PageSiteId ?? 0;
            if (ModelState.IsValid)
            {
                model.UserId = GetConfigurationUserId();
                var result = _assetsService.Transfer(model.ToDto());
                if (result.Errors?.Errors == null || result.Errors?.Errors?.Count == 0)
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        AssetsConstants.AssetsUpdateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index", new { Id = siteId });
                }
            }
            return RedirectToAction("Index", new { id = siteId });
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Assets, ActionEnum.Create)]
        public IActionResult Create(AssetsDetailViewModel model)
        {
            if (ModelState.IsValid && model?.GenericDetailViewModel?.SiteLocation?.Value != null)
            {
                model.UserId = GetConfigurationUserId();
                var result = _assetsService.Create(model.ToDto());

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        AssetsConstants.AssetsCreateSuccessMessage,
                                        FeedbackTypeEnum.Success);

                    return RedirectToAction("Index", new { Id = model.GenericDetailViewModel.FromSiteId });
                }
                var resultData = result.ToViewModel();
                resultData.Data.FromSiteId = model.FromSiteId;
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            model.GenericDetailViewModel = FillData(new GenericDetailViewModel(), model.FromSiteId);
            var results = ProcessResult(model,
                         LocalizationsConstants.Error,
                         LocalizationsConstants.FormErrorsMessage,
                         FeedbackTypeEnum.Error);
            return View("Create", results);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Assets, ActionEnum.Update)]
        public IActionResult Update(AssetsDetailViewModel model)
        {
            if (ModelState.IsValid && model.GenericDetailViewModel.SiteLocation?.Value != null)
            {
                model.UserId = GetConfigurationUserId();
                var resultUpdate = _assetsService.Update(model.ToDto());
                if (resultUpdate.Errors?.Errors == null || !resultUpdate.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        AssetsConstants.AssetsUpdateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index", new { Id = model.GenericDetailViewModel.FromSiteId });
                }
                var resultData = resultUpdate.ToViewModel();
                resultData.Data.FromSiteId = model.FromSiteId;
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Edit", resultData);
            }
            model.GenericDetailViewModel = FillData(new GenericDetailViewModel(), model.FromSiteId);
            var result = ProcessResult(model, LocalizationsConstants.Error, LocalizationsConstants.FormErrorsMessage, FeedbackTypeEnum.Error);
            return View("Edit", result);
        }

        [HttpPost]
        public IActionResult Filter(AssetsFilterViewModel filter)
        {
            var resultData = DoFilterAndPaging(filter);
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                resultData.Feedbacks = resultData.Feedbacks ?? new FeedbacksViewModel();
                resultData.Feedbacks.AddFeedback(feedback);
            }
            ViewBag.Statuses = _assetStatusesService.GetAllKeyValues()?.ToSelectList().ToList();

            return View(nameof(Index), resultData);
        }

        private ResultViewModel<AssetsListViewModel> DoFilterAndPaging(AssetsFilterViewModel filter)
        {
            var data = new ResultViewModel<AssetsListViewModel>();
            var filteredData = _assetsService.GetAllFiltered(filter.ToFilterDto()).Data.ToListViewModel();

            if (filter.Page != 0)
            {
                filter.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<AssetsViewModel>()
                {
                    Page = filter.Page,
                    PageSize = filter.Size
                };
                if (filter.SitesId != 0)
                {
                    data.Data = new AssetsListViewModel()
                    {
                        FinalClientsId = _sitesFinalClientsService.GetBySiteId(filter.SitesId).Data.FinalClientId,
                        AssetsList = new MultiItemViewModel<AssetsViewModel, int>(pager.GetPageIEnumerable(filteredData))
                    };
                }
                else
                {
                    data.Data = new AssetsListViewModel()
                    {
                        FinalClientsId = null,
                        AssetsList = new MultiItemViewModel<AssetsViewModel, int>(pager.GetPageIEnumerable(filteredData))
                    };
                }
                filter.PagesCount = pager.PagesCount;
                filter.Page = pager.Page;
                filter.TotalValues = filteredData.Count;
            }
            else
            {
                data.Data = new AssetsListViewModel()
                {
                    AssetsList = new MultiItemViewModel<AssetsViewModel, int>(filteredData)
                };
            }
            data.Data.Filters = filter;
            data.Data.Transfer = new AssetsTransferViewModel
            {
                PageClientId = data.Data?.FinalClientsId,
                PageSiteId = data.Data?.Filters?.SitesId
            };
            return data;
        }

        private GenericDetailViewModel FillData(GenericDetailViewModel source, int? fromSiteId)
        {
            source.Contracts = _contractsService.GetAllKeyValues()?.ToSelectList().ToList();
            source.Statuses = _assetStatusesService.GetAllKeyValues()?.ToSelectList().ToList();
            source.FromSiteId = fromSiteId;
            var defaultItem = new SelectListItem()
            {
                Text = "-",
                Value = ""
            };
            source.Contracts.Insert(0, defaultItem);

            var selectedContract = source.Contracts.FirstOrDefault(x => x.Value == source.ContractId?.ToString());
            if (selectedContract != null)
            {
                selectedContract.Selected = true;
            }

            var selectedStatus = source.Statuses.FirstOrDefault(x => x.Value == source.StatusId.ToString());
            if (selectedStatus != null)
            {
                selectedStatus.Selected = true;
            }

            return source;
        }
    }
}