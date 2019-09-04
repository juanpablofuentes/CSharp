using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IFamiliesRepository
    {
        IQueryable<Families> GetAll();
        Families GetFamilieWithSubFamilies(int id);
        SaveResult<Families> CreateFamilies(Families families);
        SaveResult<Families> UpdateFamilies(Families families);
        SaveResult<Families> DeleteFamilies(Families entity);
        IQueryable<Families> GetAllByFilters(FilterQueryDto filter);
    }
}