using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Group.Salto.DataAccess.Tenant.Migrations
{
    public partial class Modify_Teams_By_Assets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetsAudit_Teams",
                table: "AssetsAudit");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialForm_Teams",
                table: "MaterialForm");

            migrationBuilder.DropForeignKey(
                name: "FK_PreconditionsLiteralValues_Teams",
                table: "PreconditionsLiteralValues");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_Teams",
                table: "WorkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrdersDeritative_Teams",
                table: "WorkOrdersDeritative");

            migrationBuilder.DropTable(
                name: "TeamsContracts");

            migrationBuilder.DropTable(
                name: "TeamsHiredServices");

            migrationBuilder.DropTable(
                name: "TeamsWorkOrders");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.RenameColumn(
                name: "TeamsId",
                table: "WorkOrdersDeritative",
                newName: "AssetId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrdersDeritative_TeamsId",
                table: "WorkOrdersDeritative",
                newName: "IX_WorkOrdersDeritative_AssetId");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "WorkOrders",
                newName: "AssetId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrders_TeamId",
                table: "WorkOrders",
                newName: "IX_WorkOrders_AssetId");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "PreconditionsLiteralValues",
                newName: "AssetId");

            migrationBuilder.RenameIndex(
                name: "IX_PreconditionsLiteralValues_TeamId",
                table: "PreconditionsLiteralValues",
                newName: "IX_PreconditionsLiteralValues_AssetId");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "MaterialForm",
                newName: "AssetId");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialForm_TeamId",
                table: "MaterialForm",
                newName: "IX_MaterialForm_AssetId");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "AssetsAudit",
                newName: "AssetId");

            migrationBuilder.RenameIndex(
                name: "IX_AssetsAudit_TeamId",
                table: "AssetsAudit",
                newName: "IX_AssetsAudit_AssetId");

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SerialNumber = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    StockNumber = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    AssetNumber = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
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
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Status_AssetStatuses",
                        column: x => x.AssetStatusId,
                        principalTable: "AssetStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assets_Guarantee",
                        column: x => x.GuaranteeId,
                        principalTable: "Guarantee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assets_LocationsClient",
                        column: x => x.LocationClientId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assets_Locations",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assets_Model",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assets_Subfamilies",
                        column: x => x.SubFamilyId,
                        principalTable: "SubFamilies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Assets__UsageId__284DF453",
                        column: x => x.UsageId,
                        principalTable: "Usages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__Assets__UserId__2942188C",
                        column: x => x.UserId,
                        principalTable: "SiteUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssetsContracts",
                columns: table => new
                {
                    AssetsId = table.Column<int>(nullable: false),
                    ContractsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetsContracts", x => new { x.AssetsId, x.ContractsId });
                    table.ForeignKey(
                        name: "FK_AssetsContracts_Assets",
                        column: x => x.AssetsId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetssContracts_Contracts",
                        column: x => x.ContractsId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssetsHiredServices",
                columns: table => new
                {
                    AssetId = table.Column<int>(nullable: false),
                    HiredServiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetsHiredServices", x => new { x.AssetId, x.HiredServiceId });
                    table.ForeignKey(
                        name: "FK_AssetsHiredServices_Assets",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetsHiredServices_HiredServices",
                        column: x => x.HiredServiceId,
                        principalTable: "HiredServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssetsWorkOrders",
                columns: table => new
                {
                    WorkOrderId = table.Column<int>(nullable: false),
                    AssetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetsWorkOrders", x => new { x.WorkOrderId, x.AssetId });
                    table.ForeignKey(
                        name: "FK_AssetsWorkOrders_Assets",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetsWorkOrders_WorkOrders",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AssetStatusId",
                table: "Assets",
                column: "AssetStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_GuaranteeId",
                table: "Assets",
                column: "GuaranteeId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_LocationClientId",
                table: "Assets",
                column: "LocationClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_LocationId",
                table: "Assets",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_ModelId",
                table: "Assets",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_SubFamilyId",
                table: "Assets",
                column: "SubFamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_UsageId",
                table: "Assets",
                column: "UsageId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_UserId",
                table: "Assets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetsContracts_ContractsId",
                table: "AssetsContracts",
                column: "ContractsId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetsHiredServices_HiredServiceId",
                table: "AssetsHiredServices",
                column: "HiredServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetsWorkOrders_AssetId",
                table: "AssetsWorkOrders",
                column: "AssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetsAudit_Assets",
                table: "AssetsAudit",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialForm_Assets",
                table: "MaterialForm",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreconditionsLiteralValues_Assets",
                table: "PreconditionsLiteralValues",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_Assets",
                table: "WorkOrders",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrdersDeritative_Assets",
                table: "WorkOrdersDeritative",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetsAudit_Assets",
                table: "AssetsAudit");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialForm_Assets",
                table: "MaterialForm");

            migrationBuilder.DropForeignKey(
                name: "FK_PreconditionsLiteralValues_Assets",
                table: "PreconditionsLiteralValues");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_Assets",
                table: "WorkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrdersDeritative_Assets",
                table: "WorkOrdersDeritative");

            migrationBuilder.DropTable(
                name: "AssetsContracts");

            migrationBuilder.DropTable(
                name: "AssetsHiredServices");

            migrationBuilder.DropTable(
                name: "AssetsWorkOrders");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.RenameColumn(
                name: "AssetId",
                table: "WorkOrdersDeritative",
                newName: "TeamsId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrdersDeritative_AssetId",
                table: "WorkOrdersDeritative",
                newName: "IX_WorkOrdersDeritative_TeamsId");

            migrationBuilder.RenameColumn(
                name: "AssetId",
                table: "WorkOrders",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkOrders_AssetId",
                table: "WorkOrders",
                newName: "IX_WorkOrders_TeamId");

            migrationBuilder.RenameColumn(
                name: "AssetId",
                table: "PreconditionsLiteralValues",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_PreconditionsLiteralValues_AssetId",
                table: "PreconditionsLiteralValues",
                newName: "IX_PreconditionsLiteralValues_TeamId");

            migrationBuilder.RenameColumn(
                name: "AssetId",
                table: "MaterialForm",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialForm_AssetId",
                table: "MaterialForm",
                newName: "IX_MaterialForm_TeamId");

            migrationBuilder.RenameColumn(
                name: "AssetId",
                table: "AssetsAudit",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_AssetsAudit_AssetId",
                table: "AssetsAudit",
                newName: "IX_AssetsAudit_TeamId");

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssetStatusId = table.Column<int>(nullable: false),
                    GuaranteeId = table.Column<int>(nullable: true),
                    LocationClientId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: true),
                    ModelId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Observations = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    SerialNumber = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    StockNumber = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    SubFamilyId = table.Column<int>(nullable: true),
                    TeamNumber = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    UpdateDate = table.Column<DateTime>(nullable: false),
                    UsageId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
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
                name: "TeamsContracts",
                columns: table => new
                {
                    TeamsId = table.Column<int>(nullable: false),
                    ContractsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamsContracts", x => new { x.TeamsId, x.ContractsId });
                    table.ForeignKey(
                        name: "FK_TeamsContracts_Contracts",
                        column: x => x.ContractsId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeamsContracts_Teams",
                        column: x => x.TeamsId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_TeamsContracts_ContractsId",
                table: "TeamsContracts",
                column: "ContractsId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamsHiredServices_HiredServiceId",
                table: "TeamsHiredServices",
                column: "HiredServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamsWorkOrders_TeamId",
                table: "TeamsWorkOrders",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetsAudit_Teams",
                table: "AssetsAudit",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialForm_Teams",
                table: "MaterialForm",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreconditionsLiteralValues_Teams",
                table: "PreconditionsLiteralValues",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_Teams",
                table: "WorkOrders",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrdersDeritative_Teams",
                table: "WorkOrdersDeritative",
                column: "TeamsId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
