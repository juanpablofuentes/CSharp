using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IPaymentMethodRepository : IRepository<PaymentMethods>
    {
        IQueryable<PaymentMethods> GetAll();
        PaymentMethods GetById(int id);
        SaveResult<PaymentMethods> CreatePaymentMethod(PaymentMethods paymentmethod);
        SaveResult<PaymentMethods> UpdatePaymentMethod(PaymentMethods paymentmethod);
        SaveResult<PaymentMethods> DeletePaymentMethod(PaymentMethods entity);
        Dictionary<int, string> GetPaymentMethodKeyValues();
    }
}