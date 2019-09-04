using System.Collections.Generic;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Expenses;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PaymentMethodDtoExtensions
    {
        public static PaymentMethodDto ToDto(this PaymentMethods dbModel)
        {
            var dto = new PaymentMethodDto
            {
                Id = dbModel.Id,
                Model = dbModel.Mode
            };

            return dto;
        }

        public static IEnumerable<PaymentMethodDto> ToDto(this IEnumerable<PaymentMethods> dbModelList)
        {
            var dtoList = new List<PaymentMethodDto>();
            foreach (var dbModel in dbModelList)
            {
                dtoList.Add(dbModel.ToDto());
            }

            return dtoList;
        }

        public static PaymentMethods ToEntity(this PaymentMethodDto source)
        {
            PaymentMethods result = null;
            if (source != null)
            {
                result = new PaymentMethods()
                {
                    Mode = source.Model
                };
            }
            return result;
        }

        public static PaymentMethods Update(this PaymentMethods target, PaymentMethodDto source)
        {
            if (target != null && source != null)
            {
                target.Id = source.Id;
                target.Mode = source.Model;
            }
            return target;
        }
    }
}