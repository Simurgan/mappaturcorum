using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mappa.Migrations
{
    /// <inheritdoc />
    public partial class NavigationFieldsOfCity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_WrittenSources_WrittenSourceId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_WrittenSources_WrittenSourceId1",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_WrittenSourceId",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_WrittenSourceId1",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "WrittenSourceId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "WrittenSourceId1",
                table: "Cities");

            migrationBuilder.CreateTable(
                name: "CityWrittenSource",
                columns: table => new
                {
                    CitiesMentionedByTheSourceId = table.Column<int>(type: "integer", nullable: false),
                    SourcesMentioningTheCityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityWrittenSource", x => new { x.CitiesMentionedByTheSourceId, x.SourcesMentioningTheCityId });
                    table.ForeignKey(
                        name: "FK_CityWrittenSource_Cities_CitiesMentionedByTheSourceId",
                        column: x => x.CitiesMentionedByTheSourceId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CityWrittenSource_WrittenSources_SourcesMentioningTheCityId",
                        column: x => x.SourcesMentioningTheCityId,
                        principalTable: "WrittenSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CityWrittenSource1",
                columns: table => new
                {
                    CitiesWhereSourcesAreWrittenId = table.Column<int>(type: "integer", nullable: false),
                    SourcesWrittenInTheCityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityWrittenSource1", x => new { x.CitiesWhereSourcesAreWrittenId, x.SourcesWrittenInTheCityId });
                    table.ForeignKey(
                        name: "FK_CityWrittenSource1_Cities_CitiesWhereSourcesAreWrittenId",
                        column: x => x.CitiesWhereSourcesAreWrittenId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CityWrittenSource1_WrittenSources_SourcesWrittenInTheCityId",
                        column: x => x.SourcesWrittenInTheCityId,
                        principalTable: "WrittenSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityWrittenSource_SourcesMentioningTheCityId",
                table: "CityWrittenSource",
                column: "SourcesMentioningTheCityId");

            migrationBuilder.CreateIndex(
                name: "IX_CityWrittenSource1_SourcesWrittenInTheCityId",
                table: "CityWrittenSource1",
                column: "SourcesWrittenInTheCityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityWrittenSource");

            migrationBuilder.DropTable(
                name: "CityWrittenSource1");

            migrationBuilder.AddColumn<int>(
                name: "WrittenSourceId",
                table: "Cities",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WrittenSourceId1",
                table: "Cities",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_WrittenSourceId",
                table: "Cities",
                column: "WrittenSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_WrittenSourceId1",
                table: "Cities",
                column: "WrittenSourceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_WrittenSources_WrittenSourceId",
                table: "Cities",
                column: "WrittenSourceId",
                principalTable: "WrittenSources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_WrittenSources_WrittenSourceId1",
                table: "Cities",
                column: "WrittenSourceId1",
                principalTable: "WrittenSources",
                principalColumn: "Id");
        }
    }
}
