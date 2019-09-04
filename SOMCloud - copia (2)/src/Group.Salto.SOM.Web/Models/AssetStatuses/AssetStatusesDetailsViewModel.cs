using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.AssetStatuses
{
    public class AssetStatusesDetailsViewModel: AssetStatusesViewModel
    {
        public bool IsDefault { get; set; }
        public bool IsRetiredState { get; set; }
    }
}