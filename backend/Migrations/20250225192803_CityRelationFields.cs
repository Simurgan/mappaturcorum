using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mappa.Migrations
{
    /// <inheritdoc />
    public partial class CityRelationFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfBackgroundCityOf",
                table: "Cities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfBirthPlaceOf",
                table: "Cities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfDeathPlaceOf",
                table: "Cities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfLocationOf",
                table: "Cities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSourcesMentioningTheCity",
                table: "Cities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSourcesWrittenInTheCity",
                table: "Cities",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfBackgroundCityOf",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "NumberOfBirthPlaceOf",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "NumberOfDeathPlaceOf",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "NumberOfLocationOf",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "NumberOfSourcesMentioningTheCity",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "NumberOfSourcesWrittenInTheCity",
                table: "Cities");
        }
    }
}
