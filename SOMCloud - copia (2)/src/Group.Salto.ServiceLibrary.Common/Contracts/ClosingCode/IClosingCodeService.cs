using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.ClosingCode;

namespace Group.Salto.ServiceLibrary.Common.Contracts.ClosingCode
{
    public interface IClosingCodeService
    {
        ResultDto<bool> CanDelete(int id);
        ClosingCodesAnalysisDto GetAnalyzeClosingCodesById(int id);
    }
}