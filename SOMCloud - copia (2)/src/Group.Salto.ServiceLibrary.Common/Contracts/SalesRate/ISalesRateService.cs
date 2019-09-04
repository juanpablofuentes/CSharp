using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.SalesRate;

namespace Group.Salto.ServiceLibrary.Common.Contracts.SalesRate
{
    public interface ISalesRateService
    {
        ResultDto<SalesRateBaseDto> GetById(int id);
        ResultDto<IList<SalesRateBaseDto>> GetAllFiltered(SalesRateFilterDto filter);
        ResultDto<SalesRateBaseDto> Create(SalesRateBaseDto model);
        ResultDto<SalesRateBaseDto> Update(SalesRateBaseDto model);
        ResultDto<bool> Delete(int id);
    }
}