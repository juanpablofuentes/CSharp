using Group.Salto.ServiceLibrary.Common.Dtos.AssetStatuses;
using Group.Salto.ServiceLibrary.Common.Dtos.Brand;
using Group.Salto.ServiceLibrary.Common.Dtos.Families;
using Group.Salto.ServiceLibrary.Common.Dtos.FinalClients;
using Group.Salto.ServiceLibrary.Common.Dtos.Models;
using Group.Salto.ServiceLibrary.Common.Dtos.Sites;
using Group.Salto.ServiceLibrary.Common.Dtos.SubFamilies;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Assets
{
    public class AssetsFilterDto : BaseFilterDto
    {
        public string SerialNumber { get; set; }
        public int SitesId { get; set; }
        public IList<AssetStatusesDto> StatusesSelected { get; set; }
        public IList<ModelsDto> ModelsSelected { get; set; }
        public IList<BrandsDto> BrandsSelected { get; set; }
        public IList<FamiliesDto> FamiliesSelected { get; set; }
        public IList<SubFamiliesDto> SubFamiliesSelected { get; set; }
        public IList<SitesDto> SitesSelected { get; set; }
        public IList<FinalClientsDto> FinalClientsSelected { get; set; }
    }
}