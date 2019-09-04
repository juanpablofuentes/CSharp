using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class PeopleCostRepository : BaseRepository<PeopleCost>, IPeopleCostRepository
    {
        public PeopleCostRepository(ITenantUnitOfWork uow) : base(uow)
        {

        }

        public PeopleCost GetByIdNotDeleted(int Id)
        {
            return Filter(x => x.Id == Id && !x.IsDeleted).FirstOrDefault();
        }

        public IList<PeopleCost> GetByPeopleIdNotDeleted(int peopleId)
        {
            return Filter(x => x.PeopleId == peopleId && !x.IsDeleted).OrderBy(o => o.StartDate).ToList();
        }

        public SaveResult<PeopleCost> CreatePeopleCost(PeopleCost peopleCost)
        {
            peopleCost.UpdateDate = DateTime.UtcNow;
            Create(peopleCost);
            var result = SaveChange(peopleCost);
            return result;
        }

        public SaveResult<PeopleCost> UpdatePeopleCost(PeopleCost peopleCost)
        {
            peopleCost.UpdateDate = DateTime.UtcNow;
            Update(peopleCost);
            var result = SaveChange(peopleCost);
            return result;
        }

        public bool DeletePeopleCost(PeopleCost peopleCost)
        {
            Delete(peopleCost);
            SaveResult<PeopleCost> result = SaveChange(peopleCost);
            result.Entity = peopleCost;

            return result.IsOk;
        }
    }
}