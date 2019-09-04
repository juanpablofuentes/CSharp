using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Families;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Families
{
    public interface IFamiliesService
    {
        ResultDto<FamiliesDetailsDto> GetById(int id);
        ResultDto<IList<FamiliesDto>> GetAllFiltered(FamiliesFilterDto filter);
        ResultDto<FamiliesDetailsDto> CreateFamilies(FamiliesDetailsDto source);
        ResultDto<FamiliesDetailsDto> UpdateFamilies(FamiliesDetailsDto source);
        ResultDto<bool> DeleteFamilies(int id);
        IList<BaseNameIdDto<int>> Filter(QueryRequestDto queryRequest);
    }
}