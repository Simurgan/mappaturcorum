using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mappa.Migrations
{
    /// <inheritdoc />
    public partial class FormerReligionAndCopiesRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdinaryPersonReligion");

            migrationBuilder.DropTable(
                name: "ReligionUnordinaryPerson");

            migrationBuilder.AlterColumn<string>(
                name: "SurvivedCopies",
                table: "WrittenSources",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "KnownCopies",
                table: "WrittenSources",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReligionId1",
                table: "UnordinaryPersons",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FormerReligionId",
                table: "OrdinaryPersons",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AsciiName",
                table: "Cities",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_UnordinaryPersons_ReligionId1",
                table: "UnordinaryPersons",
                column: "ReligionId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrdinaryPersons_FormerReligionId",
                table: "OrdinaryPersons",
                column: "FormerReligionId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdinaryPersons_Religions_FormerReligionId",
                table: "OrdinaryPersons",
                column: "FormerReligionId",
                principalTable: "Religions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnordinaryPersons_Religions_ReligionId1",
                table: "UnordinaryPersons",
                column: "ReligionId1",
                principalTable: "Religions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdinaryPersons_Religions_FormerReligionId",
                table: "OrdinaryPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_UnordinaryPersons_Religions_ReligionId1",
                table: "UnordinaryPersons");

            migrationBuilder.DropIndex(
                name: "IX_UnordinaryPersons_ReligionId1",
                table: "UnordinaryPersons");

            migrationBuilder.DropIndex(
                name: "IX_OrdinaryPersons_FormerReligionId",
                table: "OrdinaryPersons");

            migrationBuilder.DropColumn(
                name: "ReligionId1",
                table: "UnordinaryPersons");

            migrationBuilder.DropColumn(
                name: "FormerReligionId",
                table: "OrdinaryPersons");

            migrationBuilder.AlterColumn<int>(
                name: "SurvivedCopies",
                table: "WrittenSources",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "KnownCopies",
                table: "WrittenSources",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "AsciiName",
                table: "Cities",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

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
    }
}
