using Group.Salto.ServiceLibrary.Common.Dtos.Expenses;
using Group.Salto.SOM.Web.Models.PaymentMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class PaymentMethodFilterViewModelExtensions
    {
        public static PaymentMethodFilterDto ToDto(this PaymentMethodFilterViewModel source)
        {
            PaymentMethodFilterDto result = null;
            if (source != null)
            {
                result = new PaymentMethodFilterDto()
                {
                    Name = source.Name,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }
            return result;
        }
    }
}