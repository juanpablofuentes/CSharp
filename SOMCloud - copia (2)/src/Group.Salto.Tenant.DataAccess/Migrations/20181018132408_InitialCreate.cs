using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Color = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    IsDefault = table.Column<bool>(nullable: true),
                    IsRetiredState = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Url = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Calendars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Description = table.Column<string>(unicode: false, maxLength: 140, nullable: true),
                    Color = table.Column<string>(unicode: false, maxLength: 8, nullable: true),
                    IsPrivate = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    CorporateName = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Phone = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Observations = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    IdICG = table.Column<int>(nullable: true),
                    InternCode = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    ContableCode = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    ComercialName = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Alias = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Address = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    City = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Province = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Country = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    MobilePhone = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Mail = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Web = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    BankCode = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    BranchNumber = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    ControlDigit = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    AccountNumber = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    SwiftCode = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    BankName = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    BankAddress = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    BankPostalCode = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    BankCity = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    UnListed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollectionsClosureCodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionsClosureCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollectionsExtraField",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Description = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Observations = table.Column<string>(unicode: false, maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionsExtraField", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollectionsTypesWorkOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Description = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionsTypesWorkOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    CostKm = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    FirstSurname = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SecondSurname = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Telephone = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Position = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ErpSystemInstance",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ErpSystemIdentifier = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    DatabaseIpAddress = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    DatabaseUser = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    DatabasePwd = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    DatabaseName = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErpSystemInstance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Type = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Unit = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExternalWorOrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Color = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    IdICG = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalWorOrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Families",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Families", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flows",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Page = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    PageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Guarantee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    IdExternal = table.Column<int>(nullable: true),
                    Standard = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    StdStartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    StdEndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Armored = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    BlnStartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    BlnEndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Provider = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ProStartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ProEndDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guarantee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HiredServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Observations = table.Column<string>(unicode: false, maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HiredServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ErpReference = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    SyncErp = table.Column<bool>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    ErpVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Knowledge",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Observations = table.Column<string>(unicode: false, maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knowledge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MailTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    Content = table.Column<string>(unicode: false, nullable: false),
                    Subject = table.Column<string>(unicode: false, maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainOTStatics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ColumnName = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Percentage = table.Column<double>(nullable: true, defaultValueSql: "((0.0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainOTStatics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainWORegistry",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Duration = table.Column<int>(nullable: true),
                    PersonId = table.Column<int>(nullable: true),
                    VisibleWO = table.Column<int>(nullable: true),
                    FilteredWO = table.Column<int>(nullable: true),
                    ArchivedWO = table.Column<int>(nullable: true),
                    ExportWO = table.Column<bool>(nullable: true),
                    ExportServices = table.Column<bool>(nullable: true),
                    OnlyServices = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainWORegistry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OptimizationFunctionWeights",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    TravelCost = table.Column<double>(nullable: false),
                    EconomicCostWeight = table.Column<double>(nullable: false),
                    ExternalTechnicianCostFactor = table.Column<double>(nullable: false),
                    UnattendedWOPenalty = table.Column<double>(nullable: false),
                    FulfillSLAWeight = table.Column<double>(nullable: false),
                    UnmetSkillPenalty = table.Column<double>(nullable: false),
                    ReplanPenalty = table.Column<double>(nullable: false),
                    ReplanPenaltyFixed = table.Column<double>(nullable: false),
                    OverMeanWorkLoadCost = table.Column<double>(nullable: false),
                    TravelSpeed = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptimizationFunctionWeights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Mode = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeopleCollections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Info = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleCollections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Observations = table.Column<string>(nullable: true),
                    CanBeDeleted = table.Column<int>(nullable: false),
                    Tasks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanificationProcessCalendarChangeTracker",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    PlanificationProcessId = table.Column<int>(nullable: false),
                    LastCheckTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    PersonId = table.Column<int>(nullable: true),
                    CalendarPriority = table.Column<int>(nullable: true),
                    CalendarId = table.Column<int>(nullable: true),
                    EventId = table.Column<int>(nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDisponible = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanificationProcessCalendarChangeTracker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlanificationProcessWorkOrderChangeTracker",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    PlanificationProcessId = table.Column<int>(nullable: false),
                    LastCheckTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    WorkOrderId = table.Column<int>(nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanificationProcessWorkOrderChangeTracker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PointsRate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ErpReference = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointsRate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseRate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ErpReference = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseRate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PushNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Message = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    Creator = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Queues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Description = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Queues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesRate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ErpReference = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesRate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SaltoCSVersion",
                columns: table => new
                {
                    Version = table.Column<string>(unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaltoCSVersion", x => x.Version);
                });

            migrationBuilder.CreateTable(
                name: "SLA",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    MinutesResponse = table.Column<int>(nullable: true),
                    MinutesNaturalResponse = table.Column<bool>(nullable: true),
                    MinutesResolutions = table.Column<int>(nullable: true),
                    MinutesResolutionNaturals = table.Column<bool>(nullable: true),
                    MinutesUnansweredPenalty = table.Column<int>(nullable: true),
                    MinutesWithoutNaturalResponse = table.Column<bool>(nullable: true),
                    MinutesPenaltyWithoutResolution = table.Column<int>(nullable: true),
                    MinutesPenaltyWithoutNaturalResolution = table.Column<bool>(nullable: true),
                    ReferenceMinutesResponse = table.Column<string>(unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('DataCreacio')"),
                    ReferenceMinutesResolution = table.Column<string>(unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('DataCreacio')"),
                    ReferenceMinutesPenaltyUnanswered = table.Column<string>(unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('DataCreacio')"),
                    ReferenceMinutesPenaltyWithoutResolution = table.Column<string>(unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('DataCreacio')"),
                    TimeResponseActive = table.Column<bool>(nullable: false, defaultValueSql: "('true')"),
                    TimeResolutionActive = table.Column<bool>(nullable: false, defaultValueSql: "('true')"),
                    TimePenaltyWithoutResponseActive = table.Column<bool>(nullable: false, defaultValueSql: "('true')"),
                    TimePenaltyWhithoutResolutionActive = table.Column<bool>(nullable: false, defaultValueSql: "('true')"),
                    MinutesResponseOtDefined = table.Column<bool>(nullable: false, defaultValueSql: "('false')"),
                    MinutesResolutionOtDefined = table.Column<bool>(nullable: false, defaultValueSql: "('false')"),
                    MinutesPenaltyWithoutResponseOtDefined = table.Column<bool>(nullable: false, defaultValueSql: "('false')"),
                    MinutesPenaltyWithoutResolutionOtDefined = table.Column<bool>(nullable: false, defaultValueSql: "('false')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SLA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SomFiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Container = table.Column<string>(unicode: false, maxLength: 1000, nullable: false),
                    Directory = table.Column<string>(unicode: false, maxLength: 1000, nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 1000, nullable: false),
                    ContentMd5 = table.Column<string>(unicode: false, maxLength: 32, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SomFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StopSlaReason",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Description = table.Column<string>(unicode: false, maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StopSlaReason", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SynchronizationSessions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Origin = table.Column<string>(unicode: false, maxLength: 250, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SynchronizationSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Message = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    PublicationDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    VisibilityEndTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Type = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Creator = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Global = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskTokens",
                columns: table => new
                {
                    Token = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Consumed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTokens", x => x.Token)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "TenantConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Value = table.Column<string>(unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantConfiguration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToolsType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Observations = table.Column<string>(unicode: false, maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolsType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Description = table.Column<string>(unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderCategoriesCollections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Info = table.Column<string>(unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderCategoriesCollections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Color = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    IdICG = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    IsWoclosed = table.Column<bool>(nullable: true),
                    IsPlannable = table.Column<bool>(nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    BrandId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Url = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Models_Brands",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalendarEvents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    CalendarId = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Description = table.Column<string>(unicode: false, maxLength: 140, nullable: true),
                    Category = table.Column<int>(nullable: true),
                    RepetitionType = table.Column<int>(nullable: true),
                    StartTime = table.Column<TimeSpan>(nullable: true),
                    Duration = table.Column<int>(nullable: true),
                    HasEnd = table.Column<bool>(nullable: true),
                    NumberOfRepetitions = table.Column<int>(nullable: true),
                    RepetitionPeriod = table.Column<int>(nullable: true),
                    RepeatOnMonday = table.Column<bool>(nullable: true),
                    RepeatOnTuesday = table.Column<bool>(nullable: true),
                    RepeatOnWednesday = table.Column<bool>(nullable: true),
                    RepeatOnThursday = table.Column<bool>(nullable: true),
                    RepeatOnFriday = table.Column<bool>(nullable: true),
                    RepeatOnSaturday = table.Column<bool>(nullable: true),
                    RepeatOnSunday = table.Column<bool>(nullable: true),
                    RepeatOnDayNumber = table.Column<int>(nullable: true),
                    RepeatOnMonthNumber = table.Column<int>(nullable: true),
                    Color = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ParentEventId = table.Column<int>(nullable: true),
                    DeletedOccurrence = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    ReplacedEventOccurrenceTS = table.Column<long>(nullable: true),
                    CostHour = table.Column<double>(nullable: true, defaultValueSql: "((0))"),
                    ModificationDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK__CalendarE__Calen__5B438874",
                        column: x => x.CalendarId,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__CalendarE__Paren__5C37ACAD",
                        column: x => x.ParentEventId,
                        principalTable: "CalendarEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClosingCodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    CollectionsClosureCodesId = table.Column<int>(nullable: true),
                    ClosingCodesFatherId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosingCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClosingCodes_ClosingCodesFather",
                        column: x => x.ClosingCodesFatherId,
                        principalTable: "ClosingCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClosingCodes_CollectionsClosureCodes",
                        column: x => x.CollectionsClosureCodesId,
                        principalTable: "CollectionsClosureCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompaniesCostHistorical",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    CompanyId = table.Column<int>(nullable: true),
                    CostKm = table.Column<double>(nullable: false),
                    Until = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompaniesCostHistorical", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Companies__Compa__66B53B20",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Departmen__Compa__6F4A8121",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ErpItemsSyncConfig",
                columns: table => new
                {
                    TenantId = table.Column<int>(nullable: false),
                    ErpSystemInstanceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErpItemsSyncConfig", x => x.TenantId);
                    table.ForeignKey(
                        name: "FK_ErpItemsSyncConfig_ErpSystemInstance",
                        column: x => x.ErpSystemInstanceId,
                        principalTable: "ErpSystemInstance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ErpSystemInstanceQuery",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ErpSystemInstanceId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    SqlQuery = table.Column<string>(unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErpSystemInstanceQuery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErpSystemInstanceQuery_ErpSystemInstance",
                        column: x => x.ErpSystemInstanceId,
                        principalTable: "ErpSystemInstance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WsErpSystemInstance",
                columns: table => new
                {
                    ErpSystemInstanceId = table.Column<int>(nullable: false),
                    WebServiceIpAddress = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    WebServiceUser = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    WebServicePwd = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    WebServiceMethod = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WsErpSystemInstance", x => x.ErpSystemInstanceId);
                    table.ForeignKey(
                        name: "FK_WsErpSystemInstance_ErpSystemInstance",
                        column: x => x.ErpSystemInstanceId,
                        principalTable: "ErpSystemInstance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubFamilies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    FamilyId = table.Column<int>(nullable: false),
                    Nom = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Descripcio = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubFamilies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubFamilies_Families",
                        column: x => x.FamilyId,
                        principalTable: "Families",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FormElements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    FormConfigsId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Value = table.Column<string>(unicode: false, maxLength: 5000, nullable: true),
                    Type = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormElements_FormConfigs",
                        column: x => x.FormConfigsId,
                        principalTable: "FormConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemsSerialNumber",
                columns: table => new
                {
                    ItemId = table.Column<int>(nullable: false),
                    SerialNumber = table.Column<string>(unicode: false, maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsSerialNumber", x => new { x.ItemId, x.SerialNumber });
                    table.ForeignKey(
                        name: "FK_ItemsSerialNumber_Items",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanificationProcesses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ExecutionCalendar = table.Column<int>(nullable: false),
                    CheckFrequency = table.Column<int>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    StartCriteria = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    MinutesToSLAEnd = table.Column<int>(nullable: true),
                    WorkOrdersFilter = table.Column<int>(nullable: false),
                    HumanResourcesFilter = table.Column<int>(nullable: false),
                    MaxDuration = table.Column<int>(nullable: false),
                    Weights = table.Column<int>(nullable: false),
                    LastModification = table.Column<DateTime>(type: "datetime", nullable: true),
                    Horizon = table.Column<int>(nullable: false, defaultValueSql: "((3))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanificationProcesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Planifica__Execu__4F9CCB9E",
                        column: x => x.ExecutionCalendar,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Planifica__Human__5090EFD7",
                        column: x => x.HumanResourcesFilter,
                        principalTable: "FormConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Planifica__Weigh__51851410",
                        column: x => x.Weights,
                        principalTable: "OptimizationFunctionWeights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Planifica__WorkO__52793849",
                        column: x => x.WorkOrdersFilter,
                        principalTable: "FormConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PeopleCollectionCalendars",
                columns: table => new
                {
                    PeopleCollectionId = table.Column<int>(nullable: false),
                    CalendarId = table.Column<int>(nullable: false),
                    CalendarPriority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleCollectionCalendars", x => new { x.PeopleCollectionId, x.CalendarId });
                    table.ForeignKey(
                        name: "FK_PeopleCollectionCalendars_Calendars",
                        column: x => x.CalendarId,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PeopleCollectionCalendars_PeopleCollections",
                        column: x => x.PeopleCollectionId,
                        principalTable: "PeopleCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PeopleCollectionsPermissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(nullable: false),
                    PeopleCollectionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleCollectionsPermissions", x => new { x.PermissionId, x.PeopleCollectionId });
                    table.ForeignKey(
                        name: "FK_PeopleCollectionsPermissions_PeopleCollections_PeopleCollectionId",
                        column: x => x.PeopleCollectionId,
                        principalTable: "PeopleCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeopleCollectionsPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemsPointsRate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    PointsRateId = table.Column<int>(nullable: false),
                    Points = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsPointsRate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsPointsRate_Items",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemsPointsRate_PointsRate",
                        column: x => x.PointsRateId,
                        principalTable: "PointsRate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemsPurchaseRate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    PurchaseRateId = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsPurchaseRate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsPurchaseRate_Items",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemsPurchaseRate_PurchaseRate",
                        column: x => x.PurchaseRateId,
                        principalTable: "PurchaseRate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PushNotificationsPeopleCollections",
                columns: table => new
                {
                    NotificationId = table.Column<int>(nullable: false),
                    PeopleCollectionsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushNotificationsPeopleCollections", x => new { x.NotificationId, x.PeopleCollectionsId });
                    table.ForeignKey(
                        name: "FK_PushNotificationsPeopleCollections_Notification",
                        column: x => x.NotificationId,
                        principalTable: "PushNotifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PushNotificationsPeopleCollections_PeopleCollection",
                        column: x => x.PeopleCollectionsId,
                        principalTable: "PeopleCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermissionsQueues",
                columns: table => new
                {
                    PermissionId = table.Column<int>(nullable: false),
                    QueueId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionsQueues", x => new { x.PermissionId, x.QueueId });
                    table.ForeignKey(
                        name: "FK_PermissionsQueues_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionsQueues_Queues_QueueId",
                        column: x => x.QueueId,
                        principalTable: "Queues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemsSalesRate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    SalesRateId = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsSalesRate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsSalesRate_Items",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemsSalesRate_SalesRate",
                        column: x => x.SalesRateId,
                        principalTable: "SalesRate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubContracts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Observations = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    SalesRateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK__SubContra__Sales__1BE81D6E",
                        column: x => x.SalesRateId,
                        principalTable: "SalesRate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StatesSla",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    SlaId = table.Column<int>(nullable: false),
                    MinutesToTheEnd = table.Column<int>(nullable: true),
                    RowColor = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatesSla", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatesSla_Sla",
                        column: x => x.SlaId,
                        principalTable: "SLA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Observations = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    CollectionsTypesWorkOrdersId = table.Column<int>(nullable: true),
                    WorkOrderTypesFatherId = table.Column<int>(nullable: true),
                    SlaId = table.Column<int>(nullable: true),
                    EstimatedDuration = table.Column<int>(nullable: true),
                    Serie = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderTypes_CollectionsTypesWorkOrders",
                        column: x => x.CollectionsTypesWorkOrdersId,
                        principalTable: "CollectionsTypesWorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrderTypes_Sla",
                        column: x => x.SlaId,
                        principalTable: "SLA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrderTypes_WorkOrderTypesFather",
                        column: x => x.WorkOrderTypesFatherId,
                        principalTable: "WorkOrderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeToolsType",
                columns: table => new
                {
                    KnowledgeId = table.Column<int>(nullable: false),
                    ToolsTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeToolsType", x => new { x.KnowledgeId, x.ToolsTypeId });
                    table.ForeignKey(
                        name: "FK_KnowledgeToolsType_Knowledge",
                        column: x => x.KnowledgeId,
                        principalTable: "Knowledge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KnowledgeToolsType_ToolsType",
                        column: x => x.ToolsTypeId,
                        principalTable: "ToolsType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServicesViewConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    UserConfigurationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesViewConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicesViewConfigurations_UserConfigurations_UserConfigurationId",
                        column: x => x.UserConfigurationId,
                        principalTable: "UserConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    DateLastActivity = table.Column<DateTime>(type: "datetime", nullable: false),
                    SecondsExpiration = table.Column<int>(nullable: false),
                    AndroidVersion = table.Column<int>(nullable: true),
                    AndroidRelease = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    AppVersion = table.Column<int>(nullable: true),
                    UserConfigurationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_UserConfigurations_UserConfigurationId",
                        column: x => x.UserConfigurationId,
                        principalTable: "UserConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersMainWOViewConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    UserConfigurationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersMainWOViewConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersMainWOViewConfigurations_UserConfigurations_UserConfigurationId",
                        column: x => x.UserConfigurationId,
                        principalTable: "UserConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Info = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    SlaId = table.Column<int>(nullable: false),
                    WorkOrderCategoriesCollectionId = table.Column<int>(nullable: false),
                    EstimatedDuration = table.Column<double>(nullable: true),
                    EconomicCharge = table.Column<double>(nullable: true),
                    ProjectCalendarPreference = table.Column<int>(nullable: false),
                    CategoryCalendarPreference = table.Column<int>(nullable: false),
                    ClientSiteCalendarPreference = table.Column<int>(nullable: false),
                    SiteCalendarPreference = table.Column<int>(nullable: false),
                    Serie = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Url = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    DefaultTechnicalCode = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    TechnicalResponsible = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    BackOfficeResponsible = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    IsGhost = table.Column<bool>(nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderCategories_Sla",
                        column: x => x.SlaId,
                        principalTable: "SLA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrderCategories_WorkOrderCategoriesCollections",
                        column: x => x.WorkOrderCategoriesCollectionId,
                        principalTable: "WorkOrderCategoriesCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderCategoriesCollectionCalendar",
                columns: table => new
                {
                    WorkOrderCategoriesCollectionId = table.Column<int>(nullable: false),
                    CalendarId = table.Column<int>(nullable: false),
                    CalendarPriority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderCategoriesCollectionCalendar", x => new { x.WorkOrderCategoriesCollectionId, x.CalendarId });
                    table.ForeignKey(
                        name: "FK__WorkOrder__Calen__420DC656",
                        column: x => x.CalendarId,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__WorkOrder__WorkO__4301EA8F",
                        column: x => x.WorkOrderCategoriesCollectionId,
                        principalTable: "WorkOrderCategoriesCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExtraFields",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 900, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    IsMandatory = table.Column<bool>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Observations = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    AllowedStringValues = table.Column<string>(unicode: false, maxLength: 5000, nullable: true),
                    MultipleChoice = table.Column<bool>(nullable: true),
                    ErpSystemInstanceQueryId = table.Column<int>(nullable: true),
                    DelSystem = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtraFields_ErpSystemInstanceQuery",
                        column: x => x.ErpSystemInstanceQueryId,
                        principalTable: "ErpSystemInstanceQuery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeSubContracts",
                columns: table => new
                {
                    KnowledgeId = table.Column<int>(nullable: false),
                    SubContractId = table.Column<int>(nullable: false),
                    Maturity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeSubContracts", x => new { x.KnowledgeId, x.SubContractId });
                    table.ForeignKey(
                        name: "FK_KnowledgeSubContracts_Knowledge",
                        column: x => x.KnowledgeId,
                        principalTable: "Knowledge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KnowledgeSubContracts_SubContracts",
                        column: x => x.SubContractId,
                        principalTable: "SubContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Dni = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    FisrtSurname = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SecondSurname = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Telephone = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    IsClientWorker = table.Column<int>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    IcgId = table.Column<int>(nullable: true),
                    SubcontractId = table.Column<int>(nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    WorkRadiusKm = table.Column<double>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    SubcontractorResponsible = table.Column<bool>(nullable: true),
                    WarehouseId = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Extension = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CompanyId = table.Column<int>(nullable: true),
                    CostKm = table.Column<double>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: true),
                    DocumentationUrl = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    PointsRateId = table.Column<int>(nullable: true),
                    UserConfigurationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK__People__CompanyI__3AA1AEB8",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__People__Departme__3B95D2F1",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__People__PointsRa__3C89F72A",
                        column: x => x.PointsRateId,
                        principalTable: "PointsRate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_SubContracts",
                        column: x => x.SubcontractId,
                        principalTable: "SubContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_UserConfigurations_UserConfigurationId",
                        column: x => x.UserConfigurationId,
                        principalTable: "UserConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgeWorkOrderTypes",
                columns: table => new
                {
                    KnowledgeId = table.Column<int>(nullable: false),
                    WorkOrderTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeWorkOrderTypes", x => new { x.KnowledgeId, x.WorkOrderTypeId });
                    table.ForeignKey(
                        name: "FK_KnowledgeWorkOrderTypes_Knowledge",
                        column: x => x.KnowledgeId,
                        principalTable: "Knowledge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KnowledgeWorkOrderTypes_WorkOrderTypes",
                        column: x => x.WorkOrderTypeId,
                        principalTable: "WorkOrderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ToolsTypeWorkOrderTypes",
                columns: table => new
                {
                    WorkOrderTypesId = table.Column<int>(nullable: false),
                    ToolsTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolsTypeWorkOrderTypes", x => new { x.WorkOrderTypesId, x.ToolsTypeId });
                    table.ForeignKey(
                        name: "FK_ToolsTypeWorkOrderTypes_ToolsType",
                        column: x => x.ToolsTypeId,
                        principalTable: "ToolsType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ToolsTypeWorkOrderTypes_WorkOrderTypes",
                        column: x => x.WorkOrderTypesId,
                        principalTable: "WorkOrderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServicesViewConfigurationsColumns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ColumnId = table.Column<int>(nullable: false),
                    ColumnOrder = table.Column<int>(nullable: false),
                    FilterValues = table.Column<string>(unicode: false, maxLength: 1000, nullable: false, defaultValueSql: "('')"),
                    FilterStartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    FilterEndDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesViewConfigurationsColumns", x => new { x.Id, x.ColumnId });
                    table.ForeignKey(
                        name: "FK_ServicesViewConfigurationsColumns_ServicesViewConfigurations",
                        column: x => x.Id,
                        principalTable: "ServicesViewConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalendarPlanningViewConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    IsDefault = table.Column<bool>(nullable: true),
                    CalendarPlanningViewConfigurationId = table.Column<int>(nullable: true),
                    UserConfigurationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarPlanningViewConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK__CalendarP__Calen__5D2BD0E6",
                        column: x => x.CalendarPlanningViewConfigurationId,
                        principalTable: "UsersMainWOViewConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalendarPlanningViewConfiguration_UserConfigurations_UserConfigurationId",
                        column: x => x.UserConfigurationId,
                        principalTable: "UserConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MainWoViewConfigurationsColumns",
                columns: table => new
                {
                    UserMainWOViewConfigurationId = table.Column<int>(nullable: false),
                    ColumnId = table.Column<int>(nullable: false),
                    ColumnOrder = table.Column<int>(nullable: false),
                    FilterValues = table.Column<string>(unicode: false, maxLength: 1000, nullable: false, defaultValueSql: "('')"),
                    FilterStartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    FilterEndDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainWoViewConfigurationsColumns", x => new { x.UserMainWOViewConfigurationId, x.ColumnId });
                    table.ForeignKey(
                        name: "FK_MainWOViewConfigurationsColumns_UsersMainWOViewConfigurations",
                        column: x => x.UserMainWOViewConfigurationId,
                        principalTable: "UsersMainWOViewConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MainWOViewConfigurationsGroups",
                columns: table => new
                {
                    UserMainWOViewConfigurationId = table.Column<int>(nullable: false),
                    PeopleCollectionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainWOViewConfigurationsGroups", x => new { x.UserMainWOViewConfigurationId, x.PeopleCollectionId });
                    table.ForeignKey(
                        name: "FK_MainWOViewConfigurationsGroups_PeopleCollection",
                        column: x => x.PeopleCollectionId,
                        principalTable: "PeopleCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MainWOViewConfigurationsGroups_UserMainWoViewConfiguration",
                        column: x => x.UserMainWOViewConfigurationId,
                        principalTable: "UsersMainWOViewConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderCategoryCalendar",
                columns: table => new
                {
                    WorkOrderCategoryId = table.Column<int>(nullable: false),
                    CalendarId = table.Column<int>(nullable: false),
                    CalendarPriority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderCategoryCalendar", x => new { x.WorkOrderCategoryId, x.CalendarId });
                    table.ForeignKey(
                        name: "FK__WorkOrder__Calen__43F60EC8",
                        column: x => x.CalendarId,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__WorkOrder__WorkO__44EA3301",
                        column: x => x.WorkOrderCategoryId,
                        principalTable: "WorkOrderCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderCategoryKnowledge",
                columns: table => new
                {
                    WorkOrderCategoryId = table.Column<int>(nullable: false),
                    KnowledgeId = table.Column<int>(nullable: false),
                    KnowledgeLevel = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderCategoryKnowledge", x => new { x.WorkOrderCategoryId, x.KnowledgeId });
                    table.ForeignKey(
                        name: "FK__WorkOrder__Knowl__45DE573A",
                        column: x => x.KnowledgeId,
                        principalTable: "Knowledge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__WorkOrder__WorkO__46D27B73",
                        column: x => x.WorkOrderCategoryId,
                        principalTable: "WorkOrderCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderCategoryPermissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(nullable: false),
                    WorkOrderCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderCategoryPermissions", x => new { x.PermissionId, x.WorkOrderCategoryId });
                    table.ForeignKey(
                        name: "FK_WorkOrderCategoryPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkOrderCategoryPermissions_WorkOrderCategories_WorkOrderCategoryId",
                        column: x => x.WorkOrderCategoryId,
                        principalTable: "WorkOrderCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CollectionsExtraFieldExtraField",
                columns: table => new
                {
                    CollectionsExtraFieldId = table.Column<int>(nullable: false),
                    ExtraFieldId = table.Column<int>(nullable: false),
                    Position = table.Column<int>(nullable: true, defaultValueSql: "((2147483647))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionsExtraFieldExtraField", x => new { x.CollectionsExtraFieldId, x.ExtraFieldId });
                    table.ForeignKey(
                        name: "FK_CollectionsExtraFieldExtraField_CollectionsExtraField",
                        column: x => x.CollectionsExtraFieldId,
                        principalTable: "CollectionsExtraField",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollectionsExtraFieldExtraField_ExtraField",
                        column: x => x.ExtraFieldId,
                        principalTable: "ExtraFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Object = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Signer = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Observations = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    Reference = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    ContractTypeId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ContractUrl = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    PeopleId = table.Column<int>(nullable: true),
                    ClientId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Client",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_People",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinalClients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    IdExtern = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    OriginId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    NIF = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Phone1 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Phone2 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Phone3 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Fax = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Status = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Observations = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    PeopleCommercialId = table.Column<int>(nullable: true),
                    IcgId = table.Column<int>(nullable: true),
                    Code = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalClients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinalClients_People",
                        column: x => x.PeopleCommercialId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Journeys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    PeopleId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsCompanyVehicle = table.Column<bool>(nullable: false),
                    CompanyVehicleId = table.Column<int>(nullable: true),
                    Finished = table.Column<bool>(nullable: false),
                    StartKm = table.Column<double>(nullable: false),
                    EndKm = table.Column<double>(nullable: true),
                    Observations = table.Column<string>(unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Journeys_People",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KnowledgePeople",
                columns: table => new
                {
                    KnowledgeId = table.Column<int>(nullable: false),
                    PeopleId = table.Column<int>(nullable: false),
                    Maturity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgePeople", x => new { x.KnowledgeId, x.PeopleId });
                    table.ForeignKey(
                        name: "FK_KnowledgePeople_Knowledge",
                        column: x => x.KnowledgeId,
                        principalTable: "Knowledge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KnowledgePeople_People",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    StreetType = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Street = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Code = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Number = table.Column<int>(nullable: true),
                    Escala = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    GateNumber = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    City = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Province = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    PostalCode = table.Column<int>(nullable: true),
                    Country = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Area = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Zone = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Subzone = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    Observations = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    PeopleResponsibleLocationId = table.Column<int>(nullable: true),
                    HashCity = table.Column<int>(nullable: true),
                    HashProvincie = table.Column<int>(nullable: true),
                    HashZone = table.Column<int>(nullable: true),
                    HashSubzone = table.Column<int>(nullable: true),
                    StateId = table.Column<int>(nullable: true),
                    CityId = table.Column<int>(nullable: true),
                    CountryId = table.Column<int>(nullable: true),
                    MunicipalityId = table.Column<int>(nullable: true),
                    RegionId = table.Column<int>(nullable: true),
                    PostalCodeId = table.Column<int>(nullable: true),
                    Phone1 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Phone2 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Phone3 = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_People",
                        column: x => x.PeopleResponsibleLocationId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MainWoViewConfigurationsPeople",
                columns: table => new
                {
                    UserMainWoViewConfigurationId = table.Column<int>(nullable: false),
                    PeopleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainWoViewConfigurationsPeople", x => new { x.UserMainWoViewConfigurationId, x.PeopleId });
                    table.ForeignKey(
                        name: "FK_MainWoViewConfigurationsPeople_People",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MainWoViewConfigurationsPeople_UserMainWoViewConfiguration",
                        column: x => x.UserMainWoViewConfigurationId,
                        principalTable: "UsersMainWOViewConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PeopleCalendars",
                columns: table => new
                {
                    PeopleId = table.Column<int>(nullable: false),
                    CalendarId = table.Column<int>(nullable: false),
                    CalendarPriority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleCalendars", x => new { x.PeopleId, x.CalendarId });
                    table.ForeignKey(
                        name: "FK__PeopleCal__Calen__405A880E",
                        column: x => x.CalendarId,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__PeopleCal__Peopl__414EAC47",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PeopleCollectionsAdmins",
                columns: table => new
                {
                    PeopleCollectionId = table.Column<int>(nullable: false),
                    PeopleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleCollectionsAdmins", x => new { x.PeopleCollectionId, x.PeopleId });
                    table.ForeignKey(
                        name: "FK_PeopleCollectionsAdmins_PeopleCollection",
                        column: x => x.PeopleCollectionId,
                        principalTable: "PeopleCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PeopleCollectionsAdmins_People",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PeopleCollectionsPeople",
                columns: table => new
                {
                    PeopleCollectionId = table.Column<int>(nullable: false),
                    PeopleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleCollectionsPeople", x => new { x.PeopleId, x.PeopleCollectionId });
                    table.ForeignKey(
                        name: "FK_PeopleCollectionsPeople_PeopleCollections",
                        column: x => x.PeopleCollectionId,
                        principalTable: "PeopleCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PeopleCollectionsPeople_People",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PeopleCost",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    PeopleId = table.Column<int>(nullable: false),
                    HourCost = table.Column<decimal>(nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleCost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeopleCost_People",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PeopleCostHistorical",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    PeopleId = table.Column<int>(nullable: true),
                    CostKm = table.Column<double>(nullable: false),
                    Until = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleCostHistorical", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderCategories_People",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PeoplePermissions",
                columns: table => new
                {
                    PeopleId = table.Column<int>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false),
                    AssignmentDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeoplePermissions", x => new { x.PeopleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_PeoplePermissions_People_PeopleId",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeoplePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PeopleRegisteredPda",
                columns: table => new
                {
                    PeopleId = table.Column<int>(nullable: false),
                    DeviceId = table.Column<string>(unicode: false, maxLength: 1000, nullable: false),
                    DeviceName = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    GcmId = table.Column<string>(unicode: false, maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleRegisteredPda", x => new { x.PeopleId, x.DeviceId });
                    table.ForeignKey(
                        name: "FK_PeopleRegisteredPda_People",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanificationCriterias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PeopleId = table.Column<int>(nullable: false),
                    AllowTollRoads = table.Column<bool>(nullable: false),
                    TollCostFactor = table.Column<decimal>(nullable: false),
                    TechnicianCostFactor = table.Column<decimal>(nullable: false),
                    ExternalTechnicianCostFactor = table.Column<decimal>(nullable: false),
                    UnmetSkillOverhead = table.Column<decimal>(nullable: false),
                    ReplanPenalty = table.Column<decimal>(nullable: false),
                    ReplanPenaltyFixed = table.Column<decimal>(nullable: false),
                    UnattendedWOPenalty = table.Column<decimal>(nullable: false),
                    MaxExecutionTime = table.Column<int>(nullable: false),
                    OverMeanWorkLoadCost = table.Column<decimal>(nullable: false),
                    EconomicCostWeight = table.Column<decimal>(nullable: false),
                    FullfillSLAWeight = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanificationCriterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PLANIFICATIONCRITERIAS_PERSONES",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanningPanelViewConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    PeopleOwnerId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    IsDefault = table.Column<bool>(nullable: true),
                    UsersMainWoViewConfigurationId = table.Column<int>(nullable: true),
                    StartViewTime = table.Column<int>(nullable: true),
                    EndViewTime = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanningPanelViewConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanningPanelViewConfiguration_People",
                        column: x => x.PeopleOwnerId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanningPanelViewConfiguration_UsersMainWoViewConfiguration",
                        column: x => x.UsersMainWoViewConfigurationId,
                        principalTable: "UsersMainWOViewConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PushNotificationsPeople",
                columns: table => new
                {
                    NotificationId = table.Column<int>(nullable: false),
                    PeopleId = table.Column<int>(nullable: false),
                    NotificationToGroup = table.Column<bool>(nullable: true),
                    Status = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushNotificationsPeople", x => new { x.NotificationId, x.PeopleId });
                    table.ForeignKey(
                        name: "FK_PushNotificationsPeople_PushNotification",
                        column: x => x.NotificationId,
                        principalTable: "PushNotifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PushNotificationsPeople_People",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TechnicianListFilters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    PeopleId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Active = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicianListFilters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TECHNICIANLISTFILTERS_PERSONES",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    Height = table.Column<double>(nullable: true),
                    Direction = table.Column<double>(nullable: true),
                    Speed = table.Column<double>(nullable: true),
                    Observations = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    PeopleDriverId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_People",
                        column: x => x.PeopleDriverId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalendarPlanningViewConfigurationPeople",
                columns: table => new
                {
                    ViewId = table.Column<int>(nullable: false),
                    PeopleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarPlanningViewConfigurationPeople", x => new { x.ViewId, x.PeopleId });
                    table.ForeignKey(
                        name: "FK__CalendarP__Peopl__5F141958",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__CalendarP__ViewI__60083D91",
                        column: x => x.ViewId,
                        principalTable: "CalendarPlanningViewConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalendarPlanningViewConfigurationPeopleCollection",
                columns: table => new
                {
                    ViewId = table.Column<int>(nullable: false),
                    PeopleCollectionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarPlanningViewConfigurationPeopleCollection", x => new { x.ViewId, x.PeopleCollectionId });
                    table.ForeignKey(
                        name: "FK__CalendarP__Peopl__60FC61CA",
                        column: x => x.PeopleCollectionId,
                        principalTable: "PeopleCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__CalendarP__ViewI__61F08603",
                        column: x => x.ViewId,
                        principalTable: "CalendarPlanningViewConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractContacts",
                columns: table => new
                {
                    ContractId = table.Column<int>(nullable: false),
                    ContactId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractContacts", x => new { x.ContractId, x.ContactId });
                    table.ForeignKey(
                        name: "FK_ContractContacts_Contacts",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractContacts_Contracts",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    Serie = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Counter = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CollectionsClosureCodesId = table.Column<int>(nullable: false),
                    WorkOrderStatusesId = table.Column<int>(nullable: true),
                    Status = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Observations = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    IdICG = table.Column<int>(nullable: true),
                    CollectionsTypesWorkOrdersId = table.Column<int>(nullable: false),
                    CollectionsExtraFieldId = table.Column<int>(nullable: true),
                    ContractId = table.Column<int>(nullable: true),
                    WorkOrderCategoriesCollectionId = table.Column<int>(nullable: false),
                    VisibilityPda = table.Column<int>(nullable: true),
                    DefaultTechnicalCode = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    BackOfficeResponsible = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    TechnicalResponsible = table.Column<string>(unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_CollectionsClosureCodes",
                        column: x => x.CollectionsClosureCodesId,
                        principalTable: "CollectionsClosureCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_CollectionsExtraField",
                        column: x => x.CollectionsExtraFieldId,
                        principalTable: "CollectionsExtraField",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_CollectionsTypesWorkOrders",
                        column: x => x.CollectionsTypesWorkOrdersId,
                        principalTable: "CollectionsTypesWorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Contract",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_WorkOrderCategoriesCollections",
                        column: x => x.WorkOrderCategoriesCollectionId,
                        principalTable: "WorkOrderCategoriesCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_WorkOrderStatuses",
                        column: x => x.WorkOrderStatusesId,
                        principalTable: "WorkOrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactsFinalClients",
                columns: table => new
                {
                    FinalClientId = table.Column<int>(nullable: false),
                    ContactId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactsFinalClients", x => new { x.FinalClientId, x.ContactId });
                    table.ForeignKey(
                        name: "FK_ContactsFinalClients_Contacts",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactsFinalClients_FinalClients",
                        column: x => x.FinalClientId,
                        principalTable: "FinalClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinalClientSiteCalendar",
                columns: table => new
                {
                    FinalClientSiteId = table.Column<int>(nullable: false),
                    CalendarId = table.Column<int>(nullable: false),
                    CalendarPriority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalClientSiteCalendar", x => new { x.FinalClientSiteId, x.CalendarId });
                    table.ForeignKey(
                        name: "FK__FinalClie__Calen__184C96B4",
                        column: x => x.CalendarId,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__FinalClie__Final__1940BAED",
                        column: x => x.FinalClientSiteId,
                        principalTable: "FinalClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JourneysStates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    PredefinedDayStatesId = table.Column<int>(nullable: false),
                    JourneyId = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(type: "datetime", nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    PredefinedReasonsId = table.Column<int>(nullable: true),
                    Observations = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JourneysStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JourneysStates_Journeys",
                        column: x => x.JourneyId,
                        principalTable: "Journeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactsLocationsFinalClients",
                columns: table => new
                {
                    LocationId = table.Column<int>(nullable: false),
                    ContactId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactsLocationsFinalClients", x => new { x.LocationId, x.ContactId });
                    table.ForeignKey(
                        name: "FK_ContactsLocationsFinalClients_Contacts",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactsLocationsFinalClients_Locations",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationCalendar",
                columns: table => new
                {
                    LocationId = table.Column<int>(nullable: false),
                    CalendarId = table.Column<int>(nullable: false),
                    CalendarPriority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationCalendar", x => new { x.LocationId, x.CalendarId });
                    table.ForeignKey(
                        name: "FK__LocationC__Calen__2D47B39A",
                        column: x => x.CalendarId,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__LocationC__Locat__2E3BD7D3",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationsFinalClients",
                columns: table => new
                {
                    FinalClientId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    Phone1 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Phone2 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Phone3 = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Fax = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    PeopleCommercialId = table.Column<int>(nullable: true),
                    OriginId = table.Column<int>(nullable: false),
                    CompositeCode = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationsFinalClients", x => new { x.FinalClientId, x.LocationId });
                    table.ForeignKey(
                        name: "FK_LocationsFinalClients_FinalClients",
                        column: x => x.FinalClientId,
                        principalTable: "FinalClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocationsFinalClients_Locations",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocationsFinalClients_People",
                        column: x => x.PeopleCommercialId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SiteUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    FirstSurname = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SecondSurname = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Telephone = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Position = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK__SiteUser__Locati__19FFD4FC",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanningPanelViewConfigurationPeople",
                columns: table => new
                {
                    PlanningPanelViewConfigurationId = table.Column<int>(nullable: false),
                    PeopleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanningPanelViewConfigurationPeople", x => new { x.PlanningPanelViewConfigurationId, x.PeopleId });
                    table.ForeignKey(
                        name: "FK_PlanningPanelViewConfiguration_People_People",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlanningPanelViewConfiguration_People_PlanningPanelViewConfiguration",
                        column: x => x.PlanningPanelViewConfigurationId,
                        principalTable: "PlanningPanelViewConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlanningPanelViewConfigurationPeopleCollection",
                columns: table => new
                {
                    PlanningPanelViewConfigurationId = table.Column<int>(nullable: false),
                    PeopleCollectionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanningPanelViewConfigurationPeopleCollection", x => new { x.PlanningPanelViewConfigurationId, x.PeopleCollectionId });
                    table.ForeignKey(
                        name: "FK__PlanningP__Peopl__573DED66",
                        column: x => x.PeopleCollectionId,
                        principalTable: "PeopleCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__PlanningP__Plann__5832119F",
                        column: x => x.PlanningPanelViewConfigurationId,
                        principalTable: "PlanningPanelViewConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdvancedTechnicianListFilters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvancedTechnicianListFilters", x => x.Id);
                    table.ForeignKey(
                        name: "FK__AdvancedTech__Id__3F9B6DFF",
                        column: x => x.Id,
                        principalTable: "TechnicianListFilters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasicTechnicianListFilters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    WorkLoad = table.Column<decimal>(nullable: true),
                    MaxDistance = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicTechnicianListFilters", x => x.Id);
                    table.ForeignKey(
                        name: "FK__BasicTechnic__Id__4830B400",
                        column: x => x.Id,
                        principalTable: "TechnicianListFilters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tools",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Observations = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    VehicleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tools_Vehicles",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PeopleProjects",
                columns: table => new
                {
                    PeopleId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    IsManager = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleProjects", x => new { x.PeopleId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_PeopleProjects_People",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PeopleProjects_Projects",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PredefinedServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    LinkClosingCode = table.Column<bool>(nullable: false),
                    CollectionExtraFieldId = table.Column<int>(nullable: true),
                    Billable = table.Column<bool>(nullable: true),
                    MustValidate = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredefinedServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PredefinedServices_CollectionsExtraField",
                        column: x => x.CollectionExtraFieldId,
                        principalTable: "CollectionsExtraField",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PredefinedServices_Projects",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectsCalendars",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false),
                    CalendarId = table.Column<int>(nullable: false),
                    CalendarPriority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectsCalendars", x => new { x.ProjectId, x.CalendarId });
                    table.ForeignKey(
                        name: "FK_ProjectsCalendars_Calendars",
                        column: x => x.CalendarId,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectsCalendars_Projects",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectsContacts",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false),
                    ContactId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectsContacts", x => new { x.ProjectId, x.ContactId });
                    table.ForeignKey(
                        name: "FK_ProjectsContacts_Contact",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectsContacts_Project",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectsPermissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectsPermissions", x => new { x.PermissionId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_ProjectsPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectsPermissions_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalCodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ProjectId = table.Column<int>(nullable: true),
                    PeopleTechnicId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    WorkOrderCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicalCodes_People",
                        column: x => x.PeopleTechnicId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TechnicalCodes_Projects",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Technical__WorkO__33BFA6FF",
                        column: x => x.WorkOrderCategoryId,
                        principalTable: "WorkOrderCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZoneProject",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ZoneId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneProject", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ZoneProje__Proje__6C040022",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ZoneProje__ZoneI__6CF8245B",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SerialNumber = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    StockNumber = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    TeamNumber = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Observations = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    LocationClientId = table.Column<int>(nullable: false),
                    GuaranteeId = table.Column<int>(nullable: true),
                    LocationId = table.Column<int>(nullable: true),
                    SubFamilyId = table.Column<int>(nullable: true),
                    ModelId = table.Column<int>(nullable: true),
                    AssetStatusId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: true),
                    UsageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Status_AssetStatuses",
                        column: x => x.AssetStatusId,
                        principalTable: "AssetStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Guarantee",
                        column: x => x.GuaranteeId,
                        principalTable: "Guarantee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_LocationsClient",
                        column: x => x.LocationClientId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Locations",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Model",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Subfamilies",
                        column: x => x.SubFamilyId,
                        principalTable: "SubFamilies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Teams__UsageId__284DF453",
                        column: x => x.UsageId,
                        principalTable: "Usages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Teams__UserId__2942188C",
                        column: x => x.UserId,
                        principalTable: "SiteUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdvancedTechnicianListFilterPersons",
                columns: table => new
                {
                    TechnicianListFilterId = table.Column<int>(nullable: false),
                    PeopleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvancedTechnicianListFilterPersons", x => new { x.TechnicianListFilterId, x.PeopleId });
                    table.ForeignKey(
                        name: "FK_ADVANCEDTECHNICIANLISTFILTERSPERSONS_PERSONS",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ADVANCEDTECHNICIANLISTFILTERSPERSONS_ADVANCEDTECHNICIANLISTFILTER",
                        column: x => x.TechnicianListFilterId,
                        principalTable: "AdvancedTechnicianListFilters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasicTechnicianListFilterCalendarPlanningViewConfiguration",
                columns: table => new
                {
                    TechnicianListFilterId = table.Column<int>(nullable: false),
                    CalendarPlanningViewConfigurationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicTechnicianListFilterCalendarPlanningViewConfiguration", x => new { x.TechnicianListFilterId, x.CalendarPlanningViewConfigurationId });
                    table.ForeignKey(
                        name: "FK_BasicTechniciansListCalendarPlanningViewConfiguration_CalendarPlanningViewConfiguration",
                        column: x => x.CalendarPlanningViewConfigurationId,
                        principalTable: "CalendarPlanningViewConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalendarPlanningViewConfiguration_BasicTechnicianListFilters",
                        column: x => x.TechnicianListFilterId,
                        principalTable: "BasicTechnicianListFilters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasicTechnicianListFilterSkills",
                columns: table => new
                {
                    TechnicianListFilterId = table.Column<int>(nullable: false),
                    SkillId = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicTechnicianListFilterSkills", x => new { x.TechnicianListFilterId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_BASICTECHNICIANLISTFILTERSSKILLS_SKILLS",
                        column: x => x.SkillId,
                        principalTable: "Knowledge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BASICTECHNICIANLISTFILTERSSKILLS_BASICTECHNICIANLISTFILTERS",
                        column: x => x.TechnicianListFilterId,
                        principalTable: "BasicTechnicianListFilters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ToolsToolTypes",
                columns: table => new
                {
                    ToolId = table.Column<int>(nullable: false),
                    ToolTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolsToolTypes", x => new { x.ToolId, x.ToolTypeId });
                    table.ForeignKey(
                        name: "FK_ToolsToolTypes_Tools",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ToolsToolTypes_ToolsType",
                        column: x => x.ToolTypeId,
                        principalTable: "ToolsType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PredefinedServicesPermission",
                columns: table => new
                {
                    PredefinedServiceId = table.Column<int>(nullable: false),
                    PermissionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredefinedServicesPermission", x => new { x.PermissionId, x.PredefinedServiceId });
                    table.ForeignKey(
                        name: "FK_PredefinedServicesPermission_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PredefinedServicesPermission_PredefinedServices_PredefinedServiceId",
                        column: x => x.PredefinedServiceId,
                        principalTable: "PredefinedServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    PredefinedServiceId = table.Column<int>(nullable: false),
                    IdentifyInternal = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    IdentifyExternal = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Description = table.Column<string>(unicode: false, maxLength: 8000, nullable: true),
                    Observations = table.Column<string>(unicode: false, maxLength: 8000, nullable: true),
                    PeopleResponsibleId = table.Column<int>(nullable: true),
                    SubcontractResponsibleId = table.Column<int>(nullable: true),
                    WorkOrderId = table.Column<int>(nullable: false),
                    ServiceStateId = table.Column<int>(nullable: true),
                    ClosingCodeFirstId = table.Column<int>(nullable: true),
                    ClosingCodeSecondId = table.Column<int>(nullable: true),
                    ClosingCodeThirdId = table.Column<int>(nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    IcgId = table.Column<int>(nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    FormState = table.Column<string>(unicode: false, maxLength: 100, nullable: false, defaultValueSql: "('Billed')"),
                    DeliveryNote = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    DeliveryProcessInit = table.Column<DateTime>(type: "datetime", nullable: true),
                    Cancelled = table.Column<bool>(nullable: true),
                    ServicesCancelFormId = table.Column<int>(nullable: true),
                    ClosingCodeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_ClosingCodesFirst",
                        column: x => x.ClosingCodeFirstId,
                        principalTable: "ClosingCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Services__Closin__0D99FE17",
                        column: x => x.ClosingCodeId,
                        principalTable: "ClosingCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_ClosingCodesSecond",
                        column: x => x.ClosingCodeSecondId,
                        principalTable: "ClosingCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_ClosingCodesThird",
                        column: x => x.ClosingCodeThirdId,
                        principalTable: "ClosingCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_People",
                        column: x => x.PeopleResponsibleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_PredefinedServices",
                        column: x => x.PredefinedServiceId,
                        principalTable: "PredefinedServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Services__Servic__0E8E2250",
                        column: x => x.ServicesCancelFormId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Services_Subcontracts",
                        column: x => x.SubcontractResponsibleId,
                        principalTable: "SubContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    FlowId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    NameFieldModel = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    QueueId = table.Column<int>(nullable: true),
                    WorkOrderStatusId = table.Column<int>(nullable: true),
                    ExternalWorOrderStatusId = table.Column<int>(nullable: true),
                    PeopleTechnicianId = table.Column<int>(nullable: true),
                    PeopleManipulatorId = table.Column<int>(nullable: true),
                    EnterValue = table.Column<int>(nullable: true),
                    DecimalValue = table.Column<double>(nullable: true),
                    StringValue = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    BooleanValue = table.Column<bool>(nullable: true),
                    DateValue = table.Column<DateTime>(type: "datetime", nullable: true),
                    PredefinedServiceId = table.Column<int>(nullable: true),
                    ExtraFieldId = table.Column<int>(nullable: true),
                    MailSubjectToPrepend = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    MailSubscribers = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    AllowAdditionalSubscribers = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    PeopleResponsibleTechniciansId = table.Column<int>(nullable: true),
                    ExternalCall = table.Column<string>(unicode: false, maxLength: 500, nullable: false, defaultValueSql: "('Empty')"),
                    WebServiceCallId = table.Column<int>(nullable: true),
                    MailTemplateId = table.Column<int>(nullable: true),
                    SendMailToTechnician = table.Column<bool>(nullable: false),
                    SendMailToProjectResponsible = table.Column<bool>(nullable: false),
                    SendMailToSiteUser = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_ExternalWorOrderStatuses",
                        column: x => x.ExternalWorOrderStatusId,
                        principalTable: "ExternalWorOrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_ExtraFields",
                        column: x => x.ExtraFieldId,
                        principalTable: "ExtraFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_Flows",
                        column: x => x.FlowId,
                        principalTable: "Flows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Tasks__MailTempl__1DD065E0",
                        column: x => x.MailTemplateId,
                        principalTable: "MailTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_PeopleManipulator",
                        column: x => x.PeopleManipulatorId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_PeopleResponsibleTechnicians",
                        column: x => x.PeopleResponsibleTechniciansId,
                        principalTable: "PeopleCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_PeopleTechnician",
                        column: x => x.PeopleTechnicianId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_PredefinedServices",
                        column: x => x.PredefinedServiceId,
                        principalTable: "PredefinedServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_Queues",
                        column: x => x.QueueId,
                        principalTable: "Queues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_WorkOrderStatuses",
                        column: x => x.WorkOrderStatusId,
                        principalTable: "WorkOrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ZoneProjectPostalCode",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ZoneProjectId = table.Column<int>(nullable: false),
                    PostalCode = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneProjectPostalCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ZoneProje__ZoneP__6DEC4894",
                        column: x => x.ZoneProjectId,
                        principalTable: "ZoneProject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssetsAudit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    TeamId = table.Column<int>(nullable: true),
                    RegistryDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserName = table.Column<string>(unicode: false, maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetsAudit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetsAudit_Teams",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamsHiredServices",
                columns: table => new
                {
                    TeamId = table.Column<int>(nullable: false),
                    HiredServiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamsHiredServices", x => new { x.TeamId, x.HiredServiceId });
                    table.ForeignKey(
                        name: "FK_TeamsHiredServices_HiredServices",
                        column: x => x.HiredServiceId,
                        principalTable: "HiredServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamsHiredServices_Teams",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    InternalIdentifier = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ExternalIdentifier = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TextRepair = table.Column<string>(unicode: false, maxLength: 8000, nullable: false),
                    Observations = table.Column<string>(unicode: false, maxLength: 8000, nullable: true),
                    PickUpTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    FinalClientClosingTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    InternalClosingTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    AssignmentTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PeopleResponsibleId = table.Column<int>(nullable: true),
                    OriginId = table.Column<int>(nullable: false),
                    PeopleIntroducedById = table.Column<int>(nullable: false),
                    WorkOrderTypesId = table.Column<int>(nullable: true),
                    LocationId = table.Column<int>(nullable: false),
                    FinalClientId = table.Column<int>(nullable: false),
                    WorkOrderStatusId = table.Column<int>(nullable: false),
                    IcgId = table.Column<int>(nullable: true),
                    TeamId = table.Column<int>(nullable: true),
                    PeopleManipulatorId = table.Column<int>(nullable: true),
                    QueueId = table.Column<int>(nullable: false),
                    ExternalWorOrderStatusId = table.Column<int>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false),
                    WorkOrdersFatherId = table.Column<int>(nullable: true),
                    DateStopTimerSla = table.Column<DateTime>(type: "datetime", nullable: true),
                    ResponseDateSla = table.Column<DateTime>(type: "datetime", nullable: true),
                    ResolutionDateSla = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateUnansweredPenaltySla = table.Column<DateTime>(type: "datetime", nullable: true),
                    DatePenaltyWithoutResolutionSla = table.Column<DateTime>(type: "datetime", nullable: true),
                    ServiceId = table.Column<int>(nullable: true),
                    ReferenceGeneratorService = table.Column<bool>(nullable: false),
                    ReferenceOtherServices = table.Column<bool>(nullable: false),
                    WorkOrderCategoryId = table.Column<int>(nullable: false),
                    Archived = table.Column<bool>(nullable: false),
                    ActuationEndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    IsResponsibleFixed = table.Column<bool>(nullable: false),
                    IsActuationDateFixed = table.Column<bool>(nullable: false),
                    SiteUserId = table.Column<int>(nullable: true),
                    Billable = table.Column<bool>(nullable: false),
                    ExternalSystemId = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    ClosingOTDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    AccountingClosingDate = table.Column<DateTime>(type: "date", nullable: true),
                    ClientClosingDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SystemDateWhenOTClosed = table.Column<DateTime>(type: "datetime", nullable: true),
                    InternalCreationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    MeetSLAResponse = table.Column<bool>(nullable: true),
                    MeetSLAResolution = table.Column<bool>(nullable: true),
                    Overhead = table.Column<int>(nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrders_ExternalWorOrderStatuses",
                        column: x => x.ExternalWorOrderStatusId,
                        principalTable: "ExternalWorOrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrders_FinalClients",
                        column: x => x.FinalClientId,
                        principalTable: "FinalClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Locations",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrders_PeopleIntroducedBy",
                        column: x => x.PeopleIntroducedById,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrders_PeopleManipulator",
                        column: x => x.PeopleManipulatorId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrders_PeopleResponsible",
                        column: x => x.PeopleResponsibleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdreTreball_Projects",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Queues",
                        column: x => x.QueueId,
                        principalTable: "Queues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdreTreball_Services",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__WorkOrder__SiteU__4B973090",
                        column: x => x.SiteUserId,
                        principalTable: "SiteUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Teams",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrders_WorkOrderCategories",
                        column: x => x.WorkOrderCategoryId,
                        principalTable: "WorkOrderCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrders_WorkOrderStatuses",
                        column: x => x.WorkOrderStatusId,
                        principalTable: "WorkOrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrders_WorkOrderTypes",
                        column: x => x.WorkOrderTypesId,
                        principalTable: "WorkOrderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrders_WorkOrdersFather",
                        column: x => x.WorkOrdersFatherId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillingLineItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    WorkOrderCategoryId = table.Column<int>(nullable: false),
                    TaskId = table.Column<int>(nullable: true),
                    PredefinedServiceId = table.Column<int>(nullable: true),
                    Type = table.Column<string>(unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingLineItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillingLineItems_PredefinedServices",
                        column: x => x.PredefinedServiceId,
                        principalTable: "PredefinedServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillingLineItems_Projects",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillingLineItems_Tasks",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillingLineItems_WorkOrderCategories",
                        column: x => x.WorkOrderCategoryId,
                        principalTable: "WorkOrderCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillingRule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    TaskId = table.Column<int>(nullable: false),
                    Condition = table.Column<string>(unicode: false, nullable: false),
                    ErpSystemInstanceId = table.Column<int>(nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillingRule_ErpSystemInstance",
                        column: x => x.ErpSystemInstanceId,
                        principalTable: "ErpSystemInstance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillingRule_Tasks",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DerivedServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    TaskId = table.Column<int>(nullable: false),
                    PredefinedServicesId = table.Column<int>(nullable: false),
                    InternalIdentifier = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ExternalIdentifier = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Description = table.Column<string>(unicode: false, maxLength: 8000, nullable: true),
                    Observations = table.Column<string>(unicode: false, maxLength: 8000, nullable: true),
                    PeopleResponsibleId = table.Column<int>(nullable: true),
                    SubcontractResponsibleId = table.Column<int>(nullable: true),
                    ServiceStatesId = table.Column<int>(nullable: true),
                    ClosingCodesIdN1 = table.Column<int>(nullable: true),
                    ClosingCodesIdN2 = table.Column<int>(nullable: true),
                    ClosingCodesIdN3 = table.Column<int>(nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    IcgId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DerivedServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DerivedServices_ClosingCodesN1",
                        column: x => x.ClosingCodesIdN1,
                        principalTable: "ClosingCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DerivedServices_ClosingCodesN2",
                        column: x => x.ClosingCodesIdN2,
                        principalTable: "ClosingCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DerivedServices_ClosingCodesN3",
                        column: x => x.ClosingCodesIdN3,
                        principalTable: "ClosingCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DerivedServices_People",
                        column: x => x.PeopleResponsibleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DerivedServices_PredefinedServices",
                        column: x => x.PredefinedServicesId,
                        principalTable: "PredefinedServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DerivedServices_Projects",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DerivedServices_Subcontracts",
                        column: x => x.SubcontractResponsibleId,
                        principalTable: "SubContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DerivedServices_Tasks",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExternalServicesConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ExternalService = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    ProjectId = table.Column<int>(nullable: true),
                    WoCategoryId = table.Column<int>(nullable: true),
                    WoStatusId = table.Column<int>(nullable: true),
                    WoExternalStatusId = table.Column<int>(nullable: true),
                    QueueId = table.Column<int>(nullable: true),
                    FlowId = table.Column<int>(nullable: true),
                    TaskId = table.Column<int>(nullable: true),
                    FinalClientId = table.Column<int>(nullable: true),
                    LocationId = table.Column<int>(nullable: true),
                    AssetWoStatusId = table.Column<int>(nullable: true),
                    AssetWoExternalStatusId = table.Column<int>(nullable: true),
                    AssetQueueId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalServicesConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalServicesConfiguration_AssetQueue",
                        column: x => x.AssetQueueId,
                        principalTable: "Queues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExternalServicesConfiguration_AssetWoExternalStatus",
                        column: x => x.AssetWoExternalStatusId,
                        principalTable: "ExternalWorOrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExternalServicesConfiguration_AssetWoStatus",
                        column: x => x.AssetWoStatusId,
                        principalTable: "WorkOrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExternalServicesConfiguration_FinalClient",
                        column: x => x.FinalClientId,
                        principalTable: "FinalClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExternalServicesConfiguration_Flow",
                        column: x => x.FlowId,
                        principalTable: "Flows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExternalServicesConfiguration_Location",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExternalServicesConfiguration_Project",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExternalServicesConfiguration_Queue",
                        column: x => x.QueueId,
                        principalTable: "Queues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExternalServicesConfiguration_Task",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExternalServicesConfiguration_WoCategory",
                        column: x => x.WoCategoryId,
                        principalTable: "WorkOrderCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExternalServicesConfiguration_WoExternalStatus",
                        column: x => x.WoExternalStatusId,
                        principalTable: "ExternalWorOrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExternalServicesConfiguration_WoStatus",
                        column: x => x.WoStatusId,
                        principalTable: "WorkOrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PermissionsTasks",
                columns: table => new
                {
                    PermissionId = table.Column<int>(nullable: false),
                    TaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionsTasks", x => new { x.PermissionId, x.TaskId });
                    table.ForeignKey(
                        name: "FK_PermissionsTasks_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionsTasks_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostconditionCollections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    TaskId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostconditionCollections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostconditionCollections_Tasks",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskWebServiceCallItems",
                columns: table => new
                {
                    TaskId = table.Column<int>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskWebServiceCallItems", x => new { x.TaskId, x.ItemId });
                    table.ForeignKey(
                        name: "FK__TaskWebSe__TaskI__2759D01A",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrdersDeritative",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    TaskId = table.Column<int>(nullable: false),
                    InternalIdentifier = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ExternalIdentifier = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    TextRepair = table.Column<string>(unicode: false, maxLength: 8000, nullable: true),
                    Observations = table.Column<string>(unicode: false, maxLength: 8000, nullable: true),
                    PickUpTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    FinalClientClosingTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    InternalClosingTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    AssignmentTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PeopleResponsibleId = table.Column<int>(nullable: true),
                    OriginId = table.Column<int>(nullable: true),
                    PeopleIntroducedById = table.Column<int>(nullable: true),
                    WorkOrderTypeId = table.Column<int>(nullable: true),
                    LocationId = table.Column<int>(nullable: true),
                    ProjectId = table.Column<int>(nullable: true),
                    FinalClientId = table.Column<int>(nullable: true),
                    WorkOrderStatusId = table.Column<int>(nullable: true),
                    ExternalWorOrderStatusId = table.Column<int>(nullable: true),
                    IcgId = table.Column<int>(nullable: true),
                    TeamsId = table.Column<int>(nullable: true),
                    PeopleManipulatorId = table.Column<int>(nullable: true),
                    QueueId = table.Column<int>(nullable: true),
                    InheritProject = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    InheritTechnician = table.Column<bool>(nullable: true, defaultValueSql: "((0))"),
                    GeneratorServiceDuplicationPolicy = table.Column<int>(nullable: false),
                    OtherServicesDuplicationPolicy = table.Column<int>(nullable: false),
                    WorkOrderCategoryId = table.Column<int>(nullable: true),
                    PeopleResponsibleTechniciansCollectionId = table.Column<int>(nullable: true),
                    SiteUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrdersDeritative", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrdersDeritative_ExternalWorOrderStatuses",
                        column: x => x.ExternalWorOrderStatusId,
                        principalTable: "ExternalWorOrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrdersDeritative_FinalClients",
                        column: x => x.FinalClientId,
                        principalTable: "FinalClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrdersDeritative_Locations",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrdersDeritative_PeopleIntroducedBy",
                        column: x => x.PeopleIntroducedById,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrdersDeritative_PeopleManipulator",
                        column: x => x.PeopleManipulatorId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrdersDeritative_PeopleResponsible",
                        column: x => x.PeopleResponsibleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrdersDeritative_PeopleResponsibleTechniciansCollection",
                        column: x => x.PeopleResponsibleTechniciansCollectionId,
                        principalTable: "PeopleCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdreTreballDerivades_Projects",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrdersDeritative_Queues",
                        column: x => x.QueueId,
                        principalTable: "Queues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__WorkOrder__SiteU__59E54FE7",
                        column: x => x.SiteUserId,
                        principalTable: "SiteUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrdersDeritative_Tasks",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrdersDeritative_Teams",
                        column: x => x.TeamsId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrdersDeritative_WorkOrderCategories",
                        column: x => x.WorkOrderCategoryId,
                        principalTable: "WorkOrderCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrdersDeritative_WorkOrderStatuses",
                        column: x => x.WorkOrderStatusId,
                        principalTable: "WorkOrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrdersDeritative_WorkOrderTypes",
                        column: x => x.WorkOrderTypeId,
                        principalTable: "WorkOrderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssetsAuditChanges",
                columns: table => new
                {
                    AssetAuditId = table.Column<int>(nullable: false),
                    Property = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    OldValue = table.Column<string>(unicode: false, maxLength: 5000, nullable: true),
                    NewValue = table.Column<string>(unicode: false, maxLength: 5000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetsAuditChanges", x => new { x.AssetAuditId, x.Property });
                    table.ForeignKey(
                        name: "FK_AssetsAuditChanges_AssetsAudit",
                        column: x => x.AssetAuditId,
                        principalTable: "AssetsAudit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    WorkOrderId = table.Column<int>(nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserConfigurationId = table.Column<int>(nullable: false),
                    Origin = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    Height = table.Column<double>(nullable: true),
                    Observations = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    UserConfigurationSupplanterId = table.Column<int>(nullable: true),
                    TaskId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Audits_Tasks",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Audits_UserConfigurations_UserConfigurationId",
                        column: x => x.UserConfigurationId,
                        principalTable: "UserConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Audits_UserConfigurations_UserConfigurationSupplanterId",
                        column: x => x.UserConfigurationSupplanterId,
                        principalTable: "UserConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Auditoria_WorkOrders",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    WorkorderId = table.Column<int>(nullable: false),
                    ServiceId = table.Column<int>(nullable: true),
                    Task = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    PeopleId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    ExternalSystemNumber = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    TaskId = table.Column<int>(nullable: true),
                    ErpSystemInstanceId = table.Column<int>(nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bill_ErpSystemInstance",
                        column: x => x.ErpSystemInstanceId,
                        principalTable: "ErpSystemInstance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bill_People",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bill_Services",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bill_Task",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bill_WorkOrders",
                        column: x => x.WorkorderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpensesTickets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    PeopleId = table.Column<int>(nullable: false),
                    WorkOrderId = table.Column<int>(nullable: true),
                    Status = table.Column<string>(unicode: false, maxLength: 100, nullable: false, defaultValueSql: "('Pending')"),
                    UniqueId = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    PeopleValidatorId = table.Column<int>(nullable: true),
                    ValidationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ValidationObservations = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    PaymentInformation = table.Column<string>(unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpensesTickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ExpensesT__Peopl__7D98A078",
                        column: x => x.PeopleId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ExpensesT__Peopl__7E8CC4B1",
                        column: x => x.PeopleValidatorId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ExpensesT__WorkO__7F80E8EA",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExternalSystemImportData",
                columns: table => new
                {
                    ImportCode = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    ExternalSystem = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Property = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Value = table.Column<string>(unicode: false, maxLength: 5000, nullable: true),
                    WorkOrderId = table.Column<int>(nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalSystemImportData", x => new { x.ImportCode, x.Property });
                    table.ForeignKey(
                        name: "FK_ExternalSystemImportData_WorkOrder",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServicesAnalysis",
                columns: table => new
                {
                    ServiceCode = table.Column<int>(nullable: false),
                    WorkOrderCode = table.Column<int>(nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Technician = table.Column<int>(nullable: false),
                    DeliveryNote = table.Column<int>(nullable: true),
                    Observacions = table.Column<string>(unicode: false, maxLength: 8000, nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndingTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    OnSiteTime = table.Column<int>(nullable: true),
                    TravelTime = table.Column<int>(nullable: true),
                    WaitTime = table.Column<int>(nullable: true),
                    Kilometers = table.Column<decimal>(nullable: true),
                    ServiceDescription = table.Column<string>(unicode: false, maxLength: 8000, nullable: true),
                    SubcontractorCode = table.Column<int>(nullable: true),
                    SubcontractorName = table.Column<string>(maxLength: 50, nullable: true),
                    StandardPersonCost = table.Column<decimal>(nullable: true),
                    KmCost = table.Column<decimal>(nullable: true),
                    TravelTimeCost = table.Column<decimal>(nullable: true),
                    ProductionCost = table.Column<decimal>(nullable: true),
                    SubcontractorCost = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    ClosingCodeName1 = table.Column<string>(maxLength: 50, nullable: true),
                    ClosingCodeDesc1 = table.Column<string>(maxLength: 50, nullable: true),
                    ClosingCodeName2 = table.Column<string>(maxLength: 50, nullable: true),
                    ClosingCodeDesc2 = table.Column<string>(maxLength: 50, nullable: true),
                    ClosingCodeName3 = table.Column<string>(maxLength: 50, nullable: true),
                    ClosingCodeDesc3 = table.Column<string>(maxLength: 50, nullable: true),
                    ClosingCodeName4 = table.Column<string>(maxLength: 50, nullable: true),
                    ClosingCodeDesc4 = table.Column<string>(maxLength: 50, nullable: true),
                    ClosingCodeName5 = table.Column<string>(maxLength: 50, nullable: true),
                    ClosingCodeDesc5 = table.Column<string>(maxLength: 50, nullable: true),
                    ClosingCodeName6 = table.Column<string>(maxLength: 50, nullable: true),
                    ClosingCodeDesc6 = table.Column<string>(maxLength: 50, nullable: true),
                    WorkedTime = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesAnalysis", x => x.ServiceCode)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_SERVICEANALYSIS_ORDRES",
                        column: x => x.WorkOrderCode,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SgsClosingInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    WorkOrderId = table.Column<int>(nullable: false),
                    ParametersSent = table.Column<string>(unicode: false, maxLength: 5000, nullable: false),
                    Response = table.Column<string>(unicode: false, maxLength: 5000, nullable: true),
                    StatusResponse = table.Column<int>(nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SgsClosingInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK__SgsClosin__WorkO__190BB0C3",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamsWorkOrders",
                columns: table => new
                {
                    WorkOrderId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamsWorkOrders", x => new { x.WorkOrderId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_TeamsWorkOrders_Teams",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamsWorkOrders_WorkOrders",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderAnalysis",
                columns: table => new
                {
                    WorkOrderCode = table.Column<int>(nullable: false),
                    WorkOrderClientCode = table.Column<string>(maxLength: 50, nullable: true),
                    WorkOrderCampainCode = table.Column<string>(maxLength: 50, nullable: true),
                    InternalAssetCode = table.Column<int>(nullable: true),
                    ProjectCode = table.Column<int>(nullable: false),
                    AssignedTechnicianCode = table.Column<int>(nullable: false),
                    ClientCreationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    InternalCreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ActuationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClosingClientTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    InternalSystemTimeWhenOTClosed = table.Column<DateTime>(type: "datetime", nullable: true),
                    SLAResponseDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SLAResolutionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    MeetResponseSLA = table.Column<bool>(nullable: true),
                    MeetResolutionSLA = table.Column<bool>(nullable: true),
                    ResponseTime = table.Column<int>(nullable: true),
                    ResolutionTime = table.Column<int>(nullable: true),
                    TotalWorkedTime = table.Column<int>(nullable: false),
                    ExpectedTimeWorked = table.Column<int>(nullable: true),
                    ExpectedvsWorkedTime = table.Column<decimal>(nullable: true),
                    OnSiteTime = table.Column<int>(nullable: true),
                    TravelTime = table.Column<int>(nullable: true),
                    WaitTime = table.Column<int>(nullable: true),
                    TotalTime = table.Column<int>(nullable: true),
                    Kilometers = table.Column<decimal>(nullable: true),
                    Tolls = table.Column<decimal>(nullable: true),
                    Parking = table.Column<decimal>(nullable: true),
                    Expenses = table.Column<decimal>(nullable: true),
                    OtherCosts = table.Column<decimal>(nullable: true),
                    WorkOrderCategory = table.Column<int>(nullable: false),
                    NumberOfVisitsToClient = table.Column<int>(nullable: true),
                    NumberOfIntervention = table.Column<int>(nullable: true),
                    OTStatus = table.Column<int>(nullable: false),
                    ExternalOTStatus = table.Column<int>(nullable: true),
                    FinalClientCode = table.Column<int>(nullable: false),
                    FinalClientName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    LocationCode = table.Column<int>(nullable: true),
                    LocationName = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    LocationAddress = table.Column<string>(maxLength: 200, nullable: true),
                    LocationCity = table.Column<string>(maxLength: 100, nullable: true),
                    LocationState = table.Column<string>(maxLength: 100, nullable: true),
                    LocationRegion = table.Column<string>(maxLength: 100, nullable: true),
                    LocationPostalCode = table.Column<string>(maxLength: 50, nullable: true),
                    LocationCountry = table.Column<string>(maxLength: 100, nullable: true),
                    LocationTown = table.Column<string>(maxLength: 100, nullable: true),
                    LocationObservation = table.Column<string>(unicode: false, maxLength: 8000, nullable: true),
                    LocationClientCode = table.Column<string>(maxLength: 100, nullable: true),
                    ClosingClientDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClosingSystemDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClosingWODate = table.Column<DateTime>(type: "datetime", nullable: false),
                    AccountingClosingDate = table.Column<DateTime>(type: "date", nullable: true),
                    TotalWOSalesAmount = table.Column<decimal>(nullable: true),
                    TotalWOProductionCost = table.Column<decimal>(nullable: true),
                    TotalWOTravelTimeCost = table.Column<decimal>(nullable: true),
                    TotalWOSubcontractorCost = table.Column<decimal>(nullable: true),
                    TotalWOMaterialsCost = table.Column<decimal>(nullable: true),
                    TotalWOExpensesCost = table.Column<decimal>(nullable: true),
                    GrossMargin = table.Column<decimal>(nullable: true),
                    SlaResponsePenaltyDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    SlaResolutionPenaltyDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    MeetResponsePenaltySla = table.Column<bool>(nullable: true),
                    MeetResolutionPenaltySla = table.Column<bool>(nullable: true),
                    ResponsePenaltyTime = table.Column<int>(nullable: true),
                    ResolutionPenaltyTime = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderAnalysis", x => x.WorkOrderCode)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_WorkOrderAnalysis_WorkOrders",
                        column: x => x.WorkOrderCode,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillingItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    BillingLineItemId = table.Column<int>(nullable: false),
                    Reference = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    Units = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillingItems_BillingLineItems",
                        column: x => x.BillingLineItemId,
                        principalTable: "BillingLineItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillingRuleItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    BillingRuleId = table.Column<int>(nullable: false),
                    Units = table.Column<string>(unicode: false, nullable: false),
                    ItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingRuleItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillingRuleItem_BillingRule",
                        column: x => x.BillingRuleId,
                        principalTable: "BillingRule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BillingRuleItem_Items",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExternalServicesConfigurationProjectCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ConfigurationId = table.Column<int>(nullable: true),
                    ProjectId = table.Column<int>(nullable: true),
                    WoCategoryId = table.Column<int>(nullable: true),
                    AssetSerialNumberProperty = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    AssetMailAlert = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalServicesConfigurationProjectCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ExternalS__Confi__0BE6BFCF",
                        column: x => x.ConfigurationId,
                        principalTable: "ExternalServicesConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ExternalS__Proje__0CDAE408",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__ExternalS__WoCat__0DCF0841",
                        column: x => x.WoCategoryId,
                        principalTable: "WorkOrderCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExternalServicesConfigurationSites",
                columns: table => new
                {
                    ExternalServicesConfigurationId = table.Column<int>(nullable: false),
                    FinalClientId = table.Column<int>(nullable: false),
                    ExtClientId = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    ExtSiteInitialChar = table.Column<int>(nullable: true),
                    ExtSiteLength = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalServicesConfigurationSites", x => new { x.ExternalServicesConfigurationId, x.FinalClientId, x.ExtClientId });
                    table.ForeignKey(
                        name: "FK_ExternalServicesConfiguration_Sites_ExternalServicesConfiguration",
                        column: x => x.ExternalServicesConfigurationId,
                        principalTable: "ExternalServicesConfiguration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExternalServicesConfiguration_Sites_FinalClients",
                        column: x => x.FinalClientId,
                        principalTable: "FinalClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Postconditions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    PostconditionCollectionsId = table.Column<int>(nullable: false),
                    NameFieldModel = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    QueueId = table.Column<int>(nullable: true),
                    WorkOrderStatusId = table.Column<int>(nullable: true),
                    ExternalWorOrderStatusId = table.Column<int>(nullable: true),
                    PeopleTechniciansId = table.Column<int>(nullable: true),
                    PeopleManipulatorId = table.Column<int>(nullable: true),
                    EnterValue = table.Column<int>(nullable: true),
                    DecimalValue = table.Column<double>(nullable: true),
                    StringValue = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    BooleanValue = table.Column<bool>(nullable: true),
                    DateValue = table.Column<DateTime>(type: "datetime", nullable: true),
                    ExtraFieldId = table.Column<int>(nullable: true),
                    PeopleResponsibleTechniciansCollectionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postconditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Postconditions_ExternalWorOrderStatus",
                        column: x => x.ExternalWorOrderStatusId,
                        principalTable: "ExternalWorOrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Postconditions_ExtraFields",
                        column: x => x.ExtraFieldId,
                        principalTable: "ExtraFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Postconditions_PeopleManipulator",
                        column: x => x.PeopleManipulatorId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Postconditions_PeopleCollections",
                        column: x => x.PeopleResponsibleTechniciansCollectionId,
                        principalTable: "PeopleCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Postconditions_PeopleTechnicians",
                        column: x => x.PeopleTechniciansId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Postconditions_PostconditionCollections",
                        column: x => x.PostconditionCollectionsId,
                        principalTable: "PostconditionCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Postconditions_Queues",
                        column: x => x.QueueId,
                        principalTable: "Queues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Postconditions_WorkOrderStatus",
                        column: x => x.WorkOrderStatusId,
                        principalTable: "WorkOrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Preconditions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    TaskId = table.Column<int>(nullable: true),
                    PostconditionCollectionId = table.Column<int>(nullable: true),
                    PeopleResponsibleTechniciansCollectionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preconditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Preconditions_PeopleCollections",
                        column: x => x.PeopleResponsibleTechniciansCollectionId,
                        principalTable: "PeopleCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Postcondicions_PostconditionCollections",
                        column: x => x.PostconditionCollectionId,
                        principalTable: "PostconditionCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Preconditions_Tasks",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExtraFieldsValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ServiceId = table.Column<int>(nullable: true),
                    WorkOrderDeritativeId = table.Column<int>(nullable: true),
                    DerivedServiceId = table.Column<int>(nullable: true),
                    WorkOrderId = table.Column<int>(nullable: true),
                    ExtraFieldId = table.Column<int>(nullable: false),
                    EnterValue = table.Column<int>(nullable: true),
                    DataValue = table.Column<DateTime>(type: "datetime", nullable: true),
                    DecimalValue = table.Column<double>(nullable: true),
                    BooleaValue = table.Column<bool>(nullable: true),
                    StringValue = table.Column<string>(unicode: false, maxLength: 5000, nullable: true),
                    Signature = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraFieldsValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtraFieldsValues_DerivedServices",
                        column: x => x.DerivedServiceId,
                        principalTable: "DerivedServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExtraFieldsValues_ExtraFields",
                        column: x => x.ExtraFieldId,
                        principalTable: "ExtraFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExtraFieldsValues_Services",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExtraFieldsValues_WorkOrdersDeritative",
                        column: x => x.WorkOrderDeritativeId,
                        principalTable: "WorkOrdersDeritative",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillLine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    BillId = table.Column<int>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    Units = table.Column<double>(nullable: false),
                    SerialNumber = table.Column<string>(unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemSerialNumber_Bill",
                        column: x => x.BillId,
                        principalTable: "Bill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemSerialNumber_Items",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemsSerialNumber",
                        columns: x => new { x.ItemId, x.SerialNumber },
                        principalTable: "ItemsSerialNumber",
                        principalColumns: new[] { "ItemId", "SerialNumber" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ExpenseTypeId = table.Column<int>(nullable: false),
                    PaymentMethodId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(19, 4)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ExpenseTicketId = table.Column<int>(nullable: false),
                    Factor = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Expenses__Expens__79C80F94",
                        column: x => x.ExpenseTicketId,
                        principalTable: "ExpensesTickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Expenses__Paymen__7ABC33CD",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpensesTicketFile",
                columns: table => new
                {
                    ExpenseTicketId = table.Column<int>(nullable: false),
                    SomFileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpensesTicketFile", x => new { x.ExpenseTicketId, x.SomFileId });
                    table.ForeignKey(
                        name: "FK_ExpensesTicket_File_ExpenseTicket",
                        column: x => x.ExpenseTicketId,
                        principalTable: "ExpensesTickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpensesTicket_File_SomFiles",
                        column: x => x.SomFileId,
                        principalTable: "SomFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpensesTicketsFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExpenseTicketId = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpensesTicketsFiles", x => new { x.ExpenseTicketId, x.Id });
                    table.ForeignKey(
                        name: "FK_ExpensesTicketsFiles_ExpenseTicket",
                        column: x => x.ExpenseTicketId,
                        principalTable: "ExpensesTickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExternalServicesConfigurationProjectCategoriesProperties",
                columns: table => new
                {
                    ExternalServicesConfiguration_ProjectCategoriesId = table.Column<int>(nullable: false),
                    ColumnName = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    NoApplies = table.Column<bool>(nullable: true),
                    Value = table.Column<string>(unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalServicesConfigurationProjectCategoriesProperties", x => new { x.ExternalServicesConfiguration_ProjectCategoriesId, x.ColumnName });
                    table.ForeignKey(
                        name: "FK__ExternalS__Exter__0EC32C7A",
                        column: x => x.ExternalServicesConfiguration_ProjectCategoriesId,
                        principalTable: "ExternalServicesConfigurationProjectCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LiteralsPreconditions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    PreconditionId = table.Column<int>(nullable: false),
                    NomCampModel = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    ComparisonOperator = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ExtraFieldId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiteralsPreconditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiteralsPreconditions_ExtraFields",
                        column: x => x.ExtraFieldId,
                        principalTable: "ExtraFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LiteralsPreconditions_Preconditions",
                        column: x => x.PreconditionId,
                        principalTable: "Preconditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialForm",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    ExtraFieldValueId = table.Column<int>(nullable: false),
                    SerialNumber = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Reference = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    Description = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Units = table.Column<int>(nullable: true),
                    TeamId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialForm_ExtraFieldsValues",
                        column: x => x.ExtraFieldValueId,
                        principalTable: "ExtraFieldsValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialForm_Teams",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DnAndMaterialsAnalysis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    WorkOrder = table.Column<int>(nullable: true),
                    Bill = table.Column<int>(nullable: true),
                    People = table.Column<int>(nullable: true),
                    ExternalSystemNumber = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Status = table.Column<int>(nullable: true),
                    ItemCode = table.Column<int>(nullable: true),
                    ItemName = table.Column<string>(maxLength: 100, nullable: true),
                    ItemSerialNumber = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    Units = table.Column<decimal>(nullable: true),
                    PVP = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    PurchaseCost = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    TotalDeliveryNoteCost = table.Column<decimal>(type: "decimal(18, 0)", nullable: true),
                    TotalDeliveryNoteSalePrice = table.Column<decimal>(type: "decimal(18, 0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DnAndMaterialsAnalysis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DnAndMaterialsAnalysis_BillLine",
                        column: x => x.Id,
                        principalTable: "BillLine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PreconditionsLiteralValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    LiteralPreconditionId = table.Column<int>(nullable: true),
                    QueueId = table.Column<int>(nullable: true),
                    WorkOrderStatusId = table.Column<int>(nullable: true),
                    ExternalWorOrderStatusId = table.Column<int>(nullable: true),
                    PeopleTechnicianId = table.Column<int>(nullable: true),
                    PeopleManipulatorId = table.Column<int>(nullable: true),
                    OriginId = table.Column<int>(nullable: true),
                    WorkOrderTypesN1Id = table.Column<int>(nullable: true),
                    WorkOrderTypesN2Id = table.Column<int>(nullable: true),
                    WorkOrderTypesN3Id = table.Column<int>(nullable: true),
                    WorkOrderTypesN4Id = table.Column<int>(nullable: true),
                    WorkOrderTypesN5Id = table.Column<int>(nullable: true),
                    FinalClientId = table.Column<int>(nullable: true),
                    LocationId = table.Column<int>(nullable: true),
                    TeamId = table.Column<int>(nullable: true),
                    EnterValue = table.Column<int>(nullable: true),
                    DecimalValue = table.Column<double>(nullable: true),
                    StringValue = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    BooleanValue = table.Column<bool>(nullable: true),
                    DataValue = table.Column<DateTime>(type: "datetime", nullable: true),
                    ProjectId = table.Column<int>(nullable: true),
                    WorkOrderCategoryId = table.Column<int>(nullable: true),
                    PeopleResponsibleTechniciansCollectionId = table.Column<int>(nullable: true),
                    RegionId = table.Column<int>(nullable: true),
                    ZoneId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreconditionsLiteralValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreconditionsLiteralValues_ExternalWorOrderStatuses",
                        column: x => x.ExternalWorOrderStatusId,
                        principalTable: "ExternalWorOrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreconditionsLiteralValues_FinalClients",
                        column: x => x.FinalClientId,
                        principalTable: "FinalClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreconditionsLiteralValues_LiteralsPreconditions",
                        column: x => x.LiteralPreconditionId,
                        principalTable: "LiteralsPreconditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreconditionsLiteralValues_Locations",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreconditionsLiteralValues_PeopleManipulator",
                        column: x => x.PeopleManipulatorId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreconditionsLiteralValues_PeopleCollections",
                        column: x => x.PeopleResponsibleTechniciansCollectionId,
                        principalTable: "PeopleCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreconditionsLiteralValues_PeopleTechnicians",
                        column: x => x.PeopleTechnicianId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreconditionsLiteralValues_Projects",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreconditionsLiteralValues_Queues",
                        column: x => x.QueueId,
                        principalTable: "Queues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreconditionsLiteralValues_Teams",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ValorsLiteralsPrecondicio_WorkOrderCategories",
                        column: x => x.WorkOrderCategoryId,
                        principalTable: "WorkOrderCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreconditionsLiteralValues_WorkOrderStatuses",
                        column: x => x.WorkOrderStatusId,
                        principalTable: "WorkOrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreconditionsLiteralValues_WorkOrderTypesN1",
                        column: x => x.WorkOrderTypesN1Id,
                        principalTable: "WorkOrderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreconditionsLiteralValues_WorkOrderTypesN2",
                        column: x => x.WorkOrderTypesN2Id,
                        principalTable: "WorkOrderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreconditionsLiteralValues_WorkOrderTypesN3",
                        column: x => x.WorkOrderTypesN3Id,
                        principalTable: "WorkOrderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreconditionsLiteralValues_WorkOrderTypesN4",
                        column: x => x.WorkOrderTypesN4Id,
                        principalTable: "WorkOrderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreconditionsLiteralValues_WorkOrderTypesN5",
                        column: x => x.WorkOrderTypesN5Id,
                        principalTable: "WorkOrderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ValorsLiteralsPrecondicio_Zones",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvancedTechnicianListFilterPersons_PeopleId",
                table: "AdvancedTechnicianListFilterPersons",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "UQ__Advanced__3214EC06FC66D039",
                table: "AdvancedTechnicianListFilters",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssetsAudit_TeamId",
                table: "AssetsAudit",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Audits_TaskId",
                table: "Audits",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Audits_UserConfigurationId",
                table: "Audits",
                column: "UserConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_Audits_UserConfigurationSupplanterId",
                table: "Audits",
                column: "UserConfigurationSupplanterId");

            migrationBuilder.CreateIndex(
                name: "IX_Audits_WorkOrderId",
                table: "Audits",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_BasicTechnicianListFilterCalendarPlanningViewConfiguration_CalendarPlanningViewConfigurationId",
                table: "BasicTechnicianListFilterCalendarPlanningViewConfiguration",
                column: "CalendarPlanningViewConfigurationId");

            migrationBuilder.CreateIndex(
                name: "UQ__BasicTec__3214EC06B9ADA133",
                table: "BasicTechnicianListFilters",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasicTechnicianListFilterSkills_SkillId",
                table: "BasicTechnicianListFilterSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_ErpSystemInstanceId",
                table: "Bill",
                column: "ErpSystemInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_PeopleId",
                table: "Bill",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_ServiceId",
                table: "Bill",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_TaskId",
                table: "Bill",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_WorkorderId",
                table: "Bill",
                column: "WorkorderId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingItems_BillingLineItemId",
                table: "BillingItems",
                column: "BillingLineItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingLineItems_PredefinedServiceId",
                table: "BillingLineItems",
                column: "PredefinedServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingLineItems_ProjectId",
                table: "BillingLineItems",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingLineItems_TaskId",
                table: "BillingLineItems",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingLineItems_WorkOrderCategoryId",
                table: "BillingLineItems",
                column: "WorkOrderCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingRule_ErpSystemInstanceId",
                table: "BillingRule",
                column: "ErpSystemInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingRule_TaskId",
                table: "BillingRule",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingRuleItem_BillingRuleId",
                table: "BillingRuleItem",
                column: "BillingRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_BillingRuleItem_ItemId",
                table: "BillingRuleItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BillLine_BillId",
                table: "BillLine",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillLine_ItemId_SerialNumber",
                table: "BillLine",
                columns: new[] { "ItemId", "SerialNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEvents_CalendarId",
                table: "CalendarEvents",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarEvents_ParentEventId",
                table: "CalendarEvents",
                column: "ParentEventId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarPlanningViewConfiguration_CalendarPlanningViewConfigurationId",
                table: "CalendarPlanningViewConfiguration",
                column: "CalendarPlanningViewConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarPlanningViewConfiguration_UserConfigurationId",
                table: "CalendarPlanningViewConfiguration",
                column: "UserConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarPlanningViewConfigurationPeople_PeopleId",
                table: "CalendarPlanningViewConfigurationPeople",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarPlanningViewConfigurationPeopleCollection_PeopleCollectionId",
                table: "CalendarPlanningViewConfigurationPeopleCollection",
                column: "PeopleCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ClosingCodes_ClosingCodesFatherId",
                table: "ClosingCodes",
                column: "ClosingCodesFatherId");

            migrationBuilder.CreateIndex(
                name: "IX_ClosingCodes_CollectionsClosureCodesId",
                table: "ClosingCodes",
                column: "CollectionsClosureCodesId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionsExtraFieldExtraField_ExtraFieldId",
                table: "CollectionsExtraFieldExtraField",
                column: "ExtraFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_CompaniesCostHistorical_CompanyId",
                table: "CompaniesCostHistorical",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactsFinalClients_ContactId",
                table: "ContactsFinalClients",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactsLocationsFinalClients_ContactId",
                table: "ContactsLocationsFinalClients",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractContacts_ContactId",
                table: "ContractContacts",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ClientId",
                table: "Contracts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_PeopleId",
                table: "Contracts",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CompanyId",
                table: "Departments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DerivedServices_ClosingCodesIdN1",
                table: "DerivedServices",
                column: "ClosingCodesIdN1");

            migrationBuilder.CreateIndex(
                name: "IX_DerivedServices_ClosingCodesIdN2",
                table: "DerivedServices",
                column: "ClosingCodesIdN2");

            migrationBuilder.CreateIndex(
                name: "IX_DerivedServices_ClosingCodesIdN3",
                table: "DerivedServices",
                column: "ClosingCodesIdN3");

            migrationBuilder.CreateIndex(
                name: "IX_DerivedServices_PeopleResponsibleId",
                table: "DerivedServices",
                column: "PeopleResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_DerivedServices_PredefinedServicesId",
                table: "DerivedServices",
                column: "PredefinedServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_DerivedServices_ProjectId",
                table: "DerivedServices",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DerivedServices_SubcontractResponsibleId",
                table: "DerivedServices",
                column: "SubcontractResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_DerivedServices_TaskId",
                table: "DerivedServices",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ErpItemsSyncConfig_ErpSystemInstanceId",
                table: "ErpItemsSyncConfig",
                column: "ErpSystemInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_ErpSystemInstanceQuery_ErpSystemInstanceId",
                table: "ErpSystemInstanceQuery",
                column: "ErpSystemInstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ExpenseTicketId",
                table: "Expenses",
                column: "ExpenseTicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_PaymentMethodId",
                table: "Expenses",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesTicketFile_SomFileId",
                table: "ExpensesTicketFile",
                column: "SomFileId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesTickets_PeopleId",
                table: "ExpensesTickets",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesTickets_PeopleValidatorId",
                table: "ExpensesTickets",
                column: "PeopleValidatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesTickets_WorkOrderId",
                table: "ExpensesTickets",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalServicesConfiguration_AssetQueueId",
                table: "ExternalServicesConfiguration",
                column: "AssetQueueId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalServicesConfiguration_AssetWoExternalStatusId",
                table: "ExternalServicesConfiguration",
                column: "AssetWoExternalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalServicesConfiguration_AssetWoStatusId",
                table: "ExternalServicesConfiguration",
                column: "AssetWoStatusId");

            migrationBuilder.CreateIndex(
                name: "UQ__External__E71D0598903D98C7",
                table: "ExternalServicesConfiguration",
                column: "ExternalService",
                unique: true,
                filter: "[ExternalService] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalServicesConfiguration_FinalClientId",
                table: "ExternalServicesConfiguration",
                column: "FinalClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalServicesConfiguration_FlowId",
                table: "ExternalServicesConfiguration",
                column: "FlowId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalServicesConfiguration_LocationId",
                table: "ExternalServicesConfiguration",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalServicesConfiguration_ProjectId",
                table: "ExternalServicesConfiguration",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalServicesConfiguration_QueueId",
                table: "ExternalServicesConfiguration",
                column: "QueueId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalServicesConfiguration_TaskId",
                table: "ExternalServicesConfiguration",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalServicesConfiguration_WoCategoryId",
                table: "ExternalServicesConfiguration",
                column: "WoCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalServicesConfiguration_WoExternalStatusId",
                table: "ExternalServicesConfiguration",
                column: "WoExternalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalServicesConfiguration_WoStatusId",
                table: "ExternalServicesConfiguration",
                column: "WoStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalServicesConfigurationProjectCategories_ConfigurationId",
                table: "ExternalServicesConfigurationProjectCategories",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalServicesConfigurationProjectCategories_ProjectId",
                table: "ExternalServicesConfigurationProjectCategories",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalServicesConfigurationProjectCategories_WoCategoryId",
                table: "ExternalServicesConfigurationProjectCategories",
                column: "WoCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalServicesConfigurationSites_FinalClientId",
                table: "ExternalServicesConfigurationSites",
                column: "FinalClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalSystemImportData_WorkOrderId",
                table: "ExternalSystemImportData",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraFields_ErpSystemInstanceQueryId",
                table: "ExtraFields",
                column: "ErpSystemInstanceQueryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraFieldsValues_DerivedServiceId",
                table: "ExtraFieldsValues",
                column: "DerivedServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraFieldsValues_ExtraFieldId",
                table: "ExtraFieldsValues",
                column: "ExtraFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraFieldsValues_ServiceId",
                table: "ExtraFieldsValues",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtraFieldsValues_WorkOrderDeritativeId",
                table: "ExtraFieldsValues",
                column: "WorkOrderDeritativeId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalClients_PeopleCommercialId",
                table: "FinalClients",
                column: "PeopleCommercialId");

            migrationBuilder.CreateIndex(
                name: "IX_FinalClientSiteCalendar_CalendarId",
                table: "FinalClientSiteCalendar",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_FormElements_FormConfigsId",
                table: "FormElements",
                column: "FormConfigsId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsPointsRate_ItemId",
                table: "ItemsPointsRate",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsPointsRate_PointsRateId",
                table: "ItemsPointsRate",
                column: "PointsRateId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsPurchaseRate_ItemId",
                table: "ItemsPurchaseRate",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsPurchaseRate_PurchaseRateId",
                table: "ItemsPurchaseRate",
                column: "PurchaseRateId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsSalesRate_ItemId",
                table: "ItemsSalesRate",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsSalesRate_SalesRateId",
                table: "ItemsSalesRate",
                column: "SalesRateId");

            migrationBuilder.CreateIndex(
                name: "UQ__ItemsSer__048A000827618D28",
                table: "ItemsSerialNumber",
                column: "SerialNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Journeys_PeopleId",
                table: "Journeys",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_JourneysStates_JourneyId",
                table: "JourneysStates",
                column: "JourneyId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgePeople_PeopleId",
                table: "KnowledgePeople",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeSubContracts_SubContractId",
                table: "KnowledgeSubContracts",
                column: "SubContractId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeToolsType_ToolsTypeId",
                table: "KnowledgeToolsType",
                column: "ToolsTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeWorkOrderTypes_WorkOrderTypeId",
                table: "KnowledgeWorkOrderTypes",
                column: "WorkOrderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LiteralsPreconditions_ExtraFieldId",
                table: "LiteralsPreconditions",
                column: "ExtraFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_LiteralsPreconditions_PreconditionId",
                table: "LiteralsPreconditions",
                column: "PreconditionId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationCalendar_CalendarId",
                table: "LocationCalendar",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_PeopleResponsibleLocationId",
                table: "Locations",
                column: "PeopleResponsibleLocationId");

            migrationBuilder.CreateIndex(
                name: "UQ_LocationsFinalClients_CompositeCode",
                table: "LocationsFinalClients",
                column: "CompositeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocationsFinalClients_LocationId",
                table: "LocationsFinalClients",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationsFinalClients_PeopleCommercialId",
                table: "LocationsFinalClients",
                column: "PeopleCommercialId");

            migrationBuilder.CreateIndex(
                name: "IX_MainWOViewConfigurationsGroups_PeopleCollectionId",
                table: "MainWOViewConfigurationsGroups",
                column: "PeopleCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_MainWoViewConfigurationsPeople_PeopleId",
                table: "MainWoViewConfigurationsPeople",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialForm_ExtraFieldValueId",
                table: "MaterialForm",
                column: "ExtraFieldValueId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialForm_TeamId",
                table: "MaterialForm",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Models_BrandId",
                table: "Models",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_People_CompanyId",
                table: "People",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_People_DepartmentId",
                table: "People",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_People_PointsRateId",
                table: "People",
                column: "PointsRateId");

            migrationBuilder.CreateIndex(
                name: "IX_People_SubcontractId",
                table: "People",
                column: "SubcontractId");

            migrationBuilder.CreateIndex(
                name: "IX_People_UserConfigurationId",
                table: "People",
                column: "UserConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_PeopleCalendars_CalendarId",
                table: "PeopleCalendars",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_PeopleCollectionCalendars_CalendarId",
                table: "PeopleCollectionCalendars",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_PeopleCollectionsAdmins_PeopleId",
                table: "PeopleCollectionsAdmins",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_PeopleCollectionsPeople_PeopleCollectionId",
                table: "PeopleCollectionsPeople",
                column: "PeopleCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_PeopleCollectionsPermissions_PeopleCollectionId",
                table: "PeopleCollectionsPermissions",
                column: "PeopleCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_PeopleCost_PeopleId",
                table: "PeopleCost",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_PeopleCostHistorical_PeopleId",
                table: "PeopleCostHistorical",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_PeoplePermissions_PermissionId",
                table: "PeoplePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PeopleProjects_ProjectId",
                table: "PeopleProjects",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "UniqueDevice",
                table: "PeopleRegisteredPda",
                column: "DeviceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermissionsQueues_QueueId",
                table: "PermissionsQueues",
                column: "QueueId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionsTasks_TaskId",
                table: "PermissionsTasks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanificationCriterias_PeopleId",
                table: "PlanificationCriterias",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanificationProcesses_ExecutionCalendar",
                table: "PlanificationProcesses",
                column: "ExecutionCalendar");

            migrationBuilder.CreateIndex(
                name: "IX_PlanificationProcesses_HumanResourcesFilter",
                table: "PlanificationProcesses",
                column: "HumanResourcesFilter");

            migrationBuilder.CreateIndex(
                name: "IX_PlanificationProcesses_Weights",
                table: "PlanificationProcesses",
                column: "Weights");

            migrationBuilder.CreateIndex(
                name: "IX_PlanificationProcesses_WorkOrdersFilter",
                table: "PlanificationProcesses",
                column: "WorkOrdersFilter");

            migrationBuilder.CreateIndex(
                name: "IX_PlanningPanelViewConfiguration_PeopleOwnerId",
                table: "PlanningPanelViewConfiguration",
                column: "PeopleOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanningPanelViewConfiguration_UsersMainWoViewConfigurationId",
                table: "PlanningPanelViewConfiguration",
                column: "UsersMainWoViewConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanningPanelViewConfigurationPeople_PeopleId",
                table: "PlanningPanelViewConfigurationPeople",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanningPanelViewConfigurationPeopleCollection_PeopleCollectionId",
                table: "PlanningPanelViewConfigurationPeopleCollection",
                column: "PeopleCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_PostconditionCollections_TaskId",
                table: "PostconditionCollections",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Postconditions_ExternalWorOrderStatusId",
                table: "Postconditions",
                column: "ExternalWorOrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Postconditions_ExtraFieldId",
                table: "Postconditions",
                column: "ExtraFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Postconditions_PeopleManipulatorId",
                table: "Postconditions",
                column: "PeopleManipulatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Postconditions_PeopleResponsibleTechniciansCollectionId",
                table: "Postconditions",
                column: "PeopleResponsibleTechniciansCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Postconditions_PeopleTechniciansId",
                table: "Postconditions",
                column: "PeopleTechniciansId");

            migrationBuilder.CreateIndex(
                name: "IX_Postconditions_PostconditionCollectionsId",
                table: "Postconditions",
                column: "PostconditionCollectionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Postconditions_QueueId",
                table: "Postconditions",
                column: "QueueId");

            migrationBuilder.CreateIndex(
                name: "IX_Postconditions_WorkOrderStatusId",
                table: "Postconditions",
                column: "WorkOrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Preconditions_PeopleResponsibleTechniciansCollectionId",
                table: "Preconditions",
                column: "PeopleResponsibleTechniciansCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Preconditions_PostconditionCollectionId",
                table: "Preconditions",
                column: "PostconditionCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Preconditions_TaskId",
                table: "Preconditions",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_PreconditionsLiteralValues_ExternalWorOrderStatusId",
                table: "PreconditionsLiteralValues",
                column: "ExternalWorOrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PreconditionsLiteralValues_FinalClientId",
                table: "PreconditionsLiteralValues",
                column: "FinalClientId");

            migrationBuilder.CreateIndex(
                name: "IX_PreconditionsLiteralValues_LiteralPreconditionId",
                table: "PreconditionsLiteralValues",
                column: "LiteralPreconditionId");

            migrationBuilder.CreateIndex(
                name: "IX_PreconditionsLiteralValues_LocationId",
                table: "PreconditionsLiteralValues",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_PreconditionsLiteralValues_PeopleManipulatorId",
                table: "PreconditionsLiteralValues",
                column: "PeopleManipulatorId");

            migrationBuilder.CreateIndex(
                name: "IX_PreconditionsLiteralValues_PeopleResponsibleTechniciansCollectionId",
                table: "PreconditionsLiteralValues",
                column: "PeopleResponsibleTechniciansCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_PreconditionsLiteralValues_PeopleTechnicianId",
                table: "PreconditionsLiteralValues",
                column: "PeopleTechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_PreconditionsLiteralValues_ProjectId",
                table: "PreconditionsLiteralValues",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PreconditionsLiteralValues_QueueId",
                table: "PreconditionsLiteralValues",
                column: "QueueId");

            migrationBuilder.CreateIndex(
                name: "IX_PreconditionsLiteralValues_TeamId",
                table: "PreconditionsLiteralValues",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_PreconditionsLiteralValues_WorkOrderCategoryId",
                table: "PreconditionsLiteralValues",
                column: "WorkOrderCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PreconditionsLiteralValues_WorkOrderStatusId",
                table: "PreconditionsLiteralValues",
                column: "WorkOrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PreconditionsLiteralValues_WorkOrderTypesN1Id",
                table: "PreconditionsLiteralValues",
                column: "WorkOrderTypesN1Id");

            migrationBuilder.CreateIndex(
                name: "IX_PreconditionsLiteralValues_WorkOrderTypesN2Id",
                table: "PreconditionsLiteralValues",
                column: "WorkOrderTypesN2Id");

            migrationBuilder.CreateIndex(
                name: "IX_PreconditionsLiteralValues_WorkOrderTypesN3Id",
                table: "PreconditionsLiteralValues",
                column: "WorkOrderTypesN3Id");

            migrationBuilder.CreateIndex(
                name: "IX_PreconditionsLiteralValues_WorkOrderTypesN4Id",
                table: "PreconditionsLiteralValues",
                column: "WorkOrderTypesN4Id");

            migrationBuilder.CreateIndex(
                name: "IX_PreconditionsLiteralValues_WorkOrderTypesN5Id",
                table: "PreconditionsLiteralValues",
                column: "WorkOrderTypesN5Id");

            migrationBuilder.CreateIndex(
                name: "IX_PreconditionsLiteralValues_ZoneId",
                table: "PreconditionsLiteralValues",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_PredefinedServices_CollectionExtraFieldId",
                table: "PredefinedServices",
                column: "CollectionExtraFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_PredefinedServices_ProjectId",
                table: "PredefinedServices",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PredefinedServicesPermission_PredefinedServiceId",
                table: "PredefinedServicesPermission",
                column: "PredefinedServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CollectionsClosureCodesId",
                table: "Projects",
                column: "CollectionsClosureCodesId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CollectionsExtraFieldId",
                table: "Projects",
                column: "CollectionsExtraFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CollectionsTypesWorkOrdersId",
                table: "Projects",
                column: "CollectionsTypesWorkOrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ContractId",
                table: "Projects",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_WorkOrderCategoriesCollectionId",
                table: "Projects",
                column: "WorkOrderCategoriesCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_WorkOrderStatusesId",
                table: "Projects",
                column: "WorkOrderStatusesId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsCalendars_CalendarId",
                table: "ProjectsCalendars",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsContacts_ContactId",
                table: "ProjectsContacts",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsPermissions_ProjectId",
                table: "ProjectsPermissions",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PushNotificationsPeople_PeopleId",
                table: "PushNotificationsPeople",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_PushNotificationsPeopleCollections_PeopleCollectionsId",
                table: "PushNotificationsPeopleCollections",
                column: "PeopleCollectionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ClosingCodeFirstId",
                table: "Services",
                column: "ClosingCodeFirstId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ClosingCodeId",
                table: "Services",
                column: "ClosingCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ClosingCodeSecondId",
                table: "Services",
                column: "ClosingCodeSecondId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ClosingCodeThirdId",
                table: "Services",
                column: "ClosingCodeThirdId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_PeopleResponsibleId",
                table: "Services",
                column: "PeopleResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_PredefinedServiceId",
                table: "Services",
                column: "PredefinedServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServicesCancelFormId",
                table: "Services",
                column: "ServicesCancelFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_SubcontractResponsibleId",
                table: "Services",
                column: "SubcontractResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesAnalysis_WorkOrderCode",
                table: "ServicesAnalysis",
                column: "WorkOrderCode");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesViewConfigurations_UserConfigurationId",
                table: "ServicesViewConfigurations",
                column: "UserConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_UserConfigurationId",
                table: "Sessions",
                column: "UserConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_SgsClosingInfo_WorkOrderId",
                table: "SgsClosingInfo",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteUser_LocationId",
                table: "SiteUser",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_StatesSla_SlaId",
                table: "StatesSla",
                column: "SlaId");

            migrationBuilder.CreateIndex(
                name: "UK_Subcontracta",
                table: "SubContracts",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubContracts_SalesRateId",
                table: "SubContracts",
                column: "SalesRateId");

            migrationBuilder.CreateIndex(
                name: "IX_SubFamilies_FamilyId",
                table: "SubFamilies",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ExternalWorOrderStatusId",
                table: "Tasks",
                column: "ExternalWorOrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ExtraFieldId",
                table: "Tasks",
                column: "ExtraFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_FlowId",
                table: "Tasks",
                column: "FlowId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_MailTemplateId",
                table: "Tasks",
                column: "MailTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_PeopleManipulatorId",
                table: "Tasks",
                column: "PeopleManipulatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_PeopleResponsibleTechniciansId",
                table: "Tasks",
                column: "PeopleResponsibleTechniciansId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_PeopleTechnicianId",
                table: "Tasks",
                column: "PeopleTechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_PredefinedServiceId",
                table: "Tasks",
                column: "PredefinedServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_QueueId",
                table: "Tasks",
                column: "QueueId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_WorkOrderStatusId",
                table: "Tasks",
                column: "WorkOrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_AssetStatusId",
                table: "Teams",
                column: "AssetStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_GuaranteeId",
                table: "Teams",
                column: "GuaranteeId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LocationClientId",
                table: "Teams",
                column: "LocationClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LocationId",
                table: "Teams",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ModelId",
                table: "Teams",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_SubFamilyId",
                table: "Teams",
                column: "SubFamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_UsageId",
                table: "Teams",
                column: "UsageId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_UserId",
                table: "Teams",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamsHiredServices_HiredServiceId",
                table: "TeamsHiredServices",
                column: "HiredServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamsWorkOrders_TeamId",
                table: "TeamsWorkOrders",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalCodes_PeopleTechnicId",
                table: "TechnicalCodes",
                column: "PeopleTechnicId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalCodes_WorkOrderCategoryId",
                table: "TechnicalCodes",
                column: "WorkOrderCategoryId");

            migrationBuilder.CreateIndex(
                name: "PK_PeopleTechnicalCodes",
                table: "TechnicalCodes",
                columns: new[] { "ProjectId", "WorkOrderCategoryId", "PeopleTechnicId" },
                unique: true,
                filter: "[ProjectId] IS NOT NULL AND [WorkOrderCategoryId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicianListFilters_PeopleId",
                table: "TechnicianListFilters",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_Tools_VehicleId",
                table: "Tools",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_ToolsToolTypes_ToolTypeId",
                table: "ToolsToolTypes",
                column: "ToolTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ToolsTypeWorkOrderTypes_ToolsTypeId",
                table: "ToolsTypeWorkOrderTypes",
                column: "ToolsTypeId");

            migrationBuilder.CreateIndex(
                name: "UQ__Usages__737584F6108C583C",
                table: "Usages",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UsersMainWOViewConfigurations_UserConfigurationId",
                table: "UsersMainWOViewConfigurations",
                column: "UserConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_PeopleDriverId",
                table: "Vehicles",
                column: "PeopleDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderCategories_SlaId",
                table: "WorkOrderCategories",
                column: "SlaId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderCategories_WorkOrderCategoriesCollectionId",
                table: "WorkOrderCategories",
                column: "WorkOrderCategoriesCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderCategoriesCollectionCalendar_CalendarId",
                table: "WorkOrderCategoriesCollectionCalendar",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderCategoryCalendar_CalendarId",
                table: "WorkOrderCategoryCalendar",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderCategoryKnowledge_KnowledgeId",
                table: "WorkOrderCategoryKnowledge",
                column: "KnowledgeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderCategoryPermissions_WorkOrderCategoryId",
                table: "WorkOrderCategoryPermissions",
                column: "WorkOrderCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_ExternalWorOrderStatusId",
                table: "WorkOrders",
                column: "ExternalWorOrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_FinalClientId",
                table: "WorkOrders",
                column: "FinalClientId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_LocationId",
                table: "WorkOrders",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_PeopleIntroducedById",
                table: "WorkOrders",
                column: "PeopleIntroducedById");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_PeopleManipulatorId",
                table: "WorkOrders",
                column: "PeopleManipulatorId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_PeopleResponsibleId",
                table: "WorkOrders",
                column: "PeopleResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_ProjectId",
                table: "WorkOrders",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_QueueId",
                table: "WorkOrders",
                column: "QueueId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_ServiceId",
                table: "WorkOrders",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_SiteUserId",
                table: "WorkOrders",
                column: "SiteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_TeamId",
                table: "WorkOrders",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_WorkOrderCategoryId",
                table: "WorkOrders",
                column: "WorkOrderCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_WorkOrderStatusId",
                table: "WorkOrders",
                column: "WorkOrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_WorkOrderTypesId",
                table: "WorkOrders",
                column: "WorkOrderTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_WorkOrdersFatherId",
                table: "WorkOrders",
                column: "WorkOrdersFatherId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrdersDeritative_ExternalWorOrderStatusId",
                table: "WorkOrdersDeritative",
                column: "ExternalWorOrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrdersDeritative_FinalClientId",
                table: "WorkOrdersDeritative",
                column: "FinalClientId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrdersDeritative_LocationId",
                table: "WorkOrdersDeritative",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrdersDeritative_PeopleIntroducedById",
                table: "WorkOrdersDeritative",
                column: "PeopleIntroducedById");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrdersDeritative_PeopleManipulatorId",
                table: "WorkOrdersDeritative",
                column: "PeopleManipulatorId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrdersDeritative_PeopleResponsibleId",
                table: "WorkOrdersDeritative",
                column: "PeopleResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrdersDeritative_PeopleResponsibleTechniciansCollectionId",
                table: "WorkOrdersDeritative",
                column: "PeopleResponsibleTechniciansCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrdersDeritative_ProjectId",
                table: "WorkOrdersDeritative",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrdersDeritative_QueueId",
                table: "WorkOrdersDeritative",
                column: "QueueId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrdersDeritative_SiteUserId",
                table: "WorkOrdersDeritative",
                column: "SiteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrdersDeritative_TaskId",
                table: "WorkOrdersDeritative",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrdersDeritative_TeamsId",
                table: "WorkOrdersDeritative",
                column: "TeamsId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrdersDeritative_WorkOrderCategoryId",
                table: "WorkOrdersDeritative",
                column: "WorkOrderCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrdersDeritative_WorkOrderStatusId",
                table: "WorkOrdersDeritative",
                column: "WorkOrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrdersDeritative_WorkOrderTypeId",
                table: "WorkOrdersDeritative",
                column: "WorkOrderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderTypes_CollectionsTypesWorkOrdersId",
                table: "WorkOrderTypes",
                column: "CollectionsTypesWorkOrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderTypes_SlaId",
                table: "WorkOrderTypes",
                column: "SlaId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderTypes_WorkOrderTypesFatherId",
                table: "WorkOrderTypes",
                column: "WorkOrderTypesFatherId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneProject_ProjectId",
                table: "ZoneProject",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "UQ_Zone_Project",
                table: "ZoneProject",
                columns: new[] { "ZoneId", "ProjectId" },
                unique: true,
                filter: "[ProjectId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ_ZoneProject_PostalCode",
                table: "ZoneProjectPostalCode",
                columns: new[] { "ZoneProjectId", "PostalCode" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvancedTechnicianListFilterPersons");

            migrationBuilder.DropTable(
                name: "AssetsAuditChanges");

            migrationBuilder.DropTable(
                name: "Audits");

            migrationBuilder.DropTable(
                name: "BasicTechnicianListFilterCalendarPlanningViewConfiguration");

            migrationBuilder.DropTable(
                name: "BasicTechnicianListFilterSkills");

            migrationBuilder.DropTable(
                name: "BillingItems");

            migrationBuilder.DropTable(
                name: "BillingRuleItem");

            migrationBuilder.DropTable(
                name: "CalendarEvents");

            migrationBuilder.DropTable(
                name: "CalendarPlanningViewConfigurationPeople");

            migrationBuilder.DropTable(
                name: "CalendarPlanningViewConfigurationPeopleCollection");

            migrationBuilder.DropTable(
                name: "CollectionsExtraFieldExtraField");

            migrationBuilder.DropTable(
                name: "CompaniesCostHistorical");

            migrationBuilder.DropTable(
                name: "ContactsFinalClients");

            migrationBuilder.DropTable(
                name: "ContactsLocationsFinalClients");

            migrationBuilder.DropTable(
                name: "ContractContacts");

            migrationBuilder.DropTable(
                name: "DnAndMaterialsAnalysis");

            migrationBuilder.DropTable(
                name: "ErpItemsSyncConfig");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "ExpensesTicketFile");

            migrationBuilder.DropTable(
                name: "ExpensesTicketsFiles");

            migrationBuilder.DropTable(
                name: "ExpenseTypes");

            migrationBuilder.DropTable(
                name: "ExternalServicesConfigurationProjectCategoriesProperties");

            migrationBuilder.DropTable(
                name: "ExternalServicesConfigurationSites");

            migrationBuilder.DropTable(
                name: "ExternalSystemImportData");

            migrationBuilder.DropTable(
                name: "FinalClientSiteCalendar");

            migrationBuilder.DropTable(
                name: "FormElements");

            migrationBuilder.DropTable(
                name: "ItemsPointsRate");

            migrationBuilder.DropTable(
                name: "ItemsPurchaseRate");

            migrationBuilder.DropTable(
                name: "ItemsSalesRate");

            migrationBuilder.DropTable(
                name: "JourneysStates");

            migrationBuilder.DropTable(
                name: "KnowledgePeople");

            migrationBuilder.DropTable(
                name: "KnowledgeSubContracts");

            migrationBuilder.DropTable(
                name: "KnowledgeToolsType");

            migrationBuilder.DropTable(
                name: "KnowledgeWorkOrderTypes");

            migrationBuilder.DropTable(
                name: "LocationCalendar");

            migrationBuilder.DropTable(
                name: "LocationsFinalClients");

            migrationBuilder.DropTable(
                name: "MainOTStatics");

            migrationBuilder.DropTable(
                name: "MainWORegistry");

            migrationBuilder.DropTable(
                name: "MainWoViewConfigurationsColumns");

            migrationBuilder.DropTable(
                name: "MainWOViewConfigurationsGroups");

            migrationBuilder.DropTable(
                name: "MainWoViewConfigurationsPeople");

            migrationBuilder.DropTable(
                name: "MaterialForm");

            migrationBuilder.DropTable(
                name: "PeopleCalendars");

            migrationBuilder.DropTable(
                name: "PeopleCollectionCalendars");

            migrationBuilder.DropTable(
                name: "PeopleCollectionsAdmins");

            migrationBuilder.DropTable(
                name: "PeopleCollectionsPeople");

            migrationBuilder.DropTable(
                name: "PeopleCollectionsPermissions");

            migrationBuilder.DropTable(
                name: "PeopleCost");

            migrationBuilder.DropTable(
                name: "PeopleCostHistorical");

            migrationBuilder.DropTable(
                name: "PeoplePermissions");

            migrationBuilder.DropTable(
                name: "PeopleProjects");

            migrationBuilder.DropTable(
                name: "PeopleRegisteredPda");

            migrationBuilder.DropTable(
                name: "PermissionsQueues");

            migrationBuilder.DropTable(
                name: "PermissionsTasks");

            migrationBuilder.DropTable(
                name: "PlanificationCriterias");

            migrationBuilder.DropTable(
                name: "PlanificationProcessCalendarChangeTracker");

            migrationBuilder.DropTable(
                name: "PlanificationProcesses");

            migrationBuilder.DropTable(
                name: "PlanificationProcessWorkOrderChangeTracker");

            migrationBuilder.DropTable(
                name: "PlanningPanelViewConfigurationPeople");

            migrationBuilder.DropTable(
                name: "PlanningPanelViewConfigurationPeopleCollection");

            migrationBuilder.DropTable(
                name: "Postconditions");

            migrationBuilder.DropTable(
                name: "PreconditionsLiteralValues");

            migrationBuilder.DropTable(
                name: "PredefinedServicesPermission");

            migrationBuilder.DropTable(
                name: "ProjectsCalendars");

            migrationBuilder.DropTable(
                name: "ProjectsContacts");

            migrationBuilder.DropTable(
                name: "ProjectsPermissions");

            migrationBuilder.DropTable(
                name: "PushNotificationsPeople");

            migrationBuilder.DropTable(
                name: "PushNotificationsPeopleCollections");

            migrationBuilder.DropTable(
                name: "SaltoCSVersion");

            migrationBuilder.DropTable(
                name: "ServicesAnalysis");

            migrationBuilder.DropTable(
                name: "ServicesViewConfigurationsColumns");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "SgsClosingInfo");

            migrationBuilder.DropTable(
                name: "StatesSla");

            migrationBuilder.DropTable(
                name: "StopSlaReason");

            migrationBuilder.DropTable(
                name: "SynchronizationSessions");

            migrationBuilder.DropTable(
                name: "SystemNotifications");

            migrationBuilder.DropTable(
                name: "TaskTokens");

            migrationBuilder.DropTable(
                name: "TaskWebServiceCallItems");

            migrationBuilder.DropTable(
                name: "TeamsHiredServices");

            migrationBuilder.DropTable(
                name: "TeamsWorkOrders");

            migrationBuilder.DropTable(
                name: "TechnicalCodes");

            migrationBuilder.DropTable(
                name: "TenantConfiguration");

            migrationBuilder.DropTable(
                name: "ToolsToolTypes");

            migrationBuilder.DropTable(
                name: "ToolsTypeWorkOrderTypes");

            migrationBuilder.DropTable(
                name: "WorkOrderAnalysis");

            migrationBuilder.DropTable(
                name: "WorkOrderCategoriesCollectionCalendar");

            migrationBuilder.DropTable(
                name: "WorkOrderCategoryCalendar");

            migrationBuilder.DropTable(
                name: "WorkOrderCategoryKnowledge");

            migrationBuilder.DropTable(
                name: "WorkOrderCategoryPermissions");

            migrationBuilder.DropTable(
                name: "WsErpSystemInstance");

            migrationBuilder.DropTable(
                name: "ZoneProjectPostalCode");

            migrationBuilder.DropTable(
                name: "AdvancedTechnicianListFilters");

            migrationBuilder.DropTable(
                name: "AssetsAudit");

            migrationBuilder.DropTable(
                name: "BasicTechnicianListFilters");

            migrationBuilder.DropTable(
                name: "BillingLineItems");

            migrationBuilder.DropTable(
                name: "BillingRule");

            migrationBuilder.DropTable(
                name: "CalendarPlanningViewConfiguration");

            migrationBuilder.DropTable(
                name: "BillLine");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "SomFiles");

            migrationBuilder.DropTable(
                name: "ExpensesTickets");

            migrationBuilder.DropTable(
                name: "ExternalServicesConfigurationProjectCategories");

            migrationBuilder.DropTable(
                name: "PurchaseRate");

            migrationBuilder.DropTable(
                name: "Journeys");

            migrationBuilder.DropTable(
                name: "ExtraFieldsValues");

            migrationBuilder.DropTable(
                name: "FormConfigs");

            migrationBuilder.DropTable(
                name: "OptimizationFunctionWeights");

            migrationBuilder.DropTable(
                name: "PlanningPanelViewConfiguration");

            migrationBuilder.DropTable(
                name: "LiteralsPreconditions");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "PushNotifications");

            migrationBuilder.DropTable(
                name: "ServicesViewConfigurations");

            migrationBuilder.DropTable(
                name: "HiredServices");

            migrationBuilder.DropTable(
                name: "Tools");

            migrationBuilder.DropTable(
                name: "ToolsType");

            migrationBuilder.DropTable(
                name: "Calendars");

            migrationBuilder.DropTable(
                name: "Knowledge");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "ZoneProject");

            migrationBuilder.DropTable(
                name: "TechnicianListFilters");

            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "ItemsSerialNumber");

            migrationBuilder.DropTable(
                name: "ExternalServicesConfiguration");

            migrationBuilder.DropTable(
                name: "DerivedServices");

            migrationBuilder.DropTable(
                name: "WorkOrdersDeritative");

            migrationBuilder.DropTable(
                name: "UsersMainWOViewConfigurations");

            migrationBuilder.DropTable(
                name: "Preconditions");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropTable(
                name: "WorkOrders");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "PostconditionCollections");

            migrationBuilder.DropTable(
                name: "FinalClients");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "WorkOrderCategories");

            migrationBuilder.DropTable(
                name: "WorkOrderTypes");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "ClosingCodes");

            migrationBuilder.DropTable(
                name: "AssetStatuses");

            migrationBuilder.DropTable(
                name: "Guarantee");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropTable(
                name: "SubFamilies");

            migrationBuilder.DropTable(
                name: "Usages");

            migrationBuilder.DropTable(
                name: "SiteUser");

            migrationBuilder.DropTable(
                name: "SLA");

            migrationBuilder.DropTable(
                name: "ExternalWorOrderStatuses");

            migrationBuilder.DropTable(
                name: "ExtraFields");

            migrationBuilder.DropTable(
                name: "Flows");

            migrationBuilder.DropTable(
                name: "MailTemplate");

            migrationBuilder.DropTable(
                name: "PeopleCollections");

            migrationBuilder.DropTable(
                name: "PredefinedServices");

            migrationBuilder.DropTable(
                name: "Queues");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Families");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "ErpSystemInstanceQuery");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "ErpSystemInstance");

            migrationBuilder.DropTable(
                name: "CollectionsClosureCodes");

            migrationBuilder.DropTable(
                name: "CollectionsExtraField");

            migrationBuilder.DropTable(
                name: "CollectionsTypesWorkOrders");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "WorkOrderCategoriesCollections");

            migrationBuilder.DropTable(
                name: "WorkOrderStatuses");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "PointsRate");

            migrationBuilder.DropTable(
                name: "SubContracts");

            migrationBuilder.DropTable(
                name: "UserConfigurations");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "SalesRate");
        }
    }
}
