using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Knowledge;
using Group.Salto.Common.Constants.Vehicles;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.ServiceLibrary.Common.Contracts.Vehicles;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Vehicles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.Vehicles
{
    public class VehiclesController : BaseController
    {
        private readonly IVehiclesService _vehiclesService;
        private readonly IPeopleService _peopleService;

        public VehiclesController(ILoggingService loggingService,
                                    IConfiguration configuration,
                                    IHttpContextAccessor accessor,
                                    IVehiclesService vehiclesService,
                                    IPeopleService peopleService) : base(loggingService, configuration, accessor)
        {
            _vehiclesService = vehiclesService ?? throw new ArgumentNullException($"{nameof(vehiclesService)} is null");
            _peopleService = peopleService ?? throw new ArgumentNullException($"{nameof(peopleService)} is null");
        }

        [CustomAuthorization(ActionGroupEnum.Vehicles, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Actions Get get all Vehicles");
            var result = DoFilterAndPaging(new VehiclesFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [CustomAuthorization(ActionGroupEnum.Vehicles, ActionEnum.GetById)]
        public IActionResult Details(int id)
        {
            LoggingService.LogInfo($"Actions Get get by Vehicles id: {id}");
            var result = _vehiclesService.GetById(id);

            var resultData = result.ToViewModel();
            FillData(resultData.Data);

            if (result != null && (result.Errors?.Errors == null || !result.Errors.Errors.Any()))
            {
                return View("Details", resultData);
            }

            return View();
        }
        
        [HttpGet]
        [CustomAuthorization(ActionGroupEnum.Vehicles, ActionEnum.Create)]
        public IActionResult Create()
        {

            LoggingService.LogInfo("Return Vehicles Create");
            var response = new ResultViewModel<VehicleDetailsViewModel>()
            {
                Data = FillData(new VehicleDetailsViewModel())
            };
            return View(response);
        }

        [HttpGet]        
        public IActionResult Update(int id)
        {


            LoggingService.LogInfo($"Vehicle .get for id:{id}");
            var result = _vehiclesService.GetById(id);
            var response = result.ToViewModel();
            FillData(response.Data);
            LogFeedbacks(response.Feedbacks?.Feedbacks);
            if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
            {
                return View("Details", response);
            }
            SetFeedbackTempData(LocalizationsConstants.Error,
                LocalizationsConstants.ErrorLoadingDataMessage,
                FeedbackTypeEnum.Error);
            return Redirect(nameof(Index));
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Vehicles, ActionEnum.Create)]
        public IActionResult Create(VehicleDetailsViewModel vehicleData)
        {
            LoggingService.LogInfo($"Actions Post create Vehicles");
            if (ModelState.IsValid)
            {
                var result = _vehiclesService.CreateVehicle(vehicleData.ToDto());

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        VehiclesConstants.VehiclesCreateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            var results = ProcessResult(vehicleData,
                         LocalizationsConstants.Error,
                         LocalizationsConstants.FormErrorsMessage,
                         FeedbackTypeEnum.Error);
            FillData(vehicleData);
            return View("Create", results);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Vehicles, ActionEnum.Update)]
        public IActionResult Update(VehicleDetailsViewModel vehicleData)
        {
            LoggingService.LogInfo($"Actions Post Update Vehicle by id {vehicleData.Id}");

            if (ModelState.IsValid)
            {
                var resultUpdate = _vehiclesService.UpdateVehicle(vehicleData.ToDto());
                if (resultUpdate.Errors?.Errors == null || !resultUpdate.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        VehiclesConstants.VehiclesUpdateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");

                }
                var resultData = resultUpdate.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);

                return View("Details", resultData);
            }
            var result = ProcessResult(vehicleData, LocalizationsConstants.Error, LocalizationsConstants.FormErrorsMessage, FeedbackTypeEnum.Error);
            FillData(vehicleData);
            return View("Details", result);
        }

        [HttpPost]
        [CustomAuthorization(ActionGroupEnum.Vehicles, ActionEnum.Delete)]
        public IActionResult Delete(int id)
        {
            LoggingService.LogInfo($"Actions Post Delete Vehicle by id {id}");
            var deleteResult = _vehiclesService.DeleteVehicle(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    VehiclesConstants.VehiclesDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
                return Ok(deleteResult.Data);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                                    VehiclesConstants.VehiclesDeleteErrorMessage,
                                    FeedbackTypeEnum.Error);
                return BadRequest(deleteResult.Data);
            }
        }

        [HttpPost]
        public IActionResult Filter(VehiclesFilterViewModel filter)
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
        public IActionResult GetVehicles()
        {
            var vehicles = _vehiclesService.GetAllKeyValues().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            });
            return Ok(vehicles);
        }

        private VehicleDetailsViewModel FillData(VehicleDetailsViewModel source)
        {
            source.Drivers = _peopleService.GetAllDriversKeyValue()?.ToSelectList();

            return source;
        }

        private ResultViewModel<VehiclesViewModel> DoFilterAndPaging(VehiclesFilterViewModel filters)
        {
            var data = new ResultViewModel<VehiclesViewModel>();
            var filteredData = _vehiclesService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();

            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<VehicleViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new VehiclesViewModel()
                {
                    Vehicle = new MultiItemViewModel<VehicleViewModel, int>(pager.GetPageIEnumerable(filteredData))
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new VehiclesViewModel()
                {
                    Vehicle = new MultiItemViewModel<VehicleViewModel, int>(filteredData)
                };
            }

            data.Data.VehicleFilters = filters;
            return data;
        }

        
    }
}