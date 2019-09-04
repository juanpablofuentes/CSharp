using System.Collections.Generic;

namespace Group.Salto.Infrastructure.Common.Dto
{
    public class AssetsFilterRepositoryDto
    {
        public string SerialNumber { get; set; }
        public int SitesId { get; set; }
        public IEnumerable<int> StatusesSelected { get; set; }
        public IEnumerable<int> ModelsSelected { get; set; }
        public IEnumerable<int> BrandsSelected { get; set; }
        public IEnumerable<int> FamiliesSelected { get; set; }
        public IEnumerable<int> SubFamiliesSelected { get; set; }
        public IEnumerable<int> SitesSelected { get; set; }
        public IEnumerable<int> FinalClientsSelected { get; set; }
    }
}