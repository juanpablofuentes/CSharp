using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class PaymentMethodRepository : BaseRepository<PaymentMethods>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<PaymentMethods> GetAll()
        {
            return All();
        }

        public PaymentMethods GetById(int id)
        {
            return Filter(x => x.Id == id).Include(x=>x.Expenses).FirstOrDefault();
        }

        public SaveResult<PaymentMethods> CreatePaymentMethod(PaymentMethods paymentmethod)
        {
            paymentmethod.UpdateDate = DateTime.UtcNow;
            Create(paymentmethod);
            var result = SaveChange(paymentmethod);
            return result;
        }

        public SaveResult<PaymentMethods> UpdatePaymentMethod(PaymentMethods paymentmethod)
        {
            paymentmethod.UpdateDate = DateTime.UtcNow;
            Update(paymentmethod);
            var result = SaveChange(paymentmethod);
            return result;
        }

        public SaveResult<PaymentMethods> DeletePaymentMethod(PaymentMethods entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            SaveResult<PaymentMethods> result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public Dictionary<int, string> GetPaymentMethodKeyValues()
        {
            return All()
                .OrderBy(o => o.Mode)
                .ToDictionary(t => t.Id, t => t.Mode);
        }
    }
}
