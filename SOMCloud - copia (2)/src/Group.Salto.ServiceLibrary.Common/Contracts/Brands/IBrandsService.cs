using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Brand;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Brand
{
    public interface IBrandsService
    {
        ResultDto<BrandsDetailsDto> GetById(int id);
        BrandsDetailsDto GetBrandsWithModels(int id);
        ResultDto<IList<BrandsDto>> GetAllFiltered(BrandsFilterDto filter);
        ResultDto<BrandsDetailsDto> CreateBrands(BrandsDetailsDto source);
        ResultDto<BrandsDetailsDto> UpdateBrands(BrandsDetailsDto source);
        ResultDto<bool> DeleteBrands(int id);
        IList<BaseNameIdDto<int>> Filter(QueryRequestDto queryRequest);
    }
}