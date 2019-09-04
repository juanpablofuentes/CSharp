using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.AssetsAudit
{
    public class AssetsAuditDto
    {
        public DateTime Registry { get; set; }        
        public string  UserName { get; set; }
        public IList<AssetsChangesDto> Changes { get; set; }
    }
}