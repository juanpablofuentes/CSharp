using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Items;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Items;
using Group.Salto.ServiceLibrary.Common.Contracts.ItemTypes;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Items;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.Items
{
    public class ItemsController : BaseController
    {
        private readonly IItemsService _itemsService;
        private readonly IItemTypesService _itemTypesService;

        public ItemsController(ILoggingService loggingService,
                                IConfiguration configuration,
                                IHttpContextAccessor accessor,
                                IItemTypesService itemTypesService,
                                IItemsService itemsService) : base(loggingService, configuration, accessor)
        {
            _itemsService = itemsService ?? throw new ArgumentNullException($"{nameof(IItemsService)} is null");
            _itemTypesService = itemTypesService ?? throw new ArgumentNullException($"{nameof(IItemTypesService)} is null");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Items, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Items get all ");

            var result = DoFilterAndPaging(new ItemsFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Items, ActionEnum.Create)]
        public IActionResult Create()
        {
            var response = new ResultViewModel<ItemsDetailViewModel>()
            {
                Data = new ItemsDetailViewModel()
                {
                    GeneralViewModel = new ItemGeneralViewModel()
                }
            };
            response.Data.GeneralViewModel = FillData(response.Data.GeneralViewModel);
            return View(response);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Items, ActionEnum.GetById)]
        public IActionResult Edit(int Id)
        {
            var result = _itemsService.GetById(Id);
            var response = result.ToResultViewModel();
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                response.Feedbacks = response.Feedbacks ?? new FeedbacksViewModel();
                response.Feedbacks.AddFeedback(feedback);
            }
            LogFeedbacks(response.Feedbacks?.Feedbacks);
            response.Data.GeneralViewModel = FillData(response.Data.GeneralViewModel);
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
        [CustomAuthorization(ActionGroupEnum.Items, ActionEnum.Update)]
        public async Task<IActionResult> Edit(ItemsDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var item = model.ToDto();
                if (model.GeneralViewModel?.Picture != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.GeneralViewModel.Picture.CopyToAsync(memoryStream);
                        item.Picture = memoryStream?.ToArray() ?? new byte[0];
                    }
                }
                var resultUpdate = _itemsService.UpdateItem(item);
                if (resultUpdate.Errors?.Errors == null || !resultUpdate.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        ItemsConstants.ItemsUpdateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                else
                {
                    SetFeedbackTempData(LocalizationsConstants.Error,
                                       ItemsConstants.ItemsUpdateErrorMessage,
                                       FeedbackTypeEnum.Error);
                    return RedirectToAction("Edit");
                }
            }
            var result = ProcessResult(model, LocalizationsConstants.Error,
                LocalizationsConstants.FormErrorsMessage,
                FeedbackTypeEnum.Error);
            return View("Edit", result);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Items, ActionEnum.Create)]
        public async Task<IActionResult> Create(ItemsDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var item = model.ToDto();
                if (model.GeneralViewModel?.Picture != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await model.GeneralViewModel.Picture.CopyToAsync(memoryStream);
                        item.Picture = memoryStream?.ToArray() ?? new byte[0];
                    }
                }
                var result = _itemsService.CreateItem(item);
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        ItemsConstants.ItemsCreateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = new ResultViewModel<ItemsDetailViewModel>()
                {
                    Data = result.Data.ToViewModel(),
                    Feedbacks = result.Errors.ToViewModel()
                };
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
        public IActionResult Filter(ItemsFilterViewModel filter)
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
        [CustomAuthorization(ActionGroupEnum.Items, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            var deleteResult = _itemsService.DeleteItem(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    ItemsConstants.ItemsDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                    ItemsConstants.ItemsDeleteErrorMessage,
                    FeedbackTypeEnum.Error);
            }
            return Ok(deleteResult.Data);
        }

        private ResultViewModel<ItemsListViewModel> DoFilterAndPaging(ItemsFilterViewModel filters)
        {
            var data = new ResultViewModel<ItemsListViewModel>();
            var filteredData = _itemsService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();

            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<ItemViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new ItemsListViewModel()
                {
                    Items = new MultiItemViewModel<ItemViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new ItemsListViewModel()
                {
                    Items = new MultiItemViewModel<ItemViewModel, int>(filteredData)
                };
            }

            data.Data.ItemsFilters = filters;
            return data;
        }

        private ItemGeneralViewModel FillData(ItemGeneralViewModel source)
        {
            source.ItemTypes = _itemTypesService.GetAllKeyValues().ToSelectList().ToList();
            return source;
        }
    }
}