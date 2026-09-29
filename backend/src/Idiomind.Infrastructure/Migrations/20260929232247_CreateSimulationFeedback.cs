using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Idiomind.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateSimulationFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "simulation_feedback",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SimulationId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExperienceRating = table.Column<int>(type: "integer", nullable: true),
                    Preparedness = table.Column<int>(type: "integer", nullable: true),
                    Improvement = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    Skipped = table.Column<bool>(type: "boolean", nullable: false),
                    SimulationId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_simulation_feedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_simulation_feedback_simulations_SimulationId",
                        column: x => x.SimulationId,
                        principalTable: "simulations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_simulation_feedback_simulations_SimulationId1",
                        column: x => x.SimulationId1,
                        principalTable: "simulations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_simulation_feedback_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_simulation_feedback_SimulationId",
                table: "simulation_feedback",
                column: "SimulationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_simulation_feedback_SimulationId1",
                table: "simulation_feedback",
                column: "SimulationId1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_simulation_feedback_UserId",
                table: "simulation_feedback",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "simulation_feedback");
        }
    }
}
