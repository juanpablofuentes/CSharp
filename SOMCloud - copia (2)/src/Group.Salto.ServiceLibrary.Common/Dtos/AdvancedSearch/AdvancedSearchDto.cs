using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.AdvancedSearch
{
    public class AdvancedSearchDto
    {
        public AdvancedSearchDto()
        {
            Details = new List<string>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Details { get; set; }
    }
}