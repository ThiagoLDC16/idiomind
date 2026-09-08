using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Idiomind.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSituationVariantBaseId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SituationVariantBaseId",
                table: "situation_variants",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SituationVariantBaseId",
                table: "situation_variants");
        }
    }
}
