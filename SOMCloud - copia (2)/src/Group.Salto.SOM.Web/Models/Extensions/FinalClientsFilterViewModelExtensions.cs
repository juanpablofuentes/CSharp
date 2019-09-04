using System.Collections.Generic;
using System.Linq;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.FinalClients;
using Group.Salto.SOM.Web.Models.FinalClients;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Group.Salto.SOM.Web.Models.Result;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class FinalClientsFilterViewModelExtensions
    {
        public static FinalClientsFilterViewModel ToViewModel(this FinalClientsFilterDto source)
        {
            FinalClientsFilterViewModel result = null;
            if (source != null)
            {
                result = new FinalClientsFilterViewModel()
                {
                    Name = source.Name,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }
            return result;
        }

        public static FinalClientsFilterDto ToDto(this FinalClientsFilterViewModel source)
        {
            FinalClientsFilterDto result = null;
            if (source != null)
            {
                result = new FinalClientsFilterDto()
                {
                    Name = source.Name,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy
                };
            }
            return result;
        }
    }
}