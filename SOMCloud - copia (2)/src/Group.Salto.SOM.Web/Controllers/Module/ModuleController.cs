using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Module;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ActionsGroups;
using Group.Salto.ServiceLibrary.Common.Contracts.Module;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Modules;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Modules;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Group.Salto.SOM.Web.Controllers.Module
{
    public class ModuleController : BaseController
    {
        private readonly IModuleService _moduleService;
        private readonly IActionGroupService _actionGroupService;

        public ModuleController(ILoggingService loggingService, 
            IConfiguration configuration, 
            IHttpContextAccessor accessor,
            IModuleService moduleService,
            IActionGroupService actionGroupService) : base(loggingService, configuration, accessor)
        {
            _moduleService = moduleService ?? throw new ArgumentNullException($"{nameof(IModuleService)} is null");
            _actionGroupService = actionGroupService ?? throw new ArgumentNullException($"{nameof(IActionGroupService)} is null");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Modules, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"ModuleController.Getall modules");
            var result = DoFilterAndPaging(new ModuleFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Modules, ActionEnum.Create)]
        public IActionResult Create()
        {
            var result = new ModuleDetailViewModel();
            FillData(result);
            return View(ProcessResult(result));
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Modules, ActionEnum.GetById)]
        public IActionResult Edit(Guid? id)
        {
            LoggingService.LogInfo($"Modules - Edit Id: {id}");
            var result = _moduleService.GetByIdIncludeActionGroups(id);
            var response = result.ToResultViewModel();
            FillData(response?.Data, result?.Data);
            LogFeedbacks(response?.Feedbacks?.Feedbacks);
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
        public IActionResult Filter(ModuleFilterViewModel filter)
        {
            var resultData = DoFilterAndPaging(filter);
            return View(nameof(Index), resultData);
        }        

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Modules, ActionEnum.Create)]
        public IActionResult Create( ModuleDetailViewModel model)
        {
            LoggingService.LogInfo("Modules - Create");
            if (ModelState.IsValid)
            {
                var result = _moduleService.Create(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        ModuleConstants.ModuleSuccessCreateMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction(nameof(Index));
                }
                var resultData = ProcessResult(model, result.Errors.ToViewModel()?.Feedbacks);
                return View(nameof(Create), resultData);
            }
            return ModelInvalid(nameof(Create), model);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Modules, ActionEnum.Update)]
        public IActionResult Edit(ModuleDetailViewModel model)
        {
            LoggingService.LogInfo($"Module - Update Id = {model.Id}");
            if (ModelState.IsValid)
            {
                var result = _moduleService.Update(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        ModuleConstants.ModuleSuccessUpdateMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction(nameof(Index));
                }

                var resultData = ProcessResult(model, result.Errors.ToViewModel()?.Feedbacks);
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View(nameof(Edit), resultData);
            }
            return ModelInvalid(nameof(Edit), model);
        }

        private ResultViewModel<ModulesListViewModel> DoFilterAndPaging(ModuleFilterViewModel filter)
        {
            var data = new ResultViewModel<ModulesListViewModel>();
            var filtered = _moduleService.GetAllFiltered(filter.ToDto()).Data.ToViewModel();
            if (filter.Page != 0)
            {
                filter.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<ModuleBaseViewModel>()
                {
                    Page = filter.Page,
                    PageSize = filter.Size
                };
                data.Data = new ModulesListViewModel()
                {
                    ModuleList = new MultiItemViewModel<ModuleBaseViewModel, Guid>(pager.GetPageIEnumerable(filtered))
                };
                filter.PagesCount = pager.PagesCount;
                filter.Page = pager.Page;
                filter.TotalValues = filtered.Count();
            }
            else
            {
                data.Data = new ModulesListViewModel()
                {
                    ModuleList = new MultiItemViewModel<ModuleBaseViewModel, Guid>(filtered)
                };
            }
            data.Data.ModuleFilter = filter;
            return data;
        }

        private void FillData(ModuleDetailViewModel model, ModuleDetailDto data = null)
        {
            if (model != null)
            {
                var actionGroupsDictionary = _actionGroupService.GetAllKeyValues().OrderBy(x => x.Value);
                var actionGroups = actionGroupsDictionary.Select(x => new BaseNameIdDto<Guid>()
                {
                    Id = x.Key,
                    Name = x.Value
                }).ToList();
                model.ActionGroups = actionGroups.SetSelectedValues(data?.ActionGroupsSelected, ModuleConstants.ModuleDetailActionGroups);
            }
        }

        private IActionResult ModelInvalid(string view, ModuleDetailViewModel moduleDetailViewModel)
        {
            return View(view, ProcessResult(moduleDetailViewModel, new List<FeedbackViewModel>()
            {
                new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = LocalizationsConstants.FormErrorsMessage,
                    FeedbackType = FeedbackTypeEnum.Error,
                }
            }));
        }
    }
}