using DataAccess.Common;
using Group.Salto.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Group.Salto.DataAccess.Context.Configurations
{
    public class CustomersConfiguration : EntityMappingConfiguration<Customers>
    {
        public override void Map(EntityTypeBuilder<Customers> entityTypeBuilder)
        {
            Configuration(entityTypeBuilder);
        }

        private static void Configuration(EntityTypeBuilder<Customers> entityTypeBuilder)
        {
            entityTypeBuilder.Property(e => e.ConnString)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

            entityTypeBuilder.Property(e => e.DateCreated).HasColumnType("datetime");

            entityTypeBuilder.Property(e => e.InvoicingContactEmail)
                .HasMaxLength(100)
                .IsUnicode(false);

            entityTypeBuilder.Property(e => e.InvoicingContactName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entityTypeBuilder.Property(e => e.InvoicingContactFirstSurname)
                .HasMaxLength(50)
                .IsUnicode(false);

            entityTypeBuilder.Property(e => e.InvoicingContactSecondSurname)
                .HasMaxLength(50)
                .IsUnicode(false);

            entityTypeBuilder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entityTypeBuilder.HasIndex(e => e.Name).IsUnique();

            entityTypeBuilder.Property(e => e.DatabaseName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entityTypeBuilder.HasIndex(e => e.DatabaseName).IsUnique();

            entityTypeBuilder.Property(e => e.TechnicalAdministratorEmail)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            entityTypeBuilder.Property(e => e.TechnicalAdministratorName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entityTypeBuilder.Property(e => e.TechnicalAdministratorFirstSurname)
                .HasMaxLength(50)
                .IsUnicode(false);

            entityTypeBuilder.Property(e => e.TechnicalAdministratorSecondSurname)
                .HasMaxLength(50)
                .IsUnicode(false);

            entityTypeBuilder.Property(e => e.NIF).IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false); 

            entityTypeBuilder.Property(e => e.CustomerCode).HasMaxLength(20)
                .IsUnicode(false);
        }
    }
}