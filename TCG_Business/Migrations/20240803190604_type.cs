﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuwaCards.Migrations
{
    /// <inheritdoc />
    public partial class type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "PokemonSingles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "PokemonSingles");
        }
    }
}
