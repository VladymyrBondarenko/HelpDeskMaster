using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDeskMaster.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EquipmentTable_ExtendedModelFieldLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Equipments",
                type: "character varying(254)",
                maxLength: 254,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Equipments",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(254)",
                oldMaxLength: 254,
                oldNullable: true);
        }
    }
}
