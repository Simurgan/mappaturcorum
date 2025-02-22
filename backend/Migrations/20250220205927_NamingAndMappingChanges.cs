using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mappa.Migrations
{
    /// <inheritdoc />
    public partial class NamingAndMappingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Languages_SecondarySources_SecondarySourceId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_WrittenSources_WrittenSourceId",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Languages_SecondarySourceId",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Languages_WrittenSourceId",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "SecondarySourceId",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "WrittenSourceId",
                table: "Languages");

            migrationBuilder.RenameColumn(
                name: "DepictionInTheSource",
                table: "OrdinaryPersons",
                newName: "DescriptionInTheSource");

            migrationBuilder.DropColumn(
                name: "SurvivedCopies",
                table: "WrittenSources"
            );

            migrationBuilder.AddColumn<int>(
                name: "SurvivedCopies",
                table: "WrittenSources",
                type: "integer",
                nullable: true
            );

            migrationBuilder.DropColumn(
                name: "KnownCopies",
                table: "WrittenSources"
            );

            migrationBuilder.AddColumn<int>(
                name: "KnownCopies",
                table: "WrittenSources",
                type: "integer",
                nullable: true
            );

            migrationBuilder.AlterColumn<List<string>>(
                name: "AlternateNames",
                table: "WrittenSources",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(List<string>),
                oldType: "text[]");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "WrittenSources",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Depiction",
                table: "UnordinaryPersons",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<List<string>>(
                name: "AlternateNames",
                table: "SecondarySources",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(List<string>),
                oldType: "text[]");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SecondarySources",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cities",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LanguageSecondarySource",
                columns: table => new
                {
                    SecondarySourceId = table.Column<int>(type: "integer", nullable: false),
                    TranslatedLanguagesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageSecondarySource", x => new { x.SecondarySourceId, x.TranslatedLanguagesId });
                    table.ForeignKey(
                        name: "FK_LanguageSecondarySource_Languages_TranslatedLanguagesId",
                        column: x => x.TranslatedLanguagesId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguageSecondarySource_SecondarySources_SecondarySourceId",
                        column: x => x.SecondarySourceId,
                        principalTable: "SecondarySources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageWrittenSource",
                columns: table => new
                {
                    TranslatedLanguagesId = table.Column<int>(type: "integer", nullable: false),
                    WrittenSourceId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageWrittenSource", x => new { x.TranslatedLanguagesId, x.WrittenSourceId });
                    table.ForeignKey(
                        name: "FK_LanguageWrittenSource_Languages_TranslatedLanguagesId",
                        column: x => x.TranslatedLanguagesId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguageWrittenSource_WrittenSources_WrittenSourceId",
                        column: x => x.WrittenSourceId,
                        principalTable: "WrittenSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LanguageSecondarySource_TranslatedLanguagesId",
                table: "LanguageSecondarySource",
                column: "TranslatedLanguagesId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageWrittenSource_WrittenSourceId",
                table: "LanguageWrittenSource",
                column: "WrittenSourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LanguageSecondarySource");

            migrationBuilder.DropTable(
                name: "LanguageWrittenSource");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "WrittenSources");

            migrationBuilder.DropColumn(
                name: "Depiction",
                table: "UnordinaryPersons");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SecondarySources");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cities");

            migrationBuilder.RenameColumn(
                name: "DescriptionInTheSource",
                table: "OrdinaryPersons",
                newName: "DepictionInTheSource");

            migrationBuilder.DropColumn(
                name: "SurvivedCopies",
                table: "WrittenSources"
            );

            migrationBuilder.AddColumn<int>(
                name: "SurvivedCopies",
                table: "WrittenSources",
                type: "text[]",
                nullable: true
            );

            migrationBuilder.DropColumn(
                name: "KnownCopies",
                table: "WrittenSources"
            );

            migrationBuilder.AddColumn<int>(
                name: "KnownCopies",
                table: "WrittenSources",
                type: "text[]",
                nullable: true
            );

            migrationBuilder.AlterColumn<List<string>>(
                name: "AlternateNames",
                table: "WrittenSources",
                type: "text[]",
                nullable: false,
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldNullable: true);

            migrationBuilder.AlterColumn<List<string>>(
                name: "AlternateNames",
                table: "SecondarySources",
                type: "text[]",
                nullable: false,
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SecondarySourceId",
                table: "Languages",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WrittenSourceId",
                table: "Languages",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_SecondarySourceId",
                table: "Languages",
                column: "SecondarySourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_WrittenSourceId",
                table: "Languages",
                column: "WrittenSourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_SecondarySources_SecondarySourceId",
                table: "Languages",
                column: "SecondarySourceId",
                principalTable: "SecondarySources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_WrittenSources_WrittenSourceId",
                table: "Languages",
                column: "WrittenSourceId",
                principalTable: "WrittenSources",
                principalColumn: "Id");
        }
    }
}
