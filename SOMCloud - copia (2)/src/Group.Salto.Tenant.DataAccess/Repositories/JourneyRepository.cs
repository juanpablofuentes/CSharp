using System;
using System.Linq;
using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class JourneyRepository : BaseRepository<Journeys>, IJourneyRepository
    {
        public JourneyRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public Journeys GetById(int id)
        {
            return Find(x => x.Id == id);
        }

        public Journeys GetByIdIncludeStates(int id)
        {
            return Filter(x => x.Id == id).Include(x => x.JourneysStates).SingleOrDefault();
        }

        public IQueryable<Journeys> GetAll()
        {
            return All();
        }

        public Journeys GetCurrentActiveJourneyByPeopleId(int peopleId)
        {
            return Filter(j => j.PeopleId == peopleId && !j.Finished && j.StartDate.Date == DateTime.UtcNow.Date).OrderByDescending(j => j.StartDate).Include(j => j.JourneysStates).FirstOrDefault();
        }

        public SaveResult<Journeys> CreateJourney(Journeys journey)
        {
            journey.UpdateDate = DateTime.UtcNow;
            Create(journey);
            var result = SaveChange(journey);
            return result;
        }

        public SaveResult<Journeys> UpdateJourney(Journeys journey)
        {
            journey.UpdateDate = DateTime.UtcNow;
            Update(journey);
            var result = SaveChange(journey);
            return result;
        }
    }
}
