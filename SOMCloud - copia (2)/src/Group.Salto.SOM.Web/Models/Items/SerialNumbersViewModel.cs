using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Items
{
    public class SerialNumbersViewModel
    {
        public int SerialNumberId { get; set; }
        public string SerialNumberSerialNumber { get; set; }
        public int SerialNumberStatusId { get; set; }
        public IEnumerable<SelectListItem> SerialNumberStatuses { get; set; }
        public string SerialNumberObservations { get; set; }
    }
}