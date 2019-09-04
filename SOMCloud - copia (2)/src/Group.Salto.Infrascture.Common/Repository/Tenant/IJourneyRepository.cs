using System.Linq;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IJourneyRepository : IRepository<Journeys>
    {
        Journeys GetById(int id);
        Journeys GetByIdIncludeStates(int id);
        IQueryable<Journeys> GetAll();
        Journeys GetCurrentActiveJourneyByPeopleId(int peopleId);
        SaveResult<Journeys> CreateJourney(Journeys journey);
        SaveResult<Journeys> UpdateJourney(Journeys journey);
    }
}
