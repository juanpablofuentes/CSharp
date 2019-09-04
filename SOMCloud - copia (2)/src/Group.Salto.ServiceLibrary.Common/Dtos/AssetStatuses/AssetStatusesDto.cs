using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.AssetStatuses
{
    public class AssetStatusesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public bool IsRetiredState { get; set; }
        public bool IsDefault { get; set; }
    }
}