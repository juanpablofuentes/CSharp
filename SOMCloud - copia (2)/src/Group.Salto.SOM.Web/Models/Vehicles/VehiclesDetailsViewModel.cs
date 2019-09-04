﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Group.Salto.Common.Constants;
using Group.Salto.SOM.Web.Extensions;
using Group.Salto.SOM.Web.Models.Modules;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Group.Salto.SOM.Web.Models.Vehicles
{
    public class VehicleDetailsViewModel : VehicleViewModel

    {
        public IEnumerable<SelectListItem> Drivers { get; set; }
    }
}