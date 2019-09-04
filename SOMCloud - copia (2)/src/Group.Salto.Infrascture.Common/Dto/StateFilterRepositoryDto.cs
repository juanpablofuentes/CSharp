using System.Collections.Generic;

namespace Group.Salto.Infrastructure.Common.Dto
{
    public class StateFilterRepositoryDto : FilterQueryDto
    {
        public IEnumerable<int> Ids { get; set; }
    }
}