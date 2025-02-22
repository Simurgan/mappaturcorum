using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mappa.Migrations
{
    /// <inheritdoc />
    public partial class ManyToManyBetweenReligionAndPersonsAlsoBetweenOrdinaryAndUnordinaryPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Religions_OrdinaryPersons_OrdinaryPersonId",
                table: "Religions");

            migrationBuilder.DropForeignKey(
                name: "FK_Religions_UnordinaryPersons_UnordinaryPersonId",
                table: "Religions");

            migrationBuilder.DropIndex(
                name: "IX_Religions_OrdinaryPersonId",
                table: "Religions");

            migrationBuilder.DropIndex(
                name: "IX_Religions_UnordinaryPersonId",
                table: "Religions");

            migrationBuilder.DropColumn(
                name: "OrdinaryPersonId",
                table: "Religions");

            migrationBuilder.DropColumn(
                name: "UnordinaryPersonId",
                table: "Religions");

            migrationBuilder.CreateTable(
                name: "OrdinaryPersonReligion",
                columns: table => new
                {
                    FormerOrdinaryPersonsId = table.Column<int>(type: "integer", nullable: false),
                    FormerReligionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdinaryPersonReligion", x => new { x.FormerOrdinaryPersonsId, x.FormerReligionId });
                    table.ForeignKey(
                        name: "FK_OrdinaryPersonReligion_OrdinaryPersons_FormerOrdinaryPerson~",
                        column: x => x.FormerOrdinaryPersonsId,
                        principalTable: "OrdinaryPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdinaryPersonReligion_Religions_FormerReligionId",
                        column: x => x.FormerReligionId,
                        principalTable: "Religions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReligionUnordinaryPerson",
                columns: table => new
                {
                    FormerReligionId = table.Column<int>(type: "integer", nullable: false),
                    FormerUnordinaryPersonsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReligionUnordinaryPerson", x => new { x.FormerReligionId, x.FormerUnordinaryPersonsId });
                    table.ForeignKey(
                        name: "FK_ReligionUnordinaryPerson_Religions_FormerReligionId",
                        column: x => x.FormerReligionId,
                        principalTable: "Religions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReligionUnordinaryPerson_UnordinaryPersons_FormerUnordinary~",
                        column: x => x.FormerUnordinaryPersonsId,
                        principalTable: "UnordinaryPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdinaryPersonReligion_FormerReligionId",
                table: "OrdinaryPersonReligion",
                column: "FormerReligionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReligionUnordinaryPerson_FormerUnordinaryPersonsId",
                table: "ReligionUnordinaryPerson",
                column: "FormerUnordinaryPersonsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdinaryPersonReligion");

            migrationBuilder.DropTable(
                name: "ReligionUnordinaryPerson");

            migrationBuilder.AddColumn<int>(
                name: "OrdinaryPersonId",
                table: "Religions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnordinaryPersonId",
                table: "Religions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Religions_OrdinaryPersonId",
                table: "Religions",
                column: "OrdinaryPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Religions_UnordinaryPersonId",
                table: "Religions",
                column: "UnordinaryPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Religions_OrdinaryPersons_OrdinaryPersonId",
                table: "Religions",
                column: "OrdinaryPersonId",
                principalTable: "OrdinaryPersons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Religions_UnordinaryPersons_UnordinaryPersonId",
                table: "Religions",
                column: "UnordinaryPersonId",
                principalTable: "UnordinaryPersons",
                principalColumn: "Id");
        }
    }
}
