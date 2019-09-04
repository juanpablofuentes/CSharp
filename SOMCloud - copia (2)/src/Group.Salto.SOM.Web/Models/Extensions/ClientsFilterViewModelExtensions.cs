using Group.Salto.ServiceLibrary.Common.Dtos.Clients;
using Group.Salto.SOM.Web.Models.Client;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ClientsFilterViewModelExtensions
    {
        public static ClientFilterDto ToDto(this ClientsFilterViewModel source)
        {
            ClientFilterDto result = null;
            if (source != null)
            {
                result = new ClientFilterDto()
                {
                    CorporateName = source.CorporateName,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }

            return result;
        }
    }
}