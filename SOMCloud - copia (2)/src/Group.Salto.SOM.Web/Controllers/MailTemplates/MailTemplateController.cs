using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.MailTemplate;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.MailTemplate;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkForm;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.MailTemplate;
using Group.Salto.SOM.Web.Models.MultiSelect;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Task;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace Group.Salto.SOM.Web.Controllers.MailTemplates
{
    public class MailTemplateController : BaseController
    {
        private readonly IMailTemplateService _mailTemplateService;
        private readonly IWorkFormService _workFormService;

        public MailTemplateController(
          ILoggingService loggingService,
          IHttpContextAccessor accessor,
          IConfiguration configuration,
          IMailTemplateService mailTemplateService,
          IWorkFormService workFormService) : base(loggingService, configuration, accessor)
        {
            _mailTemplateService = mailTemplateService ?? throw new ArgumentNullException($"{nameof(IMailTemplateService)} is null");
            _workFormService = workFormService ?? throw new ArgumentNullException($"{nameof(IWorkFormService)} is null");
        }

        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.MailTemplates, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            var filter = new MailTemplateFilterViewModel();
            var result = DoFilterAndPaging(filter);
            var feedback = GetFeedbackTempData();

            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpPost]
        public IActionResult Filter(MailTemplateFilterViewModel filter)
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
        
        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.MailTemplates, ActionEnum.Create)]
        public IActionResult Create()
        {
            LoggingService.LogInfo("MailTemplates.Create");
            var response = new ResultViewModel<MailTemplateListViewModel>();
            var WF = _workFormService.GetAllWorkForm().Data.ToListViewModel();
            response.Data = new MailTemplateListViewModel {
                MailTemplate = new MailTemplateViewModel(),
                WorkForm = WF
            };
            return View(response);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.MailTemplates, ActionEnum.Create)]
        public IActionResult Create(MailTemplateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _mailTemplateService.Create(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        MailTemplateConstant.MailTemplateSuccessCreateMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = new ResultViewModel<MailTemplateListViewModel>()//result.ToViewModel();
                {
                    Data = result.Data.ToViewModel().ToListMViewModel(),
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
        
        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.MailTemplates, ActionEnum.GetById)]
        public IActionResult Edit(int Id)
        {
            var result = _mailTemplateService.GetById(Id);

            var response = new ResultViewModel<MailTemplateListViewModel>();
            var WF = _workFormService.GetAllWorkForm().Data.ToListViewModel();
            response.Data = new MailTemplateListViewModel
            {
                MailTemplate = _mailTemplateService.GetById(Id).Data.ToViewModel(),
                WorkForm = WF
            };
            
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                response.Feedbacks = response.Feedbacks ?? new FeedbacksViewModel();
                response.Feedbacks.AddFeedback(feedback);
            }
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
        [CustomAuthorization(ActionGroupEnum.MailTemplates, ActionEnum.Update)]
        public IActionResult Edit(MailTemplateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _mailTemplateService.Update(model.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        MailTemplateConstant.MailTemplateSuccessUpdateMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = new ResultViewModel<MailTemplateListViewModel>()
                {
                    Data = result.Data.ToViewModel().ToListMViewModel(),
                    Feedbacks = result.Errors.ToViewModel()
                };
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Edit", resultData);
            }
            return ModelInvalid("Edit", model);
        }

        [HttpDelete]
        [CustomAuthorization(ActionGroupEnum.MailTemplates, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            LoggingService.LogInfo($"Delete MailTemplates by id {id}");
            var deleteResult = _mailTemplateService.DeleteMailTemplate(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    MailTemplateConstant.MailTemplateDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
                return Ok(deleteResult.Data);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                                    MailTemplateConstant.MailTemplateDeleteErrorMessage,
                                    FeedbackTypeEnum.Error);
                return BadRequest(deleteResult.Data);
            }
        }
        
        private ResultViewModel<MailTemplateListViewModel> DoFilterAndPaging(MailTemplateFilterViewModel filters)
        {
            var data = new ResultViewModel<MailTemplateListViewModel>();
            var filteredData = _mailTemplateService.GetAllFiltered(filters.ToDto()).Data.ToListViewModel();

            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<MailTemplateViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new MailTemplateListViewModel()
                {
                    MailTemplateList = new MultiItemViewModel<MailTemplateViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new MailTemplateListViewModel()
                {
                    MailTemplateList = new MultiItemViewModel<MailTemplateViewModel, int>(filteredData)
                };
            }

            data.Data.Filters = filters;
            return data;
        }
        
        private IActionResult ModelInvalid(string view, MailTemplateViewModel site)
        {
            return View(view, ProcessResult(new MailTemplateViewModel(), new List<FeedbackViewModel>()
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