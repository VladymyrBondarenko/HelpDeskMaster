using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDeskMaster.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_WorkRequest_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExecuterId = table.Column<Guid>(type: "uuid", nullable: true),
                    WorkDirectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    FailureRevealedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    DesiredExecutionDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Content = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkRequests_WorkCategories_WorkCategoryId",
                        column: x => x.WorkCategoryId,
                        principalTable: "WorkCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkRequests_WorkDirections_WorkDirectionId",
                        column: x => x.WorkDirectionId,
                        principalTable: "WorkDirections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkRequestEquipment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkRequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    EquipmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkRequestEquipment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkRequestEquipment_WorkRequests_WorkRequestId",
                        column: x => x.WorkRequestId,
                        principalTable: "WorkRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkRequestPost",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkRequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkRequestPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkRequestPost_WorkRequests_WorkRequestId",
                        column: x => x.WorkRequestId,
                        principalTable: "WorkRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkRequestStageChange",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkRequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    Stage = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkRequestStageChange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkRequestStageChange_WorkRequests_WorkRequestId",
                        column: x => x.WorkRequestId,
                        principalTable: "WorkRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkRequestEquipment_WorkRequestId",
                table: "WorkRequestEquipment",
                column: "WorkRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkRequestPost_WorkRequestId",
                table: "WorkRequestPost",
                column: "WorkRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkRequestStageChange_WorkRequestId",
                table: "WorkRequestStageChange",
                column: "WorkRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkRequests_WorkCategoryId",
                table: "WorkRequests",
                column: "WorkCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkRequests_WorkDirectionId",
                table: "WorkRequests",
                column: "WorkDirectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkRequestEquipment");

            migrationBuilder.DropTable(
                name: "WorkRequestPost");

            migrationBuilder.DropTable(
                name: "WorkRequestStageChange");

            migrationBuilder.DropTable(
                name: "WorkRequests");
        }
    }
}
