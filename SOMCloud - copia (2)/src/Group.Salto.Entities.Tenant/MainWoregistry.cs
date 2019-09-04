using Group.Salto.Common;
using System;

namespace Group.Salto.Entities.Tenant
{
    public class MainWoregistry : BaseEntity
    {
        public DateTime? ArrivalTime { get; set; }
        public int? Duration { get; set; }
        public int? PersonId { get; set; }
        public int? VisibleWo { get; set; }
        public int? FilteredWo { get; set; }
        public int? ArchivedWo { get; set; }
        public bool? ExportWo { get; set; }
        public bool? ExportServices { get; set; }
        public bool? OnlyServices { get; set; }
    }
}
