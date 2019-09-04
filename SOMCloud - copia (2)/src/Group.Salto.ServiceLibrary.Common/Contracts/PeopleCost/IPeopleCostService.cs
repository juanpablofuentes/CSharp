using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.PeopleCost;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.PeopleCost
{
    public interface IPeopleCostService
    {
        ResultDto<IList<PeopleCostDetailDto>> GetByPeopleId(int peopleId);
        ResultDto<PeopleCostDetailDto> Create(PeopleCostDetailDto peoplecost);
        ResultDto<PeopleCostDetailDto> Update(PeopleCostDetailDto peoplecost);
        ResultDto<bool> Delete(int Id);
    }
}