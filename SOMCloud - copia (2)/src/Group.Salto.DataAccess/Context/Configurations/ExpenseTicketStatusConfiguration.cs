using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class ExpenseTicketStatusConfiguration : EntityMappingConfiguration<ExpenseTicketStatus>
    {
        public override void Map(EntityTypeBuilder<ExpenseTicketStatus> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private void Configuration(EntityTypeBuilder<ExpenseTicketStatus> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);
        }
    }
}