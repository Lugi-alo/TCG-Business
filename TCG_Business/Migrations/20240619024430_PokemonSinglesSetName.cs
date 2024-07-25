using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuwaCards.Migrations
{
    /// <inheritdoc />
    public partial class PokemonSinglesSetName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Set",
                table: "PokemonSingles",
                newName: "SetNumber");

            migrationBuilder.AddColumn<string>(
                name: "SetName",
                table: "PokemonSingles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SetName",
                table: "PokemonSingles");

            migrationBuilder.RenameColumn(
                name: "SetNumber",
                table: "PokemonSingles",
                newName: "Set");
        }
    }
}
