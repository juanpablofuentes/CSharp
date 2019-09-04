using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Group.Salto.Common.Helpers;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class TenantIdsDTOExtensions
    {
        public static TenantIdsDTO ToTenantIdsDto(this Customers source)
        {
            return new TenantIdsDTO() { Id = source.Id, ConnectionString = source.ConnString ?? "" };

        }
        public static IList<TenantIdsDTO> ToTenantIdsDto(this IList<Customers> source)
        {
            List<TenantIdsDTO> res = new List<TenantIdsDTO>();
            source.ToList().ForEach(c => res.Add(c.ToTenantIdsDto()));
            return res;

        }
    }
}
