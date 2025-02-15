using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mappa.Migrations
{
    /// <inheritdoc />
    public partial class DateToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearWritten",
                table: "SecondarySources");

            migrationBuilder.AddColumn<int>(
                name: "YearWritten",
                table: "SecondarySources",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearWritten",
                table: "SecondarySources");

            migrationBuilder.AddColumn<DateOnly>(
                name: "YearWritten",
                table: "SecondarySources",
                type: "date",
                nullable: true);
        }
    }
}
