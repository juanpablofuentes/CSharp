using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.PurchaseRate;

namespace Group.Salto.ServiceLibrary.Common.Contracts.PurchaseRates
{
    public interface IPurchaseRateService
    {
        ResultDto<PurchaseRateDetailsDto> GetById(int id);
        IList<BaseNameIdDto<int>> GetBasePurchaseRate();
        ResultDto<PurchaseRateDetailsDto> CreatePurchaseRate(PurchaseRateDetailsDto source);
        ResultDto<PurchaseRateDetailsDto> UpdatePurchaseRate(PurchaseRateDetailsDto source);
        ResultDto<bool> DeletePurchaseRate(int id);
        ResultDto<IList<PurchaseRateDto>> GetAllFiltered(PurchaseRateFilterDto filter);
    }
}