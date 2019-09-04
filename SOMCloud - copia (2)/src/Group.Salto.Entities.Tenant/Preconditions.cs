using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Preconditions : BaseEntity
    {
        public int? TaskId { get; set; }
        public int? PostconditionCollectionId { get; set; }
        public int? PeopleResponsibleTechniciansCollectionId { get; set; }

        public PeopleCollections PeopleResponsibleTechniciansCollection { get; set; }
        public PostconditionCollections PostconditionCollection { get; set; }
        public Tasks Task { get; set; }
        public ICollection<LiteralsPreconditions> LiteralsPreconditions { get; set; }
    }
}