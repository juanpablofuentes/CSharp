using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.Log;
using Group.Salto.SOM.Web.Models.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Group.Salto.SOM.Web.Controllers.Contacts
{
    [Authorize]
    public class ContactsController : BaseController
    {
        public ContactsController(
            ILoggingService loggingService,
            IHttpContextAccessor accessor,
            IConfiguration configuration) : base(loggingService, configuration, accessor)
        {

        }

        public IActionResult Contact()
        {
            var model = new ContactsEditViewModel { ContactsId = 1 };

            return PartialView("_ContactsForm", model);
        }

        [HttpPost]
        public IActionResult Contact(ContactsEditViewModel model)
        {
            return PartialView("_ContactsForm", model);
        }
    }
}