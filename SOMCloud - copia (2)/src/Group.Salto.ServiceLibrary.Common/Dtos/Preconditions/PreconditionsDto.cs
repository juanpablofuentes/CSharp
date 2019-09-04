using Group.Salto.ServiceLibrary.Common.Dtos.LiteralPreconditions;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Preconditions
{
    public class PreconditionsDto
    {
        public int Id { get; set; }
        public int? TaskId { get; set; }
        public int? PostconditionCollectionId { get; set; }
        public int? PeopleResponsibleTechniciansCollectionId { get; set; }
        public IList<LiteralPreconditionsDto> LiteralsPreconditions { get; set; }
    }
}