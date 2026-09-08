using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Idiomind.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class VariantObjectivesRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SituationVariantId1",
                table: "situation_variant_objectives",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_situation_variant_objectives_SituationVariantId1",
                table: "situation_variant_objectives",
                column: "SituationVariantId1");

            migrationBuilder.AddForeignKey(
                name: "FK_situation_variant_objectives_situation_variants_SituationV~1",
                table: "situation_variant_objectives",
                column: "SituationVariantId1",
                principalTable: "situation_variants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_situation_variant_objectives_situation_variants_SituationV~1",
                table: "situation_variant_objectives");

            migrationBuilder.DropIndex(
                name: "IX_situation_variant_objectives_SituationVariantId1",
                table: "situation_variant_objectives");

            migrationBuilder.DropColumn(
                name: "SituationVariantId1",
                table: "situation_variant_objectives");
        }
    }
}
