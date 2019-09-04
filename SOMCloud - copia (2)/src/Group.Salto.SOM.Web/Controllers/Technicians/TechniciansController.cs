using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.SOM.Web.Models.Contracts;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.Technicians;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Group.Salto.SOM.Web.Controllers.Technicians
{
    [Authorize]
    public class TechniciansController : BaseController
    {
        private readonly IPeopleService _peopleService;

        public TechniciansController(
            ILoggingService loggingService,
            IHttpContextAccessor accessor,
            IConfiguration configuration,
            IPeopleService peopleService) : base(loggingService, configuration, accessor)
        {
            _peopleService = peopleService ?? throw new ArgumentNullException($"{nameof(IPeopleService)} is null ");
        }

        [HttpGet]
        public IActionResult Technicians()
        {
            var model = new TechniciansEditViewModel { TechniciansId = 1 };
            model.PeopleListItems = _peopleService.GetPeopleTechniciansKeyValues(new ServiceLibrary.Common.Dtos.People.PeopleFilterDto()).ToSelectList();
            return PartialView("_TechniciansModal", model);
        }

        [HttpPost]
        public IActionResult Technicians(TechniciansEditViewModel model)
        {
            model.PeopleListItems = _peopleService.GetPeopleTechniciansKeyValues(new ServiceLibrary.Common.Dtos.People.PeopleFilterDto()).ToSelectList();
            return PartialView("_TechniciansModal", model);
        }
    }
}