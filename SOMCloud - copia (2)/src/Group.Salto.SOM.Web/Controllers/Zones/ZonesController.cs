using Group.Salto.Common.Constants;
using Group.Salto.Common.Constants.Zones;
using Group.Salto.Common.Enums;
using Group.Salto.Controls.Table.Filter;
using Group.Salto.Controls.Table.Models;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Project;
using Group.Salto.ServiceLibrary.Common.Contracts.ZoneProject;
using Group.Salto.ServiceLibrary.Common.Contracts.ZoneProjectPostalCode;
using Group.Salto.ServiceLibrary.Common.Contracts.Zones;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.ZoneProject;
using Group.Salto.SOM.Web.Filters;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.ZoneProject;
using Group.Salto.SOM.Web.Models.ZoneProjectPostalCode;
using Group.Salto.SOM.Web.Models.Zones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Controllers.Zones
{
    public class ZonesController : BaseController
    {
        private readonly IZonesService _zonesService;
        private readonly IZoneProjectService _zoneProjectService;
        private readonly IZoneProjectPostalCodeService _zoneProjectPostalCodeService;
        private readonly IProjectsService _projectsService;


        public ZonesController(ILoggingService loggingService,
                                    IConfiguration configuration,
                                    IHttpContextAccessor accessor,
                                    IZonesService zonesService,
                                    IZoneProjectService zoneProjectService,
                                    IZoneProjectPostalCodeService zoneProjectPostalCodeService,
                                    IProjectsService projectsService) : base(loggingService, configuration, accessor)
        {
            _zonesService = zonesService ?? throw new ArgumentNullException($"{nameof(IZonesService)} is null");
            _projectsService = projectsService ?? throw new ArgumentNullException($"{nameof(IProjectsService)} is null");
            _zoneProjectService = zoneProjectService ?? throw new ArgumentNullException($"{nameof(IZoneProjectService)} is null");
            _zoneProjectPostalCodeService = zoneProjectPostalCodeService ?? throw new ArgumentNullException($"{nameof(IZoneProjectPostalCodeService)} is null");
        }

        [HttpGet]
       // [CustomAuthorization(ActionGroupEnum.Zones, ActionEnum.GetAll)]
        public IActionResult Index()
        {
            LoggingService.LogInfo($"Actions Get get all Zones");
            var result = DoFilterAndPaging(new ZonesFilterViewModel());
            var feedback = GetFeedbackTempData();
            if (feedback != null)
            {
                result.Feedbacks = result.Feedbacks ?? new FeedbacksViewModel();
                result.Feedbacks.AddFeedback(feedback);
            }
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoggingService.LogInfo("Return Zones Create");
            var response = new ResultViewModel<ZoneViewModel>()
            {
                Data = new ZoneViewModel()
            };
            response.Data.ZonesProjects = new List<ZoneProjectViewModel>();
            var defaultModel = new ZoneProjectViewModel()
            {
                ProjectName = "Default",
                ProjectId = 0,
                ZoneId = 0,
                ZoneProjectId = 0,
                State = "C"
            };
            response.Data.ZonesProjects.Add(defaultModel);
            return View(response);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            LoggingService.LogInfo("Return Zones Create");
            var data = _zonesService.GetByIdWithZoneProjects(id);
            var response = data.ToViewModel();
            LogFeedbacks(response.Feedbacks?.Feedbacks);
            if (data.Errors?.Errors == null || !data.Errors.Errors.Any())
            {
                return View("Edit", response);
            }
            SetFeedbackTempData(LocalizationsConstants.Error,
                LocalizationsConstants.ErrorLoadingDataMessage,
                FeedbackTypeEnum.Error);
            return Redirect(nameof(Index));
            
        }

        [HttpGet]
        public IActionResult PostalsCode()
        {          
            
                var model = new ZoneProjectPostalCodeViewModel { PostalCodeId = 1 };
                
                return PartialView("_PostalCodeModal", model);
                              
        }

        [HttpPost]
        public IActionResult PostalsCode(ZoneProjectPostalCodeViewModel model)
        {
            return PartialView("_PostalCodeModal", model);
        }

        [HttpGet]
        public IActionResult Project(int id)
        {
            if (id != 0 )
            {
                var res = _zoneProjectService.GetAllById(id);
                if (res == null)
                {
                    return NotFound("Not data found");
                }

                var modelProject = res.Data.ToViewModel();
                FillData(modelProject.FirstOrDefault(x => x.ZoneId == id));

                return PartialView("_ProjectsModal", modelProject);
            }
            else
            {
                var model = new ZoneProjectViewModel { ZoneProjectId = 1 };
                FillData(model);
                return PartialView("_ProjectsModal", model);
            }
        }

        [HttpPost]
        public IActionResult Project(ZoneProjectViewModel model)
        {
            LoggingService.LogInfo($"Zones --> create a new Model");
            FillData(model);
            return PartialView("_ProjectsModal", model);
        }

        [HttpPost]
        public IActionResult Filter(ZonesFilterViewModel filter)
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

        [HttpPost]
        public IActionResult Create(ZoneViewModel zonesData)
        {
            LoggingService.LogInfo($"Actions Post create Zones");
            if (ModelState.IsValid)
            {
                var result = _zonesService.CreateZones(zonesData.ToDto(),zonesData.SelectedPostalCodes.ToListDto());

                if (result.Errors?.Errors == null || !result.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        ZonesConstants.ZonesCreateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = result.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Create", resultData);
            }
            var results = ProcessResult(zonesData,
                         LocalizationsConstants.Error,
                         LocalizationsConstants.FormErrorsMessage,
                         FeedbackTypeEnum.Error);
            return View("Create", results);
        }

        [HttpPost]       
        public IActionResult Edit(ZoneViewModel zoneData)
        {
            LoggingService.LogInfo($"Actions Post Update zones by id {zoneData.Id}");

            if (ModelState.IsValid)
            {
                var resultUpdate = _zonesService.UpdateZones(zoneData.ToDto(),zoneData.SelectedPostalCodes.ToListDto());
                if (resultUpdate.Errors?.Errors == null || !resultUpdate.Errors.Errors.Any())
                {
                    SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                                        ZonesConstants.ZonesUpdateSuccessMessage,
                                        FeedbackTypeEnum.Success);
                    return RedirectToAction("Index");
                }
                var resultData = resultUpdate.ToViewModel();
                LogFeedbacks(resultData.Feedbacks?.Feedbacks);
                return View("Edit", resultData);
            }
            var result = ProcessResult(zoneData, LocalizationsConstants.Error, LocalizationsConstants.FormErrorsMessage, FeedbackTypeEnum.Error);
            return View("Edit", result);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            LoggingService.LogInfo($"Actions Post Delete Zones by id {id}");
            var deleteResult = _zonesService.DeleteZone(id);
            if (deleteResult.Errors?.Errors == null || !deleteResult.Errors.Errors.Any())
            {
                SetFeedbackTempData(LocalizationsConstants.SuccessTitle,
                    ZonesConstants.ZonesDeleteSuccessMessage,
                    FeedbackTypeEnum.Success);
                return Ok(deleteResult.Data);
            }
            else
            {
                SetFeedbackTempData(LocalizationsConstants.Error,
                                    ZonesConstants.ZonesDeleteErrorMessage,
                                    FeedbackTypeEnum.Error);
                return BadRequest(deleteResult.Data);
            }
        }

        private ZoneProjectViewModel FillData(ZoneProjectViewModel source)
        {
            if (source != null) {
                
                source.ProjectsList = _projectsService.GetAllKeyValues().ToSelectList().ToList();
                source.ProjectsList.Insert(0,new SelectListItem("Default", "0", true));
            }
            return source;
        }
 
        private ResultViewModel<ZonesViewModel> DoFilterAndPaging(ZonesFilterViewModel filters)
        {
            var data = new ResultViewModel<ZonesViewModel>();
            var filteredData = _zonesService.GetAllFiltered(filters.ToDto()).Data.ToViewModel();

            if (filters.Page != 0)
            {
                filters.Size = GetNumberEntriesPerPage();
                var pager = new PagedSelector<ZoneViewModel>()
                {
                    Page = filters.Page,
                    PageSize = filters.Size
                };
                data.Data = new ZonesViewModel()
                {
                    Zones = new MultiItemViewModel<ZoneViewModel, int>(filteredData)
                };
                filters.PagesCount = pager.PagesCount;
                filters.Page = pager.Page;
                filters.TotalValues = filteredData.Count();
            }
            else
            {
                data.Data = new ZonesViewModel()
                {
                    Zones = new MultiItemViewModel<ZoneViewModel, int>(filteredData)
                };
            }

            data.Data.ZonesFilters = filters;
            return data;
        }
    }
}