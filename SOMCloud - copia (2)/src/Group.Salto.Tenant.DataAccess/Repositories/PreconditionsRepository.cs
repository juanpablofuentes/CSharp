using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Common;
using System;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class PreconditionsRepository : BaseRepository<Preconditions>, IPreconditionsRepository
    {
        public PreconditionsRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public Preconditions CreatePrecondition(Preconditions entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result.Entity;
        }

        public SaveResult<Preconditions> DeletePreconditions(Preconditions entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public Preconditions DeleteOnContextPreconditions(Preconditions entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            return entity;
        }

        public IQueryable<Preconditions> GetAllByTaskId(int id)
        {
            return Filter(t => t.TaskId == id)
                    .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.FinalClient)
                   .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.ExternalWorOrderStatus)
                   .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.WorkOrderStatus)
                   .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.Location)
                   .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.Queue)
                   .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.Asset)
                   .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.Zone)
                   .Include(lp => lp.LiteralsPreconditions)
                        .ThenInclude(x => x.PreconditionsLiteralValues)
                                .ThenInclude(x => x.PeopleTechnician)
                   .Include(lp => lp.LiteralsPreconditions)
                        .ThenInclude(x => x.PreconditionsLiteralValues)
                                .ThenInclude(x => x.Project)
                   .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.WorkOrderTypesN1)
                   .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.WorkOrderTypesN2)
                   .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.WorkOrderTypesN3)
                    .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.WorkOrderTypesN4)
                    .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.WorkOrderTypesN5)
                    .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.PeopleManipulator)
                   .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.WorkOrderCategory)
                   .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.PeopleResponsibleTechniciansCollection)
                   .Where(x => x.PostconditionCollectionId == null);
        }

        public Preconditions GetById(int id)
        {
            return Filter(t => t.Id == id)
                    .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.FinalClient)
                   .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.ExternalWorOrderStatus)
                   .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.WorkOrderStatus)
                   .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.Location)
                   .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.Queue)
                   .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.Asset)
                   .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.Zone)
                   .Include(lp => lp.LiteralsPreconditions)
                        .ThenInclude(x => x.PreconditionsLiteralValues)
                                .ThenInclude(x => x.PeopleTechnician)
                   .Include(lp => lp.LiteralsPreconditions)
                        .ThenInclude(x => x.PreconditionsLiteralValues)
                                .ThenInclude(x => x.Project)
                   .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.WorkOrderTypesN1)
                   .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.WorkOrderTypesN2)
                   .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.WorkOrderTypesN3)
                    .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.WorkOrderTypesN4)
                    .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.WorkOrderTypesN5)
                    .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.PeopleManipulator)
                   .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.WorkOrderCategory)
                   .Include(lp => lp.LiteralsPreconditions)
                       .ThenInclude(x => x.PreconditionsLiteralValues)
                            .ThenInclude(x => x.PeopleResponsibleTechniciansCollection)
                   .FirstOrDefault();
        }
    }
}