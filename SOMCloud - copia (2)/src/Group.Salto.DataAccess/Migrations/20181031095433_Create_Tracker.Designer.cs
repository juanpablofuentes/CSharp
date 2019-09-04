﻿// <auto-generated />
using System;
using Group.Salto.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Group.Salto.DataAccess.Migrations
{
    [DbContext(typeof(SOMContext))]
    [Migration("20181031095433_Create_Tracker")]
    partial class Create_Tracker
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Group.Salto.Entities.Actions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.ToTable("Actions");
                });

            modelBuilder.Entity("Group.Salto.Entities.ActionsRoles", b =>
                {
                    b.Property<string>("RoleId");

                    b.Property<int>("ActionId");

                    b.HasKey("RoleId", "ActionId");

                    b.HasIndex("ActionId");

                    b.ToTable("ActionsRoles");
                });

            modelBuilder.Entity("Group.Salto.Entities.AvailabilityCategories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("IsAvailable");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.ToTable("AvailabilityCategories");
                });

            modelBuilder.Entity("Group.Salto.Entities.CalendarEventCategories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AvailabilityCategoriesId");

                    b.Property<string>("Color")
                        .HasMaxLength(8)
                        .IsUnicode(false);

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("AvailabilityCategoriesId");

                    b.ToTable("CalendarEventCategories");
                });

            modelBuilder.Entity("Group.Salto.Entities.Cities", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MunicipalityId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("MunicipalityId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Group.Salto.Entities.CitiesOtherNames", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id", "Name");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("UQ__CitiesOt__737584F626077C11");

                    b.ToTable("CitiesOtherNames");
                });

            modelBuilder.Entity("Group.Salto.Entities.ContractType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("Value")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("ContractType");
                });

            modelBuilder.Entity("Group.Salto.Entities.Countries", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Group.Salto.Entities.CustomerModule", b =>
                {
                    b.Property<Guid>("CustomerId");

                    b.Property<Guid>("ModuleId");

                    b.HasKey("CustomerId", "ModuleId");

                    b.HasIndex("ModuleId");

                    b.ToTable("CustomerModules");
                });

            modelBuilder.Entity("Group.Salto.Entities.Customers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdministrativeEmail")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("AdministrativeFullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("ConnString")
                        .IsRequired()
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<string>("DatabaseName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsActive");

                    b.Property<string>("NIF")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("NumberAppUsers");

                    b.Property<int>("NumberTeamMembers");

                    b.Property<int>("NumberWebUsers");

                    b.Property<string>("TechnicalEmail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("TechnicalFullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("UpdateDate");

                    b.Property<DateTime>("UpdateStatusDate");

                    b.HasKey("Id");

                    b.HasIndex("DatabaseName")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Group.Salto.Entities.InfrastructureConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Key")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<int>("TenantId");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(1)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("InfrastructureConfiguration");
                });

            modelBuilder.Entity("Group.Salto.Entities.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CultureCode")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("CultureCode")
                        .IsUnique();

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Group.Salto.Entities.Module", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Key")
                        .IsRequired();

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("Key")
                        .IsUnique();

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("Group.Salto.Entities.Municipalities", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MunicipalityCode");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<int>("StateId");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("Municipalities");
                });

            modelBuilder.Entity("Group.Salto.Entities.Origins", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CanBeDeleted");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Observations")
                        .HasMaxLength(1000)
                        .IsUnicode(false);

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("UK_Origin");

                    b.ToTable("Origins");
                });

            modelBuilder.Entity("Group.Salto.Entities.PostalCodes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityId");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("PostalCodes");
                });

            modelBuilder.Entity("Group.Salto.Entities.PredefinedDayStates", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.ToTable("PredefinedDayStates");
                });

            modelBuilder.Entity("Group.Salto.Entities.PredefinedReasons", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<int>("PredefinedDayStatesId");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("PredefinedDayStatesId");

                    b.ToTable("PredefinedReasons");
                });

            modelBuilder.Entity("Group.Salto.Entities.Regions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("Group.Salto.Entities.RepetitionTypes", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.ToTable("RepetitionTypes");
                });

            modelBuilder.Entity("Group.Salto.Entities.ServiceStates", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.ToTable("ServiceStates");
                });

            modelBuilder.Entity("Group.Salto.Entities.States", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<int>("RegionId");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("States");
                });

            modelBuilder.Entity("Group.Salto.Entities.StatesOtherNames", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id", "Name");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("UQ__StatesOt__737584F64516A7F1");

                    b.ToTable("StatesOtherNames");
                });

            modelBuilder.Entity("Group.Salto.Entities.Tracker", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EntityId")
                        .IsRequired();

                    b.Property<string>("EntityType")
                        .IsRequired();

                    b.Property<string>("NewValue")
                        .IsRequired();

                    b.Property<string>("OldValue")
                        .IsRequired();

                    b.Property<string>("OwnerId")
                        .IsRequired();

                    b.Property<string>("PropertyName")
                        .IsRequired();

                    b.Property<string>("TimeStamp")
                        .IsRequired();

                    b.Property<Guid>("TransactionId");

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.ToTable("Trackers");
                });

            modelBuilder.Entity("Group.Salto.Entities.Translation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Key")
                        .IsRequired();

                    b.Property<int>("LanguageId");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("Key", "LanguageId")
                        .IsUnique();

                    b.ToTable("Translations");
                });

            modelBuilder.Entity("Group.Salto.Entities.Users", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("ConfigurationWoFields");

                    b.Property<Guid?>("CustomerId");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstSurname");

                    b.Property<string>("Language");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<int>("NumberEntriesPerPage");

                    b.Property<string>("Observations");

                    b.Property<int>("OldUserId");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecondSurname");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<int?>("UserConfigurationId");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Group.Salto.Entities.Roles", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole");

                    b.Property<string>("Description");

                    b.ToTable("Roles");

                    b.HasDiscriminator().HasValue("Roles");
                });

            modelBuilder.Entity("Group.Salto.Entities.ActionsRoles", b =>
                {
                    b.HasOne("Group.Salto.Entities.Actions", "Actions")
                        .WithMany("ActionsRoles")
                        .HasForeignKey("ActionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Group.Salto.Entities.Roles", "Roles")
                        .WithMany("ActionsRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Group.Salto.Entities.CalendarEventCategories", b =>
                {
                    b.HasOne("Group.Salto.Entities.AvailabilityCategories", "AvailabilityCategories")
                        .WithMany("CalendarEventCategories")
                        .HasForeignKey("AvailabilityCategoriesId")
                        .HasConstraintName("FK_CalendarEventCategories_AvailabilityCategories");
                });

            modelBuilder.Entity("Group.Salto.Entities.Cities", b =>
                {
                    b.HasOne("Group.Salto.Entities.Municipalities", "Municipality")
                        .WithMany("Cities")
                        .HasForeignKey("MunicipalityId")
                        .HasConstraintName("FK__Cities__Municipa__4D94879B");
                });

            modelBuilder.Entity("Group.Salto.Entities.CitiesOtherNames", b =>
                {
                    b.HasOne("Group.Salto.Entities.Cities", "IdNavigation")
                        .WithMany("CitiesOtherNames")
                        .HasForeignKey("Id")
                        .HasConstraintName("FK__CitiesOtherN__Id__4E88ABD4");
                });

            modelBuilder.Entity("Group.Salto.Entities.CustomerModule", b =>
                {
                    b.HasOne("Group.Salto.Entities.Customers", "Customer")
                        .WithMany("ModulesAssigned")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Group.Salto.Entities.Module", "Module")
                        .WithMany("CustomersAssigned")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Group.Salto.Entities.Municipalities", b =>
                {
                    b.HasOne("Group.Salto.Entities.States", "State")
                        .WithMany("Municipalities")
                        .HasForeignKey("StateId")
                        .HasConstraintName("FK__Municipal__State__4F7CD00D");
                });

            modelBuilder.Entity("Group.Salto.Entities.PostalCodes", b =>
                {
                    b.HasOne("Group.Salto.Entities.Cities", "IdNavigation")
                        .WithMany("PostalCodes")
                        .HasForeignKey("CityId")
                        .HasConstraintName("FK__PostalCodes__Id__5070F446");
                });

            modelBuilder.Entity("Group.Salto.Entities.PredefinedReasons", b =>
                {
                    b.HasOne("Group.Salto.Entities.PredefinedDayStates", "PredefinedDayStates")
                        .WithMany("PredefinedReasons")
                        .HasForeignKey("PredefinedDayStatesId")
                        .HasConstraintName("FK_PredefinedReasons_PredefinedDayStates");
                });

            modelBuilder.Entity("Group.Salto.Entities.Regions", b =>
                {
                    b.HasOne("Group.Salto.Entities.Countries", "Country")
                        .WithMany("Regions")
                        .HasForeignKey("CountryId")
                        .HasConstraintName("FK__Regions__Country__52593CB8");
                });

            modelBuilder.Entity("Group.Salto.Entities.States", b =>
                {
                    b.HasOne("Group.Salto.Entities.Regions", "Region")
                        .WithMany("States")
                        .HasForeignKey("RegionId")
                        .HasConstraintName("FK__States__RegionId__534D60F1");
                });

            modelBuilder.Entity("Group.Salto.Entities.StatesOtherNames", b =>
                {
                    b.HasOne("Group.Salto.Entities.States", "IdNavigation")
                        .WithMany("StatesOtherNames")
                        .HasForeignKey("Id")
                        .HasConstraintName("FK__StatesOtherN__Id__5441852A");
                });

            modelBuilder.Entity("Group.Salto.Entities.Translation", b =>
                {
                    b.HasOne("Group.Salto.Entities.Language", "Language")
                        .WithMany("Translations")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Group.Salto.Entities.Users", b =>
                {
                    b.HasOne("Group.Salto.Entities.Customers", "Customer")
                        .WithMany("Users")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Group.Salto.Entities.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Group.Salto.Entities.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Group.Salto.Entities.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Group.Salto.Entities.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
