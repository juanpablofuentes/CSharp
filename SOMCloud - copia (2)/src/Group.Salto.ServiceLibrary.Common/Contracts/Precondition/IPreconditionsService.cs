using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Preconditions;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Precondition
{
    public interface IPreconditionsService
    {
        ResultDto<IList<PreconditionsDto>> GetAllByTaskId(int id);
        PreconditionsDto GetById(int id);
        PreconditionsDto CreatePrecondition(int id, int? postconditionCollectionId);
        ResultDto<bool> Delete(int id);
        ResultDto<bool> DeleteAllPreconditionsByTask(int id);
    }
}