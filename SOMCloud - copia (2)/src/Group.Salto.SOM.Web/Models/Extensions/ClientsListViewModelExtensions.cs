using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Clients;
using Group.Salto.SOM.Web.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ClientsListViewModelExtensions
    {
        public static ClientViewModel ToViewModel(this ClientListDto source)
        {
            ClientViewModel result = null;
            if (source != null)
            {
                result = new ClientViewModel()
                {
                    Id = source.Id,
                    CorporateName = source.CorporateName,
                    Observations = source.Observations,
                    Phone = source.Phone,
                    UnListed = source.UnListed
                };
            }
            return result;
        }

        public static IList<ClientViewModel> ToViewModel(this IList<ClientListDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
    }
}