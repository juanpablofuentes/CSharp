using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IBrandsRepository
    {
        Brands GetById(int id);
        IQueryable<Brands> GetAll();
        Brands GetBrandsWithModels(int id);
        SaveResult<Brands> CreateBrands(Brands brands);
        SaveResult<Brands> UpdateBrands(Brands brands);
        SaveResult<Brands> DeleteBrands(Brands entity);
        IQueryable<Brands> GetAllByFilters(FilterQueryDto filterQuery);
    }
}