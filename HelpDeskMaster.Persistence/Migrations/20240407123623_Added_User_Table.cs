using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDeskMaster.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_User_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "WorkDirections",
                type: "character varying(75)",
                maxLength: 75,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "WorkCategories",
                type: "character varying(75)",
                maxLength: 75,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Login_Value = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserEquipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UnassignedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEquipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserEquipments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserEquipments_UserId",
                table: "UserEquipments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Login_Value",
                table: "Users",
                column: "Login_Value",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserEquipments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "WorkDirections",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(75)",
                oldMaxLength: 75);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "WorkCategories",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(75)",
                oldMaxLength: 75);
        }
    }
}
