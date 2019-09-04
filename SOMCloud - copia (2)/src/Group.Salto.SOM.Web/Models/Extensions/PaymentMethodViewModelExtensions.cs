using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Expenses;
using Group.Salto.SOM.Web.Models.PaymentMethod;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class PaymentMethodViewModelExtensions
    {
        public static ResultViewModel<PaymentMethodViewModel> ToViewModel(this ResultDto<PaymentMethodDto> source)
        {
            var response = source != null ? new ResultViewModel<PaymentMethodViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static PaymentMethodViewModel ToViewModel(this PaymentMethodDto source)
        {
            PaymentMethodViewModel result = null;
            if (source != null)
            {
                result = new PaymentMethodViewModel()
                {
                    Id = source.Id,
                    Name = source.Model,
                };
            }
            return result;
        }

        public static IList<PaymentMethodViewModel> ToViewModel(this IList<PaymentMethodDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static PaymentMethodDto ToDto(this PaymentMethodViewModel source)
        {
            PaymentMethodDto result = null;
            if (source != null)
            {
                result = new PaymentMethodDto()
                {
                    Id = source.Id,
                    Model = source.Name,
                };
            }
            return result;
        }
    }
}