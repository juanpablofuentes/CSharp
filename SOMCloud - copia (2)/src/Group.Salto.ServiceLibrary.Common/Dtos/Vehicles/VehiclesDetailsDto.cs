using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Vehicles
{
    public class VehiclesDetailsDto:VehiclesDto
    {
        public IEnumerable<SelectListItem> Drivers { get; set; }
    }
}