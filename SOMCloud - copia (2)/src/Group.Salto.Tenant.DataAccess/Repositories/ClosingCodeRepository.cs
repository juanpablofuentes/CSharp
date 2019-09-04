using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class ClosingCodeRepository : BaseRepository<ClosingCodes>, IClosingCodeRepository
    {
        public ClosingCodeRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public ClosingCodes GetById(int id)
        {
            return Find(x => x.Id == id, GetIncludeAll());
        }

        private List<Expression<Func<ClosingCodes, object>>> GetIncludeAll()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(DerivedServices), typeof(Services), typeof(ClosingCodes) });
        }

        private List<Expression<Func<ClosingCodes, object>>> GetIncludesPredicate(IList<Type> includeCollection)
        {
            var includesPredicate = new List<Expression<Func<ClosingCodes, object>>>();
            foreach (var element in includeCollection)
            {
                if (element == typeof(DerivedServices))
                {
                    includesPredicate.Add(p => p.DerivedServicesClosingCodesIdN1Navigation);
                    includesPredicate.Add(p => p.DerivedServicesClosingCodesIdN2Navigation);
                    includesPredicate.Add(p => p.DerivedServicesClosingCodesIdN3Navigation);
                }

                if (element == typeof(Services))
                {
                    includesPredicate.Add(p => p.ServicesClosingCode);
                    includesPredicate.Add(p => p.ServicesClosingCodeFirst);
                    includesPredicate.Add(p => p.ServicesClosingCodeSecond);
                    includesPredicate.Add(p => p.ServicesClosingCodeThird);
                }

                if (element == typeof(ClosingCodes))
                {
                    includesPredicate.Add(p => p.InverseClosingCodesFather);
                }
            }
            return includesPredicate;
        }

        public ClosingCodes GetByIdIncludeFathers(int currentServiceClosingCodeId)
        {
            var currentClosingCode = Find(cc => cc.Id == currentServiceClosingCodeId);
            if (currentClosingCode?.ClosingCodesFatherId != null)
            {
                currentClosingCode.ClosingCodesFather = GetByIdIncludeFathers(currentClosingCode.ClosingCodesFatherId.Value);
            }

            return currentClosingCode;
        }
    }
}