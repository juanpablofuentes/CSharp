using Group.Salto.Entities.Tenant;
//using Group.Salto.Infrastructure.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IBillRepository
    {
        List<Bill> GetAllByWorkOrderId(int id);
        IQueryable<Bill> GetAll();
        int CountAll();
        Bill GetById(int Id);
        //IQueryable<Bill> GetAllByFilters(FilterQueryDto filterQuery);
    }
}