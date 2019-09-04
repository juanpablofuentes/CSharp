using Group.Salto.Common.Constants;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.RepetitionParameters;
using Group.Salto.ServiceLibrary.Common.Dtos.RepetitionParameter;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.RepetitionParameter;
using Group.Salto.SOM.Web.Models.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.SOM.Web.Controllers;
using Group.Salto.Common.Constants.RepetitionParameter;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.ServiceLibrary.Common.Contracts.CalculationType;
using Group.Salto.ServiceLibrary.Common.Contracts.DamagedEquipment;
using Group.Salto.ServiceLibrary.Common.Contracts.DaysType;
using Group.Salto.SOM.Web.Models.MultiSelect;

namespace Group.Salto.SOM.Web.Controllers.RepetitionParameter
{
    [Authorize]
    public class RepetitionParameterController : BaseController
    {
        private readonly IRepetitionParameterService _repetitionParameterService;
        private readonly ICalculationTypeService _calculationTypeService;
        private readonly IDamagedEquipmentService _damagedEquipmentService;
        private readonly IDaysTypeService _daysTypeService;

        public RepetitionParameterController(
           ILoggingService loggingService,
           IHttpContextAccessor accessor,
           IConfiguration configuration,
           IRepetitionParameterService RepetitionParameterService,
           ICalculationTypeService calculationTypeService,
           IDamagedEquipmentService damagedEquipmentService,
           IDaysTypeService daysTypeService) : base(loggingService, configuration, accessor)
        {
            _repetitionParameterService = RepetitionParameterService ?? throw new ArgumentNullException($"{nameof(IRepetitionParameterService)} is null");
            _calculationTypeService = calculationTypeService ?? throw new ArgumentNullException($"{nameof(calculationTypeService)} is null ");
            _damagedEquipmentService = damagedEquipmentService ?? throw new ArgumentNullException($"{nameof(damagedEquipmentService)} is null");
            _daysTypeService = daysTypeService ?? throw new ArgumentNullException($"{nameof(daysTypeService)} is null");
        }

        [HttpGet]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"RepetitionParameter.Get getRepetitionParameter ");
            var result = _repetitionParameterService.GetFirst();
            var response = result.ToViewModel();
            FillData(response.Data);
            if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
            {
                return View("Index", response);
            }
            SetFeedbackTempData(LocalizationsConstants.Error, LocalizationsConstants.ErrorLoadingDataMessage, FeedbackTypeEnum.Error);
            return Redirect(nameof(Index));
        }

        [HttpPost]
        public IActionResult Index(RepetitionParameterDetailsViewModel repetitonParameter)
        {
            LoggingService.LogInfo($"RepetitionParameters update for id = {repetitonParameter.Id}");
            if (ModelState.IsValid)
            {
                var result = _repetitionParameterService.Update(repetitonParameter.ToDto());
                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                        RepetitionParameterConstants.RepetitionParameterEditSuccessMessage,
                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                FillData(resultData.Data);
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Index", resultData);
            }
            return ModelInvalid("Index", repetitonParameter);
        }

        private RepetitionParameterDetailsViewModel FillData(RepetitionParameterDetailsViewModel source)
        {
            source.CalculationTypeItems = _calculationTypeService.GetAllKeyValues().ToSelectList();
            source.DamagedEquipmentItems = _damagedEquipmentService.GetAllKeyValues().ToSelectList();
            source.DaystypeItemsItems = _daysTypeService.GetAllKeyValues().ToSelectList();
            return source;
        }

        private IActionResult ModelInvalid(string view, RepetitionParameterDetailsViewModel RepetitionParameterEdit, string KeyMessage = null)
        {
            FillData(RepetitionParameterEdit);
            return View(view, ProcessResult(RepetitionParameterEdit, new List<FeedbackViewModel>()
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