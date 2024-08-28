using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuwaCards.Migrations
{
    /// <inheritdoc />
    public partial class testSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Set",
                table: "PokemonSealed",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Set",
                table: "PokemonSealed");
        }
    }
}
