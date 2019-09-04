using DataAccess.Common;
using DataAccess.Common.Context;
using Group.Salto.Common;
using Group.Salto.Common.Entities;
using Group.Salto.Common.Entities.Contracts;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Context
{
    public class SOMContext : IdentityDbContext<Users>, IOwnerIdentifier
    {

        public string OwnerId { get; set; }

        public SOMContext(DbContextOptions options) : base(options) { }

        public DbSet<Actions> Actions { get; set; }
        //TODO: Carmen. RolesActionGroupsActions
        public DbSet<ActionsRoles> ActionsRoles { get; set; }
        public DbSet<AvailabilityCategories> AvailabilityCategories { get; set; }
        public DbSet<CalendarEventCategories> CalendarEventCategories { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<CitiesOtherNames> CitiesOtherNames { get; set; }
        public DbSet<ContractType> ContractType { get; set; }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<InfrastructureConfiguration> InfrastructureConfiguration { get; set; }
        public DbSet<Municipalities> Municipalities { get; set; }
        public DbSet<Origins> Origins { get; set; }
        public DbSet<PostalCodes> PostalCodes { get; set; }
        public DbSet<PredefinedDayStates> PredefinedDayStates { get; set; }
        public DbSet<PredefinedReasons> PredefinedReasons { get; set; }
        public DbSet<Regions> Regions { get; set; }
        public DbSet<RepetitionTypes> RepetitionTypes { get; set; }
        public DbSet<ServiceStates> ServiceStates { get; set; }
        public DbSet<States> States { get; set; }
        public DbSet<StatesOtherNames> StatesOtherNames { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Translation> Translations { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<CustomerModule> CustomerModules { get; set; }
        public DbSet<Tracker> Trackers { get; set; }
        public DbSet<ReferenceTimeSla> ReferenceTimeSla { get; set; }
        public DbSet<ExpenseTicketStatus> ExpenseTicketStatuses { get; set; }
        public DbSet<ExtraFieldTypes> ExtraFieldTypes { get; set; }
        public DbSet<DamagedEquipment> DamagedEquipment { get; set; }
        public DbSet<CalculationType> CalculationTypes { get; set; }
        public DbSet<DaysType> DaysTypes { get; set; }
        public DbSet<WorkOrderColumns> WorkOrderColumns { get; set; }
        public DbSet<WorkOrderDefaultColumns> WorkOrderDefaultColumns { get; set; }
        public DbSet<ActionGroups> ActionGroups { get; set; }
        public DbSet<ModuleActionGroups> ModuleActionGroups { get; set; }
        public DbSet<RolesActionGroupsActions> RolesActionGroupsActions { get; set; }
        public DbSet<ItemTypes> ItemTypes { get; set; }
        public DbSet<WarehouseMovementTypes> WarehouseMovementTypes { get; set; }
        public DbSet<WorkForm> WorkForms { get; set; }
        public DbSet<BillStatus> BillStatus { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddEntityConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var entitiesTrackables = ChangeTracker.Entries().Where(e => e.Entity is ITrackable && e.State == EntityState.Modified).ToList();
            foreach (var entityTrackable in entitiesTrackables)
            {
                List<Tracker> trackerList = entityTrackable.GetPropertiesTrackablesModified(OwnerId);
                if (trackerList.Any()) Trackers.AddRange(trackerList);
            }

            var entitiesSoftDeleted = ChangeTracker.Entries().Where(e => e.Entity is ISoftDelete && e.State == EntityState.Deleted).ToList();
            foreach (var entitySoftDelete in entitiesSoftDeleted)
            {
                entitySoftDelete.State = EntityState.Modified;
                entitySoftDelete.Property(nameof(ISoftDelete.IsDeleted)).CurrentValue = true;
                entitySoftDelete.Property(nameof(ISoftDelete.IsDeleted)).IsModified = true;
            }

            return base.SaveChanges();
        }
    }
}