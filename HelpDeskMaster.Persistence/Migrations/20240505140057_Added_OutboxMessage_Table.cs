using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDeskMaster.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_OutboxMessage_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Content = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    OccuredOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ProcessedOnUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Error = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserEquipments_EquipmentId",
                table: "UserEquipments",
                column: "EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserEquipments_Equipments_EquipmentId",
                table: "UserEquipments",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEquipments_Equipments_EquipmentId",
                table: "UserEquipments");

            migrationBuilder.DropTable(
                name: "OutboxMessages");

            migrationBuilder.DropIndex(
                name: "IX_UserEquipments_EquipmentId",
                table: "UserEquipments");
        }
    }
}
