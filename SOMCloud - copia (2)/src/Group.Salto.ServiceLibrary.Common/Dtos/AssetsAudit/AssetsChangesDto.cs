using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.AssetsAudit
{
    public class AssetsChangesDto
    {
        public string Property { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}