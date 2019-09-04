using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common.Constants.PredefinedServices;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.CollectionsExtraField;
using Group.Salto.ServiceLibrary.Common.Contracts.Permisions;
using Group.Salto.ServiceLibrary.Common.Contracts.PredefinedServices;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.Extensions;
using Group.Salto.SOM.Web.Models.MultiSelect;
using Group.Salto.SOM.Web.Models.PredefinedServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Group.Salto.SOM.Web.Controllers.PredefinedServices
{
    [Authorize]
    public class PredefinedServicesController : BaseController
    {
        private readonly ICollectionsExtraFieldService _collectionsExtraFieldService;
        private readonly IPermissionsService _permissionsService;

        public PredefinedServicesController(
            ILoggingService loggingService,
            IHttpContextAccessor accessor,
            IConfiguration configuration,
            ICollectionsExtraFieldService collectionsExtraFieldService,
            IPermissionsService permissionsService) : base(loggingService, configuration, accessor)
        {
            _collectionsExtraFieldService = collectionsExtraFieldService ?? throw new ArgumentNullException($"{nameof(ICollectionsExtraFieldService)} is null ");
            _permissionsService = permissionsService ?? throw new ArgumentNullException($"{nameof(IPermissionsService)} is null ");
        }

        [HttpGet]
        public IActionResult PredefinedServices()
        {
            PredefinedServicesEditViewModel model = new PredefinedServicesEditViewModel { PredefinedServicesId = 1 };
            model.CollectionExtraFieldListItems = _collectionsExtraFieldService.GetAllKeyValues().ToSelectList();

            MultiSelectViewModel permissions = new MultiSelectViewModel(PredefinedServicesConstants.PredefinedServicesPermission);
            permissions.Items = _permissionsService.GetPermissionList().Data.ToViewModel();
            model.Permissions = permissions;

            return PartialView("_PredefinedServicesModal", model);
        }

        [HttpPost]
        public IActionResult PredefinedServices(PredefinedServicesEditViewModel model)
        {
            model.CollectionExtraFieldListItems = _collectionsExtraFieldService.GetAllKeyValues().ToSelectList();
            MultiSelectViewModel permissions = new MultiSelectViewModel(PredefinedServicesConstants.PredefinedServicesPermission);
            permissions.Items = _permissionsService.GetPermissionList().Data.ToViewModel();
            model.Permissions = permissions;
            return PartialView("_PredefinedServicesModal", model);
        }
    }
}