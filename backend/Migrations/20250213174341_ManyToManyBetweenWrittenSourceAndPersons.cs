using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mappa.Migrations
{
    /// <inheritdoc />
    public partial class ManyToManyBetweenWrittenSourceAndPersons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WrittenSources_OrdinaryPersons_OrdinaryPersonId",
                table: "WrittenSources");

            migrationBuilder.DropForeignKey(
                name: "FK_WrittenSources_UnordinaryPersons_UnordinaryPersonId",
                table: "WrittenSources");

            migrationBuilder.DropIndex(
                name: "IX_WrittenSources_OrdinaryPersonId",
                table: "WrittenSources");

            migrationBuilder.DropIndex(
                name: "IX_WrittenSources_UnordinaryPersonId",
                table: "WrittenSources");

            migrationBuilder.DropColumn(
                name: "OrdinaryPersonId",
                table: "WrittenSources");

            migrationBuilder.DropColumn(
                name: "UnordinaryPersonId",
                table: "WrittenSources");

            migrationBuilder.CreateTable(
                name: "OrdinaryPersonWrittenSource",
                columns: table => new
                {
                    OrdinaryPersonsId = table.Column<int>(type: "integer", nullable: false),
                    SourcesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdinaryPersonWrittenSource", x => new { x.OrdinaryPersonsId, x.SourcesId });
                    table.ForeignKey(
                        name: "FK_OrdinaryPersonWrittenSource_OrdinaryPersons_OrdinaryPersons~",
                        column: x => x.OrdinaryPersonsId,
                        principalTable: "OrdinaryPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdinaryPersonWrittenSource_WrittenSources_SourcesId",
                        column: x => x.SourcesId,
                        principalTable: "WrittenSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnordinaryPersonWrittenSource",
                columns: table => new
                {
                    SourcesId = table.Column<int>(type: "integer", nullable: false),
                    UnordinaryPersonsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnordinaryPersonWrittenSource", x => new { x.SourcesId, x.UnordinaryPersonsId });
                    table.ForeignKey(
                        name: "FK_UnordinaryPersonWrittenSource_UnordinaryPersons_UnordinaryP~",
                        column: x => x.UnordinaryPersonsId,
                        principalTable: "UnordinaryPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnordinaryPersonWrittenSource_WrittenSources_SourcesId",
                        column: x => x.SourcesId,
                        principalTable: "WrittenSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdinaryPersonWrittenSource_SourcesId",
                table: "OrdinaryPersonWrittenSource",
                column: "SourcesId");

            migrationBuilder.CreateIndex(
                name: "IX_UnordinaryPersonWrittenSource_UnordinaryPersonsId",
                table: "UnordinaryPersonWrittenSource",
                column: "UnordinaryPersonsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdinaryPersonWrittenSource");

            migrationBuilder.DropTable(
                name: "UnordinaryPersonWrittenSource");

            migrationBuilder.AddColumn<int>(
                name: "OrdinaryPersonId",
                table: "WrittenSources",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnordinaryPersonId",
                table: "WrittenSources",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WrittenSources_OrdinaryPersonId",
                table: "WrittenSources",
                column: "OrdinaryPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_WrittenSources_UnordinaryPersonId",
                table: "WrittenSources",
                column: "UnordinaryPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_WrittenSources_OrdinaryPersons_OrdinaryPersonId",
                table: "WrittenSources",
                column: "OrdinaryPersonId",
                principalTable: "OrdinaryPersons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WrittenSources_UnordinaryPersons_UnordinaryPersonId",
                table: "WrittenSources",
                column: "UnordinaryPersonId",
                principalTable: "UnordinaryPersons",
                principalColumn: "Id");
        }
    }
}
