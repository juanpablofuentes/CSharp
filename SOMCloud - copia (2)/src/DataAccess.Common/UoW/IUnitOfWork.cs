using DataAccess.Common.Context;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Common
{
    public interface IUnitOfWork : IOwnerIdentifier
    {
        DbContext Context { get; }
    }
}
