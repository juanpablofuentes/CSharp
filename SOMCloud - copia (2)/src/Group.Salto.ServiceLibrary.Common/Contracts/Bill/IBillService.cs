using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Bill;
using Group.Salto.ServiceLibrary.Common.Dtos.Billing;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Billing
{
    public interface IBillService
    {
        ResultDto<IList<BillDto>> GetAllById(int id);
        ResultDto<IList<BillInfoDto>> GetAllFiltered(BillFilterDto filter);
        int CountId(BillFilterDto filter);
        ResultDto<IList<BillDetailDto>> GetDetailById(int id);
        ResultDto<BillInfoDto> GetBillById(int id);
    }
}