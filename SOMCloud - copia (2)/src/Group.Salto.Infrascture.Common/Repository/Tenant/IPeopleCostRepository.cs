using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Collections.Generic;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IPeopleCostRepository : IRepository<PeopleCost>
    {
        PeopleCost GetByIdNotDeleted(int Id);
        IList<PeopleCost> GetByPeopleIdNotDeleted(int peopleId);
        SaveResult<PeopleCost> CreatePeopleCost(PeopleCost peopleCost);
        SaveResult<PeopleCost> UpdatePeopleCost(PeopleCost peopleCost);
        bool DeletePeopleCost(PeopleCost peopleCost);
    }
}