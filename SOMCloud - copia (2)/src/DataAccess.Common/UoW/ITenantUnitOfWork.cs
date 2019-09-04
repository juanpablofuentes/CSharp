using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Common.UoW
{
    public interface ITenantUnitOfWork : IUnitOfWork, IExplicitCreation
    {
    }
}
