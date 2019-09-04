using Group.Salto.Log;
using Group.Salto.SOM.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Group.Salto.ServiceLibrary.Common.Contracts.RolesActionGroupsActionsTenant;
using System;

namespace Group.Salto.SOM.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly IRolesActionGroupsActionsTenantAdapter _rolesActionGroupsActionsTenantAdapter;
        public HomeController(ILoggingService loggingService,
                                IConfiguration configuration,
                                IHttpContextAccessor accessor,
                                IRolesActionGroupsActionsTenantAdapter rolesActionGroupsActionsTenantAdapter) : base(loggingService, configuration, accessor)
        {
            _rolesActionGroupsActionsTenantAdapter = rolesActionGroupsActionsTenantAdapter ?? throw new ArgumentNullException($"{nameof(IRolesActionGroupsActionsTenantAdapter)} is null");
        }

        public IActionResult Index()
        {
            var userConfigurationId = GetConfigurationUserId();
            var customerId = GetTenantId();
            var existActions = _rolesActionGroupsActionsTenantAdapter.SetCacheRolesActionGroupsActionsByUserId(userConfigurationId, customerId);
            //TODO: Carmen. ¿Qué hacer en el caso de que no existan acciones para ese usuario?
            SetLanguage(CultureInfo.CurrentCulture.TwoLetterISOLanguageName.ToUpper());
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
