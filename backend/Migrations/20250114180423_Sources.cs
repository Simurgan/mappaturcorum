using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace mappa.Migrations
{
    /// <inheritdoc />
    public partial class Sources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormerReligionId",
                table: "UnordinaryPersons",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FormerReligionId",
                table: "OrdinaryPersons",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SecondarySourceId = table.Column<int>(type: "integer", nullable: true),
                    WrittenSourceId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SecondarySources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AlternateNames = table.Column<string[]>(type: "text[]", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: true),
                    Topic = table.Column<string>(type: "text", nullable: true),
                    YearWritten = table.Column<DateOnly>(type: "date", nullable: true),
                    University = table.Column<string>(type: "text", nullable: true),
                    BibliographyInformation = table.Column<string>(type: "text", nullable: true),
                    OtherInformation = table.Column<string>(type: "text", nullable: true),
                    TypeId = table.Column<int>(type: "integer", nullable: false),
                    LanguageId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondarySources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecondarySources_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecondarySources_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WrittenSources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AlternateNames = table.Column<string[]>(type: "text[]", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: true),
                    YearWritten = table.Column<DateOnly>(type: "date", nullable: true),
                    KnownCopies = table.Column<string[]>(type: "text[]", nullable: false),
                    SurvivedCopies = table.Column<string[]>(type: "text[]", nullable: false),
                    LibraryInformation = table.Column<string>(type: "text", nullable: true),
                    OtherInformation = table.Column<string>(type: "text", nullable: true),
                    RemarkableWorksOnTheBook = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    Patronage = table.Column<bool>(type: "boolean", nullable: false),
                    GenreId = table.Column<int>(type: "integer", nullable: false),
                    LanguageId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WrittenSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WrittenSources_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WrittenSources_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnordinaryPersons_FormerReligionId",
                table: "UnordinaryPersons",
                column: "FormerReligionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdinaryPersons_FormerReligionId",
                table: "OrdinaryPersons",
                column: "FormerReligionId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_SecondarySourceId",
                table: "Languages",
                column: "SecondarySourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_WrittenSourceId",
                table: "Languages",
                column: "WrittenSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_SecondarySources_LanguageId",
                table: "SecondarySources",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_SecondarySources_TypeId",
                table: "SecondarySources",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WrittenSources_GenreId",
                table: "WrittenSources",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_WrittenSources_LanguageId",
                table: "WrittenSources",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdinaryPersons_Religions_FormerReligionId",
                table: "OrdinaryPersons",
                column: "FormerReligionId",
                principalTable: "Religions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnordinaryPersons_Religions_FormerReligionId",
                table: "UnordinaryPersons",
                column: "FormerReligionId",
                principalTable: "Religions",
                principalColumn: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdinaryPersons_Religions_FormerReligionId",
                table: "OrdinaryPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_UnordinaryPersons_Religions_FormerReligionId",
                table: "UnordinaryPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_SecondarySources_SecondarySourceId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Languages_WrittenSources_WrittenSourceId",
                table: "Languages");

            migrationBuilder.DropTable(
                name: "SecondarySources");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "WrittenSources");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_UnordinaryPersons_FormerReligionId",
                table: "UnordinaryPersons");

            migrationBuilder.DropIndex(
                name: "IX_OrdinaryPersons_FormerReligionId",
                table: "OrdinaryPersons");

            migrationBuilder.DropColumn(
                name: "FormerReligionId",
                table: "UnordinaryPersons");

            migrationBuilder.DropColumn(
                name: "FormerReligionId",
                table: "OrdinaryPersons");
        }
    }
}
