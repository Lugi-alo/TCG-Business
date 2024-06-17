using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuwaCards.Migrations
{
    /// <inheritdoc />
    public partial class PokemonSinglesQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "PokemonSingles",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "AltTex",
                table: "PokemonSingles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "PokemonSingles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AltTex",
                table: "PokemonSingles");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "PokemonSingles");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "PokemonSingles",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");
        }
    }
}
