using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.PointRate;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.PointRate
{
    public interface IPointRateService
    {
        ResultDto<IList<PointRateDto>> GetAll();
        ResultDto<PointRateDto> GetById(int id);
        ResultDto<PointRateDto> UpdatePointRate(PointRateDto source);
        ResultDto<IList<PointRateDto>> GetAllFiltered(PointRateFilterDto filter);
        ResultDto<PointRateDto> CreatePointRate(PointRateDto source);
        ResultDto<bool> DeletePointRate(int id);
        IList<BaseNameIdDto<int>> GetAllKeyValues();
        ResultDto<ErrorDto> CanDelete(int id);
    }
}