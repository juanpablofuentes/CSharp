using Group.Salto.ServiceLibrary.Common.Dtos.Preconditions;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Postconditions
{
    public class PostconditionCollectionDto
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public IList<PostconditionsDto> PostconditionsList { get; set; }
        public IList<PreconditionsDto> PreconditionsList { get; set; }
    }
}