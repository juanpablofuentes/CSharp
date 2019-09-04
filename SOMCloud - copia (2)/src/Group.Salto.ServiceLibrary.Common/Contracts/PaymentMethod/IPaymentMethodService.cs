using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Expenses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Contracts.PaymentMethod
{
    public interface IPaymentMethodService
    {
        ResultDto<PaymentMethodDto> GetById(int id);
        ResultDto<IList<PaymentMethodDto>> GetAllFiltered(PaymentMethodFilterDto filter);
        ResultDto<PaymentMethodDto> CreatePaymentMethod(PaymentMethodDto source);
        ResultDto<PaymentMethodDto> UpdatePaymentMethod(PaymentMethodDto source);
        ResultDto<bool> DeletePaymentMethod(int id);
    }
}