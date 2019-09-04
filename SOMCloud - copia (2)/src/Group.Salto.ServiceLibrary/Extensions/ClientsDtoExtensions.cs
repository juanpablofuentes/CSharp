using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ClientsDtoExtensions
    {
        public static ClientListDto ToDto(this Entities.Tenant.Clients source)
        {
            ClientListDto result = null;
            if (source != null)
            {
                result = new ClientListDto();
                source.ToDto(result);
            }

            return result;
        }

        public static void ToDto(this Entities.Tenant.Clients source, ClientListDto target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.CorporateName = source.CorporateName;
                target.Phone = source.Phone;
                target.UnListed = source.UnListed;
            }
        }

        public static IList<ClientListDto> ToDto(this IList<Entities.Tenant.Clients> source)
        {
            return source?.MapList(c => c.ToDto());
        }
    }
}
